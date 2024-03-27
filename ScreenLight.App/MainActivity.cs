using _Microsoft.Android.Resource.Designer;
using Android.Content.PM;
using Android.Views;

namespace ScreenLight.App;

[Activity(
    Label = "@string/app_name",
    MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait
)]
public class MainActivity : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        HideNavigation();
        SetScreenBrightness(1f);

        SetContentView(ResourceConstant.Layout.activity_main);
    }

    private void SetScreenBrightness(float brightness)
    {
        if (Window is null)
            return;

        var windowAttributes = new WindowManagerLayoutParams();

        windowAttributes.CopyFrom(Window.Attributes);
        windowAttributes.ScreenBrightness = brightness;

        Window.Attributes = windowAttributes;
    }

    private void HideNavigation()
    {
        if (Window?.DecorView is null)
            return;

        const SystemUiFlags uiFlags = SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen;

#pragma warning disable CA1422 // Validate platform compatibility
        Window.DecorView.SystemUiFlags = uiFlags;
#pragma warning restore CA1422
    }
}
