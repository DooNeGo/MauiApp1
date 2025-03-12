using MPowerKit.Navigation.Awares;
using MPowerKit.Navigation.Interfaces;

namespace MauiApp1;

public sealed partial class MainPage : ILoadedAsyncAware
{
    private readonly INavigationService _navigationService;
    private readonly IRegionManager _regionManager;
    private readonly IPopupNavigationService _popupNavigationService;
    private readonly INavigationPopupService _navigationPopupService;
    
    private int _count;

    public MainPage(
        INavigationService navigationService,
        IRegionManager regionManager,
        IPopupNavigationService popupNavigationService,
        INavigationPopupService navigationPopupService)
    {
        InitializeComponent();
        _navigationService = navigationService;
        _regionManager = regionManager;
        _popupNavigationService = popupNavigationService;
        _navigationPopupService = navigationPopupService;
    }

    public async Task OnLoadedAsync(INavigationParameters navigationParameters)
    {
        await _regionManager.NavigateTo("MainRegion", "NewContent1");
    }
    
    private readonly PopupPageTest _popupPageTest = new();
    
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        CounterBtn.Text = _count++ is 1
            ? $"Clicked {_count} time"
            : $"Clicked {_count} times";

        if (int.IsEvenInteger(_count))
        {
            await _navigationService.NavigateAsync(nameof(NewPage1));
        }
        else
        { 
            // Problem here
            await _navigationPopupService.ShowPopupAsync(_popupPageTest);
        }
    }
}