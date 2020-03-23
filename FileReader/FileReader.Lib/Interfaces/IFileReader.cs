using System.Threading.Tasks;

namespace FileReader.Lib.Interfaces
{
	public interface IFileReader
	{
		Task<string> ReadAsync(string filePath);
	}
}
