using _Microsoft.Android.Resource.Designer;
using Android.Content.PM;
using Android.Graphics;
using Android.Views;

namespace ScreenLight.App.Activities;

[Activity(
    Label = "@string/app_name",
    MainLauncher = true,
    ScreenOrientation = ScreenOrientation.Portrait
)]
public class MainActivity : Activity
{
    private ImageView _toggleImageView = null!;
    private SeekBar _brightnessSeekBar = null!;
    private TextView _brightnessLevelTextView = null!;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        SetContentView(ResourceConstant.Layout.activity_main);

        HideNavigation();
        SetScreenBrightness(1f);
        ToggleWhiteScreen(true);

        _brightnessLevelTextView = FindViewById<TextView>(ResourceConstant.Id.brightnessLevelTextView)!;

        _toggleImageView = FindViewById<ImageView>(ResourceConstant.Id.toggleButton)!;
        var enabled = false;

        _toggleImageView.Click += (_, _) =>
        {
            ToggleWhiteScreen(enabled);
            enabled = !enabled;
        };

        _brightnessSeekBar = FindViewById<SeekBar>(ResourceConstant.Id.brightnessSeekBar)!;

        _brightnessSeekBar.StartTrackingTouch += (_, _) => ShowCurrentBrightnessLevel(true);
        _brightnessSeekBar.StopTrackingTouch += (_, _) => ShowCurrentBrightnessLevel(false);

        _brightnessSeekBar.ProgressChanged += (_, progressChangedArgs) =>
        {
            var percent = progressChangedArgs.Progress / 100f;
            SetScreenBrightness(percent);

            _brightnessLevelTextView.Text = progressChangedArgs.Progress.ToString();
        };

        ShowCurrentBrightnessLevel(false);
    }

    private void ShowCurrentBrightnessLevel(bool show)
    {
        _toggleImageView.Visibility = show ? ViewStates.Gone : ViewStates.Visible;
        _brightnessLevelTextView.Visibility = show ? ViewStates.Visible : ViewStates.Gone;
    }

    private void ToggleWhiteScreen(bool enable)
    {
        var currentColor = enable ? Color.White : Color.Black;

        Window?.DecorView.SetBackgroundColor(currentColor);
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
