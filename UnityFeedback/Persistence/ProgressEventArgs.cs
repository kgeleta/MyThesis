using System;

namespace UnityFeedback.Persistence
{
	public class ProgressEventArgs : EventArgs
	{
		/// <summary>
		/// Number between 0 and 1 representing progress.
		/// </summary>
		public readonly float Progress;

		public ProgressEventArgs(float progress)
		{
			if (progress < 0f)
			{
				throw new ArgumentException("Progress value must be greater or equal to 0");
			}
			this.Progress = progress;
		}
	}
}