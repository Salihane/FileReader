using System;
using System.IO;
using System.Reflection;
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
			var sut = new TextFileReader();
			var path = @"C:\Nowhere";

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync(path));
		}

		[Fact]
		public async Task Read_InvalidTextFile_ShouldThrowException()
		{
			// Arrange
			var sut = new TextFileReader();
			var dir = GetFilesDirectory();
			var path = Path.Combine(dir, "javascriptfile.js");

			// Act/Assert
			await Assert.ThrowsAsync<Exception>(() => sut.ReadAsync(path));

		}

		[Fact]
		public async Task Read_ValidTextFile_ShouldReturnFileContent()
		{
			// Arrange
			var sut = new TextFileReader();
			var dir = GetFilesDirectory();
			var path = Path.Combine(dir, "regulartextfile.txt");
			const string expectedFileContent = "Hello there!\r\nThis is a regular text file.\r\n\r\n\r\n";

			// Act
			var fileContent = await sut.ReadAsync(path);

			// Assert
			Assert.Equal(expectedFileContent, fileContent);
		}

		private string GetFilesDirectory()
		{
			var assemblyPath = Path
				.GetDirectoryName(Assembly.GetAssembly(typeof(TextFileReaderTests))
										  .Location);

			return Path.Combine(assemblyPath, "Resources");
		}
	}
}
