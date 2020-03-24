using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public class FileReaderDecorator : FileReader
	{
		protected Readers.FileReader Reader { get; set; }

		public FileReaderDecorator(Readers.FileReader fileReader)
		{
			Reader = fileReader;
			FilePath = fileReader.FilePath;
		}

		public override async Task<string> ReadAsync()
		{
			return await Reader.ReadAsync();
		}
	}
}
