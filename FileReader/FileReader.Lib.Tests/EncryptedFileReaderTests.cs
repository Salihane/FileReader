using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader.Lib.Encryption;
using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using Xunit;

namespace FileReader.Lib.Tests
{
    public class EncryptedFileReaderTests
    {
		[Fact]
		public async Task Read_EncryptedTextFile_ShouldReturnDecryptedFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "encryptedtextfile.txt");
			var textFileReader = new TextFileReader(path, new FileValidator());
			var sut = new EncryptedFileReader(textFileReader, new ReverseEncryption());
			const string expectedFileContent = "Hello there!\r\nThis is an encrypted text file.\r\n\r\n\r\n";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
