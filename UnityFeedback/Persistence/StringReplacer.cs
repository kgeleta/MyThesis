using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityFeedback.Persistence
{
	public class StringReplacer
	{
		/// <summary>
		/// Contains strings to replace as pairs of original string and new string.
		/// </summary>
		public Dictionary<string, string> StringsToReplace = new Dictionary<string, string>();

		private readonly string _pathToDirectory;
		private readonly string _fileExtensions;

		/// <summary>
		/// Creates new instance of <see cref="StringReplacer"/>.
		/// </summary>
		/// <param name="pathToDirectory">Path to directory with files to replace strings in.</param>
		/// <param name="fileExtensions">File extensions separated by semicolon. For example "*.json; *.xml; *.txt"</param>
		public StringReplacer(string pathToDirectory, string fileExtensions)
		{
			this._pathToDirectory = pathToDirectory;
			this._fileExtensions = fileExtensions;
		}

		/// <summary>
		/// Replaces all occurrences of all <see cref="StringsToReplace"/> keys with values in files that match fileName parameter.
		/// </summary>
		/// <param name="fileName">Optional name of file in which replacement should be performed (supports regex). By default this will match all files.</param>
		public void DoSwap(string fileName = @".*")
		{
			if (this.StringsToReplace.Count == 0)
			{
				return;
			}

			var fileNameRegex = new Regex(fileName);

			// Get files matching regex
			var fileNames = Directory.GetFiles(this._pathToDirectory, this._fileExtensions)
				.Where(path => fileNameRegex.IsMatch(path))
				.ToList();

			// for each file:
			foreach (var name in fileNames)
			{
				string text = File.ReadAllText(name);
				foreach (var stringPair in this.StringsToReplace)
				{
					text = text.Replace(stringPair.Key, stringPair.Value);
				}
				File.WriteAllText(name, text);
			}

		}

	}
}