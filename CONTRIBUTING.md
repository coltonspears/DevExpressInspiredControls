# Contributing

## Development setup

Use Windows with the .NET 10 SDK:

```powershell
dotnet restore DevExpressInspiredControls.slnx
dotnet build DevExpressInspiredControls.slnx
dotnet test tests/DevExpressInspiredControls.Tests/DevExpressInspiredControls.Tests.csproj
```

Run the gallery when changing templates:

```powershell
dotnet run --project samples/DevExpressInspiredControls.Demo/DevExpressInspiredControls.Demo.csproj
```

## Design guidelines

- Keep the core project independent of DevExpress and MVVM framework packages.
- Prefer styling native WPF controls when their existing behavior is sufficient.
- Derive custom controls from the closest WPF control base and expose state
  through dependency properties.
- Use commands and bindings instead of application event handlers.
- Reference semantic theme keys through `DynamicResource`.
- Preserve keyboard navigation, access keys, focus visuals, validation, and UI
  Automation behavior.
- Add gallery states and STA tests for every new control.
- Regenerate control screenshots after visual changes:
  `dotnet run --project tools/DevExpressInspiredControls.Capture -- docs/images/controls`
- Do not copy or redistribute proprietary source, icons, or extracted assets.

## Pull requests

Keep changes focused and describe:

1. The control or token behavior being changed.
2. The normal, hover, focus, disabled, and validation states affected.
3. Keyboard and accessibility verification.
4. Build and test results.

Do not commit `bin`, `obj`, `.vs`, test-result, coverage, or package output.
