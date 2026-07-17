using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DevExpressInspiredControls.Demo;

public sealed class MainViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = [];
    private string _displayName = string.Empty;
    private bool _isFeatureEnabled = true;
    private string _lastAction = "No command has run yet.";
    private string _selectedRole = "Developer";

    public MainViewModel()
    {
        SaveCommand = new RelayCommand(
            () => LastAction = $"Saved at {DateTime.Now:T}",
            () => !HasErrors);

        ValidateDisplayName();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IReadOnlyList<string> Roles { get; } =
        ["Developer", "Designer", "Product manager", "Administrator"];

    public ICommand SaveCommand { get; }

    public string DisplayName
    {
        get => _displayName;
        set
        {
            if (SetField(ref _displayName, value))
            {
                ValidateDisplayName();
            }
        }
    }

    public string SelectedRole
    {
        get => _selectedRole;
        set => SetField(ref _selectedRole, value);
    }

    public bool IsFeatureEnabled
    {
        get => _isFeatureEnabled;
        set => SetField(ref _isFeatureEnabled, value);
    }

    public string LastAction
    {
        get => _lastAction;
        private set => SetField(ref _lastAction, value);
    }

    public bool HasErrors => _errors.Count != 0;

    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName is not null && _errors.TryGetValue(propertyName, out var errors)
            ? errors
            : Array.Empty<string>();
    }

    private void ValidateDisplayName()
    {
        const string propertyName = nameof(DisplayName);
        var messages = new List<string>();

        if (string.IsNullOrWhiteSpace(DisplayName))
        {
            messages.Add("Display name is required.");
        }
        else if (DisplayName.Trim().Length < 3)
        {
            messages.Add("Display name must contain at least three characters.");
        }

        if (messages.Count == 0)
        {
            _errors.Remove(propertyName);
        }
        else
        {
            _errors[propertyName] = messages;
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
        (SaveCommand as RelayCommand)?.RaiseCanExecuteChanged();
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
