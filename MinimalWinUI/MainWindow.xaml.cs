using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Graphics;

namespace MiniFluent;

public sealed partial class MainWindow : Window
{
    private bool _uiReady;

    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        AppWindow.Resize(new SizeInt32(1100, 730));
        ShellNav.SelectedItem = ShellNav.MenuItems[0];
        _uiReady = true;
        ShowPage("home");
    }

    private void ShowPage(string page)
    {
        HomeView.Visibility = page == "home" ? Visibility.Visible : Visibility.Collapsed;
        ControlsView.Visibility = page == "controls" ? Visibility.Visible : Visibility.Collapsed;
        WindowsView.Visibility = page == "windows" ? Visibility.Visible : Visibility.Collapsed;
        SettingsView.Visibility = page == "settings" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ShellNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (!_uiReady) return;
        if (args.IsSettingsSelected)
        {
            ShowPage("settings");
            return;
        }
        string page = (args.SelectedItem as NavigationViewItem)?.Tag?.ToString() ?? "home";
        ShowPage(page);
    }

    private void GoHome_Click(object sender, RoutedEventArgs args)
    {
        ShellNav.SelectedItem = ShellNav.MenuItems[0];
        ShowPage("home");
    }

    private void Settings_Click(object sender, RoutedEventArgs args)
    {
        ShellNav.SelectedItem = ShellNav.SettingsItem;
        ShowPage("settings");
    }

    private void TogglePane_Click(object sender, RoutedEventArgs args)
    {
        ShellNav.IsPaneOpen = !ShellNav.IsPaneOpen;
    }

    private void LightTheme_Click(object sender, RoutedEventArgs args)
    {
        ThemeSwitch.IsOn = false;
        AppRoot.RequestedTheme = ElementTheme.Light;
    }

    private void DarkTheme_Click(object sender, RoutedEventArgs args)
    {
        ThemeSwitch.IsOn = true;
        AppRoot.RequestedTheme = ElementTheme.Dark;
    }

    private void ThemeSwitch_Toggled(object sender, RoutedEventArgs args)
    {
        if (!_uiReady) return;
        AppRoot.RequestedTheme = ThemeSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
    }

    private void Notify_Click(object sender, RoutedEventArgs args)
    {
        ShellNav.SelectedItem = ShellNav.MenuItems[0];
        ShowPage("home");
        HomeNotification.IsOpen = true;
    }

    private async void About_Click(object sender, RoutedEventArgs args)
    {
        ContentDialog dialog = new ContentDialog
        {
            XamlRoot = AppRoot.XamlRoot,
            Title = "Fluent Mini",
            Content = "Questa finestra utilizza controlli WinUI 3 originali Microsoft. Nessuna replica grafica PyQt.",
            CloseButtonText = "Chiudi"
        };
        await dialog.ShowAsync();
    }

    private void Close_Click(object sender, RoutedEventArgs args)
    {
        Close();
    }
}
