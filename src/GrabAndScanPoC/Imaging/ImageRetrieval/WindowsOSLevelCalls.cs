using System.Runtime.InteropServices;

namespace GrabAndScanPoC.Imaging.ImageRetrieval;
public partial class ImageCaptureService
{
    private class WindowsOSLevelCalls
    {
        //Functions utillizing the user32.dll 
        //Documentation on user32.dll - http://www.pinvoke.net/index.aspx

        public const int SHOWNORMAL = 1;
        public const int SHOWMINIMIZED = 2;
        public const int SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        public static extern IntPtr GetOpenClipboardWindow();
    }
}

