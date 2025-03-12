using AsyncAwaitBestPractices;
using MPowerKit.Navigation.Interfaces;
using MPowerKit.Popups;
using MPowerKit.Popups.Animations;
using MPowerKit.Popups.Interfaces;

namespace MauiApp1;

public class Popup : ContentView
{
    #region IsVisible

    public new static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible), typeof(bool), typeof(Popup), false,
        propertyChanged: (bindable, _, newValue) =>
        {
            var popup = (Popup)bindable;
            popup.UpdateVisibility((bool)newValue);
        });

    public new bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }

    #endregion

    #region BackgroundColor

    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
        nameof(BackgroundColor), typeof(Color), typeof(Popup), Colors.Transparent);
    
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    #endregion
    
    #region Animation

    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(
        nameof(Animation), typeof(IPopupAnimation), typeof(Popup), new ScaleAnimation());
    
    public IPopupAnimation Animation
    {
        get => (IPopupAnimation)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    #endregion
    
    #region CloseOnBackgroundClick

    public static readonly BindableProperty CloseOnBackgroundClickProperty = BindableProperty.Create(
        nameof(CloseOnBackgroundClick), typeof(bool), typeof(Popup), true);
    
    public bool CloseOnBackgroundClick
    {
        get => (bool)GetValue(CloseOnBackgroundClickProperty);
        set => SetValue(CloseOnBackgroundClickProperty, value);
    }

    #endregion
    
    private readonly INavigationPopupService _navigationPopupService;
    
    private PopupPage? _popupPage;

    public Popup()
    {
        base.IsVisible = false;
        base.BackgroundColor = Colors.Transparent;
        
        _navigationPopupService = ServiceProvider.GetRequiredService<INavigationPopupService>();
    }
    
    public event EventHandler? Appearing;

    private static IServiceProvider ServiceProvider =>
        Application.Current?.Handler?.MauiContext?.Services ?? throw new InvalidOperationException();
    
    private static PopupPage CreatePopupPage(Popup popup) => new()
    {
        Content = popup.Content,
        BackgroundColor = popup.BackgroundColor,
        Animation = popup.Animation,
        CloseOnBackgroundClick = popup.CloseOnBackgroundClick,
    };
    
    private void UpdateVisibility(bool isVisible)
    {
        if (isVisible)
        {
            ShowPopup();
        }
        else
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        if (_popupPage is not null) return;
        
        _popupPage = CreatePopupPage(this);
        _popupPage.Appearing += OnAppearing;
        _popupPage.BackgroundClicked += OnBackgroundClicked;
    
        _navigationPopupService.ShowPopupAsync(_popupPage).SafeFireAndForget();
    }

    private void HidePopup()
    {
        if (_popupPage is null) return;
        
        _navigationPopupService.HidePopupAsync(_popupPage).SafeFireAndForget();
        
        _popupPage.Appearing -= OnAppearing;
        _popupPage.BackgroundClicked -= OnBackgroundClicked;
        _popupPage = null;
    }
    
    private void OnAppearing(object? sender, EventArgs e) => Appearing?.Invoke(this, e);

    private void OnBackgroundClicked(object? sender, RoutedEventArgs args)
    {
        IsVisible = false;
    }
}