using MPowerKit.Navigation.Awares;
using MPowerKit.Navigation.Interfaces;

namespace MauiApp1;

public sealed partial class NewPage1 : IInitializeAware
{
    public NewPage1()
    {
        InitializeComponent();
    }

    public void Initialize(INavigationParameters parameters)
    {
        CollectionView.ItemsSource = Enumerable.Range(0, 100).Select(num => $"Item {num}").ToArray();
    }
}