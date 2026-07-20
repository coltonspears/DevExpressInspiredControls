using System.ComponentModel.DataAnnotations;
using DevExpressInspiredControls.Demo;

namespace DevExpressInspiredControls.Tests;

[TestClass]
public sealed class MainViewModelTests
{
    [TestMethod]
    public void DisplayNameValidation_UpdatesGeneratedSaveCommand()
    {
        var viewModel = new MainViewModel(TimeProvider.System);

        Assert.IsTrue(viewModel.HasErrors);
        Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));
        CollectionAssert.Contains(
            viewModel.GetErrors(nameof(MainViewModel.DisplayName))
                .Cast<ValidationResult>()
                .Select(result => result.ErrorMessage)
                .ToList(),
            "Display name is required.");

        viewModel.DisplayName = "Ada";

        Assert.IsFalse(viewModel.HasErrors);
        Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));

        viewModel.SaveCommand.Execute(null);

        StringAssert.StartsWith(viewModel.LastAction, "Saved at ");
    }

    [TestMethod]
    public void SearchQuery_FiltersNavigationAndClearCommandRestoresItems()
    {
        var viewModel = new MainViewModel(TimeProvider.System);

        viewModel.SearchQuery = "arch";

        CollectionAssert.AreEqual(
            new[] { "Archive" },
            viewModel.FilteredNavigationItems.Select(item => item.Name).ToArray());
        Assert.AreEqual("1 of 5 items", viewModel.SearchSummary);
        Assert.IsTrue(viewModel.ClearSearchCommand.CanExecute(null));

        viewModel.ClearSearchCommand.Execute(null);

        Assert.AreEqual(string.Empty, viewModel.SearchQuery);
        Assert.HasCount(5, viewModel.FilteredNavigationItems);
        Assert.IsFalse(viewModel.ClearSearchCommand.CanExecute(null));
    }

    [TestMethod]
    public void ZoomCommands_RespectConfiguredBounds()
    {
        var viewModel = new MainViewModel(TimeProvider.System);

        while (viewModel.IncrementZoomCommand.CanExecute(null))
        {
            viewModel.IncrementZoomCommand.Execute(null);
        }

        Assert.AreEqual(200, viewModel.ZoomLevel);
        Assert.IsFalse(viewModel.IncrementZoomCommand.CanExecute(null));

        while (viewModel.DecrementZoomCommand.CanExecute(null))
        {
            viewModel.DecrementZoomCommand.Execute(null);
        }

        Assert.AreEqual(50, viewModel.ZoomLevel);
        Assert.IsFalse(viewModel.DecrementZoomCommand.CanExecute(null));
    }

    [TestMethod]
    public void SelectTodayCommand_UsesInjectedTimeProvider()
    {
        var timeProvider = new FixedTimeProvider(new DateTimeOffset(2026, 7, 19, 15, 30, 0, TimeSpan.Zero));
        var viewModel = new MainViewModel(timeProvider)
        {
            SelectedDate = null
        };

        viewModel.SelectTodayCommand.Execute(null);

        Assert.AreEqual(new DateTime(2026, 7, 19), viewModel.SelectedDate);
        StringAssert.Contains(viewModel.ScheduleSummary, "2026");
    }

    [TestMethod]
    public void VolumeAndActivityCommands_UpdateBoundedStatus()
    {
        var viewModel = new MainViewModel(TimeProvider.System)
        {
            VolumeLevel = 87
        };

        Assert.IsTrue(viewModel.ResetVolumeCommand.CanExecute(null));
        viewModel.ResetVolumeCommand.Execute(null);
        Assert.AreEqual(50d, viewModel.VolumeLevel);
        Assert.IsFalse(viewModel.ResetVolumeCommand.CanExecute(null));

        var originalProgress = viewModel.ActivityProgress;
        viewModel.AdvanceActivityCommand.Execute(null);
        Assert.AreEqual(originalProgress + 10, viewModel.ActivityProgress);

        viewModel.ToggleActivityCommand.Execute(null);
        Assert.IsTrue(viewModel.IsActivityIndeterminate);
        viewModel.AdvanceActivityCommand.Execute(null);
        Assert.IsFalse(viewModel.IsActivityIndeterminate);
    }

    [TestMethod]
    public void LayoutNavigationState_RemainsBindable()
    {
        var viewModel = new MainViewModel(TimeProvider.System)
        {
            SelectedTabIndex = 2,
            IsAdvancedExpanded = false
        };

        Assert.AreEqual(2, viewModel.SelectedTabIndex);
        Assert.IsFalse(viewModel.IsAdvancedExpanded);
    }

    private sealed class FixedTimeProvider(DateTimeOffset utcNow) : TimeProvider
    {
        public override TimeZoneInfo LocalTimeZone => TimeZoneInfo.Utc;

        public override DateTimeOffset GetUtcNow() => utcNow;
    }
}
