using System;

namespace UnityFeedback.Persistence
{
	public class OutputEventArgs : EventArgs
	{
		/// <summary>
		/// Gets type of output data comes from.
		/// </summary>
		public readonly OutputType OutputType;

		public readonly string Data;

		public OutputEventArgs(OutputType outputType, string data)
		{
			this.OutputType = outputType;
			this.Data = data;
		}
	}
}