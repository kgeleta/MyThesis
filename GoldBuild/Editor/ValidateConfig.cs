using UnityEditor;
using UnityEngine;
using UnityFeedback;

namespace Assets.Editor
{
	public class ValidateConfig {
		[MenuItem("Feedback/Validate configuration file")]
		public static void Validate()
		{
			var result = FeedbackAPI.ValidateConfigurationFile();
			EditorUtility.DisplayDialog("Validation result", result.IsValid ? "Configuration is valid" : result.ErrorMessage, "Ok");
		}

	}
}