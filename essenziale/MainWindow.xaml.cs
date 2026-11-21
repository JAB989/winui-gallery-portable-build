using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Graphics;

namespace WinUIEssenziale;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        AppWindow.Resize(new SizeInt32(960, 680));
        Nav.SelectedItem = Nav.MenuItems[0];
        Show("home");
    }

    private void Show(string page)
    {
        HomePanel.Visibility = page == "home" ? Visibility.Visible : Visibility.Collapsed;
        ControlsPanel.Visibility = page == "controls" ? Visibility.Visible : Visibility.Collapsed;
        SettingsPanel.Visibility = page == "settings" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnNavSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            Show("settings");
            return;
        }

        if (args.SelectedItem is NavigationViewItem item)
            Show(item.Tag?.ToString() ?? "home");
    }

    private void OnOpenControlsClick(object sender, RoutedEventArgs e)
    {
        Nav.SelectedItem = Nav.MenuItems[1];
    }

    private void OnSampleButtonClick(object sender, RoutedEventArgs e)
    {
        StatusText.Text = "Pulsante premuto.";
    }

    private void OnSampleMenuClick(object sender, RoutedEventArgs e)
    {
        StatusText.Text = "Comando menu selezionato.";
    }

    private async void OnDialogClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = RootGrid.XamlRoot,
            Title = "Finestra WinUI originale",
            Content = "Questo è un ContentDialog Microsoft, non un popup disegnato in PyQt5.",
            CloseButtonText = "Chiudi"
        };
        await dialog.ShowAsync();
    }

    private async void OnAboutClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = RootGrid.XamlRoot,
            Title = "WinUI Essenziale",
            Content = "Una demo minima che riutilizza i controlli nativi del Windows App SDK.",
            CloseButtonText = "OK"
        };
        await dialog.ShowAsync();
    }

    private void OnCloseClick(object sender, RoutedEventArgs e) => Close();
    private void OnOpenPaneClick(object sender, RoutedEventArgs e) => Nav.IsPaneOpen = true;
    private void OnClosePaneClick(object sender, RoutedEventArgs e) => Nav.IsPaneOpen = false;

    private void OnThemeToggled(object sender, RoutedEventArgs e)
    {
        if (RootGrid is not null && DarkThemeSwitch is not null)
            RootGrid.RequestedTheme = DarkThemeSwitch.IsOn ? ElementTheme.Dark : ElementTheme.Light;
    }
}
