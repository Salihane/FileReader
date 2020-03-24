using FileReader.Lib.Authorization;
using FileReader.Lib.Encryption;
using FileReader.Lib.Enums;
using FileReader.Lib.Helpers;
using FileReader.Lib.Interfaces;

namespace FileReader.Lib.Readers
{
	public class FileReaderBuilder : IFileReaderBuilder
	{
		private readonly IFileValidator _fileValidator;
		private readonly string _filePath;
		private FileReader _fileReader;

		public FileReaderBuilder(string filePath,
								 IFileValidator fileValidator)
		{
			_filePath = filePath;
			_fileValidator = fileValidator;
		}

		public FileReaderBuilder(string filePath)
			: this(filePath, new FileValidator())
		{
		}

		public IFileReaderBuilder Init(FileType fileType)
		{
			InitFileReader(fileType);
			return this;
		}

		public IFileReaderBuilder UseEncryption(EncryptionStrategy encryptionStrategy)
		{
			if (_fileReader == null)
			{
				InitFileReader(FileType.Text);
			}

			_fileReader = new EncryptedFileReader(_fileReader, encryptionStrategy);
			return this;
		}

		public IFileReaderBuilder UseAuthorization(RoleBasedAuthorization authorization)
		{
			if (_fileReader == null)
			{
				InitFileReader(FileType.Text);
			}

			_fileReader = new SecuredFileReader(_fileReader, authorization);
			return this;
		}

		public FileReader Build()
		{
			return _fileReader;
		}

		private void InitFileReader(FileType fileType)
		{
			_fileReader = fileType switch
			{
				FileType.Text => new TextFileReader(_filePath, _fileValidator),
				FileType.Xml => new XmlFileReader(_filePath, _fileValidator),
				FileType.Json => new JsonFileReader(_filePath, _fileValidator),
				_ => _fileReader
			};
		}
	}
}
