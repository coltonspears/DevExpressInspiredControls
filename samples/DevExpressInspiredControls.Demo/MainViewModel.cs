using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DevExpressInspiredControls.Demo;

public sealed partial class MainViewModel : ObservableValidator
{
    public MainViewModel(TimeProvider timeProvider)
    {
        TimeProvider = timeProvider;
        SelectedDate = timeProvider.GetLocalNow().Date;
        ValidateAllProperties();
    }

    public IReadOnlyList<string> Roles { get; } = ["Developer", "Designer", "Product manager", "Administrator"];

    public IReadOnlyList<GalleryListItem> NavigationItems { get; } =
    [
        new("Overview"),
        new("Recent items"),
        new("Shared with me"),
        new("Unavailable item", false),
        new("Archive")
    ];

    public IReadOnlyList<GalleryRecord> Records { get; } =
    [
        new("Alpha", "Primary", "Ready"),
        new("Bravo", "Primary", "In review"),
        new("Charlie", "Secondary", "Blocked"),
        new("Delta", "Secondary", "Ready"),
        new("Echo", "Archive", "Complete"),
        new("Foxtrot", "Archive", "Complete")
    ];

    public IReadOnlyList<GalleryNode> Hierarchy { get; } =
    [
        new(
            "Workspace",
            [
                new("Active", [new("Alpha"), new("Bravo")], true),
                new("Reference", [new("Guides"), new("Examples")])
            ],
            true),
        new("Archive", [new("2025"), new("2026")])
    ];

    public IReadOnlyList<string> ScrollSamples { get; } =
        Enumerable.Range(1, 18)
            .Select(index => $"Scrollable content row {index:00}")
            .ToArray();

    public IReadOnlyList<GalleryListItem> FilteredNavigationItems
        => string.IsNullOrWhiteSpace(SearchQuery)
            ? NavigationItems
            : NavigationItems
                .Where(item => item.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                .ToArray();

    public string SearchSummary
        => $"{FilteredNavigationItems.Count} of {NavigationItems.Count} items";

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
    public partial bool IsDetailsOpen { get; set; }

    [ObservableProperty]
    public partial int SelectedTabIndex { get; set; }

    [ObservableProperty]
    public partial bool IsAdvancedExpanded { get; set; } = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduleSummary))]
    public partial DateTime? SelectedDate { get; set; }

    public string ScheduleSummary
        => SelectedDate?.ToString("D") ?? "No date selected";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VolumeSummary))]
    [NotifyCanExecuteChangedFor(nameof(ResetVolumeCommand))]
    public partial double VolumeLevel { get; set; } = 65;

    public string VolumeSummary => $"{VolumeLevel:0}%";

    [ObservableProperty]
    public partial int ActivityProgress { get; private set; } = 65;

    [ObservableProperty]
    public partial bool IsActivityIndeterminate { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(IncrementZoomCommand))]
    [NotifyCanExecuteChangedFor(nameof(DecrementZoomCommand))]
    public partial int ZoomLevel { get; private set; } = 100;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FilteredNavigationItems))]
    [NotifyPropertyChangedFor(nameof(SearchSummary))]
    [NotifyCanExecuteChangedFor(nameof(ClearSearchCommand))]
    public partial string SearchQuery { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string LastAction { get; private set; } = "No command has run yet.";

    [RelayCommand]
    private void Create()
    {
        LastAction = "Created a new gallery item.";
    }

    [RelayCommand]
    private void Undo()
    {
        LastAction = "Undid the last gallery action.";
    }

    [RelayCommand(CanExecute = nameof(CanIncrementZoom))]
    private void IncrementZoom()
    {
        ZoomLevel += 10;
        LastAction = $"Zoom set to {ZoomLevel}%.";
    }

    private bool CanIncrementZoom() => ZoomLevel < 200;

    [RelayCommand(CanExecute = nameof(CanDecrementZoom))]
    private void DecrementZoom()
    {
        ZoomLevel -= 10;
        LastAction = $"Zoom set to {ZoomLevel}%.";
    }

    private bool CanDecrementZoom() => ZoomLevel > 50;

    [RelayCommand(CanExecute = nameof(CanClearSearch))]
    private void ClearSearch()
    {
        SearchQuery = string.Empty;
        LastAction = "Cleared the gallery search.";
    }

    private bool CanClearSearch() => !string.IsNullOrWhiteSpace(SearchQuery);

    [RelayCommand]
    private void SelectToday()
    {
        SelectedDate = TimeProvider.GetLocalNow().Date;
        LastAction = "Selected today's date.";
    }

    [RelayCommand(CanExecute = nameof(CanResetVolume))]
    private void ResetVolume()
    {
        VolumeLevel = 50;
        LastAction = "Volume reset to 50%.";
    }

    private bool CanResetVolume() => Math.Abs(VolumeLevel - 50) > 0.001;

    [RelayCommand]
    private void AdvanceActivity()
    {
        IsActivityIndeterminate = false;
        ActivityProgress = ActivityProgress >= 100 ? 0 : Math.Min(100, ActivityProgress + 10);
        LastAction = $"Activity progress set to {ActivityProgress}%.";
    }

    [RelayCommand]
    private void ToggleActivity()
    {
        IsActivityIndeterminate = !IsActivityIndeterminate;
        LastAction = IsActivityIndeterminate
            ? "Activity switched to indeterminate progress."
            : "Activity returned to determinate progress.";
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        LastAction = $"Saved at {TimeProvider.GetLocalNow():T}";
    }

    private bool CanSave() => !HasErrors;
}

public sealed record GalleryListItem(string Name, bool IsEnabled = true);

public sealed record GalleryRecord(string Name, string Category, string Status);

public sealed record GalleryNode(
    string Name,
    IReadOnlyList<GalleryNode>? Children = null,
    bool IsExpanded = false);
