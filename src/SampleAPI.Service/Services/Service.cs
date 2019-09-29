﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using SampleAPI.Interface.DataAccess;
using SampleAPI.ORM;
using SampleAPI.Interface.Services;

namespace SampleAPI.Service.Services
{
    public abstract class Service<U, T> : IService<U, T>
   where U : class
   where T : class
    {
        protected IUnitOfWork _unitOfWork;
        protected IDbHelper _IDbHelper;
        protected IMapper _mapper;
        protected Service(IUnitOfWork unitOfWork, IDbHelper dbHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _IDbHelper = dbHelper;
            _mapper = mapper;
        }

        public string SourceSystemReference { get; set; }


        public async Task<T> ExecuteWithoutTransactionAsync<T>(Func<Task<T>> action)
        {
            try
            {
                _unitOfWork.OpenConnection();
                return await action();
            }
            finally
            {
                _unitOfWork.CloseConnection();
            }
        }


        public async Task<T> ExecuteWithTransactionAsync<T>(Func<Task<T>> action)
        {
            try
            {
                _unitOfWork.OpenConnection();
                _unitOfWork.StartTransaction();
                var result = await action();
                _unitOfWork.CompleteTransaction();
                return result;
            }
            catch(Exception ex)
            {
                //Rollback the transaction in case of any error
                _unitOfWork.Error();
                throw;
            }
            finally
            {
                _unitOfWork.CloseConnection();
            }
        }


        public virtual async Task<IServiceResponse<IEnumerable<U>>> GetRangeAsync(IEnumerable<int> Ids)
        {
            if (Ids == null || !Ids.Any())
                return new ServiceResponse<IEnumerable<U>>(false, ServiceResponseMessage.GetById_Success<IEnumerable<T>>());

            var response = await ExecuteWithoutTransactionAsync(async () => await _unitOfWork.Repository<T>().GetByIdRangeAsync(Ids));

            return new ServiceResponse<IEnumerable<U>>(true, ServiceResponseMessage.Get_Success<IEnumerable<U>>(), _mapper.Map<IEnumerable<U>>(response));
        }

        public virtual async Task<IServiceResponse<IEnumerable<U>>> GetAsync()
        {
            var entities = await ExecuteWithoutTransactionAsync<IEnumerable<T>>(async () => await _unitOfWork.Repository<T>().GetAsync());
            var dtos = _mapper.Map<IEnumerable<U>>(entities);
            return new ServiceResponse<IEnumerable<U>>(true, ServiceResponseMessage.Get_Success<T>(), dtos);
        }

        public virtual async Task<IServiceResponse<U>> GetByIdAsync(int id)
        {
            var entity = await ExecuteWithoutTransactionAsync<T>
            (async () => await _unitOfWork.Repository<T>().GetByIdAsync(id, _IDbHelper.GetPrimaryKeyAutoGenerated<T>()));
            var dto = _mapper.Map<U>(entity);
            return new ServiceResponse<U>(entity != null, ServiceResponseMessage.GetById_Success<T>(), dto);
        }

        public virtual async Task<IServiceResponse<U>> AddAsync(U dto)
        {
            if (dto == null)
                return new ServiceResponse<U>(false, ServiceResponseMessage.Add_Null_Error<T>());

            var entity = _mapper.Map<T>(dto);

            var dataValidationResult = await ExecuteWithoutTransactionAsync<IEnumerable<IDataValidationFailure>>(async () => await _unitOfWork.Repository<T>().CanAddAsync(entity));

            if (dataValidationResult.Any())
                return new ServiceResponse<U>(false, $"Error whilst persisting {typeof(U).Name} to Database", null, dataValidationResult);

            var id = await ExecuteWithTransactionAsync<int>(async () => {
                var returnValue = await _unitOfWork.Repository<T>().AddAsync(entity);

                return returnValue;
            });

            if (id == 0)
                return new ServiceResponse<U>(false, $"Error whilst persisting {typeof(U).Name} to Database,Please contact System Administrator");

            dynamic responseDTO = GenerateResponseDTO(dto, id);
            return new ServiceResponse<U>(true, ServiceResponseMessage.Add_Success<T>(), responseDTO);
        }

        public virtual async Task<IServiceResponse<int>> AddRangeAsync(IEnumerable<U> dtos)
        {
            if (dtos == null || !dtos.Any())
                return new ServiceResponse<int>(false, ServiceResponseMessage.Add_Null_Error<U>());

            var validDtos = dtos.Where(x => x != null);
            var entities = _mapper.Map<IEnumerable<T>>(validDtos);
            var response = await ExecuteWithTransactionAsync<int>(async () =>
            {
                var result = await _unitOfWork.Repository<T>().AddRangeAsync(entities);

                return result;
            });
            return new ServiceResponse<int>(true, ServiceResponseMessage.Add_Success<T>(), response);
        }

        public virtual async Task<IServiceResponse<int>> UpdateAsync(U dto)
        {
            if (dto == null)
                return new ServiceResponse<int>(false, ServiceResponseMessage.Update_Null_Error<T>());

            var entity = _mapper.Map<T>(dto);
            var dataValidationResult = await ExecuteWithoutTransactionAsync<IEnumerable<IDataValidationFailure>>(async () => await _unitOfWork.Repository<T>().CanUpdateAsync(entity));

            if (dataValidationResult.Any())
                return new ServiceResponse<int>(false, $"Error whilst persisting {typeof(U).Name} to Database", 0, dataValidationResult);


            var response = await ExecuteWithTransactionAsync<int>(async () => {
                var returnValue = await _unitOfWork.Repository<T>().UpdateAsync(entity);
                return returnValue;
            });

            return new ServiceResponse<int>(response == 1, response == 1 ? ServiceResponseMessage.Update_Success<T>() : ServiceResponseMessage.Update_Nonexistent_Error<T>(), response == 1 ? response : (int)_IDbHelper.GetKeyValue(entity, _IDbHelper.GetPrimaryKeyAutoGenerated<T>()));
        }

        public virtual async Task<IServiceResponse<int>> UpdateRangeAsync(IEnumerable<U> dtos)
        {
            if (dtos == null || !dtos.Any())
                return new ServiceResponse<int>(true, ServiceResponseMessage.UpdateRange_Success<T>(), 0);

            var validDtos = dtos.Where(x => x != null);
            var entities = _mapper.Map<IEnumerable<T>>(validDtos);

            var response = await ExecuteWithTransactionAsync<int>(async () => {
                var returnValue = await _unitOfWork.Repository<T>().UpdateRangeAsync(entities);
                return returnValue;
            });
            return new ServiceResponse<int>(true, ServiceResponseMessage.UpdateRange_Success<T>(), response);
        }

        public virtual async Task<IServiceResponse<int>> DeleteAsync(int id)
        {

            var response = await ExecuteWithTransactionAsync<int>(async () => {
                var returnValue = await _unitOfWork.Repository<T>().RemoveAsync(id);

                return returnValue;
            });
            return new ServiceResponse<int>(response > 0, ServiceResponseMessage.Delete_Success<T>(), response);
        }

        public virtual Task<IServiceResponse<int>> DeleteRangeAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public virtual U GenerateResponseDTO(U dto, int newId)
        {
            var responseDTO = (U)Activator.CreateInstance(typeof(U));
            Type type = typeof(U);
            var properties = type.GetProperties().Where(prop => prop.GetCustomAttribute<KeyAttribute>() != null).ToArray();
            if (properties.Length == 1)
            {
                properties[0].SetValue(responseDTO, newId, null);
            }
            return responseDTO;
        }
    }
}
