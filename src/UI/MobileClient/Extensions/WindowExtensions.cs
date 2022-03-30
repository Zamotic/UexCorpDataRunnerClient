using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace UexCorpDataRunner.DesktopClient.Extensions
{
    public static class WindowExtensions
    {
#pragma warning disable CS8603 // Possible null reference return.
        public static Location GetWindowLocation(this IWindow window)
        {
            var location = new Location(0, 0);

            if (window is null)
                return location;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return location;

            location.XLocation = appWindow.Position.X;
            location.YLocation = appWindow.Position.Y;
#endif
            return location;
        }

        public static Size GetWindowSize(this IWindow window)
        {
            var size = new Size(300, 750);

            if (window is null)
                return size;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return size;

            size.Width = appWindow.Size.Width;
            size.Height = appWindow.Size.Height;
#endif
            return size;
        }

        public static IWindow Resize(this IWindow window, int width, int height)
        {
            if (window is null)
                return window;
#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return window;

            appWindow.Resize(new Windows.Graphics.SizeInt32(width, height));
#endif
            return window;
        }

        public static IWindow Move(this IWindow window, int xLocation, int yLocation)
        {
            if (window is null)
                return window;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return window;

            appWindow.Move(new Windows.Graphics.PointInt32(xLocation, yLocation));
#endif
            return window;
        }

        public static IWindow MoveAndResize(this IWindow window, int xLocation, int yLocation, int width, int height)
        {
            if (window is null)
                return window;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return window;

            appWindow.MoveAndResize(new Windows.Graphics.RectInt32(xLocation, yLocation, width, height));
#endif
            return window;
        }

        public static IWindow HideWindowButtons(this IWindow window)
        {
            if (window is null)
                return window;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return window;

            var presenter = appWindow.Presenter as OverlappedPresenter;

            if (presenter is not null) 
            {
                presenter.SetBorderAndTitleBar(false, false);
                presenter.IsAlwaysOnTop = true;
                presenter.IsMaximizable = false;
                presenter.IsMinimizable = false;
                presenter.IsResizable = false;
            }

            //appWindow.SetPresenter(AppWindowPresenterKind.CompactOverlay);
            //appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
#endif
            return window;
        }

        public static IWindow ShowWindowButtons(this IWindow window)
        {
            if (window is null)
                return window;

#if WINDOWS
            var appWindow = GetAppWindowFromIWindowElement(window);
            if (appWindow is null)
                return window;

            var presenter = appWindow.Presenter as OverlappedPresenter;

            if (presenter is not null) 
            {
                presenter.SetBorderAndTitleBar(false, false);
                presenter.IsAlwaysOnTop = true;
                presenter.IsMaximizable = true;
                presenter.IsMinimizable = true;
                presenter.IsResizable = false;
            }

            //appWindow.SetPresenter(AppWindowPresenterKind.Overlapped);
            //appWindow.TitleBar.ExtendsContentIntoTitleBar = false;
#endif
            return window;
        }

#if WINDOWS
        private static AppWindow? GetAppWindowFromIWindowElement(IWindow windowElement)
        {
            var hWnd = GetWindowPointerFromIWindowElement(windowElement);

            if (hWnd is null) {
                return null;
            }

            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd.Value);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
            return appWindow;
        }

        private static IntPtr? GetWindowPointerFromIWindowElement(IWindow windowElement)
        {
            var platformView = windowElement?.Handler?.PlatformView;

            if (platformView is null)
                return null;

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(platformView);

            return hWnd;
        }
#endif

        //            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) => {
        //#if WINDOWS
        //                var nativeWindow = handler.PlatformView;
        //                nativeWindow.Activate();

        //                IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
        //                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        //                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        //                var displayBounds = Microsoft.UI.Windowing.DisplayArea.Primary.OuterBounds;
        //                int displayWidth = displayBounds.Width;
        //                int displayHeight = displayBounds.Height;

        //                int windowY = (displayHeight / 2) - 350;
        //                int windowX = (displayWidth / 2) - 175;

        //                appWindow.MoveAndResize(new Windows.Graphics.RectInt32(windowX, windowY, 350, 700));
        //#endif
        //            });
#pragma warning restore CS8603 // Possible null reference return.
    }
}
