using System;

namespace SampleAPI.Service
{
    public class ServiceResponseMessage
    {
        //Generic
        public static readonly string GenericGetSuccess = "{0}_GET{0}S_SUCCESS";
        public static readonly string GenericGetByIdSuccess = "{0}_GET{0}BYID_SUCCESS";
        public static readonly string GenericAddSuccess = "{0}_ADD{0}_SUCCESS";
        public static readonly string GenericAddRangeSuccess = "{0}_ADDRANGE{0}_SUCCESS";
        public static readonly string GenericAddErrorNull = "{0}_ADD{0}_ERROR_{0}_IS_NULL";
        public static readonly string GenericAddErrorDuplicate = "{0}_ADD{0}_ERROR_DUPLICATE";
        public static readonly string GenericUpdateSuccess = "{0}_UPDATE{0}_SUCCESS";
        public static readonly string GenericUpdateRangeSuccess = "{0}_UPDATERANGE{0}_SUCCESS";
        public static readonly string GenericUpdateErrorNull = "{0}_UPDATE{0}_ERROR_{0}_IS_NULL";
        public static readonly string GenericUpdateErrorNonExistent = "{0}_UPDATE{0}_ERROR_NON_EXISTENT";
        public static readonly string GenericUpdateErrorDuplicate = "{0}_UPDATE{0}_ERROR_DUPLICATE";
        public static readonly string GenericDeleteError = "{0}_DELETE{0}_ERROR_{0}";
        public static readonly string GenericDeleteSuccess = "{0}_DELETE{0}_SUCCESS";
        public static readonly string GenericDeleteRangeSuccess = "{0}_DELETERANGE{0}_SUCCESS";
        public static readonly string GenericReversalSuccess = "{0}_REVERSAL{0}_SUCCESS";
        public static readonly string GenericNoRecordsFound = "NO_RECORDS_FOUND";
        public static readonly string GenericNotValidValue = "UNKNOWN_IS_NOT_A_VALID_VALUE";
        public static readonly string DtoIsNullError = "DTO_IS_NULL_ERROR";
        public static readonly string InvalidOperation = "";

        public static string Get_Success<T>()
        {
            return String.Format(GenericGetSuccess, typeof(T).Name.ToUpper());
        }

        public static string GetById_Success<T>()
        {
            return String.Format(GenericGetByIdSuccess, typeof(T).Name.ToUpper());
        }

        public static string Add_Success<T>()
        {
            return String.Format(GenericAddSuccess, typeof(T).Name.ToUpper());
        }

        public static string AddRange_Success<T>()
        {
            return String.Format(GenericAddRangeSuccess, typeof(T).Name.ToUpper());
        }

        public static string Add_Null_Error<T>()
        {
            return String.Format(GenericAddErrorNull, typeof(T).Name.ToUpper());
        }

        public static string Add_Duplicate_Error<T>()
        {
            return String.Format(GenericAddErrorDuplicate, typeof(T).Name.ToUpper());
        }

        public static string Update_Success<T>()
        {
            return String.Format(GenericUpdateSuccess, typeof(T).Name.ToUpper());
        }

        public static string UpdateRange_Success<T>()
        {
            return String.Format(GenericUpdateRangeSuccess, typeof(T).Name.ToUpper());
        }

        public static string Update_Null_Error<T>()
        {
            return String.Format(GenericUpdateErrorNull, typeof(T).Name.ToUpper());
        }

        public static string Update_Nonexistent_Error<T>()
        {
            return String.Format(GenericUpdateErrorNonExistent, typeof(T).Name.ToUpper());
        }

        public static string Update_Duplicate_Error<T>()
        {
            return String.Format(GenericUpdateErrorDuplicate, typeof(T).Name.ToUpper());
        }

        public static string Delete_Error<T>()
        {
            return String.Format(GenericDeleteError, typeof(T).Name.ToUpper());
        }

        public static string Delete_Success<T>()
        {
            return String.Format(GenericDeleteSuccess, typeof(T).Name.ToUpper());
        }

        public static string DeleteRange_Success<T>()
        {
            return String.Format(GenericDeleteRangeSuccess, typeof(T).Name.ToUpper());
        }

    }
}

