using MPowerKit.Navigation;
using MPowerKit.Navigation.Awares;
using MPowerKit.Navigation.Interfaces;

namespace MauiApp1;

public sealed partial class MainPage : ILoadedAsyncAware
{
    private readonly INavigationService _navigationService;
    private readonly IRegionManager _regionManager;
    private readonly IPopupNavigationService _popupNavigationService;
    private int _count;

    public MainPage(INavigationService navigationService,
        IRegionManager regionManager, IPopupNavigationService popupNavigationService)
    {
        InitializeComponent();
        _navigationService = navigationService;
        _regionManager = regionManager;
        _popupNavigationService = popupNavigationService;
    }

    public async Task OnLoadedAsync(INavigationParameters navigationParameters)
    {
        await _regionManager.NavigateTo("MainRegion", "NewContent1");
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        CounterBtn.Text = _count++ is 1
            ? $"Clicked {_count} time"
            : $"Clicked {_count} times";

        NavigationResult result = int.IsEvenInteger(_count)
            ? await _popupNavigationService.ShowPopupAsync("PopupPageTest")
            : await _navigationService.NavigateAsync("NewPage1");
    }
}