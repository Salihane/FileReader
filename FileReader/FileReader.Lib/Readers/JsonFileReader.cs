using FileReader.Lib.Enums;
using FileReader.Lib.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public class JsonFileReader : FileReader
	{
		public JsonFileReader(string filePath,
							  IFileValidator fileValidator)
		{
			FilePath = filePath;
			FileValidator = fileValidator;
		}

		public override async Task<string> ReadAsync()
		{
			var (isValid, validationMsg) = FileValidator
				.Validate(FileType.Json, FilePath);

			if (!isValid)
			{
				throw new Exception(validationMsg);
			}

			// todo: the code below is disabled temporary, it causes multi-threading issues in the WPF client.
			// todo: temp solution is to use the File.ReadAllTextAsync().
			//using var reader = new StreamReader(FilePath);
			//return await reader.ReadToEndAsync();

			return await File.ReadAllTextAsync(FilePath);
		}
	}
}
