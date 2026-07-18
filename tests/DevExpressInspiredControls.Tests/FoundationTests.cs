using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DevExpressInspiredControls.Controls;

namespace DevExpressInspiredControls.Tests;

[STATestClass]
public sealed class FoundationTests
{
    [TestMethod]
    public void OfficeTheme_LoadsSemanticResourcesAndImplicitStyles()
    {
        var theme = LoadOfficeTheme();

        Assert.IsInstanceOfType(theme["DxAccentBrush"], typeof(SolidColorBrush));
        Assert.IsInstanceOfType(theme["DxControlHeight"], typeof(double));
        Assert.IsInstanceOfType(theme[typeof(Button)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TextBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ComboBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(CheckBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(RadioButton)], typeof(Style));
    }

    [TestMethod]
    public void BuiltInControlStyles_ProvideTemplates()
    {
        var theme = LoadOfficeTheme();

        AssertStyleProvidesTemplate<Button>(theme);
        AssertStyleProvidesTemplate<TextBox>(theme);
        AssertStyleProvidesTemplate<ComboBox>(theme);
        AssertStyleProvidesTemplate<CheckBox>(theme);
        AssertStyleProvidesTemplate<RadioButton>(theme);
    }

    [TestMethod]
    public void ToggleSwitch_BindsTwoWayAndExecutesCommands()
    {
        var source = new BindingSource();
        var command = new RecordingCommand();
        var toggle = new TestToggleSwitch
        {
            Content = "Feature",
            Command = command
        };

        BindingOperations.SetBinding(
            toggle,
            ToggleButton.IsCheckedProperty,
            new Binding(nameof(BindingSource.IsChecked))
            {
                Source = source,
                Mode = BindingMode.TwoWay
            });

        toggle.InvokeClick();

        Assert.IsTrue(source.IsChecked);
        Assert.AreEqual(1, command.ExecutionCount);
        Assert.AreEqual("On", toggle.OnContent);
        Assert.AreEqual("Off", toggle.OffContent);
    }

    [TestMethod]
    public void ToggleSwitch_ExposesToggleAutomationSemantics()
    {
        var toggle = new TestToggleSwitch { Content = "Feature enabled" };
        var peer = toggle.CreateAutomationPeer();

        Assert.AreEqual(nameof(ToggleSwitch), peer.GetClassName());
        Assert.AreEqual("Feature enabled", peer.GetName());
        Assert.AreEqual(AutomationControlType.CheckBox, peer.GetAutomationControlType());
        Assert.IsNotNull(peer.GetPattern(PatternInterface.Toggle));
    }

    [TestMethod]
    public void TextBoxStyle_UsesStandardValidationState()
    {
        var theme = LoadOfficeTheme();
        var textBox = new TextBox
        {
            Style = (Style)theme[typeof(TextBox)]
        };

        var expression = BindingOperations.SetBinding(
            textBox,
            TextBox.TextProperty,
            new Binding(nameof(BindingSource.Text))
            {
                Source = new BindingSource(),
                Mode = BindingMode.TwoWay
            });

        var validationError = new ValidationError(
            new ExceptionValidationRule(),
            expression)
        {
            ErrorContent = "Invalid value."
        };

        Validation.MarkInvalid(expression, validationError);
        Assert.IsTrue(Validation.GetHasError(textBox));
        Assert.AreEqual("Invalid value.", Validation.GetErrors(textBox)[0].ErrorContent);

        Validation.ClearInvalid(expression);
        Assert.IsFalse(Validation.GetHasError(textBox));
    }

    private static ResourceDictionary LoadOfficeTheme()
    {
        return (ResourceDictionary)Application.LoadComponent(
            new Uri(
                "/DevExpressInspiredControls;component/Themes/Office2019Colorful.xaml",
                UriKind.Relative));
    }

    private static void AssertStyleProvidesTemplate<TControl>(ResourceDictionary theme)
        where TControl : Control, new()
    {
        var control = new TControl
        {
            Style = (Style)theme[typeof(TControl)]
        };

        Assert.IsNotNull(control.Template);
    }

    private sealed class TestToggleSwitch : ToggleSwitch
    {
        public void InvokeClick()
        {
            OnClick();
        }

        public AutomationPeer CreateAutomationPeer()
        {
            return OnCreateAutomationPeer();
        }
    }

    private sealed class RecordingCommand : ICommand
    {
        public int ExecutionCount { get; private set; }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecutionCount++;
        }
    }

    private sealed class BindingSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsChecked
        {
            get;
            set => SetField(ref field, value);
        }

        public string Text
        {
            get;
            set => SetField(ref field, value);
        } = string.Empty;

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
