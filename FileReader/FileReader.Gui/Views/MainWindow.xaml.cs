using System.Windows;
using FileReader.Gui.ViewModels;

namespace FileReader.Gui.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var viewModel = new MainViewModel();
			DataContext = viewModel;
		}
	}
}
