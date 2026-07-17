# Getting started

## Prerequisites

- Windows 10 or later.
- .NET 10 SDK.
- A WPF application targeting `net10.0-windows`.

## Reference the library

During development, add a project reference:

```powershell
dotnet add YourApp/YourApp.csproj reference src/DevExpressInspiredControls/DevExpressInspiredControls.csproj
```

The control library has no third-party runtime dependencies.

## Load the theme

Merge the public dictionary once at application scope:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary
                Source="/DevExpressInspiredControls;component/Themes/Office2019Colorful.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

This applies implicit styles to standard WPF buttons and common input controls.
Existing bindings, commands, access keys, and automation peers continue to work
because these controls are styled rather than wrapped.

## Use ToggleSwitch

Declare the control namespace:

```xml
xmlns:controls="clr-namespace:DevExpressInspiredControls.Controls;assembly=DevExpressInspiredControls"
```

Bind the switch like a standard `ToggleButton`:

```xml
<controls:ToggleSwitch
    AutomationProperties.Name="Enable notifications"
    Content="Notifications"
    IsChecked="{Binding NotificationsEnabled, Mode=TwoWay}"
    OnContent="On"
    OffContent="Off" />
```

`ToggleSwitch` inherits `Command`, `CommandParameter`, and `IsThreeState` from
WPF button types. Its automation peer exposes the Toggle pattern.

## Validation

The editor templates respond to standard WPF validation state. No custom event
handlers or validation API are required:

```xml
<TextBox
    Text="{Binding DisplayName,
                   Mode=TwoWay,
                   UpdateSourceTrigger=PropertyChanged,
                   ValidatesOnNotifyDataErrors=True}" />
```

`ValidationRule`, `IDataErrorInfo`, and `INotifyDataErrorInfo` are supported by
the same `Validation.HasError` visual state.

## Override theme tokens

Merge overrides after the main theme, or declare them after the merged
dictionaries:

```xml
<SolidColorBrush x:Key="DxAccentBrush" Color="#7A3FF2" />
<CornerRadius x:Key="DxCornerRadius">4</CornerRadius>
```

Templates use `DynamicResource`, so token overrides do not require replacing
the control templates.
