using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using DevExpressInspiredControls.Controls;

namespace DevExpressInspiredControls.Tests;

[STATestClass]
public sealed class FoundationTests
{
    [TestMethod]
    public void OfficeTheme_LoadsSemanticResourcesAndImplicitStyles()
    {
        var theme = LoadOfficeTheme();

        Assert.IsInstanceOfType(theme["DxAccentBrush"], typeof(SolidColorBrush));
        Assert.IsInstanceOfType(theme["DxControlHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxListItemMinHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxScrollBarThickness"], typeof(double));
        Assert.IsInstanceOfType(theme["DxIconButtonSize"], typeof(double));
        Assert.IsInstanceOfType(theme["DxToolBarHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxStatusBarHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxTabItemMinHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxCalendarDaySize"], typeof(double));
        Assert.IsInstanceOfType(theme["DxSliderThumbSize"], typeof(double));
        Assert.IsInstanceOfType(theme["DxProgressBarHeight"], typeof(double));
        Assert.IsInstanceOfType(theme["DxSelectionInactiveBrush"], typeof(SolidColorBrush));
        Assert.IsInstanceOfType(theme["DxIconButtonStyle"], typeof(Style));
        Assert.IsInstanceOfType(theme["DxPopupStyle"], typeof(Style));
        Assert.IsInstanceOfType(theme["DxPopupChromeStyle"], typeof(Style));
        Assert.IsInstanceOfType(theme["DxSegmentedRadioButtonStyle"], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Button)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ToggleButton)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(RepeatButton)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TextBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ComboBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(CheckBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(RadioButton)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ScrollBar)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ScrollViewer)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ListBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ListBoxItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ListView)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ListViewItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(GridViewColumnHeader)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TreeView)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TreeViewItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Menu)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(MenuItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ContextMenu)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Separator)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ToolBarTray)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ToolBar)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ToolTip)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(StatusBar)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(StatusBarItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TabControl)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(TabItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Expander)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(GroupBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(GridSplitter)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(PasswordBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(RichTextBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Calendar)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(CalendarItem)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(CalendarDayButton)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(CalendarButton)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(DatePicker)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(DatePickerTextBox)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(Slider)], typeof(Style));
        Assert.IsInstanceOfType(theme[typeof(ProgressBar)], typeof(Style));
    }

    [TestMethod]
    public void BuiltInControlStyles_ProvideTemplates()
    {
        var theme = LoadOfficeTheme();

        AssertStyleProvidesTemplate<Button>(theme);
        AssertStyleProvidesTemplate<ToggleButton>(theme);
        AssertStyleProvidesTemplate<RepeatButton>(theme);
        AssertStyleProvidesTemplate<TextBox>(theme);
        AssertStyleProvidesTemplate<ComboBox>(theme);
        AssertStyleProvidesTemplate<CheckBox>(theme);
        AssertStyleProvidesTemplate<RadioButton>(theme);
        AssertStyleProvidesTemplate<ScrollBar>(theme);
        AssertStyleProvidesTemplate<ScrollViewer>(theme);
        AssertStyleProvidesTemplate<ListBox>(theme);
        AssertStyleProvidesTemplate<ListBoxItem>(theme);
        AssertStyleProvidesTemplate<ListView>(theme);
        AssertStyleProvidesTemplate<ListViewItem>(theme);
        AssertStyleProvidesTemplate<GridViewColumnHeader>(theme);
        AssertStyleProvidesTemplate<TreeView>(theme);
        AssertStyleProvidesTemplate<TreeViewItem>(theme);
        AssertStyleProvidesTemplate<Menu>(theme);
        AssertStyleProvidesTemplate<MenuItem>(theme);
        AssertStyleProvidesTemplate<ContextMenu>(theme);
        AssertStyleProvidesTemplate<Separator>(theme);
        AssertStyleProvidesTemplate<ToolTip>(theme);
        AssertStyleProvidesTemplate<StatusBar>(theme);
        AssertStyleProvidesTemplate<StatusBarItem>(theme);
        AssertStyleProvidesTemplate<TabControl>(theme);
        AssertStyleProvidesTemplate<TabItem>(theme);
        AssertStyleProvidesTemplate<Expander>(theme);
        AssertStyleProvidesTemplate<GroupBox>(theme);
        AssertStyleProvidesTemplate<GridSplitter>(theme);
        AssertStyleProvidesTemplate<PasswordBox>(theme);
        AssertStyleProvidesTemplate<RichTextBox>(theme);
        AssertStyleProvidesTemplate<Calendar>(theme);
        AssertStyleProvidesTemplate<CalendarDayButton>(theme);
        AssertStyleProvidesTemplate<CalendarButton>(theme);
        AssertStyleProvidesTemplate<DatePicker>(theme);
        AssertStyleProvidesTemplate<DatePickerTextBox>(theme);
        AssertStyleProvidesTemplate<Slider>(theme);
        AssertStyleProvidesTemplate<ProgressBar>(theme);
    }

    [TestMethod]
    public void ScrollTemplates_ExposeRequiredParts()
    {
        var theme = LoadOfficeTheme();
        var scrollBar = new ScrollBar
        {
            Style = (Style)theme[typeof(ScrollBar)]
        };
        var scrollViewer = new ScrollViewer
        {
            Style = (Style)theme[typeof(ScrollViewer)]
        };

        Assert.IsTrue(scrollBar.ApplyTemplate());
        Assert.IsTrue(scrollViewer.ApplyTemplate());
        Assert.IsNotNull(scrollBar.Template.FindName("PART_Track", scrollBar));
        Assert.IsNotNull(scrollViewer.Template.FindName("PART_ScrollContentPresenter", scrollViewer));
        Assert.IsNotNull(scrollViewer.Template.FindName("PART_HorizontalScrollBar", scrollViewer));
        Assert.IsNotNull(scrollViewer.Template.FindName("PART_VerticalScrollBar", scrollViewer));
    }

    [TestMethod]
    public void CollectionStyles_PreserveSelectionVirtualizationAndKeyboardNavigation()
    {
        var theme = LoadOfficeTheme();
        var items = new[] { "Alpha", "Bravo", "Charlie" };
        var listBox = new ListBox
        {
            ItemsSource = items,
            SelectedIndex = 1,
            Style = (Style)theme[typeof(ListBox)]
        };
        var listView = new ListView
        {
            ItemsSource = items,
            SelectedIndex = 2,
            Style = (Style)theme[typeof(ListView)]
        };
        var treeView = new TreeView
        {
            Style = (Style)theme[typeof(TreeView)]
        };
        var treeItem = new TreeViewItem
        {
            Header = "Root"
        };
        treeView.Items.Add(treeItem);
        treeItem.IsSelected = true;

        Assert.AreEqual("Bravo", listBox.SelectedItem);
        Assert.AreEqual("Charlie", listView.SelectedItem);
        Assert.AreSame(treeItem, treeView.SelectedItem);
        Assert.IsTrue(VirtualizingPanel.GetIsVirtualizing(listBox));
        Assert.IsTrue(VirtualizingPanel.GetIsVirtualizing(listView));
        Assert.IsTrue(VirtualizingPanel.GetIsVirtualizing(treeView));
        Assert.AreEqual(VirtualizationMode.Recycling, VirtualizingPanel.GetVirtualizationMode(listBox));
        Assert.AreEqual(VirtualizationMode.Recycling, VirtualizingPanel.GetVirtualizationMode(listView));
        Assert.AreEqual(VirtualizationMode.Recycling, VirtualizingPanel.GetVirtualizationMode(treeView));
        Assert.AreEqual(KeyboardNavigationMode.Contained, KeyboardNavigation.GetDirectionalNavigation(listBox));
        Assert.AreEqual(KeyboardNavigationMode.Contained, KeyboardNavigation.GetDirectionalNavigation(listView));
        Assert.IsInstanceOfType(listBox.ItemContainerStyle, typeof(Style));
        Assert.IsInstanceOfType(listView.ItemContainerStyle, typeof(Style));
        Assert.IsInstanceOfType(treeView.ItemContainerStyle, typeof(Style));
    }

    [TestMethod]
    public void NativeCollectionControls_RetainAutomationPatterns()
    {
        var listBoxPeer = new ListBoxAutomationPeer(new ListBox());
        var listViewPeer = new ListViewAutomationPeer(new ListView());
        var treeViewPeer = new TreeViewAutomationPeer(new TreeView());
        var scrollBarPeer = new ScrollBarAutomationPeer(new ScrollBar());
        var scrollViewerPeer = new ScrollViewerAutomationPeer(new ScrollViewer());

        Assert.IsNotNull(listBoxPeer.GetPattern(PatternInterface.Selection));
        Assert.IsNotNull(listViewPeer.GetPattern(PatternInterface.Selection));
        Assert.IsNotNull(treeViewPeer.GetPattern(PatternInterface.Selection));
        Assert.IsNotNull(scrollBarPeer.GetPattern(PatternInterface.RangeValue));
        Assert.IsNotNull(scrollViewerPeer.GetPattern(PatternInterface.Scroll));
    }

    [TestMethod]
    public void CommandButtonStyles_PreserveToggleRepeatAndIconSemantics()
    {
        var theme = LoadOfficeTheme();
        var source = new BindingSource();
        var toggleCommand = new RecordingCommand();
        var repeatCommand = new RecordingCommand();
        var toggle = new TestToggleButton
        {
            Command = toggleCommand,
            Style = (Style)theme[typeof(ToggleButton)]
        };
        var repeat = new TestRepeatButton
        {
            Command = repeatCommand,
            Style = (Style)theme[typeof(RepeatButton)]
        };
        var iconButton = new Button
        {
            Style = (Style)theme["DxIconButtonStyle"]
        };

        BindingOperations.SetBinding(
            toggle,
            ToggleButton.IsCheckedProperty,
            new Binding(nameof(BindingSource.IsChecked))
            {
                Source = source,
                Mode = BindingMode.TwoWay
            });

        toggle.InvokeClick();
        repeat.InvokeClick();

        Assert.IsTrue(source.IsChecked);
        Assert.AreEqual(1, toggleCommand.ExecutionCount);
        Assert.AreEqual(1, repeatCommand.ExecutionCount);
        Assert.IsNotNull(toggle.Template);
        Assert.IsNotNull(repeat.Template);
        Assert.IsNotNull(iconButton.Template);
        Assert.IsNotNull(toggle.FocusVisualStyle);
        Assert.IsNotNull(repeat.FocusVisualStyle);
    }

    [TestMethod]
    public void MenuStyles_ExposeSubmenuPartsAndKeyboardNavigation()
    {
        var theme = LoadOfficeTheme();
        var menu = new Menu
        {
            Style = (Style)theme[typeof(Menu)]
        };
        var header = new MenuItem
        {
            Header = "_File",
            Style = (Style)theme[typeof(MenuItem)]
        };
        header.Items.Add(new MenuItem { Header = "_New" });
        menu.Items.Add(header);

        Assert.IsTrue(menu.ApplyTemplate());
        Assert.IsTrue(header.ApplyTemplate());

        Assert.IsInstanceOfType(menu.ItemContainerStyle, typeof(Style));
        Assert.AreEqual(KeyboardNavigationMode.Cycle, KeyboardNavigation.GetDirectionalNavigation(menu));
        Assert.IsNotNull(header.Template.FindName("PART_Popup", header));

        var contextMenu = new ContextMenu
        {
            Style = (Style)theme[typeof(ContextMenu)]
        };
        contextMenu.Items.Add(new MenuItem { Header = "_Open" });
        Assert.IsInstanceOfType(contextMenu.ItemContainerStyle, typeof(Style));
        Assert.IsNotNull(contextMenu.Template);
    }

    [TestMethod]
    public void ToolBarAndStatusStyles_ExposeContainersAndOverflowParts()
    {
        var theme = LoadOfficeTheme();
        var toolBar = new ToolBar
        {
            Style = (Style)theme[typeof(ToolBar)]
        };
        toolBar.Items.Add(new Button { Content = "_New" });
        toolBar.Items.Add(new Button
        {
            Content = "Overflow",
            Command = ApplicationCommands.Help
        });
        ToolBar.SetOverflowMode((DependencyObject)toolBar.Items[1], OverflowMode.Always);

        var statusBar = new StatusBar
        {
            Style = (Style)theme[typeof(StatusBar)]
        };
        statusBar.Items.Add(new StatusBarItem { Content = "Ready" });

        Assert.IsTrue(toolBar.ApplyTemplate());
        Assert.IsNotNull(toolBar.Template.FindName("PART_ToolBarPanel", toolBar));
        Assert.IsNotNull(toolBar.Template.FindName("PART_ToolBarOverflowPanel", toolBar));
        Assert.IsInstanceOfType(statusBar.ItemContainerStyle, typeof(Style));
        Assert.IsNotNull(statusBar.Template);
    }

    [TestMethod]
    public void TransientChromeStyles_LoadWithoutOpeningSecondaryWindows()
    {
        var theme = LoadOfficeTheme();
        var popup = new Popup
        {
            Child = new TextBlock { Text = "Details" },
            Style = (Style)theme["DxPopupStyle"]
        };
        var chrome = new Border
        {
            Child = new TextBlock { Text = "Popup content" },
            Style = (Style)theme["DxPopupChromeStyle"]
        };
        var toolTip = new ToolTip
        {
            Content = "Help",
            Style = (Style)theme[typeof(ToolTip)]
        };

        Assert.IsTrue(popup.AllowsTransparency);
        Assert.IsFalse(popup.Focusable);
        Assert.AreEqual(PopupAnimation.Fade, popup.PopupAnimation);
        Assert.IsInstanceOfType(popup.Child, typeof(TextBlock));
        Assert.IsNotNull(chrome.Style);
        Assert.IsNotNull(toolTip.Template);
    }

    [TestMethod]
    public void NativeCommandControls_RetainAutomationPatterns()
    {
        var togglePeer = new ToggleButtonAutomationPeer(new ToggleButton());
        var repeatPeer = new RepeatButtonAutomationPeer(new RepeatButton());
        var menuItem = new MenuItem { Header = "More" };
        menuItem.Items.Add(new MenuItem { Header = "Child" });
        var menuItemPeer = new MenuItemAutomationPeer(menuItem);

        Assert.IsNotNull(togglePeer.GetPattern(PatternInterface.Toggle));
        Assert.IsNotNull(repeatPeer.GetPattern(PatternInterface.Invoke));
        Assert.IsNotNull(menuItemPeer.GetPattern(PatternInterface.ExpandCollapse));
    }

    [TestMethod]
    public void LayoutNavigationStyles_ExposePartsAndPreserveNativeState()
    {
        var theme = LoadOfficeTheme();
        var tabControl = new TabControl
        {
            SelectedIndex = 1,
            Style = (Style)theme[typeof(TabControl)]
        };
        tabControl.Items.Add(new TabItem { Header = "One", Content = "First" });
        tabControl.Items.Add(new TabItem { Header = "Two", Content = "Second" });

        var expander = new Expander
        {
            Header = "Details",
            IsExpanded = true,
            Style = (Style)theme[typeof(Expander)]
        };
        var groupBox = new GroupBox
        {
            Header = "Settings",
            Content = new TextBlock { Text = "Content" },
            Style = (Style)theme[typeof(GroupBox)]
        };
        var splitter = new GridSplitter
        {
            ResizeBehavior = GridResizeBehavior.PreviousAndNext,
            ResizeDirection = GridResizeDirection.Columns,
            Style = (Style)theme[typeof(GridSplitter)]
        };

        Assert.IsTrue(tabControl.ApplyTemplate());
        Assert.IsTrue(expander.ApplyTemplate());
        Assert.IsTrue(groupBox.ApplyTemplate());
        Assert.IsTrue(splitter.ApplyTemplate());

        Assert.AreEqual(1, tabControl.SelectedIndex);
        Assert.IsNotNull(tabControl.Template.FindName("PART_SelectedContentHost", tabControl));
        Assert.IsNotNull(expander.Template.FindName("PART_ToggleButton", expander));
        Assert.IsNotNull(expander.Template.FindName("PART_ExpandSite", expander));
        Assert.IsTrue(expander.IsExpanded);
        Assert.IsNotNull(groupBox.Template.FindName("PART_Header", groupBox));
        Assert.AreEqual(GridResizeDirection.Columns, splitter.ResizeDirection);
        Assert.AreEqual(GridResizeBehavior.PreviousAndNext, splitter.ResizeBehavior);
    }

    [TestMethod]
    public void ExtendedEditorAndDateStyles_ExposeRequiredParts()
    {
        var theme = LoadOfficeTheme();
        var passwordBox = new PasswordBox
        {
            Style = (Style)theme[typeof(PasswordBox)]
        };
        var richTextBox = new RichTextBox
        {
            Style = (Style)theme[typeof(RichTextBox)]
        };
        var datePickerTextBox = new DatePickerTextBox
        {
            Style = (Style)theme[typeof(DatePickerTextBox)]
        };
        var datePicker = new DatePicker
        {
            SelectedDate = new DateTime(2026, 7, 19),
            Style = (Style)theme[typeof(DatePicker)]
        };
        var calendar = new Calendar
        {
            SelectedDate = new DateTime(2026, 7, 19),
            Style = (Style)theme[typeof(Calendar)]
        };

        Assert.IsTrue(passwordBox.ApplyTemplate());
        Assert.IsTrue(richTextBox.ApplyTemplate());
        Assert.IsTrue(datePickerTextBox.ApplyTemplate());
        Assert.IsTrue(datePicker.ApplyTemplate());
        Assert.IsTrue(calendar.ApplyTemplate());

        Assert.IsNotNull(passwordBox.Template.FindName("PART_ContentHost", passwordBox));
        Assert.IsNotNull(richTextBox.Template.FindName("PART_ContentHost", richTextBox));
        Assert.IsNotNull(datePickerTextBox.Template.FindName("PART_ContentHost", datePickerTextBox));
        Assert.IsNotNull(datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox));
        Assert.IsNotNull(datePicker.Template.FindName("PART_Root", datePicker));
        Assert.IsInstanceOfType(datePicker.Template.FindName("PART_TextBox", datePicker), typeof(DatePickerTextBox));
        Assert.IsInstanceOfType(datePicker.Template.FindName("PART_Button", datePicker), typeof(Button));
        Assert.IsInstanceOfType(datePicker.Template.FindName("PART_Popup", datePicker), typeof(Popup));
        Assert.IsInstanceOfType(calendar.Template.FindName("PART_CalendarItem", calendar), typeof(CalendarItem));
        Assert.AreEqual(new DateTime(2026, 7, 19), datePicker.SelectedDate);
        Assert.AreEqual(new DateTime(2026, 7, 19), calendar.SelectedDate);
    }

    [TestMethod]
    public void RangeAndProgressStyles_ExposeOrientationSpecificParts()
    {
        var theme = LoadOfficeTheme();
        var horizontalSlider = new Slider
        {
            Maximum = 100,
            Value = 65,
            Style = (Style)theme[typeof(Slider)]
        };
        var verticalSlider = new Slider
        {
            Maximum = 100,
            Orientation = Orientation.Vertical,
            Value = 40,
            Style = (Style)theme[typeof(Slider)]
        };
        var progressBar = new ProgressBar
        {
            Maximum = 100,
            Value = 65,
            Style = (Style)theme[typeof(ProgressBar)]
        };

        Assert.IsTrue(horizontalSlider.ApplyTemplate());
        Assert.IsTrue(verticalSlider.ApplyTemplate());
        Assert.IsTrue(progressBar.ApplyTemplate());

        Assert.IsInstanceOfType(horizontalSlider.Template.FindName("PART_Track", horizontalSlider), typeof(Track));
        Assert.IsInstanceOfType(verticalSlider.Template.FindName("PART_Track", verticalSlider), typeof(Track));
        Assert.IsNotNull(progressBar.Template.FindName("PART_Track", progressBar));
        Assert.IsNotNull(progressBar.Template.FindName("PART_Indicator", progressBar));
        Assert.IsNotNull(progressBar.Template.FindName("PART_GlowRect", progressBar));
        Assert.AreEqual(65d, horizontalSlider.Value);
        Assert.AreEqual(40d, verticalSlider.Value);
        Assert.AreEqual(65d, progressBar.Value);
    }

    [TestMethod]
    public void NativeLayoutAndInputControls_RetainAutomationPatterns()
    {
        var tabPeer = new TabControlAutomationPeer(new TabControl());
        var expanderPeer = new ExpanderAutomationPeer(new Expander());
        var calendarPeer = new CalendarAutomationPeer(new Calendar());
        var sliderPeer = new SliderAutomationPeer(new Slider());
        var progressPeer = new ProgressBarAutomationPeer(new ProgressBar());

        Assert.IsNotNull(tabPeer.GetPattern(PatternInterface.Selection));
        Assert.IsNotNull(expanderPeer.GetPattern(PatternInterface.ExpandCollapse));
        Assert.AreEqual(AutomationControlType.Calendar, calendarPeer.GetAutomationControlType());
        Assert.IsNotNull(sliderPeer.GetPattern(PatternInterface.RangeValue));
        Assert.IsNotNull(progressPeer.GetPattern(PatternInterface.RangeValue));
    }

    [TestMethod]
    public void ToggleSwitch_BindsTwoWayAndExecutesCommands()
    {
        var source = new BindingSource();
        var command = new RecordingCommand();
        var toggle = new TestToggleSwitch
        {
            Content = "Feature",
            Command = command
        };

        BindingOperations.SetBinding(
            toggle,
            ToggleButton.IsCheckedProperty,
            new Binding(nameof(BindingSource.IsChecked))
            {
                Source = source,
                Mode = BindingMode.TwoWay
            });

        toggle.InvokeClick();

        Assert.IsTrue(source.IsChecked);
        Assert.AreEqual(1, command.ExecutionCount);
        Assert.AreEqual("On", toggle.OnContent);
        Assert.AreEqual("Off", toggle.OffContent);
    }

    [TestMethod]
    public void ToggleSwitch_ExposesToggleAutomationSemantics()
    {
        var toggle = new TestToggleSwitch { Content = "Feature enabled" };
        var peer = toggle.CreateAutomationPeer();

        Assert.AreEqual(nameof(ToggleSwitch), peer.GetClassName());
        Assert.AreEqual("Feature enabled", peer.GetName());
        Assert.AreEqual(AutomationControlType.CheckBox, peer.GetAutomationControlType());
        Assert.IsNotNull(peer.GetPattern(PatternInterface.Toggle));
    }

    [TestMethod]
    public void TextBoxStyle_UsesStandardValidationState()
    {
        var theme = LoadOfficeTheme();
        var textBox = new TextBox
        {
            Style = (Style)theme[typeof(TextBox)]
        };

        var expression = BindingOperations.SetBinding(
            textBox,
            TextBox.TextProperty,
            new Binding(nameof(BindingSource.Text))
            {
                Source = new BindingSource(),
                Mode = BindingMode.TwoWay
            });

        var validationError = new ValidationError(
            new ExceptionValidationRule(),
            expression)
        {
            ErrorContent = "Invalid value."
        };

        Validation.MarkInvalid(expression, validationError);
        Assert.IsTrue(Validation.GetHasError(textBox));
        Assert.AreEqual("Invalid value.", Validation.GetErrors(textBox)[0].ErrorContent);

        Validation.ClearInvalid(expression);
        Assert.IsFalse(Validation.GetHasError(textBox));
    }

    private static ResourceDictionary LoadOfficeTheme()
    {
        var theme = (ResourceDictionary)Application.LoadComponent(
            new Uri(
                "/DevExpressInspiredControls;component/Themes/Office2019Colorful.xaml",
                UriKind.Relative));

        var application = Application.Current ?? new Application
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown
        };
        application.Resources.MergedDictionaries.Add(theme);
        return theme;
    }

    private static void AssertStyleProvidesTemplate<TControl>(ResourceDictionary theme)
        where TControl : Control, new()
    {
        var control = new TControl
        {
            Style = (Style)theme[typeof(TControl)]
        };

        Assert.IsNotNull(control.Template);
    }

    private sealed class TestToggleButton : ToggleButton
    {
        public void InvokeClick()
        {
            OnClick();
        }
    }

    private sealed class TestRepeatButton : RepeatButton
    {
        public void InvokeClick()
        {
            OnClick();
        }
    }

    private sealed class TestToggleSwitch : ToggleSwitch
    {
        public void InvokeClick()
        {
            OnClick();
        }

        public AutomationPeer CreateAutomationPeer()
        {
            return OnCreateAutomationPeer();
        }
    }

    private sealed class RecordingCommand : ICommand
    {
        public int ExecutionCount { get; private set; }

        public event EventHandler? CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecutionCount++;
        }
    }

    private sealed class BindingSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsChecked
        {
            get;
            set => SetField(ref field, value);
        }

        public string Text
        {
            get;
            set => SetField(ref field, value);
        } = string.Empty;

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
