namespace SampleAPI.DataTransferObjects
{
    public class ConfigurationSettings
    {
        public string AllowedDesignationsToGiveSampleAPI { get; set; }
        public bool LowerEnvironment { get; set; }
        public string FromEmail { get; set; }
        public string SmtpServer { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
    }
}
