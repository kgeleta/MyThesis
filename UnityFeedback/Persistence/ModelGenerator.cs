using System;
using System.Diagnostics;
using UnityFeedback.Configuration;

namespace UnityFeedback.Persistence
{

	/// <summary>
	/// Provides methods and properties used to generate model classes from database schema from <see cref="AppSettings.ConnectionString"/>.
	/// </summary>
	public class ModelGenerator
	{
		private readonly Process _process;

		#region Events

		/// <summary>
		/// Subscribe to receive standard output and standard error.
		/// </summary>
		public event EventHandler<OutputEventArgs> OutputReceived;

		#endregion

		#region Properties

		/// <summary>
		/// Gets error message if <see cref="ModelGenerator.Generate"/> returned <see cref="ExitStatus.ExitFailure"/> or empty string otherwise.
		/// </summary>
		public string ErrorMessage { get; private set; } = string.Empty;

		/// <summary>
		/// Gets all output from stdout.
		/// </summary>
		public string OutputMessage { get; private set; } = string.Empty;

		#endregion

		public ModelGenerator()
		{
			var psi = new ProcessStartInfo("powershell.exe", ConfigurationConstants.InternalConstants.MODEL_SCRIPT_PATH)
			{
				UseShellExecute = false,
				ErrorDialog = false,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardError = true,
				RedirectStandardInput = true,
				RedirectStandardOutput = true
			};

			this._process = new Process()
			{
				StartInfo = psi

			};

			// redirect stdout and stderr
			_process.OutputDataReceived += (sender, args) =>
				OnOutputReceived(new OutputEventArgs(OutputType.StandardOutput, args.Data));
			_process.ErrorDataReceived += (sender, args) =>
				OnOutputReceived(new OutputEventArgs(OutputType.StandardError, args.Data));
		}

		/// <summary>
		/// Generates model classes to \Assets\Models directory.
		/// </summary>
		/// <param name="provider">Database provider</param>
		public ExitStatus Generate(DatabaseProvider provider)
		{
			_process.Start();

			_process.BeginOutputReadLine();
			_process.BeginErrorReadLine();

			// write to stdin
			// first argument is connection string
			_process.StandardInput.WriteLine(AppSettings.Instance.ConnectionString(false));
			// second argument is DB provider
			_process.StandardInput.WriteLine(provider.ToString());

			_process.WaitForExit();

			this.OutputMessage = _process.StandardOutput.ReadToEnd();
			if (_process.ExitCode != 0)
			{
				this.ErrorMessage = _process.StandardError.ReadToEnd();
				return ExitStatus.ExitFailure;
			}

			return ExitStatus.ExitSuccess;
		}

		protected virtual void OnOutputReceived(OutputEventArgs e)
		{
			var handler = OutputReceived;
			handler?.Invoke(this, e);
		}

	}
}