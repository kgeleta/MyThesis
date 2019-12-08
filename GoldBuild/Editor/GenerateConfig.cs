using UnityEditor;
using UnityEngine;
using UnityFeedback;
using UnityFeedback.Configuration;

namespace Assets.Editor
{
	[InitializeOnLoad]
	public class GenerateConfig
	{
		// this method will be called on editor start
		static GenerateConfig()
		{
			FeedbackAPI.CreateDefaultConfigurationFile(false);
		}

		[MenuItem("Feedback/Create default configuration file")]
		public static void Generate()
		{
			if (EditorUtility.DisplayDialog("Warning",
				"This operation will override existing configuration file. Do you want to continue?", "Ok", "Cancel"))
			{
				FeedbackAPI.CreateDefaultConfigurationFile(true);

				TextAsset configAsset = Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME);
				AssetDatabase.OpenAsset(configAsset, 0, 0);
			}
		}
	}
}