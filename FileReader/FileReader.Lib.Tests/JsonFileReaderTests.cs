using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class JsonFileReaderTests
	{
		[Fact]
		public async Task Read_UnexistingFile_ShouldThrowException()
		{
			// Arrange
			var path = @"C:\Nowhere";
			var sut = new JsonFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());
		}

		[Fact]
		public async Task Read_InvalidJsonFile_ShouldThrowException()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "javascriptfile.js");
			var sut = new JsonFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());
		}

		[Fact]
		public async Task Read_ValidJsonFile_ReturnsFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularjsonfile.json");
			var sut = new JsonFileReader(path, new FileValidator());
			const string expectedFileContent =
				"{\r\n\t\"customers\": [\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 1\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Hello street\",\r\n\t\t\t\t\"houseNr\": \"87\",\r\n\t\t\t\t\"box\": \"4b\",\r\n\t\t\t\t\"zipCode\": \"1000\",\r\n\t\t\t\t\"city\": \"Brussels\"\r\n\t\t\t}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 2\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Morning street\",\r\n\t\t\t\t\"houseNr\": \"5\",\r\n\t\t\t\t\"box\": \"10\",\r\n\t\t\t\t\"zipCode\": \"9900\",\r\n\t\t\t\t\"city\": \"Gent\"\r\n\t\t\t}\r\n\t\t}\r\n\t]\r\n}";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
