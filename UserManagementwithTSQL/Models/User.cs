using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserManagementwithTSQL.Models;

public class User : INotifyPropertyChanged
{
	private Guid _id;
	private string _username;
	private string _password;

	public Guid Id { get => _id; set { _id=value; OnPropertyChanged(); } }
	public string Username { get => _username; set { _username=value; OnPropertyChanged(); } }
	public string Password { get => _password; set { _password=value; OnPropertyChanged(); } }

	public User()
	{
		Id = Guid.NewGuid();
	}
	public User(string username, string password) : this()
	{
		Username = username;
		Password = password;
	}
	public User(Guid id, string username, string password) : this()
	{
		Id = id;
		Username = username;
		Password = password;
	}

	public override string ToString() => Username;

	#region Notify Service Section

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	#endregion
}
