using System;

namespace FileReader.Gui.Helpers
{
	public static class Utils
	{
		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
	}
}
