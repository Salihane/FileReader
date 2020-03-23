using FileReader.Lib.Encryption;
using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public class EncryptedFileReader : FileReaderDecorator
	{
		private EncryptionStrategy _encryptionStrategy;

		public EncryptedFileReader(FileReader fileReader,
							   EncryptionStrategy encryptionStrategy)
			: base(fileReader)
		{
			_encryptionStrategy = encryptionStrategy;
		}

		public void SetEncryptionStrategy(EncryptionStrategy encryptionStrategy)
		{
			_encryptionStrategy = encryptionStrategy;
		}

		public override async Task<string> ReadAsync()
		{
			var content = await base.ReadAsync();
			var encryptedContent = _encryptionStrategy.Decrypt(content);

			return encryptedContent;
		}
	}
}
