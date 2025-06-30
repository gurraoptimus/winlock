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
        this.InitializeComponent():
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId = WndID = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appW = AppWindow.GetFromWindowId(WndID);
        OverlappedPresenter presenter = appW.Presenter as OverlappedPresenter;
        presenter.IsAlwaysOnTop = true;
        appW.SetPresenter(AppWindowPresenterKind.FullScreen);
        this.Closed += MainWindow_Closed;
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
                statusText.text = "No camera found.";
                return;
            }
        }
        
        mediaFrameSourceGroup = groups.First();

        mediaCapture = new MediaCapture();
        var mediaCaptureInitializationSettings = new MediaCaptureInitializationSettings
        {
            SourceGroup = mediaFrameSourceGroup,
            SharingMode = MediaCaptureSharingMode.SharedReadOnly,
            StreamingCaptureMode = StreamingCaptureMode.Video
            MemoryPreference = MediaMemoryPreference.Auto       
        };
        await mediaCapture.InitializeAsync(mediaCaptureInitializationSettings);
        var frameSource = mediaCapture.FrameSources[this.mediaFrameSourceGroup.SourceInfos[0].Id];
        captureElement.Source = Windows.Media.Core.MediaSource.CreateFromMidiaFrameSource(frameSource);

        StartScanning();
    }
    private async void StartScanning()
    {
        isScanning = true;
        statusText.Text = "Scanning...";

            while (isScanning)
            {
                var imgFormat = ImageEncodingProperties.CreateJpeg();
                var stream = new InMemoryRandomAccessStream();
                await mediaCapture.CapturePhotoToStreamAsync(imgFormat, stream);
                stream.Seek(0);
                BitmapImage bmpImage = new BitmapImage();
                wait bmpImage.SetSourceAsync(stream);


                try
        }
            byte[] byteArray;
                using (DataReader dataReader = new DataReader(stream.GetInputStreamAt(0)))
                {
                    byteArray = new byte[stream.Size];
                    await dataReader.LoadAsync((uint)stream.Size);
                    dataReader.ReadBytes(byteArray);
                }
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    Bitmap bitmap = new Bitmap(memoryStream);
                    BarcodeReader reader = new BarcodeReader { AutoRotate = true, Options = { TryInverted = true } };

                    Result result = reader.Decode(bitmap);

                    if (result != null)
                    {
                        string Decoded = result.ToString().Trim();
                        if (Decoded == "password")
                        { stopScanningButton_click(null, null)
                            windowHandled = false;

                            this.Close();

                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                console.WriteLine($"Error occurred: {ex.Message}");
            }
            await Task.Delay(2000); // Delay to avoid high CPU usage
        }
    }
private void 