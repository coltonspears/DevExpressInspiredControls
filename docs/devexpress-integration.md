# Optional DevExpress WPF integration

The core `DevExpressInspiredControls` assembly intentionally has no DevExpress
dependency. Applications can use it by itself, or place a future adapter after
the core theme when licensed DevExpress packages are available.

## Planned adapter

Create a separate `DevExpressInspiredControls.DevExpress` WPF class library that:

1. Targets `net10.0-windows`.
2. References the core project and the version-matched licensed
   `DevExpress.Xpf.Core` package.
3. Declares `DXThemeInfo` for its theme resources.
4. Exposes a resource dictionary that replaces the core semantic brush keys
   (`DxAccentBrush`, `DxSurfaceBrush`, `DxBorderBrush`, and related keys) with
   values obtained from DevExpress theme resources.
5. Contains no control templates unless DevExpress-specific behavior cannot be
   represented by a semantic token.

The adapter dictionary must be merged after the independent theme:

```xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary
        Source="/DevExpressInspiredControls;component/Themes/Office2019Colorful.xaml" />
    <ResourceDictionary
        Source="/DevExpressInspiredControls.DevExpress;component/Themes/DevExpress.xaml" />
</ResourceDictionary.MergedDictionaries>
```

This ordering lets the adapter replace resource values while the existing
templates continue to resolve them through `DynamicResource`.

## Theme-manager registration

DevExpress documents the following assembly-level registration for custom
control theme resources:

```csharp
[assembly: DevExpress.Xpf.Core.DXThemeInfo(typeof(AdapterThemeMarker))]
```

The adapter can then map its brushes with the DevExpress `ThemeResource` markup
extension and version-appropriate theme keys. Keep those references isolated in
the adapter because theme-key types and package assembly names can vary by
DevExpress release.

The host application remains responsible for selecting
`Office2019Colorful` through the DevExpress theme manager. The demo and core
library do not attempt to detect or change an application's selected theme.

## Version and licensing policy

- Add the adapter only after the private NuGet feed or local licensed packages
  are configured.
- Pin all DevExpress packages in the adapter to the same release.
- Do not redistribute DevExpress assemblies, source, icons, or extracted theme
  assets.
- Build and test the adapter separately against every supported DevExpress
  release; a failure in the optional adapter must not prevent the core package
  from building.
