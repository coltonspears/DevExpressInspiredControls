using System.IO;
using System.Windows;
using System.Windows.Controls;
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
            }))
        ];

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
            Height = 1200,
            MinHeight = 1200
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
