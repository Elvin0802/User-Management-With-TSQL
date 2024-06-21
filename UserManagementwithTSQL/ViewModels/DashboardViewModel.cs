using System.Windows;
using System.Windows.Input;
using UserManagementwithTSQL.Models;
using UserManagementwithTSQL.ViewModels.Global;
using UserManagementwithTSQL.Views.Pages;

namespace UserManagementwithTSQL.ViewModels;

public class DashboardViewModel : BaseViewModel
{
	public DashboardViewModel()
	{
		User = new();
		Users = new();

		SignInCommand = new RelayCommand(SignInExecute, CanSignIn);
		SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);

		GetAllCommand = new RelayCommand(GetAllExecute, CanGetAll);
		SaveAllCommand = new RelayCommand(SaveAllExecute, CanSaveAll);

		RefreshCommand = new RelayCommand(RefreshExecute, CanRefresh);

		DeleteUserCommand = new RelayCommand(DeleteUserExecute, CanDeleteUser);
		DeleteAllCommand =new RelayCommand(DeleteAllExecute, CanDeleteAll);
	}

	private User? _user;
	private List<User>? _users;

	public User? User { get => _user; set { _user = value; OnPropertyChanged(); } }
	public List<User>? Users { get => _users; set { _users = value; OnPropertyChanged(); } }


	#region SignUpCommand Section

	public ICommand SignUpCommand { get; set; }

	public bool CanSignUp(object? param)
	{
		foreach (var user in Users!)
		{
			if (user.Username == User!.Username)
				return false;
		}
		return true;
	}

	public void SignUpExecute(object? param)
	{
		try
		{
			Users!.Add(User!);
			User = new();

			MessageBox.Show("Sign Up Successfull.", "Sign Up Info", MessageBoxButton.OK, MessageBoxImage.Information);
			return;
		}
		catch
		{
			MessageBox.Show("Sign Up Not Successfull.", "Sign Up Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region SignInCommand Section

	public ICommand SignInCommand { get; set; }

	public bool CanSignIn(object? param)
	{
		if (string.IsNullOrEmpty(User!.Username) || string.IsNullOrEmpty(User!.Password))
			return false;
		return true;
	}
	public void SignInExecute(object? param)
	{
		try
		{
			foreach (var user in Users!)
			{
				if (User!.Username == user.Username)
				{
					if (User.Password == user.Password)
					{
						MessageBox.Show("Sign In Successfull.", "Sign In Info", MessageBoxButton.OK, MessageBoxImage.Information);
						return;
					}
				}
			}
			MessageBox.Show("Sign In Not Successfull.", "Sign In Info", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		catch
		{
			MessageBox.Show("Error in Sign In.", "Sign In Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region GetAllCommand Section

	public ICommand GetAllCommand { get; set; }

	public bool CanGetAll(object? param)
	{
		return true;
	}
	public void GetAllExecute(object? param)
	{
		try
		{
			Users = UserDataAccess.GetAllUsers();
		}
		catch
		{
			MessageBox.Show("Error in Get All Users.", "GetAllUsers Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region SaveAllCommand Section

	public ICommand SaveAllCommand { get; set; }

	public bool CanSaveAll(object? param)
	{
		if (Users is not null || Users!.Count > 0)
			return true;
		return false;
	}
	public void SaveAllExecute(object? param)
	{
		try
		{
			UserDataAccess.DeleteAllUsers();
			UserDataAccess.SaveAllUsers(Users!);
		}
		catch
		{
			MessageBox.Show("Error in Save All Users.", "SaveAllUsers Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region DeleteAllCommand Section

	public ICommand DeleteAllCommand { get; set; }

	public bool CanDeleteAll(object? param)
	{
		if (Users is not null || Users!.Count > 0)
			return true;
		return false;
	}
	public void DeleteAllExecute(object? param)
	{
		try
		{
			Users!.Clear();
		}
		catch
		{
			MessageBox.Show("Error in Delete All Users.", "DeleteAllUsersExecute Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region DeleteUserCommand Section

	public ICommand DeleteUserCommand { get; set; }

	public bool CanDeleteUser(object? param)
	{
		if (Users is not null || Users!.Count > 0)
			return true;
		return false;
	}
	public void DeleteUserExecute(object? param)
	{
		try
		{
			Users!.RemoveAt(App.Container!.GetInstance<DashboardView>().UsersList.SelectedIndex);
		}
		catch
		{
			MessageBox.Show("Error in Delete User.", "DeleteUserExecute Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		finally
		{
			RefreshExecute(null);
		}
	}

	#endregion

	#region RefreshCommand Section

	public ICommand RefreshCommand { get; set; }

	public bool CanRefresh(object? param) => true;

	public void RefreshExecute(object? param)
	{
		try
		{
			App.Container!.GetInstance<DashboardView>().UsersList.Items.Refresh();
		}
		catch
		{
			MessageBox.Show("Error in Refreshing.", "Refresh Info", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	#endregion


}
