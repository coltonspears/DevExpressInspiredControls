# DevExpressInspiredControls

A dependency-free .NET 10 WPF control foundation inspired by the DevExpress
Office2019Colorful visual language. The project uses standard WPF templates,
dependency properties, commands, bindings, validation, keyboard navigation, and
UI Automation.

This is an independent project. It does not include or redistribute DevExpress
source code, assemblies, icons, or theme assets.

## Included controls

- Office2019Colorful-inspired `Button`, `TextBox`, `ComboBox`, `CheckBox`, and
  `RadioButton` styles.
- A bindable, command-capable `ToggleSwitch` custom control.
- Normal, hover, pressed, focused, selected, read-only, disabled, and validation
  states.
- Semantic resource tokens that applications can override.
- An MVVM gallery and STA WPF regression tests.

## Repository layout

```text
src/        Reusable WPF control library
samples/    MVVM gallery application
tests/      STA WPF regression tests
docs/       Usage, architecture, demo, and integration notes
```

## Quick start

Requirements:

- Windows 10 or later.
- .NET 10 SDK.
- Visual Studio 2026, Cursor, or another editor with WPF support.

Build, test, and run the gallery:

```powershell
dotnet restore DevExpressInspiredControls.slnx
dotnet build DevExpressInspiredControls.slnx
dotnet test tests/DevExpressInspiredControls.Tests/DevExpressInspiredControls.Tests.csproj
dotnet run --project samples/DevExpressInspiredControls.Demo/DevExpressInspiredControls.Demo.csproj
```

Reference the library and merge its public theme in `App.xaml`:

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

Use the custom switch with normal MVVM bindings:

```xml
<Window
    xmlns:controls="clr-namespace:DevExpressInspiredControls.Controls;assembly=DevExpressInspiredControls">
    <controls:ToggleSwitch
        Content="Feature"
        IsChecked="{Binding IsFeatureEnabled, Mode=TwoWay}"
        Command="{Binding FeatureChangedCommand}" />
</Window>
```

## Documentation

- [Getting started](docs/getting-started.md)
- [Architecture and theming](docs/architecture.md)
- [Demo and recording guide](docs/demo-guide.md)
- [Optional DevExpress integration](docs/devexpress-integration.md)
- [Contributing](CONTRIBUTING.md)

## Status

The current milestone establishes the theme foundation and common input
controls. More complex editors, navigation controls, and data controls can be
added incrementally without introducing a DevExpress dependency into the core
package.
