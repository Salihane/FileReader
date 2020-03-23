using FileReader.Lib.Enums;
using FileReader.Lib.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public class TextFileReader : FileReader
	{
		public TextFileReader(string filePath,
							  IFileValidator fileValidator)
		{
			FilePath = filePath;
			FileValidator = fileValidator;
		}

		public override async Task<string> ReadAsync()
		{
			var (isValid, validationMsg) = FileValidator
				.Validate(FileType.Text, FilePath);

			if (!isValid)
			{
				throw new Exception(validationMsg);
			}

			return await File.ReadAllTextAsync(FilePath);
		}
	}
}
