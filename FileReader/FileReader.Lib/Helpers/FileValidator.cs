using FileReader.Lib.Enums;
using FileReader.Lib.Interfaces;
using FileReader.Lib.Utils;
using System;
using System.IO;

namespace FileReader.Lib.Helpers
{
	public class FileValidator : IFileValidator
	{
		public (bool isValid, string validationMsg) Validate(FileType fileType, string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return (false, "No file path provided");
			}

			if (!File.Exists(filePath))
			{
				return (false, $"The file '{filePath}' could not be found.");
			}

			var pathExtension = Path.GetExtension(filePath);
			var typeExtension = FileUtility.GetFileExtensionFromType(fileType);

			return !string.Equals(pathExtension, typeExtension, StringComparison.CurrentCultureIgnoreCase)
				? (false, $"The file '{filePath}' is not a valid {fileType.ToString()} file.")
				: (true, null);
		}
	}
}
