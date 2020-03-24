using FileReader.Lib.Enums;
using FileReader.Lib.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace FileReader.Lib.Readers
{
	public class XmlFileReader : FileReader
	{
		public bool Strict { get; set; } = true;

		public XmlFileReader(string filePath,
							 IFileValidator fileValidator)
		{
			FilePath = filePath;
			FileValidator = fileValidator;
		}

		public override async Task<string> ReadAsync()
		{
			var (isValid, validationMsg) = FileValidator
				.Validate(FileType.Xml, FilePath);

			if (!isValid)
			{
				throw new Exception(validationMsg);
			}

			if (Strict)
			{
				var xmlDoc = new XmlDocument();
				await Task.Run(() => xmlDoc.Load(FilePath));
				return xmlDoc.InnerXml;
			}

			// Read XML file with invalid content (e.g. encrypted content)
			return await File.ReadAllTextAsync(FilePath);
		}
	}
}
