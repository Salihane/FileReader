using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FileReader.Gui.Helpers
{
	public class NotificationObject : INotifyPropertyChanged
	{
		protected void RaisePropertyChanged<T>(Expression<Func<T>> action)
		{
			RaisePropertyChanged(GetPropertyName(action));
		}

		private static string GetPropertyName<T>(Expression<Func<T>> action)
		{
			var expression = (MemberExpression)action.Body;
			return expression.Member.Name;
		}

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
