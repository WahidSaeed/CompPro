namespace CRMData.ViewModels
{
    public class AppSettingModel
    {
        #region FilePath
        public string UploadDocumentPath { get; set; }
        public string TaskConfigPath { get; set; }
        #endregion FilePath

        #region EndPointDetails
        public string EndPointUrl { get; set; }
        public string EndPointUserName { get; set; }
        public string EndPointPassword { get; set; }
        #endregion EndPointDetails
    }
}
