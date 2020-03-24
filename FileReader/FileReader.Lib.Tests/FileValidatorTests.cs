using FileReader.Lib.Enums;
using FileReader.Lib.Helpers;
using System.IO;
using Xunit;

namespace FileReader.Lib.Tests
{
	public class FileValidatorTests
	{
		[Fact]
		public void Validate_EmptyFilePath_ShouldReturnFalseAndValidationMsg()
		{
			// Arrange
			const string filePath = @"";
			const string expectedValidationMsg = "No file path provided";
			var sut = new FileValidator();

			// Act
			var (isValid, validationMsg) = sut.Validate(FileType.Text, filePath);

			// Assert
			Assert.False(isValid);
			Assert.Equal(expectedValidationMsg, validationMsg);
		}

		[Fact]
		public void Validate_UnexistingFile_ShouldReturnFalseAndValidationMsg()
		{
			// Arrange
			const string filePath = @"C:\Nowhere";
			var expectedValidationMsg = $"The file '{filePath}' could not be found.";
			var sut = new FileValidator();

			// Act
			var (isValid, validationMsg) = sut.Validate(FileType.Text, filePath);

			// Assert
			Assert.False(isValid);
			Assert.Equal(expectedValidationMsg, validationMsg);
		}

		[Fact]
		public void Validate_InvalidFileExtensions_ShouldReturnFalseAndValidationMsg()
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var filePath = Path.Combine(dir, "javascriptfile.js");
			var expectedValidationMsg = $"The file '{filePath}' is not a valid {FileType.Text.ToString()} file.";
			var sut = new FileValidator();

			// Act
			var (isValid, validationMsg) = sut.Validate(FileType.Text, filePath);

			// Assert
			Assert.False(isValid);
			Assert.Equal(expectedValidationMsg, validationMsg);
		}

		[Theory]
		[InlineData(0, "regulartextfile.txt")]
		[InlineData(1, "regularxmlfile.xml")]
		public void Validate_ValidFile_ShouldReturnTrueAndNoValidationMsg(int fileType, string fileName)
		{
			// Arrange
			var dir = TestHelper.GetFilesDirectory();
			var filePath = Path.Combine(dir, fileName);
			var type = (FileType)fileType;
			var sut = new FileValidator();

			// Act
			var (isValid, validationMsg) = sut.Validate(type, filePath);

			// Assert
			Assert.True(isValid);
			Assert.Null(validationMsg);
		}
	}
}
