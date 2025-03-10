using MPowerKit.Navigation.Interfaces;

namespace MauiApp1;

public sealed partial class MainFlyoutPage
{
    private readonly INavigationService _navigationService;

    public MainFlyoutPage(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    private async void MainPage_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateThrougFlyoutPageAsync("NavigationPage/MainPage");
    }

    private async void NewPage1_OnClicked(object? sender, EventArgs e)
    {
        await _navigationService.NavigateThrougFlyoutPageAsync("NavigationPage/NewPage1");
    }
}