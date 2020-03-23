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
				"<?xml version=\"1.0\" encoding=\"utf-8\"?><customers><customer><name>Customer 1</name><Address><Street>Hello street</Street><HouseNr>87</HouseNr><Box>4b</Box><ZipCode>1000</ZipCode><City>Brussels</City></Address></customer><customer><name>Customer 2</name><Address><Street>Morning street</Street><HouseNr>5</HouseNr><Box>10</Box><ZipCode>9900</ZipCode><City>Gent</City></Address></customer></customers>";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
