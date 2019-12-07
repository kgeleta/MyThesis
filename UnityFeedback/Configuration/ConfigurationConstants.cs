namespace UnityFeedback.Configuration
{
    public static class ConfigurationConstants
    {
        public const string CONFIG_FILE_NAME = "BuildConfig";
		//		TODO:
//		public const string CONFIG_FILE_PATH = @"";
//		public const string MODEL_CLASS_DIRECTORY = "";
		public const string WARNING_MESSAGE = @"#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.";
	    public const string REFERENCE_TO_CONNECTION_STRING = @"UnityFeedback.FeedbackAPI.Settings.ConnectionString()";

        public static class NodeName
        {
            public const string SCENES = "scene";
            public const string CONNECTION_STRING = "connectionString";
            public const string DATABASE_PROVIDER = "databaseProvider";
            public const string POWERSHELL_PATH = "powershellPath";
        }

        public static class Attribute
        {
            public const string PATH = "path";
            public const string VALUE = "value";
        }

        internal class InternalConstants
        {
	        public const string MODEL_SCRIPT_PATH = @"Assets\Resources\generateModel.ps1";

        }
    }
}