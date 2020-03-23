using FileReader.Lib.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReader.Lib
{
	public class TextFileReader : IFileReader
	{
		private const string TextExtension = ".txt";

		public async Task<string> ReadAsync(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new Exception($"The file '{filePath}' could not be found.");
			}

			var extension = Path.GetExtension(filePath);
			if (!string.Equals(extension, TextExtension, StringComparison.CurrentCultureIgnoreCase))
			{
				throw new Exception($"The file '{filePath}' is not a valid TEXT file");
			}

			var content = await File.ReadAllTextAsync(filePath);
			return content;
		}
	}
}
