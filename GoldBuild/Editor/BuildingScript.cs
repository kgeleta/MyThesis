// C# example.

using UnityEditor;
using UnityFeedback;

namespace Assets.Editor
{
	public class BuildingScript
	{
		[MenuItem("Feedback/Build for Windows")]
		public static void BuildGame()
		{
			// Get filepath.
			string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");

			if (path.Length != 0)
			{
				// get scenes
				string[] scenes = FeedbackAPI.Configuration.Scenes();

				// Build player.
				BuildPipeline.BuildPlayer(scenes, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

			}
		}
	}
}