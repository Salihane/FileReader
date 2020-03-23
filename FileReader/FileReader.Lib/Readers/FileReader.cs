using FileReader.Lib.Interfaces;
using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public abstract class FileReader
	{
		protected IFileValidator FileValidator { get; set; }
		public string FilePath { get; protected set; }
		public abstract Task<string> ReadAsync();
	}
}
