using Microsoft.UI;
using Microsoft.UI.Windowing;
using UexCorpDataRunner.DesktopClient.Views;

namespace UexCorpDataRunner.DesktopClient;

public partial class App : Application
{
    public App(ActiveInterfaceView activeInterfaceView)
    {
        InitializeComponent();

        MainPage = new NavigationPage(activeInterfaceView);

        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) => {
#if WINDOWS
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();

            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            var displayBounds = Microsoft.UI.Windowing.DisplayArea.Primary.OuterBounds;
            int displayWidth = displayBounds.Width;
            int displayHeight = displayBounds.Height;

            int windowY = (displayHeight / 2) - 350;
            int windowX = (displayWidth / 2) - 175;

            appWindow.MoveAndResize(new Windows.Graphics.RectInt32(windowX, windowY, 350, 700));
#endif
        });

    }

}
