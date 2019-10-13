using System.Diagnostics;
using UnityFeedback.Configuration;

namespace UnityFeedback.Persistence
{
	public class ModelGenerator
	{
		public static void Execute()
		{
			var psi = new ProcessStartInfo("powershell.exe", "powershell/generateModel.ps1")
			{
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardError = true,
				RedirectStandardInput = true,
				RedirectStandardOutput = true
			};

			var process = new Process()
			{
				StartInfo = psi

			};

			// redirect stdout and stderr
			process.OutputDataReceived += (sender, args) => UnityEngine.Debug.Log("output: " + args.Data);
			process.ErrorDataReceived += (sender, args) => UnityEngine.Debug.LogError("error: " + args.Data);

			process.Start();

			process.BeginOutputReadLine();
			process.BeginErrorReadLine();

			// write to stdin
			process.StandardInput.WriteLine(AppSettings.Instance.ConnectionString);

		}

	}
}