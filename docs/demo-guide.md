# Demo and recording guide

This guide provides a repeatable walkthrough for screenshots, release notes, or
a short YouTube demonstration.

## Prepare the repository

From the repository root:

```powershell
dotnet restore DevExpressInspiredControls.slnx
dotnet build DevExpressInspiredControls.slnx
dotnet test tests/DevExpressInspiredControls.Tests/DevExpressInspiredControls.Tests.csproj
dotnet run --project samples/DevExpressInspiredControls.Demo/DevExpressInspiredControls.Demo.csproj
```

Record the successful build and test summary before opening the gallery if the
video is intended as a technical walkthrough.

## Suggested walkthrough

1. Show the `/src`, `/samples`, `/tests`, and `/docs` repository layout.
2. Open the gallery and demonstrate button hover, press, default, disabled, and
   command states.
3. Tab through the text and selection editors to show focus visuals and access
   keys.
4. Compare editable, read-only, and disabled editor states.
5. Toggle check boxes, radio buttons, and `ToggleSwitch` using both mouse and
   keyboard.
6. Enter fewer than three characters in the display-name field to demonstrate
   `INotifyDataErrorInfo` validation.
7. Show `Office2019Colorful.xaml` and explain that all templates consume
   overridable semantic resources.
8. Finish with the passing STA WPF test suite.

## Accurate project description

Suggested wording:

> DevExpressInspiredControls is an independent .NET 10 WPF control foundation
> inspired by the Office2019Colorful visual language. It uses standard WPF
> styling, MVVM bindings, commands, validation, and UI Automation, and does not
> redistribute DevExpress code or assets.

Avoid presenting the project as an official DevExpress product or claiming
pixel-perfect compatibility. DevExpress and its product names are trademarks of
their respective owner.

## Useful links for a video description

Once the repository is published, include links to:

- `README.md` for the quick start.
- `docs/getting-started.md` for application integration.
- `docs/architecture.md` for design decisions.
- `docs/devexpress-integration.md` for the optional future adapter boundary.

Replace these relative paths with public repository URLs after publishing.
