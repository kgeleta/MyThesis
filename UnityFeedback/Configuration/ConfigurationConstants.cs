namespace UnityFeedback.Configuration
{
    public static class ConfigurationConstants
    {
        public const string CONFIG_FILE_NAME = "BuildConfig";

        public static class NodeName
        {
            public const string SCENES = "scene";
            public const string CONNECTION_STRING = "connectionString";
        }

        public static class Attribute
        {
            public const string SCENES = "path";
            public const string CONNECTION_STRING = "value";
        }

        internal class InternalConstants
        {
	        public const string MODEL_SCRIPT_PATH = "powershell/generateModel.ps1";

        }
    }
}