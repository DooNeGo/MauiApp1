using MPowerKit.Navigation.Awares;
using MPowerKit.Navigation.Popups;

namespace MauiApp1;

public sealed partial class PopupPageTest : IPopupDialogAware
{
    public PopupPageTest()
    {
        InitializeComponent();
    }

    public Action<(Confirmation Confirmation, bool Animated)>? RequestClose { get; set; }
}