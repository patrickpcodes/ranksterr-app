using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Ranksterr.Mobile;
public abstract class BaseContentPage : ContentPage
{
    public BaseContentPage() : base()
    {
        On<iOS>().SetUseSafeArea( true );
    }
}
