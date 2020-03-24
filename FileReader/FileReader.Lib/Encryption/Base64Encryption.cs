using System;
using System.Text;

namespace FileReader.Lib.Encryption
{
	public class Base64Encryption : EncryptionStrategy
	{
		public override string Decrypt(string content)
		{
			try
			{
				var decryptedContent = Encoding.UTF8.GetString(Convert.FromBase64String(content));
				return decryptedContent;
			}
			catch (Exception ex)
			{
				// todo: log the exception details

				return "An error occured while decrypting the content.";
			}
		}
	}
}
