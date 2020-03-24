using FileReader.Lib.Enums;
using System.Collections.Generic;

namespace FileReader.Lib.Utils
{
	public class FileUtility
	{
		private static readonly IDictionary<FileType, string> FileTypes = new Dictionary<FileType, string>
		{
			{FileType.Text, ".txt"},
			{FileType.Xml, ".xml"},
			{FileType.Json, ".json"}
		};

		public static string GetFileExtensionFromType(FileType fileType)
		{
			return FileTypes.ContainsKey(fileType) ? FileTypes[fileType] : null;
		}
	}
}
