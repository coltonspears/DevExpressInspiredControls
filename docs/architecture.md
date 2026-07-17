# Architecture

## Repository boundaries

The repository separates reusable product code from hosts and verification:

```text
src/DevExpressInspiredControls/
  Controls/                 Custom control behavior and automation peers
  Properties/               WPF assembly theme registration
  Themes/
    Controls/               Focused control templates
    Tokens/                 Semantic colors, brushes, typography, and metrics
    Generic.xaml            Default custom-control styles
    Office2019Colorful.xaml Public theme entry point

samples/DevExpressInspiredControls.Demo/
tests/DevExpressInspiredControls.Tests/
docs/
```

The core assembly is independent of the sample, tests, MVVM frameworks, and
DevExpress packages.

## Styling strategy

Standard WPF controls are styled directly. This preserves their established
dependency properties, routed commands, keyboard behavior, validation
integration, and automation peers.

`ToggleSwitch` is a custom control because classic WPF does not provide that
control type. It derives from `ToggleButton`, keeps the normal command and
binding surface, and supplies a semantic automation peer.

## Resource flow

`Office2019Colorful.xaml` is the application-facing dictionary. It merges the
semantic token dictionary before each standard-control style dictionary.
`Generic.xaml` separately provides the custom-control default style required by
WPF theme discovery.

Control templates resolve semantic resources through `DynamicResource`:

```text
Application override
        |
        v
Semantic token key
        |
        v
Control template
```

This indirection allows applications to customize the independent theme and
allows a future optional adapter to map the same keys to DevExpress theme
resources.

## MVVM boundary

The reusable library contains no view models and subscribes to no application
events. Consumers use standard `ICommand`, dependency-property bindings, and
WPF validation interfaces.

The sample application demonstrates:

- `INotifyPropertyChanged` for state.
- `ICommand` for actions.
- `INotifyDataErrorInfo` for editor validation.
- Labels, access keys, focus behavior, and automation properties.

Its window code-behind contains initialization only.

## Verification

The test project targets `net10.0-windows` and uses MSTest STA test classes.
Tests cover:

- Theme loading and semantic resource availability.
- Implicit standard-control styles and templates.
- ToggleSwitch two-way binding and command execution.
- Toggle automation semantics.
- Standard WPF validation transitions.

The gallery provides the complementary visual and keyboard verification surface.
