# Composable control roadmap

The library grows from reusable WPF primitives toward sample applications.
Application concepts stay in sample views and view models; the core library
does not define mail folders, messages, channels, contacts, or application
shells. Implement in an MVVM approach avoiding code-behind where possible 
and preferring behaviors.

## Design rules

1. Style an existing WPF control when its behavior, keyboard support, and UI
   Automation semantics already fit the need.
2. Add a custom control only when WPF has no suitable primitive. Derive it from
   the closest framework control and expose state through dependency properties.
3. Keep templates data-agnostic. A mail row, channel row, or chat entry is a
   consumer-owned `DataTemplate`, not a library control.
4. Build shells with standard layout panels and small controls. Do not add
   `OutlookShell`, `MailboxPane`, `DiscordWindow`, `ChannelList`, or similar
   domain-specific controls.
5. Add semantic tokens, gallery states, keyboard/accessibility checks, and STA
   regression tests with every control.

## Existing foundation

- Styled native controls: `Button`, `TextBox`, `ComboBox`, `CheckBox`, and
  `RadioButton`.
- Custom primitive: `ToggleSwitch`.
- Styled scrolling and collection controls through `TreeView`.
- Styled command and transient UI controls through `StatusBar`.
- Styled layout/navigation and input/status controls through `ProgressBar`.
- Search presentation recipe composed from `TextBox`, an icon button, and
  native collection controls in the MVVM gallery.
- Semantic colors, brushes, typography, dimensions, and focus visuals.
- MVVM bindings, validation, commands, keyboard navigation, and automation
  coverage.

## Delivery order

### 1. Scrolling and collections (implemented)

- `ScrollBar` and `ScrollViewer`
- `ListBox` and `ListBoxItem`
- `ListView`, `ListViewItem`, and `GridViewColumnHeader`
- `TreeView` and `TreeViewItem`

These primitives provide virtualized folder, navigation, member, message, and
conversation lists without introducing any application-specific data model.

### 2. Commands and transient UI (implemented)

- `ToggleButton`, `RepeatButton`, and icon-button styles
- `Menu`, `MenuItem`, `ContextMenu`, `ToolBar`, and `Separator`
- `ToolTip`, `Popup` chrome, and `StatusBar`
- Search presentation composed from `TextBox` and `Button`

Search remains a composition recipe unless behavior beyond normal WPF text and
commands proves necessary.

### 3. Layout and navigation (implemented)

- `TabControl` and `TabItem`
- `Expander` and `GroupBox`
- `GridSplitter`
- Layout recipes using `Grid`, `DockPanel`, and shared-size groups
- Segmented navigation composed from styled `RadioButton` controls

Resizable multi-pane views should use `GridSplitter`; a custom split container
is justified only if native WPF behavior cannot meet a demonstrated need.

### 4. Input and status (implemented)

- `PasswordBox` and `RichTextBox`
- `DatePicker` and `Calendar`
- `Slider` and `ProgressBar`

These cover account settings, composition, scheduling, volume, transfer, and
sync scenarios while retaining framework behavior.

### 5. Small custom atoms

Consider these only after the native-control layers are established:

- `Avatar`: image or initials fallback with reusable sizing.
- `Badge`: compact count or status content that can be overlaid by a consumer.
- `PresenceIndicator`: a small, accessible status presentation.
- `SplitButton` or `DropDownButton`: command plus secondary menu, if native
  composition cannot provide correct keyboard and automation behavior.

Chat bubbles, typing indicators, reactions, mail previews, and contact rows
should first be implemented as styles or data templates. Promote one to a
custom control only when it has reusable behavior and semantics rather than
application data.

## Sample composition boundary

An Outlook-inspired sample can compose a `ToolBar`, `TreeView`, `ListView`,
`TabControl`, `GridSplitter`, editor controls, and `StatusBar`. A
communications sample can compose navigation `ListBox` controls, a virtualized
message items control, avatars, badges, an editor, and command buttons.

Those samples own their fake data, view models, item templates, navigation, and
pane arrangement. They validate that the primitives compose well; they do not
move application semantics into the reusable assembly.
