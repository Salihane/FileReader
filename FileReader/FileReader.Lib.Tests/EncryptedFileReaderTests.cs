using FileReader.Lib.Encryption;
using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using System.IO;
using System.Threading.Tasks;
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

		[Fact]
		public async Task Read_EncryptedXmlFile_ShouldReturnDecryptedFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "encryptedxmlfile.xml");
			var xmlFileReader = new XmlFileReader(path, new FileValidator()) { Strict = false };
			var sut = new EncryptedFileReader(xmlFileReader, new Base64Encryption());
			const string expectedFileContent =
				"<?xml version=\"1.0\" encoding=\"utf-8\"?><customers><customer><name>Customer 1</name><Address><Street>Hello street</Street><HouseNr>87</HouseNr><Box>4b</Box><ZipCode>1000</ZipCode><City>Brussels</City></Address></customer><customer><name>Customer 2</name><Address><Street>Morning street</Street><HouseNr>5</HouseNr><Box>10</Box><ZipCode>9900</ZipCode><City>Gent</City></Address></customer></customers>";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		[Fact]
		public async Task Read_EncryptedJsonFile_ShouldReturnDecryptedFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "encryptedjsonfile.json");
			var jsonFileReader = new JsonFileReader(path, new FileValidator());
			var sut = new EncryptedFileReader(jsonFileReader, new Base64Encryption());
			const string expectedFileContent =
				"{\r\n\t\"customers\": [\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 1\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Hello street\",\r\n\t\t\t\t\"houseNr\": \"87\",\r\n\t\t\t\t\"box\": \"4b\",\r\n\t\t\t\t\"zipCode\": \"1000\",\r\n\t\t\t\t\"city\": \"Brussels\"\r\n\t\t\t}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 2\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Morning street\",\r\n\t\t\t\t\"houseNr\": \"5\",\r\n\t\t\t\t\"box\": \"10\",\r\n\t\t\t\t\"zipCode\": \"9900\",\r\n\t\t\t\t\"city\": \"Gent\"\r\n\t\t\t}\r\n\t\t}\r\n\t]\r\n}";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
