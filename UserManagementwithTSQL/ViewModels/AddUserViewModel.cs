using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using UserManagementwithTSQL.Models;

namespace UserManagementwithTSQL.ViewModels;

public class AddUserViewModel : INotifyPropertyChanged
{
	public AddUserViewModel()
	{
		User = new();
		Users = new();

		SignInCommand = new RelayCommand(SignInExecute, CanSignIn);
		SignUpCommand = new RelayCommand(SignUpExecute, CanSignUp);

		GetAllCommand = new RelayCommand(GetAllExecute, CanGetAll);
		SaveAllCommand = new RelayCommand(SaveAllExecute, CanSaveAll);
	}

	private User? _user;
	private List<User>? _users;

	public User? User { get => _user; set { _user = value; OnPropertyChanged(); } }
	public List<User>? Users { get => _users; set { _users = value; OnPropertyChanged(); } }

	//-----------------------------------------------------------------------------------------

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
	}

	#endregion

	//-----------------------------------------------------------------------------------------

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

	#endregion

	//-----------------------------------------------------------------------------------------

	#region GetAllCommand Section

	public ICommand GetAllCommand { get; set; }

	public bool CanGetAll(object? param)
	{
		return true;
	}
	public void GetAllExecute(object? param)
	{
		Users = UserDataAccess.GetAllUsers();
	}

	#endregion

	//-----------------------------------------------------------------------------------------

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
		UserDataAccess.DeleteAllUsers();
		UserDataAccess.SaveAllUsers(Users!);
	}

	#endregion

	//-----------------------------------------------------------------------------------------

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
		//UserDataAccess.DeleteAllUsers();
	}

	#endregion

	//-----------------------------------------------------------------------------------------

	#region Notify Service Section

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	#endregion
}
