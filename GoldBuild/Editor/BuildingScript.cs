// C# example.

using UnityEditor;
using UnityFeedback;

namespace Assets.Editor
{
	public class BuildingScript {
//        [MenuItem("MyTools/Windows Build With Postprocess")]
        public static void BuildGame()
        {
            // Get filename.
            string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
           // string configSourcePath = $@"{Application.dataPath}\Configuration\BuildConfig.xml";
           // string configDestinationPath = $@"{Application.persistentDataPath}\BuildConfig.xml";

            // copy config file
            //File.Copy(configSourcePath, configDestinationPath, true);

			// get scenes
			string[] scenes = FeedbackAPI.Settings.Scenes(); //ConfigReader.ReadScenes(configSourcePath);
                     
            // Build player.
            BuildPipeline.BuildPlayer(scenes, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        }
    }
}