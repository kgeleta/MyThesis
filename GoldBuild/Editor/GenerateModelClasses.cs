using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityFeedback;
using UnityFeedback.Persistence;

namespace Assets.Editor
{
	public class GenerateModelClasses
	{
		[MenuItem("Feedback/Generate model classes")]
		public static void GenerateModel()
		{
			// TODO: check config first

			var window = (ModelGenerationProgress)EditorWindow.GetWindow(typeof(ModelGenerationProgress));

			var context = TaskScheduler.FromCurrentSynchronizationContext();
			var task = FeedbackAPI.GetModelCreatorTask();

			task.ContinueWith(t => {
				window.EndMessage = t.Result.ExitStatus == ExitStatus.ExitSuccess ? "Successfully generated model classes" : "Error occurred while generating";
				window.ProgressValue = t.Result.ExitStatus == ExitStatus.ExitSuccess ? 1f : 0f;
				AssetDatabase.Refresh();
			}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, context);

			task.ContinueWith(t =>
			{
				window.EndMessage = "Error occured";
				if (t.Exception == null)
				{
					return;
				}
				foreach (var exception in t.Exception.InnerExceptions)
				{
					window.AddError(exception.Message);
				}
			}, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, context);
			task.Start();

			window.titleContent = new GUIContent("Generating model classes");
			window.maxSize = new Vector2(675f, 560f);
			window.minSize = window.maxSize;

			FeedbackAPI.ModelCreatorOutput += (sender, e) => {
				if (e.OutputType == OutputType.StandardError)
				{
					window.AddError(e.Data);
				}
				else
				{
					window.AddMessage(e.Data);
				}
			};

			FeedbackAPI.ModelCreatorProgress += (sender, e) => window.ProgressValue = e.Progress;

			window.Show();
		}
	}
}
