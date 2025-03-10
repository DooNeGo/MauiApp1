using MPowerKit.Navigation.Awares;
using MPowerKit.Navigation.Interfaces;

namespace MauiApp1;

public sealed partial class NewContent2 : IInitializeAware, INavigationAware
{
    public NewContent2()
    {
        InitializeComponent();
    }

    public void Initialize(INavigationParameters parameters)
    {
        Console.WriteLine("Initialize NewContent2");
    }

    public void OnNavigatedFrom(INavigationParameters navigationParameters)
    {
        Console.WriteLine("OnNavigatedFrom NewContent2");
    }

    public void OnNavigatedTo(INavigationParameters navigationParameters)
    {
        Console.WriteLine("OnNavigatedTo NewContent2");
    }
}