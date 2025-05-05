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
        thisInitializeComponent();
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WindowId = WndID = Win32Interop.GetWindowIdFromWindow(hwnd);
        AppWindow appW = AppWindow.GetFromWindowId(WindID);
        OverlappedPresenter presenter = appW.Presenter as OverlappedPresenter;
        pr
        }
    
    }
}