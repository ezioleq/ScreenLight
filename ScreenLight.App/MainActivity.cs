using _Microsoft.Android.Resource.Designer;
using Android.Views;

namespace ScreenLight.App;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        HideNavigation();

        SetContentView(ResourceConstant.Layout.activity_main);
    }

    private void HideNavigation()
    {
        Window.DecorView.SystemUiFlags = SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen;
    }
}
