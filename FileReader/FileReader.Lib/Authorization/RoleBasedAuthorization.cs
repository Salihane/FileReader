using FileReader.Lib.Enums;

namespace FileReader.Lib.Authorization
{
	public abstract class RoleBasedAuthorization
	{
		public abstract bool IsAuthorized(string filePath);
		protected UserRole UserRole { get; set; }
	}
}
