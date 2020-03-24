using FileReader.Lib.Authorization;
using FileReader.Lib.Encryption;
using FileReader.Lib.Enums;
using FileReader.Lib.Readers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class FileReaderBuilderTests
	{

		[Fact]
		public async Task Read_RegularJsonFile_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularjsonfile.json");
			var sut = new FileReaderBuilder(path)
			          .Init(FileType.Json)
			          .Build();

			const string expectedFileContent =
				"{\r\n\t\"customers\": [\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 1\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Hello street\",\r\n\t\t\t\t\"houseNr\": \"87\",\r\n\t\t\t\t\"box\": \"4b\",\r\n\t\t\t\t\"zipCode\": \"1000\",\r\n\t\t\t\t\"city\": \"Brussels\"\r\n\t\t\t}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 2\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Morning street\",\r\n\t\t\t\t\"houseNr\": \"5\",\r\n\t\t\t\t\"box\": \"10\",\r\n\t\t\t\t\"zipCode\": \"9900\",\r\n\t\t\t\t\"city\": \"Gent\"\r\n\t\t\t}\r\n\t\t}\r\n\t]\r\n}";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredXmlFileAsReceptionist_ShouldReturnUnauthorizedMsg()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regulartextfile.xml");
			var sut = new FileReaderBuilder(path)
					  .Init(FileType.Xml)
					  .UseAuthorization(new ReadAuthorization(UserRole.Receptionist))
					  .Build();

			const string expectedFileContent = "Unauthorized request.";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredAndEncryptedTextFileAsAdmin_ShouldReturnDecryptedFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "encryptedtextfile.txt");
			var sut = new FileReaderBuilder(path)
					  .Init(FileType.Text)
					  .UseEncryption(new ReverseEncryption())
					  .UseAuthorization(new ReadAuthorization(UserRole.Admin))
					  .Build();

			const string expectedFileContent = "Hello there!\r\nThis is an encrypted text file.\r\n\r\n\r\n";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
