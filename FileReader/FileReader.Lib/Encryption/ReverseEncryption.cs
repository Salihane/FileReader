using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader.Lib.Encryption
{
	public class ReverseEncryption : EncryptionStrategy
	{
		private const string WordsSeparator = " ";

		public override string Decrypt(string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return content;
			}

			var lines = content.Split(Environment.NewLine);
			var reversedContent = ReverseLines(lines);
			return reversedContent;
		}

		private static string ReverseWord(string word)
		{
			if (string.IsNullOrEmpty(word) || word.Length == 1)
			{
				return word;
			}

			var chars = word.ToCharArray();
			Array.Reverse(chars);
			return new string(chars);
		}

		private static string ReverseLine(string line)
		{
			if (string.IsNullOrEmpty(line) || line.Length == 1)
			{
				return line;
			}

			var words = line.Split(WordsSeparator);
			var builder = new StringBuilder();
			foreach (var word in words)
			{
				builder.Append($"{ReverseWord(word)}{WordsSeparator}");
			}

			return builder.ToString().TrimEnd();
		}

		private static string ReverseLines(IReadOnlyList<string> lines)
		{
			var builder = new StringBuilder();
			for (var i = 0; i < lines.Count; i++)
			{
				if (i != 0)
				{
					builder.Append(Environment.NewLine);
				}

				builder.Append(ReverseLine(lines[i]));
			}

			return builder.ToString();
		}
	}
}
