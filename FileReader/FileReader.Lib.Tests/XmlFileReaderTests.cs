using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class XmlFileReaderTests
	{

		[Fact]
		public async Task Read_UnexistingFile_ShouldThrowException()
		{
			// Arrange
			var path = @"C:\Nowhere";
			var sut = new XmlFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());
		}

		[Fact]
		public async Task Read_InvalidXmlFile_ShouldThrowException()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "javascriptfile.js");
			var sut = new XmlFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());

		}

		[Fact]
		public async Task Read_ValidXmlFile_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularxmlfile.xml");
			var sut = new XmlFileReader(path, new FileValidator());
			const string expectedFileContent =
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<customers>\r\n\t<customer>\r\n\t\t<name>Customer 1</name>\r\n\t\t<Address>\r\n\t\t\t<Street>Hello street</Street>\r\n\t\t\t<HouseNr>87</HouseNr>\r\n\t\t\t<Box>4b</Box>\r\n\t\t\t<ZipCode>1000</ZipCode>\r\n\t\t\t<City>Brussels</City>\r\n\t\t</Address>\r\n\t</customer>\r\n\t<customer>\r\n\t\t<name>Customer 2</name>\r\n\t\t<Address>\r\n\t\t\t<Street>Morning street</Street>\r\n\t\t\t<HouseNr>5</HouseNr>\r\n\t\t\t<Box>10</Box>\r\n\t\t\t<ZipCode>9900</ZipCode>\r\n\t\t\t<City>Gent</City>\r\n\t\t</Address>\r\n\t</customer>\r\n</customers>";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
