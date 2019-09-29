namespace SampleAPI.Interface.DataAccess
{
    public interface IDataValidationFailure
    {
        string Name { get; set; }
        string ErrorMessage { get; set; }
    }
}
