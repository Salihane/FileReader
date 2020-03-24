using FileReader.Lib.Authorization;
using System.Threading.Tasks;

namespace FileReader.Lib.Readers
{
	public class SecuredFileReader : FileReaderDecorator
	{
		private RoleBasedAuthorization _roleBasedAuthorization;

		public SecuredFileReader(FileReader fileReader,
								 RoleBasedAuthorization roleBasedAuthorization)
			: base(fileReader)
		{
			_roleBasedAuthorization = roleBasedAuthorization;
		}

		public void SetAuthorization(RoleBasedAuthorization roleBasedAuthorization)
		{
			_roleBasedAuthorization = roleBasedAuthorization;
		}

		public override async Task<string> ReadAsync()
		{
			if (!_roleBasedAuthorization.IsAuthorized(FilePath))
			{
				return "Unauthorized request.";
			}

			var content = await base.ReadAsync();
			return content;
		}
	}
}
