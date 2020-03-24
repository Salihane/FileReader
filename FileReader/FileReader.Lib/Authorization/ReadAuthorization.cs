using FileReader.Lib.Enums;
using System.IO;

namespace FileReader.Lib.Authorization
{
	public class ReadAuthorization : RoleBasedAuthorization
	{
		public ReadAuthorization(UserRole userRole)
		{
			UserRole = userRole;
		}

		// This is a dummy implementation to demonstrate an authorization process
		public override bool IsAuthorized(string filePath)
		{
			var fileName = Path.GetFileName(filePath);

			return UserRole switch
			{
				UserRole.Admin => true,
				UserRole.Consultant => (fileName.Length > 10),
				UserRole.Accountant => (fileName.Length < 15),
				_ => false
			};
		}
	}
}
