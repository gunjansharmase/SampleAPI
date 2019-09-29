using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAPI.DataTransferObjects.DTO
{
    public class UnhandledExceptionDTO
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }

        public string Data { get; set; }

        public string GUID { get; set; }

        public string StackTrace { get; set; }
    }
}
