using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Overlay
{

	[StructLayout(LayoutKind.Sequential)]
	public class RECT 
	{
		public Int32 left; 
		public Int32 top; 
		public Int32 right; 
		public Int32 bottom; 

		public RECT() 
		{
		}

		public RECT(System.Drawing.RectangleF r) 
		{
			left = (int)Math.Floor(r.Left);
			right = (int)Math.Floor(r.Right);
			top = (int)Math.Ceiling(r.Top);
			bottom = (int)Math.Ceiling(r.Bottom);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public class SIZE 
	{
		public Int32 cx;
		public Int32 cy;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public Int32 x;
		public Int32 y;

		public POINT(PointF p) 
		{
			x = (int)Math.Round(p.X);
			y = (int)Math.Round(p.Y);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public class WINDOWINFO
	{
		public Int32 cbSize;
		public RECT  rcWindow;
		public RECT  rcClient;
		public Int32 dwStyle;
		public Int32 dwExStyle;
		public Int32 dwWindowStatus;
		public Int32 cxWindowBorders;
		public Int32 cyWindowBorders;
		public Int32 atomWindowType;
		public Int16 wCreatorVersion;
	}


	public enum ShapeDrawing { Outline, Fill, Both }

	public class WinApi
	{
		public const int DT_TOP = 0;
		public const int DT_LEFT = 0;
		public const int DT_CENTER = 1;
		public const int DT_RIGHT = 2;
		public const int DT_VCENTER = 4;
		public const int DT_BOTTOM = 8;
		public const int DT_WORDBREAK = 0x10;
		public const int DT_SINGLELINE = 0x20;
		public const int DT_EXPANDTABS = 0x40;
		public const int DT_TABSTOP = 0x80;
		public const int DT_NOCLIP = 0x100;
		public const int DT_EXTERNALLEADING = 0x200;
		public const int DT_CALCRECT = 0x400;
		public const int DT_NOPREFIX = 0x800;
		public const int DT_INTERNAL = 0x1000;
		public const int DT_HIDEPREFIX = 0x00100000;
		public const int DT_PREFIXONLY = 0x00200000;
		public const int DT_EDITCONTROL = 0x2000;
		public const int DT_PATH_ELLIPSIS = 0x4000;
		public const int DT_END_ELLIPSIS = 0x8000;
		public const int DT_MODIFYSTRING = 0x10000;
		public const int DT_RTLREADING = 0x20000;
		public const int DT_WORD_ELLIPSIS = 0x40000;

		public const int SRCCOPY = 0xCC0020;

		public const int VK_SHIFT = 0x10;

		public abstract class GdiObject : IDisposable
		{
			private IntPtr hdc;
			protected IntPtr hgdiobj = IntPtr.Zero;
			private IntPtr hgdiobj_old;

			public GdiObject(IntPtr hdc, IntPtr hgdiobj) 
			{
				this.hdc = hdc;
				this.hgdiobj = hgdiobj;
				hgdiobj_old = WinApi.SelectObject(hdc, hgdiobj);
			}

			~GdiObject() 
			{
				Dispose();
			}
			
			public void Dispose()
			{
				if (hgdiobj == IntPtr.Zero) return;
				WinApi.SelectObject(hdc, hgdiobj_old);
				WinApi.DeleteObject(hgdiobj);
				hgdiobj = IntPtr.Zero;
			}

			public IntPtr Handle 
			{
				get { return hgdiobj; }
			}
		}

    public class GdiPen : GdiObject
		{
			public GdiPen(IntPtr hdc, int width, Color color) : 
				base(hdc, WinApi.CreatePen(0, width, WinApi.RGBValue(color))) {}
		}

		public class GdiBrush : GdiObject
		{
			public GdiBrush(IntPtr hdc, Color color) :
				base(hdc, WinApi.CreateSolidBrush(WinApi.RGBValue(color))) {}
		}

		public class GdiDC : IDisposable 
		{
			IntPtr hwnd;
			IntPtr hdc = IntPtr.Zero;

			public GdiDC(IntPtr hwnd) 
			{
				this.hwnd = hwnd;
				hdc = GetDC(hwnd);
			}

			public IntPtr Handle 
			{
				get { return hdc; }
			}

			~GdiDC() 
			{
				Dispose();
			}

			public void Dispose() 
			{
				if (hdc == IntPtr.Zero) return;
				ReleaseDC(hwnd, hdc);
				hdc = IntPtr.Zero;
			}
		}

		public class GdiBitmap : IDisposable 
		{
			int width, height;
			IntPtr hbitmap = IntPtr.Zero;
			IntPtr hdc;
			IntPtr hbm_old;

			private GdiBitmap(IntPtr proto_dc, IntPtr hbitmap, int width, 
				int height) 
			{
				this.hbitmap = hbitmap;
				this.width = width;
				this.height = height;
				hdc = CreateCompatibleDC(proto_dc);
				hbm_old = SelectObject(hdc, hbitmap);
			}

			public static GdiBitmap Create(IntPtr proto_hwnd, int width, int height) 
			{
				IntPtr proto_dc = GetDC(proto_hwnd);
				IntPtr hbm = CreateCompatibleBitmap(proto_dc, width, height);
				GdiBitmap result = null;
				if (hbm != IntPtr.Zero) 
					result = new GdiBitmap(proto_dc, hbm, width, height);
				ReleaseDC(proto_hwnd, proto_dc);
				return result;
			}

			public static GdiBitmap FromBitmap(IntPtr proto_hwnd, Bitmap bmp)
			{
				IntPtr proto_dc = GetDC(proto_hwnd);
				IntPtr hbm = bmp.GetHbitmap();
				GdiBitmap result = null;
				if (hbm != IntPtr.Zero)
					result = new GdiBitmap(proto_dc, hbm, bmp.Width, bmp.Height);
				ReleaseDC(proto_hwnd, proto_dc);
				return result;
			}

			~GdiBitmap() 
			{
				Dispose();
			}

			public void Dispose() 
			{
				if (hbitmap == IntPtr.Zero) return;
				SelectObject(hdc, hbm_old);
				DeleteObject(hbitmap);
				DeleteDC(hdc);
			}

			public IntPtr Hdc 
			{
				get { return hdc; }
			}

			public IntPtr Hbitmap
			{
				get { return hbitmap; }
			}

			public int Width 
			{
				get { return width; }
			}

			public int Height 
			{
				get { return height; }
			}

			public void DrawToDC(IntPtr hdc, int dest_x, int dest_y, 
				int dest_wid, int dest_hei, int src_x, int src_y) 
			{
				BitBlt(hdc, dest_x, dest_y, dest_wid, dest_hei, this.hdc, 
					src_x, src_y, SRCCOPY);
			}

			public void DrawToDC(IntPtr hdc, int dest_x, int dest_y, 
				int dest_wid, int dest_hei, int src_x, int src_y, int src_wid,
				int src_hei) 
			{
				StretchBlt(hdc, dest_x, dest_y, dest_wid, dest_hei, this.hdc, 
					src_x, src_y, src_wid, src_hei, SRCCOPY);
			}
		}

		/*
		[System.Runtime.InteropServices.DllImport("User32")]
		public static extern bool ShowWindow(IntPtr hwnd, int cmd);

		[System.Runtime.InteropServices.DllImport("User32")]
		public static extern bool UpdateWindow(IntPtr hwnd);

		[System.Runtime.InteropServices.DllImport("User32")]
		public static extern bool BringWindowToTop(IntPtr hwnd);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern IntPtr CreateDC(string driver, string device,
		string output, int initdata);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern int GetDeviceCaps(IntPtr hdc, int index);

		[System.Runtime.InteropServices.DllImport("User32")]
		public static extern bool GetWindowInfo(IntPtr hwnd, 
		[MarshalAs(UnmanagedType.LPStruct)] WINDOWINFO pwi);

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern int DrawText(IntPtr hdc,	string lpString,
			int nCount, RECT lpRect, int uFormat);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern bool TextOut(IntPtr hdc, int nXStart,
			int nYStart, string lpString,	int length);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern int SetTextColor(IntPtr hdc, int crColor);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern IntPtr CreateFont(int nHeight, int nWidth,
			int nEscapement, int nOrientation, int fnWeight, int fdwItalic,
			int fdwUnderline,	int fdwStrikeOut,	int fdwCharSet,
			int fdwOutputPrecision, int fdwClipPrecision,	int fdwQuality,
			int fdwPitchAndFamily, string lpszFace);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		public static extern bool SystemParametersInfo(
			int uiAction, bool uiParam, int pvParam, int fWinIni);

		*/

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern IntPtr GetDC(IntPtr hwnd);

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc,
		int width, int height);
			
		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool DeleteDC(IntPtr hdc);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool DeleteObject(IntPtr hgdiobj);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest,  
			int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest,  
			int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, 
			int nSrcWidth, int nSrcHeight, int dwRop);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool MaskBlt(IntPtr hdcDest, int nXDest, int nYDest,  
			int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, 
			IntPtr hbmMask, int xMask, int yMask, int dwRop);

		// Shapes
		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool Rectangle(IntPtr hdc, int nLeftRect,
			int nTopRect,	int nRightRect,	int nBottomRect);

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern int FrameRect(IntPtr hdc, RECT lprc,
			IntPtr hbr);

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern int FillRect(IntPtr hdc, RECT lprc,
			IntPtr hbr);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool Ellipse(IntPtr hdc,
			int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr CreateSolidBrush(int color);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool GetTextExtentPoint32(IntPtr hdc,
			string lpString, int cbString, SIZE lpSize);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool Arc(IntPtr hdc, int nLeftRect,
			int nTopRect, int nRightRect, int nBottomRect,
			int nXStartArc,	int nYStartArc,	int nXEndArc,
			int nYEndArc);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool Polyline(IntPtr hdc, POINT[] lppt,
			int cPoints);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool Polygon(IntPtr hdc, POINT[] lppt,
			int cPoints);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr CreatePen(int fnPenStyle,	int nWidth,
			int crColor);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern bool PtInRegion(IntPtr hrgn, int X, int Y);

		[System.Runtime.InteropServices.DllImport("Gdi32")]
		private static extern IntPtr CreatePolygonRgn(POINT[] lppt,
			int cPoints, int fnPolyFillMode);

		[System.Runtime.InteropServices.DllImport("User32")]
		public static extern byte GetKeyState(int nVirtKey);

		[System.Runtime.InteropServices.DllImport("Kernel32")]
		public static extern int GetLastError();

		public WinApi()
		{
		}

		public static int RGBValue(System.Drawing.Color color) 
		{
			return color.R + color.G * 0x100 + color.B * 0x10000;
		}

		public static Size GetTextExtents(IntPtr hdc, string s) 
		{
			SIZE size = new SIZE();
			WinApi.GetTextExtentPoint32(hdc, s, s.Length, size);
			return new Size(size.cx, size.cy);
		}

		public static void Rectangle(IntPtr hdc, RectangleF rect, Color color,
			ShapeDrawing mode) 
		{
			Rectangle(hdc, rect, color, color, mode);
		}

		public static void Rectangle(IntPtr hdc, RectangleF rect, Color line, 
			Color fill, ShapeDrawing mode)
		{
			RECT pr = new RECT(rect);
			if (mode != ShapeDrawing.Outline) 
			{
				GdiBrush brush = new GdiBrush(hdc, fill);
				FillRect(hdc, pr, brush.Handle);
				brush.Dispose();
			}
			if (mode != ShapeDrawing.Fill) 
			{
				GdiBrush brush = new GdiBrush(hdc, line);
				FrameRect(hdc, pr, brush.Handle);
				brush.Dispose();
			}
		}

		public static void Ellipse(IntPtr hdc, RectangleF rect, Color color, 
			int penWidth, ShapeDrawing mode) 
		{
			Ellipse(hdc, rect, color, color, penWidth, mode);
		}
		
		public static void Ellipse(IntPtr hdc, RectangleF rect, Color line, 
			Color fill, int penWidth, ShapeDrawing mode) 
		{
			GdiPen pen = new GdiPen(hdc, penWidth, line);
			if (mode != ShapeDrawing.Outline) 
			{
				GdiBrush brush = new GdiBrush(hdc, fill);
				Ellipse(hdc, (int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
				brush.Dispose();
			}
			if (mode == ShapeDrawing.Outline) 
				Arc(hdc, (int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom,
					(int)rect.Left, (int)rect.Top, (int)rect.Left, (int)rect.Top);
			pen.Dispose();
		}

		public static void Polygon(IntPtr hdc, PointF[] points, Color color,
			int penWidth, ShapeDrawing mode) 
		{
			Polygon(hdc, points, color, color, penWidth, mode);
		}

		public static void Polygon(IntPtr hdc, PointF[] points, Color line,
			Color fill, int penWidth, ShapeDrawing mode) 
		{
			POINT[] pts = new POINT[points.Length];
			for (int i = 0; i < pts.Length; i++)
				pts[i] = new POINT(points[i]);
			GdiPen pen = new GdiPen(hdc, penWidth, line);
			if (mode != ShapeDrawing.Outline) 
			{
				GdiBrush brush = new GdiBrush(hdc, fill);
				Polygon(hdc, pts, pts.Length);
				brush.Dispose();
			}
			else 
				Polyline(hdc, pts, pts.Length);
			pen.Dispose();
		}

		public static bool PtInRegion(PointF[] pts, Point p) 
		{
			POINT[] pts1 = new POINT[pts.Length];
			for (int i = 0; i < pts.Length; i++)
				pts1[i] = new POINT(pts[i]);
			IntPtr hreg = CreatePolygonRgn(pts1, pts1.Length, 1);
			bool res = PtInRegion(hreg, p.X, p.Y);
			DeleteObject(hreg);
			return res;
		}

	}
}
