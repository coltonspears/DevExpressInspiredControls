using System.Windows.Automation.Peers;

namespace DevExpressInspiredControls.Controls;

internal sealed class ToggleSwitchAutomationPeer(ToggleSwitch owner)
    : ToggleButtonAutomationPeer(owner)
{
    protected override string GetClassNameCore()
    {
        return nameof(ToggleSwitch);
    }

    protected override string GetLocalizedControlTypeCore()
    {
        return "toggle switch";
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        return AutomationControlType.CheckBox;
    }

    protected override string GetNameCore()
    {
        var name = base.GetNameCore();
        return string.IsNullOrWhiteSpace(name) && Owner is ToggleSwitch { Content: string content }
            ? content
            : name;
    }
}
