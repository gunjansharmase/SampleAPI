using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleAPI.DataTransferObjects.DTO
{
    public class ValidationErrorDTO
    {
        private ModelStateDictionary modelStateDictionary;
        public ValidationErrorDTO(ModelStateDictionary modelStateDictionary)
        {
            this.modelStateDictionary = modelStateDictionary;
        }
        public SerializableError Errors
        {
            get
            {
                if (modelStateDictionary == null)
                    return null;

                return new SerializableError(modelStateDictionary);
            }
        }
    }
}
