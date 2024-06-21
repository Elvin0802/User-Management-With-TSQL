using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UserManagementwithTSQL.ViewModels.Global;

public abstract class BaseViewModel : INotifyPropertyChanged
{

	#region Notify Service Section

	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	#endregion


}
