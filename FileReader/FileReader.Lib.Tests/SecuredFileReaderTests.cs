﻿using FileReader.Lib.Authorization;
using FileReader.Lib.Enums;
using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class SecuredFileReaderTests
	{
		[Fact]
		public async Task Read_SecuredXmlFileAsAdmin_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularxmlfile.xml");
			var xmlFileReader = new XmlFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(xmlFileReader, new ReadAuthorization(UserRole.Admin));
			const string expectedFileContent =
				"<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<customers>\r\n\t<customer>\r\n\t\t<name>Customer 1</name>\r\n\t\t<Address>\r\n\t\t\t<Street>Hello street</Street>\r\n\t\t\t<HouseNr>87</HouseNr>\r\n\t\t\t<Box>4b</Box>\r\n\t\t\t<ZipCode>1000</ZipCode>\r\n\t\t\t<City>Brussels</City>\r\n\t\t</Address>\r\n\t</customer>\r\n\t<customer>\r\n\t\t<name>Customer 2</name>\r\n\t\t<Address>\r\n\t\t\t<Street>Morning street</Street>\r\n\t\t\t<HouseNr>5</HouseNr>\r\n\t\t\t<Box>10</Box>\r\n\t\t\t<ZipCode>9900</ZipCode>\r\n\t\t\t<City>Gent</City>\r\n\t\t</Address>\r\n\t</customer>\r\n</customers>";

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
			var path = Path.Combine(dir, "regularxmlfile.xml");
			var xmlFileReader = new XmlFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(xmlFileReader, new ReadAuthorization(UserRole.Receptionist));
			const string expectedContent = "Unauthorized request.";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredTextFileAsAdmin_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regulartextfile.txt");
			var textFileReader = new TextFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(textFileReader, new ReadAuthorization(UserRole.Admin));
			const string expectedFileContent = "Hello there!\r\nThis is a regular text file.\r\n\r\n\r\n";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredTextFileAsReceptionist_ShouldReturnUnauthorizedMsg()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regulartextfile.txt");
			var textFileReader = new TextFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(textFileReader, new ReadAuthorization(UserRole.Receptionist));
			const string expectedContent = "Unauthorized request.";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredJsonFileAsAdmin_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularjsonfile.json");
			var jsonFileReader = new JsonFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(jsonFileReader, new ReadAuthorization(UserRole.Admin));
			const string expectedFileContent =
				"{\r\n\t\"customers\": [\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 1\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Hello street\",\r\n\t\t\t\t\"houseNr\": \"87\",\r\n\t\t\t\t\"box\": \"4b\",\r\n\t\t\t\t\"zipCode\": \"1000\",\r\n\t\t\t\t\"city\": \"Brussels\"\r\n\t\t\t}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"Customer 2\",\r\n\t\t\t\"address\": {\r\n\t\t\t\t\"street\": \"Morning street\",\r\n\t\t\t\t\"houseNr\": \"5\",\r\n\t\t\t\t\"box\": \"10\",\r\n\t\t\t\t\"zipCode\": \"9900\",\r\n\t\t\t\t\"city\": \"Gent\"\r\n\t\t\t}\r\n\t\t}\r\n\t]\r\n}";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		[Fact]
		public async Task Read_SecuredJsonFileAsReceptionist_ShouldReturnUnauthorizedMsg()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regularjsonfile.json");
			var jsonFileReader = new JsonFileReader(path, new FileValidator());
			var sut = new SecuredFileReader(jsonFileReader, new ReadAuthorization(UserRole.Receptionist));
			const string expectedContent = "Unauthorized request.";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedContent, fileContent);
		}
	}
}
