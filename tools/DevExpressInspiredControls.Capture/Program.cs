using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpressInspiredControls.Controls;
using DevExpressInspiredControls.Demo;

namespace DevExpressInspiredControls.Capture;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        var outputDir = ResolveOutputDir(args);
        Directory.CreateDirectory(outputDir);

        var app = new Application
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown
        };
        app.Resources.MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri(
                "pack://application:,,,/DevExpressInspiredControls;component/Themes/Office2019Colorful.xaml",
                UriKind.Absolute)
        });

        var captures = BuildCaptures();
        var written = 0;

        foreach (var (fileName, factory) in captures)
        {
            var path = Path.Combine(outputDir, fileName + ".png");
            var visual = factory();
            CaptureToFile(visual, path, sizeToContent: true);
            written++;
            Console.WriteLine($"Wrote {path}");
        }

        var galleryPath = Path.Combine(Directory.GetParent(outputDir)!.FullName, "Gallery.png");
        CaptureGallery(galleryPath);
        written++;
        Console.WriteLine($"Wrote {galleryPath}");

        app.Shutdown();
        Console.WriteLine($"Captured {written} image(s)");
        return 0;
    }

    private static string ResolveOutputDir(string[] args)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            return Path.GetFullPath(args[0]);

        // Default: <repo>/docs/images/controls
        var baseDir = AppContext.BaseDirectory;
        var repoRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", ".."));
        return Path.Combine(repoRoot, "docs", "images", "controls");
    }

    private static IReadOnlyList<(string FileName, Func<FrameworkElement> Factory)> BuildCaptures()
        =>
        [
            ("Button", () => Card("Button", new WrapPanel
            {
                Children =
                {
                    new Button { Content = "Standard", MinWidth = 100, Margin = new Thickness(0, 0, 10, 0) },
                    new Button { Content = "Default", MinWidth = 100, Margin = new Thickness(0, 0, 10, 0), IsDefault = true },
                    new Button { Content = "Disabled", MinWidth = 100, IsEnabled = false }
                }
            })),
            ("TextBox", () => Card("TextBox", new StackPanel
            {
                Width = 260,
                Children =
                {
                    Labeled("Standard", new TextBox { Text = "Editable value", Margin = new Thickness(0, 0, 0, 8) }),
                    Labeled("Read only", new TextBox { Text = "Read-only value", IsReadOnly = true, Margin = new Thickness(0, 0, 0, 8) }),
                    Labeled("Disabled", new TextBox { Text = "Disabled value", IsEnabled = false })
                }
            })),
            ("ComboBox", () => Card("ComboBox", new StackPanel
            {
                Width = 260,
                Children =
                {
                    Labeled("Role", new ComboBox
                    {
                        ItemsSource = Roles(),
                        SelectedItem = "Developer",
                        Margin = new Thickness(0, 0, 0, 8)
                    }),
                    Labeled("Editable", new ComboBox
                    {
                        ItemsSource = Roles(),
                        IsEditable = true,
                        Text = "Developer"
                    })
                }
            })),
            ("CheckBox", () => Card("CheckBox", new WrapPanel
            {
                Children =
                {
                    new CheckBox { Content = "Enabled option", IsChecked = true, Margin = new Thickness(0, 0, 24, 0) },
                    new CheckBox { Content = "Indeterminate", IsChecked = null, IsThreeState = true, Margin = new Thickness(0, 0, 24, 0) },
                    new CheckBox { Content = "Disabled", IsEnabled = false }
                }
            })),
            ("RadioButton", () => Card("RadioButton", new WrapPanel
            {
                Children =
                {
                    new RadioButton { Content = "Choice one", GroupName = "CaptureChoice", IsChecked = true, Margin = new Thickness(0, 0, 24, 0) },
                    new RadioButton { Content = "Choice two", GroupName = "CaptureChoice", Margin = new Thickness(0, 0, 24, 0) },
                    new RadioButton { Content = "Disabled", GroupName = "CaptureChoice", IsEnabled = false }
                }
            })),
            ("ToggleSwitch", () => Card("ToggleSwitch", new WrapPanel
            {
                Children =
                {
                    new ToggleSwitch
                    {
                        Content = "Feature",
                        IsChecked = true,
                        Margin = new Thickness(0, 0, 28, 0)
                    },
                    new ToggleSwitch
                    {
                        Content = "Disabled",
                        IsChecked = true,
                        IsEnabled = false
                    }
                }
            })),
            ("CommandButtons", CommandButtonsCard),
            ("Menu", MenuCard),
            ("ToolBar", ToolBarCard),
            ("TransientUi", TransientUiCard),
            ("SearchComposition", SearchCompositionCard),
            ("LayoutNavigation", LayoutNavigationCard),
            ("ExtendedEditors", ExtendedEditorsCard),
            ("DateTime", DateTimeCard),
            ("ProgressRange", ProgressRangeCard)
        ];

    private static FrameworkElement CommandButtonsCard()
    {
        var iconButton = new Button
        {
            Style = ThemeStyle("DxIconButtonStyle"),
            ToolTip = "Create"
        };
        iconButton.Content = new System.Windows.Shapes.Path
        {
            Width = 12,
            Height = 12,
            Data = Geometry.Parse("M 6 0 L 6 12 M 0 6 L 12 6"),
            Stroke = ThemeBrush("DxTextBrush"),
            StrokeThickness = 2
        };

        return Card("Command buttons", new WrapPanel
        {
            Children =
            {
                new ToggleButton
                {
                    Content = "Checked",
                    IsChecked = true,
                    MinWidth = 90,
                    Margin = new Thickness(0, 0, 10, 0)
                },
                new ToggleButton
                {
                    Content = "Disabled",
                    IsChecked = true,
                    IsEnabled = false,
                    MinWidth = 90,
                    Margin = new Thickness(0, 0, 10, 0)
                },
                new RepeatButton
                {
                    Content = "Hold +",
                    MinWidth = 80,
                    Margin = new Thickness(0, 0, 10, 0)
                },
                iconButton
            }
        });
    }

    private static FrameworkElement MenuCard()
    {
        var file = new MenuItem { Header = "_File" };
        file.Items.Add(new MenuItem { Header = "_New", InputGestureText = "Ctrl+N" });
        file.Items.Add(new MenuItem { Header = "_Save", InputGestureText = "Ctrl+S" });
        file.Items.Add(new Separator());
        file.Items.Add(new MenuItem { Header = "Unavailable", IsEnabled = false });

        var view = new MenuItem { Header = "_View" };
        view.Items.Add(new MenuItem { Header = "_Feature enabled", IsCheckable = true, IsChecked = true });

        var menu = new Menu();
        menu.Items.Add(file);
        menu.Items.Add(view);

        var contextHost = new Border
        {
            Margin = new Thickness(0, 10, 0, 0),
            Padding = new Thickness(10),
            Background = ThemeBrush("DxSurfaceSubtleBrush"),
            BorderBrush = ThemeBrush("DxBorderBrush"),
            BorderThickness = new Thickness(1),
            Child = new TextBlock { Text = "Right-click target with themed ContextMenu" }
        };
        var contextMenu = new ContextMenu();
        contextMenu.Items.Add(new MenuItem { Header = "_Open" });
        contextMenu.Items.Add(new MenuItem { Header = "_Remove" });
        contextHost.ContextMenu = contextMenu;

        return Card("Menu and ContextMenu", new StackPanel
        {
            Width = 420,
            Children = { menu, contextHost }
        });
    }

    private static FrameworkElement ToolBarCard()
    {
        var toolBar = new ToolBar { Width = 430 };
        toolBar.Items.Add(new Button { Content = "_New" });
        toolBar.Items.Add(new Button { Content = "_Save" });
        toolBar.Items.Add(new Separator());
        toolBar.Items.Add(new ToggleButton { Content = "_Feature", IsChecked = true });
        var overflow = new Button { Content = "Overflow command" };
        ToolBar.SetOverflowMode(overflow, OverflowMode.Always);
        toolBar.Items.Add(overflow);

        return Card("ToolBar and Separator", new ToolBarTray
        {
            Width = 430,
            ToolBars = { toolBar }
        });
    }

    private static FrameworkElement TransientUiCard()
    {
        var popupChrome = new Border
        {
            Width = 260,
            Margin = new Thickness(0, 0, 0, 10),
            Style = ThemeStyle("DxPopupChromeStyle"),
            Child = new TextBlock
            {
                Text = "Reusable Popup chrome",
                TextWrapping = TextWrapping.Wrap
            }
        };
        var toolTipHost = new Button
        {
            Content = "Hover for themed ToolTip",
            Margin = new Thickness(0, 0, 0, 10),
            ToolTip = new ToolTip
            {
                Content = "Themed ToolTip"
            }
        };
        var statusBar = new StatusBar
        {
            Items =
            {
                new StatusBarItem { Content = "Ready" },
                new StatusBarItem { Content = "3 of 5 items" }
            }
        };

        return Card("ToolTip, Popup chrome, and StatusBar", new StackPanel
        {
            Width = 420,
            Children = { popupChrome, toolTipHost, statusBar }
        });
    }

    private static FrameworkElement SearchCompositionCard()
    {
        var grid = new Grid { Width = 380 };
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        var textBox = new TextBox { Text = "arch" };
        var clearButton = new Button
        {
            Margin = new Thickness(6, 0, 0, 0),
            Style = ThemeStyle("DxIconButtonStyle"),
            ToolTip = "Clear search"
        };
        clearButton.Content = new System.Windows.Shapes.Path
        {
            Width = 10,
            Height = 10,
            Data = Geometry.Parse("M 1 1 L 9 9 M 9 1 L 1 9"),
            Stroke = ThemeBrush("DxTextBrush"),
            StrokeThickness = 1.8
        };
        Grid.SetColumn(clearButton, 1);
        grid.Children.Add(textBox);
        grid.Children.Add(clearButton);
        return Card("Search composition", grid);
    }

    private static FrameworkElement LayoutNavigationCard()
    {
        var segments = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 12) };
        segments.Children.Add(new RadioButton
        {
            Content = "Overview",
            GroupName = "CaptureSegment",
            IsChecked = true,
            Style = ThemeStyle("DxSegmentedRadioButtonFirstStyle")
        });
        segments.Children.Add(new RadioButton
        {
            Content = "Details",
            GroupName = "CaptureSegment",
            Style = ThemeStyle("DxSegmentedRadioButtonMiddleStyle")
        });
        segments.Children.Add(new RadioButton
        {
            Content = "History",
            GroupName = "CaptureSegment",
            Style = ThemeStyle("DxSegmentedRadioButtonLastStyle")
        });

        var tabControl = new TabControl { Height = 145, Margin = new Thickness(0, 0, 0, 12) };
        tabControl.Items.Add(new TabItem
        {
            Header = "General",
            Content = new GroupBox
            {
                Header = "Settings",
                Margin = new Thickness(4),
                Content = new TextBlock { Text = "Grouped tab content" }
            }
        });
        tabControl.Items.Add(new TabItem
        {
            Header = "Advanced",
            Content = new Expander
            {
                Header = "Advanced options",
                IsExpanded = true,
                Margin = new Thickness(4),
                Padding = new Thickness(8),
                Content = new CheckBox { Content = "Enable diagnostics", IsChecked = true }
            }
        });
        tabControl.Items.Add(new TabItem { Header = "Disabled", IsEnabled = false });

        var splitGrid = new Grid { Height = 62 };
        splitGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        splitGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5) });
        splitGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        var left = new Border
        {
            Padding = new Thickness(8),
            Background = ThemeBrush("DxSurfaceSubtleBrush"),
            Child = new TextBlock { Text = "Left pane" }
        };
        var splitter = new GridSplitter
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ResizeDirection = GridResizeDirection.Columns,
            ResizeBehavior = GridResizeBehavior.PreviousAndNext
        };
        var right = new Border
        {
            Padding = new Thickness(8),
            Background = ThemeBrush("DxSelectionInactiveBrush"),
            Child = new TextBlock { Text = "Right pane" }
        };
        Grid.SetColumn(splitter, 1);
        Grid.SetColumn(right, 2);
        splitGrid.Children.Add(left);
        splitGrid.Children.Add(splitter);
        splitGrid.Children.Add(right);

        return Card("Layout and navigation", new StackPanel
        {
            Width = 520,
            Children = { segments, tabControl, splitGrid }
        });
    }

    private static FrameworkElement ExtendedEditorsCard()
    {
        var password = new PasswordBox { Password = "sample", Margin = new Thickness(0, 0, 0, 8) };
        var disabledPassword = new PasswordBox { IsEnabled = false, Margin = new Thickness(0, 0, 0, 12) };
        var richText = new RichTextBox { Height = 110 };
        richText.Document.Blocks.Add(new Paragraph(new Run("Formatted notes retain native editing and scrolling."))
        {
            FontWeight = FontWeights.SemiBold
        });

        return Card("PasswordBox and RichTextBox", new StackPanel
        {
            Width = 430,
            Children =
            {
                Labeled("Password", password),
                Labeled("Disabled", disabledPassword),
                Labeled("Notes", richText)
            }
        });
    }

    private static FrameworkElement DateTimeCard()
    {
        var selectedDate = new DateTime(2026, 7, 19);
        return Card("DatePicker and Calendar", new StackPanel
        {
            Width = 470,
            Children =
            {
                new DatePicker
                {
                    SelectedDate = selectedDate,
                    Margin = new Thickness(0, 0, 0, 12)
                },
                new Calendar
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    SelectedDate = selectedDate
                }
            }
        });
    }

    private static FrameworkElement ProgressRangeCard()
    {
        return Card("Slider and ProgressBar", new StackPanel
        {
            Width = 430,
            Children =
            {
                new Slider
                {
                    Maximum = 100,
                    Value = 65,
                    TickFrequency = 10,
                    TickPlacement = TickPlacement.BottomRight,
                    IsSnapToTickEnabled = true,
                    Margin = new Thickness(0, 0, 0, 14)
                },
                new ProgressBar
                {
                    Maximum = 100,
                    Value = 65,
                    Margin = new Thickness(0, 0, 0, 12)
                },
                new ProgressBar
                {
                    IsIndeterminate = true,
                    Margin = new Thickness(0, 0, 0, 12)
                },
                new ProgressBar
                {
                    IsEnabled = false,
                    Maximum = 100,
                    Value = 40
                }
            }
        });
    }

    private static void CaptureGallery(string path)
    {
        var viewModel = new MainViewModel(TimeProvider.System);
        var window = new MainWindow(viewModel)
        {
            WindowStartupLocation = WindowStartupLocation.Manual,
            Left = -20000,
            Top = -20000,
            ShowInTaskbar = false,
            ShowActivated = false,
            Width = 980,
            // Tall enough that the gallery content is fully visible (no scroll clip).
            Height = 4300,
            MinHeight = 4300
        };

        try
        {
            window.Show();
            window.UpdateLayout();
            for (var i = 0; i < 12; i++)
                DoEvents();

            // Prefer the logical content tree so chrome/chrome insets don't crop sections.
            if (window.Content is FrameworkElement content)
                CaptureVisualToFile(content, path, ThemeBrush("DxWindowBrush"));
            else
                CaptureWindowToFile(window, path);
        }
        finally
        {
            window.Close();
        }
    }

    private static FrameworkElement Card(string title, UIElement content)
    {
        var stack = new StackPanel { Margin = new Thickness(20) };
        stack.Children.Add(new TextBlock
        {
            Text = title,
            FontSize = 16,
            FontWeight = FontWeights.SemiBold,
            Margin = new Thickness(0, 0, 0, 12),
            Foreground = ThemeBrush("DxTextBrush")
        });
        stack.Children.Add(content);

        return new Border
        {
            Background = ThemeBrush("DxSurfaceBrush"),
            BorderBrush = ThemeBrush("DxBorderBrush"),
            BorderThickness = new Thickness(1),
            SnapsToDevicePixels = true,
            Child = stack,
            MinWidth = 200,
            MinHeight = 80
        };
    }

    private static UIElement Labeled(string label, FrameworkElement control)
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        var text = new TextBlock
        {
            Text = label + ":",
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 12, 0),
            Foreground = ThemeBrush("DxTextBrush")
        };
        Grid.SetColumn(text, 0);
        Grid.SetColumn(control, 1);
        grid.Children.Add(text);
        grid.Children.Add(control);
        return grid;
    }

    private static string[] Roles()
        => ["Developer", "Designer", "Product manager", "Administrator"];

    private static Brush ThemeBrush(string key)
        => Application.Current?.TryFindResource(key) as Brush
           ?? new SolidColorBrush(Colors.White);

    private static Style ThemeStyle(string key)
        => Application.Current?.TryFindResource(key) as Style
           ?? throw new InvalidOperationException($"Theme style '{key}' was not found.");

    private static void CaptureToFile(FrameworkElement visual, string path, bool sizeToContent)
    {
        var host = new AdornerDecorator { Child = visual };
        var window = new Window
        {
            Title = "DevExpressInspiredControls Capture",
            Content = host,
            SizeToContent = sizeToContent ? SizeToContent.WidthAndHeight : SizeToContent.Manual,
            WindowStyle = WindowStyle.None,
            ResizeMode = ResizeMode.NoResize,
            ShowInTaskbar = false,
            ShowActivated = false,
            Background = ThemeBrush("DxWindowBrush"),
            Left = -20000,
            Top = -20000
        };

        try
        {
            window.Show();
            window.UpdateLayout();
            for (var i = 0; i < 8; i++)
                DoEvents();

            CaptureVisualToFile(host, path, ThemeBrush("DxWindowBrush"));
        }
        finally
        {
            window.Close();
        }
    }

    private static void CaptureWindowToFile(Window window, string path)
    {
        window.UpdateLayout();
        for (var i = 0; i < 4; i++)
            DoEvents();

        CaptureVisualToFile(window, path, ThemeBrush("DxWindowBrush"));
    }

    private static void CaptureVisualToFile(Visual visual, string path, Brush background)
    {
        var bounds = VisualTreeHelper.GetDescendantBounds(visual);
        if (bounds.IsEmpty || bounds.Width < 1 || bounds.Height < 1)
        {
            if (visual is FrameworkElement fe)
                bounds = new Rect(0, 0, Math.Max(fe.ActualWidth, 1), Math.Max(fe.ActualHeight, 1));
            else
                bounds = new Rect(0, 0, 1, 1);
        }

        var dpi = VisualTreeHelper.GetDpi(visual);
        var scaleX = Math.Max(dpi.DpiScaleX, 1.0);
        var scaleY = Math.Max(dpi.DpiScaleY, 1.0);
        var pixelWidth = Math.Max(1, (int)Math.Ceiling(bounds.Width * scaleX));
        var pixelHeight = Math.Max(1, (int)Math.Ceiling(bounds.Height * scaleY));

        var rtb = new RenderTargetBitmap(pixelWidth, pixelHeight, 96 * scaleX, 96 * scaleY, PixelFormats.Pbgra32);
        var dv = new DrawingVisual();
        using (var ctx = dv.RenderOpen())
        {
            var vb = new VisualBrush(visual)
            {
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                ViewboxUnits = BrushMappingMode.Absolute,
                Viewbox = bounds,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewport = new Rect(bounds.Size)
            };
            ctx.DrawRectangle(background, null, new Rect(bounds.Size));
            ctx.DrawRectangle(vb, null, new Rect(bounds.Size));
        }

        rtb.Render(dv);

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(rtb));
        using var stream = File.Create(path);
        encoder.Save(stream);
    }

    private static void DoEvents()
    {
        var frame = new System.Windows.Threading.DispatcherFrame();
        System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(
            System.Windows.Threading.DispatcherPriority.Background,
            new Action(() => frame.Continue = false));
        System.Windows.Threading.Dispatcher.PushFrame(frame);
    }
}
