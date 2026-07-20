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

To regenerate README control previews and the full-gallery PNG:

```powershell
dotnet run --project tools/DevExpressInspiredControls.Capture -- docs/images/controls
```

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
7. Toggle the command button with Space, then press and hold each
   `RepeatButton` to demonstrate bounded zoom commands.
8. Press Alt+F, navigate the menu and submenu with arrow keys, invoke a command
   with Enter, and dismiss the menu with Escape.
9. Right-click the context-menu list, then use the toolbar with Tab, arrow
   keys, and its overflow chevron.
10. Hover the tooltip button, open and close the details popup with Space, then
    verify light-dismiss with an outside click.
11. Focus the search field through its `_Search` access key, filter the list,
    and clear it with the icon button.
12. Move through segmented navigation with arrow keys and Space, verifying that
    it retains native grouped `RadioButton` semantics.
13. Enter the tab strip and use arrow keys or Ctrl+Tab to switch content,
    confirming the disabled tab is skipped.
14. Toggle the expander with Space and resize the split panes by dragging the
    `GridSplitter` or using its keyboard controls.
15. Type into `PasswordBox` and confirm the gallery never binds or displays the
    plaintext value; then exercise standard editing shortcuts in `RichTextBox`.
16. Open `DatePicker` with F4 or Alt+Down, navigate with arrow, Home, End,
    Page Up, and Page Down keys, select with Enter, and dismiss with Escape.
17. Repeat date navigation in the standalone `Calendar`; automated captures
    cover the closed DatePicker and standalone Calendar because popup pixels
    live in a separate visual root.
18. Adjust horizontal and vertical sliders with arrow, Page Up, Page Down,
    Home, and End keys.
19. Advance progress and toggle the indeterminate state, checking the polite
    automation live-region announcement.
20. Show `Office2019Colorful.xaml` and explain that all templates consume
   overridable semantic resources.
21. Finish with the passing STA WPF test suite.

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
