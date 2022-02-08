using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GenshinAutoE
{
	internal class Native
	{
		[DllImport("GDI32.DLL", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest, int nDestWidth, int nDestHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int nSrcWidth, int nSrcHeight, Native.CopyPixelOperation dwRop);

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetDC(IntPtr hWnd);

		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("gdi32.dll", ExactSpelling = true)]
		public static extern IntPtr BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		[DllImport("user32")]
		public static extern int mouse_event(Native.MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int X, int Y);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("User32.dll ")]
		public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childe, string strclass, string FrmText);

		[DllImport("user32.dll")]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("User32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
		public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hWnd, ref Native.RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		[DllImport("user32.dll")]
		internal static extern bool HideCaret(IntPtr controlHandle);

		public Native()
		{
		}

		[Flags]
		internal enum CopyPixelOperation
		{
			NoMirrorBitmap = -2147483648,
			Blackness = 66,
			NotSourceErase = 1114278,
			NotSourceCopy = 3342344,
			SourceErase = 4457256,
			DestinationInvert = 5570569,
			PatInvert = 5898313,
			SourceInvert = 6684742,
			SourceAnd = 8913094,
			MergePaint = 12255782,
			MergeCopy = 12583114,
			SourceCopy = 13369376,
			SourcePaint = 15597702,
			PatCopy = 15728673,
			PatPaint = 16452105,
			Whiteness = 16711778,
			CaptureBlt = 1073741824
		}

		[Flags]
		public enum MouseEventFlag : uint
		{
			Move = 1U,
			LeftDown = 2U,
			LeftUp = 4U,
			RightDown = 8U,
			RightUp = 16U,
			MiddleDown = 32U,
			MiddleUp = 64U,
			XDown = 128U,
			XUp = 256U,
			Wheel = 2048U,
			VirtualDesk = 16384U,
			Absolute = 32768U
		}

		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}
	}
}
