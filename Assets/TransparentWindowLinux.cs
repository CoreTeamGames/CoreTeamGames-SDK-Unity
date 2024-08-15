using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindowLinux : MonoBehaviour
{
    [DllImport("libX11")]
    private static extern IntPtr XOpenDisplay(IntPtr display);

    [DllImport("libX11")]
    private static extern int XCloseDisplay(IntPtr display);

    [DllImport("libX11")]
    private static extern IntPtr XRootWindow(IntPtr display, int screenNumber);

    [DllImport("libX11")]
    private static extern IntPtr XInternAtom(IntPtr display, string atomName, bool onlyIfExists);

    [DllImport("libX11")]
    private static extern int XChangeProperty(
        IntPtr display, 
        IntPtr window, 
        IntPtr property, 
        IntPtr type, 
        int format, 
        int mode, 
        IntPtr data, 
        int nelements);

    [DllImport("libX11")]
    private static extern IntPtr XDefaultRootWindow(IntPtr display);

    [DllImport("libX11")]
    private static extern int XMapWindow(IntPtr display, IntPtr window);

    [DllImport("libX11")]
    private static extern int XUnmapWindow(IntPtr display, IntPtr window);

    [DllImport("libX11")]
    private static extern int XFlush(IntPtr display);

    [DllImport("libX11")]
    private static extern IntPtr XGetWindowAttributes(IntPtr display, IntPtr window, out XWindowAttributes attributes);

    [DllImport("libX11")]
    private static extern int XQueryTree(
        IntPtr display, 
        IntPtr window, 
        out IntPtr root, 
        out IntPtr parent, 
        out IntPtr children, 
        out uint nchildren);

    [StructLayout(LayoutKind.Sequential)]
    private struct XWindowAttributes
    {
        public int x, y;
        public int width, height;
        public int border_width;
        public int depth;
        public IntPtr visual;
        public IntPtr root;
        public int classType;
        public int bitGravity;
        public int winGravity;
        public int backingStore;
        public uint backing_planes;
        public uint backing_pixel;
        public bool saveUnder;
        public IntPtr colormap;
        public bool mapInstalled;
        public int mapState;
        public long allEventMasks;
        public long yourEventMask;
        public long doNotPropagateMask;
        public bool overrideRedirect;
        public IntPtr screen;
    }

    private void Start()
    {
        SetWindowTransparent();
    }

    private void SetWindowTransparent()
    {
        IntPtr display = XOpenDisplay(IntPtr.Zero);
        IntPtr rootWindow = XDefaultRootWindow(display);
        IntPtr window = GetUnityWindow(display, rootWindow);

        if (window == IntPtr.Zero)
        {
            Debug.LogError("Failed to get Unity window.");
            return;
        }

        // Установить атрибуты прозрачности
        ulong opacity = 0x00000000;
        IntPtr opacityAtom = XInternAtom(display, "_NET_WM_WINDOW_OPACITY", false);
        IntPtr XA_CARDINAL = XInternAtom(display, "CARDINAL", true);
        IntPtr propModeReplace = new IntPtr(0); // PropModeReplace

        // Подготовка указателя на данные
        GCHandle handle = GCHandle.Alloc(opacity, GCHandleType.Pinned);
        IntPtr ptrOpacity = handle.AddrOfPinnedObject();

        XChangeProperty(display, window, opacityAtom, XA_CARDINAL, 32, propModeReplace.ToInt32(), ptrOpacity, 1);

        // Обновить окно
        XUnmapWindow(display, window);
        XMapWindow(display, window);
        XFlush(display);

        handle.Free();
        XCloseDisplay(display);
    }

    private IntPtr GetUnityWindow(IntPtr display, IntPtr rootWindow)
    {
        IntPtr window = IntPtr.Zero;
        XQueryTree(display, rootWindow, out _, out _, out IntPtr children, out uint nchildren);
        
        IntPtr[] childWindows = new IntPtr[nchildren];
        Marshal.Copy(children, childWindows, 0, (int)nchildren);

        foreach (IntPtr child in childWindows)
        {
            if (XGetWindowAttributes(display, child, out XWindowAttributes attributes) != IntPtr.Zero)
            {
                if (attributes.mapState == 2) // Проверка отображаемых окон
                {
                    window = child;
                    break;
                }
            }
        }

        return window;
    }
}
