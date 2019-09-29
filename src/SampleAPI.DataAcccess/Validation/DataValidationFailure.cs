using SampleAPI.Interface.DataAccess;

namespace SampleAPI.DataAcccess.Validation
{
    public class DataValidationFailure : IDataValidationFailure
    {
        public string ErrorMessage
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}
