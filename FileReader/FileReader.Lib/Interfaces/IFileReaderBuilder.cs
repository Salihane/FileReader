using FileReader.Lib.Authorization;
using FileReader.Lib.Encryption;
using FileReader.Lib.Enums;

namespace FileReader.Lib.Interfaces
{
	public interface IFileReaderBuilder
	{
		IFileReaderBuilder Init(FileType fileType);
		IFileReaderBuilder UseEncryption(EncryptionStrategy encryptionStrategy);
		IFileReaderBuilder UseAuthorization(RoleBasedAuthorization authorization);
		Readers.FileReader Build();
	}
}
