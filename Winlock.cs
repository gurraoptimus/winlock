using System.Collections.Generic;
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
        this.InitializeComponent().
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId = WndID = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appW = AppWindow.GetFromWindowId(WndID);
        OverlappedPresenter presenter = appW.Presenter as OverlappedPresenter;
        presenter.IsAlwaysOnTop = true;
        appW.SetPresenter(AppWindowPresenterKind.FullScreen);
        this.closed += MainWindow_Closed;
        }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "ButtonPassword")
            {
                passwordBox.Visibility = Visibility.Visible;
                ButtonQRCode.Visibility = Visibility.Collapsed;
                ButtonPassword.Visibility = Visibility.Collapsed;
                OkButton.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                return;
            }
            else if (((Button)sender).Name == "ButtonQRCode")
            {
                ButtonQRCode.Visibility = Visibility.Collapsed;
                OkButton.Visibility = Visibility.Visible;
                CaptureElementPanel.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
                ButtonPassword.Visibility = Visibility.Collapsed;
                startCaptureElement();
                return;
            }

            if (passwordBox.Password != "password")
            {
                statusText.Text = "the password is incorrect. Try again.";
        }
        else
        {
            WindowHandled = false;
            this.Close();
        }
        private MediaFrameSourceGroup mediaFrameSourceGroup;
        private MediaCapture mediaCapture;
        private bool isScanning = false;

        private async void startCaptureElement()
        {
            var groups= await MediaFrameSourceGroup.FindAllAsync();
            if (groups.Count == 0)
            {
                statu
            }
        }
        
        }
    }    
}