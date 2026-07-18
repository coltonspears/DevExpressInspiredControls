using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DevExpressInspiredControls.Demo;

public sealed partial class MainViewModel : ObservableValidator
{
    public MainViewModel(TimeProvider timeProvider)
    {
        TimeProvider = timeProvider;
        ValidateAllProperties();
    }

    public IReadOnlyList<string> Roles { get; } = ["Developer", "Designer", "Product manager", "Administrator"];

    private TimeProvider TimeProvider { get; }

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    [Required(ErrorMessage = "Display name is required.")]
    [MinLength(3, ErrorMessage = "Display name must contain at least three characters.")]
    public partial string DisplayName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string SelectedRole { get; set; } = "Developer";

    [ObservableProperty]
    public partial bool IsFeatureEnabled { get; set; } = true;

    [ObservableProperty]
    public partial string LastAction { get; private set; } = "No command has run yet.";

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        LastAction = $"Saved at {TimeProvider.GetLocalNow():T}";
    }

    private bool CanSave() => !HasErrors;
}
