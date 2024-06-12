using System.Windows;
using UserManagementwithTSQL.ViewModels;

namespace UserManagementwithTSQL.Views;

public partial class AddUserWindow : Window
{
	public AddUserWindow()
	{
		InitializeComponent();

		DataContext = new AddUserViewModel();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		UsersList.Items.Refresh();
	}
}
