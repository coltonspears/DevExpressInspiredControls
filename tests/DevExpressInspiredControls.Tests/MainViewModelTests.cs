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
}
