using System;
using System.Diagnostics;
using System.Text;
using UnityFeedback.Configuration;

namespace UnityFeedback.Persistence
{

	/// <summary>
	/// Provides methods and properties used to generate model classes from database schema from <see cref="AppSettings.ConnectionString"/>.
	/// </summary>
	public class ModelGenerator
	{
		private readonly StringBuilder _errorBuilder = new StringBuilder();
		private readonly string _powerShellPath;

		#region Events

		/// <summary>
		/// Subscribe to receive standard output and standard error.
		/// </summary>
		public event EventHandler<OutputEventArgs> OutputReceived;

		#endregion

		#region Constractors

		public ModelGenerator(string powerShellPath)
		{
			this._powerShellPath = powerShellPath;
		}

		#endregion

		/// <summary>
		/// Generates model classes to \Assets\Models directory.
		/// </summary>
		/// <param name="provider">Database provider.</param>
		/// <param name="connectionString">Connection string from database.</param>
		public GenerationResult Generate(DatabaseProvider provider, string connectionString)
		{
			var psi = new ProcessStartInfo(this._powerShellPath, ConfigurationConstants.InternalConstants.MODEL_SCRIPT_PATH)
			{
				UseShellExecute = false,
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
			process.OutputDataReceived += (sender, args) =>
				OnOutputReceived(new OutputEventArgs(OutputType.StandardOutput, args.Data));
			process.ErrorDataReceived += (sender, args) =>
				OnOutputReceived(new OutputEventArgs(OutputType.StandardError, args.Data));
			var result = new GenerationResult();
			var started = false;
			try
			{
				started = process.Start();
			}
			catch (Exception e)
			{
				result.ExitStatus = ExitStatus.ExitFailure;
				result.ErrorMessage = e.Message;
			}

			if (started)
			{
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				// write to stdin
				// first argument is connection string
				process.StandardInput.WriteLine(connectionString);
				// second argument is DB provider
				process.StandardInput.WriteLine(provider.ToString());
				process.StandardInput.Flush();

				process.WaitForExit();

				if (process.ExitCode != 0)
				{
					result.ExitStatus = ExitStatus.ExitFailure;
					result.ErrorMessage = this._errorBuilder.ToString();
				}
				else
				{
					result.ExitStatus = ExitStatus.ExitSuccess;
					result.ErrorMessage = string.Empty;
				}
			}

			process.StandardInput.Close();

			return result;
		}

		protected virtual void OnOutputReceived(OutputEventArgs e)
		{
			if (string.IsNullOrEmpty(e.Data))
			{
				return;
			}

			if (e.OutputType == OutputType.StandardError)
			{
				this._errorBuilder.Append(e.Data);
			}

			var handler = OutputReceived;
			handler?.Invoke(this, e);
		}

		public struct GenerationResult
		{
			public ExitStatus ExitStatus;
			public string ErrorMessage;
		}
	}
}