using System;

namespace LockScreen
{
    /// <summary>
    /// An empty window that can be used to its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
{
    private bool WindowHandled = false;
    public MainWindow()
    {
        this.InitializeComponent()
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId = WndID = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appW = AppWindow.GetFromWindowId(WndID);
        OverlappedPresenter presenter = appW.Presenter as OverlappedPresenter;
        presenter.IsAlwaysOnTop = true;
        appW.SetPresenter(AppWindowPresenterKind.FullScreen);
        this.closed += MainWindow_Closed;
        }
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).name == "ButtonPassword")
            {
                passwordBox.Visibility = Visibility.Visible;
                ButtonQRCode.Visibility = Visibility.Collapsed;
                ButtonPassword.Visibility = Visibility.Collapsed;
                ButtonOK.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                return;
            }
            else if (((Button)sender).name == "ButtonQRCode")
            {
                ButtonQRCode.Visibility = Visibility.Collapsed;
                ButtonOK.Visibility = Visibility.Visible;
                CaptureElementPanel.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                ButtonPassword.Visibility = Visibility.Collapsed;
                startCaptureElement();
                return;
            }

            if (passwordBox.Pasword != "password")
            {
                statusText.Text = "the passwowrd is incorrect. Try again.";
            }
        }
    }
}