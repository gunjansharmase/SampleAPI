using System;
using System.Collections.Generic;
using System.Reflection;
using SampleAPI.Interface.DataAccess;

namespace SampleAPI.DataAcccess.RulesEngine
{
    public class BaseRulesEngine<T> : IRulesEngine<T>
      where T : class
    {
        public virtual T AddEntity(T entity)
        {
            entity= ApplyEnabledDateRule(entity);
            return ApplyCreateDateRule(entity);
        }

        public virtual IEnumerable<T> AddEntities(IEnumerable<T> entities)
        {
            var newEntities = new List<T>();
            foreach (T entity in entities)
            {
                var tempentity= ApplyCreateDateRule(entity);
                newEntities.Add(ApplyEnabledDateRule(tempentity));
            }
            return newEntities;
        }

        public virtual T UpdateEntity(T entity)
        {
            return ApplyModifyDateRule(entity);
        }

        public virtual IEnumerable<T> UpdateEntities(IEnumerable<T> entities)
        {
            var newEntities = new List<T>();
            foreach (T entity in entities)
            {
                newEntities.Add(ApplyModifyDateRule(entity));
            }
            return newEntities;
        }

        public virtual T RemoveEntity(T entity)
        {
            return entity;
        }

        public T ApplyCreateDateRule(T entity)
        {
            PropertyInfo createDate = entity.GetType().GetProperty("CreatedOn");
            createDate?.SetValue(entity, DateTime.UtcNow);
            return entity;
        }

        public T ApplyEnabledDateRule(T entity)
        {
            PropertyInfo enabled = entity.GetType().GetProperty("Enabled");
            enabled?.SetValue(entity, true);
            return entity;
        }

        public T ApplyModifyDateRule(T entity)
        {
            PropertyInfo modifyDate = entity.GetType().GetProperty("ModifiedOn");
            modifyDate?.SetValue(entity, DateTime.UtcNow);
            return entity;
        }


    }

}
