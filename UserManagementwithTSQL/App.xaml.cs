using SimpleInjector;
using System.Windows;
using UserManagementwithTSQL.ViewModels;
using UserManagementwithTSQL.Views.Pages;
using UserManagementwithTSQL.Views.Windows;

namespace UserManagementwithTSQL;

public partial class App : Application
{
	public static Container? Container { get; set; } = new();

	private void StartApp(object sender, StartupEventArgs e)
	{
		try
		{
			RegisterOfViews();
			RegisterOfViewModels();

			var main = Container?.GetInstance<MainWindowView>();
			main!.DataContext = Container?.GetInstance<MainWindowViewModel>();

			var firstPage = Container?.GetInstance<DashboardView>();
			firstPage!.DataContext = Container?.GetInstance<DashboardViewModel>();

			main.MainFrame.Navigate(firstPage);

			main.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"{ex.Message}", "Error in StartApp", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	public void RegisterOfViews()
	{
		Container?.RegisterSingleton<MainWindowView>();
		Container?.RegisterSingleton<DashboardView>();

	}

	public void RegisterOfViewModels()
	{
		Container?.RegisterSingleton<MainWindowViewModel>();
		Container?.RegisterSingleton<DashboardViewModel>();

	}
}
