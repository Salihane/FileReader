using System.IO;
using System.Reflection;

namespace FileReader.Lib.Tests
{
	public static class TestHelper
	{
		public static string GetFilesDirectory()
		{
			var assemblyPath = Path
				.GetDirectoryName(Assembly.GetAssembly(typeof(TextFileReaderTests))
										  .Location);

			return Path.Combine(assemblyPath, "Resources");
		}
	}
}
