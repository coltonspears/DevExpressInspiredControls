using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls.Primitives;

namespace DevExpressInspiredControls.Controls;

/// <summary>
/// A command-capable, three-state-aware switch control designed for data binding.
/// </summary>
public class ToggleSwitch : ToggleButton
{
    public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register(
        nameof(OnContent),
        typeof(object),
        typeof(ToggleSwitch),
        new FrameworkPropertyMetadata("On"));

    public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register(
        nameof(OffContent),
        typeof(object),
        typeof(ToggleSwitch),
        new FrameworkPropertyMetadata("Off"));

    static ToggleSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ToggleSwitch),
            new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
    }

    public object? OnContent
    {
        get => GetValue(OnContentProperty);
        set => SetValue(OnContentProperty, value);
    }

    public object? OffContent
    {
        get => GetValue(OffContentProperty);
        set => SetValue(OffContentProperty, value);
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new ToggleSwitchAutomationPeer(this);
    }
}
