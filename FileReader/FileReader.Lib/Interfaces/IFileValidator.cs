using FileReader.Lib.Enums;

namespace FileReader.Lib.Interfaces
{
	public interface IFileValidator
	{
		(bool isValid, string validationMsg) Validate(FileType fileType, string filePath);
	}
}
