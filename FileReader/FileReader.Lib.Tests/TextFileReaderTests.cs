using FileReader.Lib.Helpers;
using FileReader.Lib.Readers;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class TextFileReaderTests
	{
		[Fact]
		public async Task Read_UnexistingFile_ShouldThrowException()
		{
			// Arrange
			var path = @"C:\Nowhere";
			var sut = new TextFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());
		}

		[Fact]
		public async Task Read_InvalidTextFile_ShouldThrowException()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "javascriptfile.js");
			var sut = new TextFileReader(path, new FileValidator());

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync());

		}

		[Fact]
		public async Task Read_ValidTextFile_ShouldReturnFileContent()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var path = Path.Combine(dir, "regulartextfile.txt");
			var sut = new TextFileReader(path, new FileValidator());
			const string expectedFileContent = "Hello there!\r\nThis is a regular text file.\r\n\r\n\r\n";

			// Act
			var fileContent = await sut.ReadAsync();

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}
	}
}
