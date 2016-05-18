using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Windows.Forms;

namespace Overlay
{
	public class Display
	{
		private static Image original;
		private static AreaList areas;
		private static int zoom = 100;
		private static Point view = new Point(0, 0);
		private static Rectangle bounds = new Rectangle();
		private static Point panStart = new Point();

		// Handle to window where painting will occur
		public static IntPtr Handle;
		// Buffer for zoomed picture without areas
		private static WinApi.GdiBitmap hbmClear = null;
		// Buffer for picture with areas, ready for screen
		private static WinApi.GdiBitmap hbmScreen = null;

		private class BufferException : Exception {}
			
		public static void SetScan(Image image, AreaList Areas)
		{
			view = new Point(0, 0);
			original = image;
			areas = Areas;
			areas.RecalcAll();
			try 
			{
				UpdateBuffers();
			}
			catch (BufferException) 
			{
				MessageBox.Show("Picture too large to show, trying to zoom out",
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ZoomActual();
			}
		}

		public static int Zoom 
		{
			get {	return zoom; }
		}

		public static Point View 
		{
			get { return view; }
		}

		public static Rectangle Bounds
		{
			get { return bounds; }
			set 
			{
				bounds = value;
				AdjustView();
			}
		}

		public static void SetPanStart(int x, int y) 
		{
			panStart = new Point(x, y);
		}

		public static void PanTo(int x, int y) 
		{
			int x1 = (panStart.X - x);
			int y1 = (panStart.Y - y);
			view.X = Math.Min(Math.Max(0, view.X + x1), Math.Max(0, 
				(original.Width * zoom / 100) - bounds.Width));
			view.Y = Math.Min(Math.Max(0, view.Y + y1), Math.Max(0, 
				(original.Height * zoom / 100) - bounds.Height));
			if (view.X != x1) panStart.X = x;
			if (view.Y != y1) panStart.Y = y;
		}

		private static void SetZoom(int value) 
		{
			view = new Point(view.X * value / zoom, view.Y * value / zoom);
			view.Offset((bounds.Width * value - bounds.Width * zoom) / (value * 2),
				(bounds.Height * value - bounds.Height * zoom) / (value * 2));
			zoom = value;
			areas.RecalcAll();
			AdjustView();
			UpdateBuffers();
		}

		public static void ZoomToFit() 
		{
			if (original == null) return;
			SetZoom(Math.Min(100, Math.Min(bounds.Width * 100 / original.Width, 
				bounds.Height * 100 / original.Height)));
		}

		public static void ZoomIn() 
		{
			if (zoom < 20) SetZoom(Math.Min(zoom + 5, 200));
			else if (zoom < 100) SetZoom(Math.Min(zoom + 20, 200));
			else SetZoom(Math.Min(zoom + 50, 200));
		}

		public static void ZoomOut() 
		{
			if (zoom <= 20) SetZoom(Math.Max(zoom - 5, 10));
			else if (zoom <= 100) SetZoom(Math.Max(zoom - 20, 10));
			else SetZoom(Math.Max(zoom - 50, 10));
		}

		public static void ZoomActual() 
		{
			SetZoom(100);
		}

		// Ensure that viewpoint is not outside picture
		private static void AdjustView() 
		{
			if (original == null) return;
			view.X = Math.Min(Math.Max(0, view.X), Math.Max(0, 
				(original.Width * zoom / 100) - bounds.Width));
			view.Y = Math.Min(Math.Max(0, view.Y), Math.Max(0, 
				(original.Height * zoom / 100) - bounds.Height));
		}

		// Get size of zoomed picture
		private static Size GetZoomedSize(bool limited) 
		{
			if (limited && (zoom > 100))
				return original.Size;
			else
				return new Size((int)(original.Width * zoom / 100),
					(int)(original.Height * zoom / 100));
		}

		private static void UpdateBuffers() 
		{
			// Free old buffers
			if (hbmClear != null) 
			{
				hbmClear.Dispose();
				hbmClear = null;
			}
			if (hbmScreen != null) 
			{
				hbmScreen.Dispose();
				hbmScreen = null;
			}
			
			if (original == null) return;

			// Create clear buffer for zoomed picture (up to 100% zoom)
			Size size = GetZoomedSize(true);
			Bitmap imgZoom = new Bitmap(size.Width, size.Height);
			Graphics grZoom = Graphics.FromImage(imgZoom);
			grZoom.InterpolationMode = InterpolationMode.High;
			grZoom.DrawImage(original, new Rectangle(0, 0, imgZoom.Width, imgZoom.Height), 
				new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
			hbmClear = WinApi.GdiBitmap.FromBitmap(Handle, imgZoom);

			// Create screen buffer
			size = GetZoomedSize(false);
			hbmScreen = WinApi.GdiBitmap.Create(Handle, size.Width, size.Height);
			if (hbmScreen == null) throw new BufferException();
			UpdateScreenBuffer();
		}

		public static void UpdateScreenBuffer()
		{
			if (original == null) return;
			Size size = GetZoomedSize(false);
			UpdateScreenBuffer(new Rectangle(0, 0, size.Width, size.Height), false);
		}

		public static void UpdateScreenBuffer(Rectangle clip) 
		{
			UpdateScreenBuffer(clip, true);
		}

		private static void UpdateScreenBuffer(Rectangle clip, bool buffered)
		{
			if (original == null) return;

			WinApi.GdiBitmap bmp = null;
			if (buffered) 
				bmp = WinApi.GdiBitmap.Create(Handle, clip.Width, clip.Height);
			
			// Copy unchanged picture to buffer
			if (zoom < 100) 
			{
				if (!buffered)
					hbmClear.DrawToDC(hbmScreen.Hdc, clip.X, clip.Y, clip.Width, 
						clip.Height, clip.X, clip.Y);
				else
					hbmClear.DrawToDC(bmp.Hdc, 0, 0, clip.Width, clip.Height, 
						clip.X, clip.Y);
			}
			else 
			{
				// Normalize rectangle for large-pixel modes 150, 200 (maybe 300...)
				if (zoom == 150) 
					clip = Rectangle.FromLTRB(clip.Left - clip.Left % 3, 
						clip.Top - clip.Top % 3, clip.Right + 3 - clip.Right % 3,
						clip.Bottom + 3 - clip.Bottom % 3);
				else if (zoom > 100)
					clip = Rectangle.FromLTRB(clip.Left - clip.Left % 2, 
						clip.Top - clip.Top % 2, clip.Right + 2 - clip.Right % 2,
						clip.Bottom + 2 - clip.Bottom % 2);

				Rectangle src = new Rectangle(clip.X * 100 / zoom, 
					clip.Y * 100 / zoom, clip.Width * 100 / zoom, 
					clip.Height * 100 / zoom);

				if (!buffered)
					hbmClear.DrawToDC(hbmScreen.Hdc, clip.X, clip.Y, clip.Width, 
						clip.Height, src.X, src.Y, src.Width, src.Height);
				else
					hbmClear.DrawToDC(bmp.Hdc, 0, 0, clip.Width, 
						clip.Height, src.X, src.Y, src.Width, src.Height);
			}

			// Draw areas
			if (!buffered) 
				areas.DrawToDC(hbmScreen.Hdc, clip, new Point(0, 0));
			else 
			{
				areas.DrawToDC(bmp.Hdc, new Rectangle(0, 0, clip.Width, clip.Height), 
					new Point(clip.X, clip.Y));
				bmp.DrawToDC(hbmScreen.Hdc, clip.X, clip.Y, clip.Width, clip.Height, 0, 0);
				bmp.Dispose();
			}
		}

		public static void DrawToDC() 
		{
			WinApi.GdiDC dc = new WinApi.GdiDC(Handle);
			DrawToDC(dc.Handle, bounds);
			dc.Dispose();
		}

		public static void DrawToDC(IntPtr hdc, Rectangle dest) 
		{
			// dest is drawing rectangle within bounds
      if (original == null) return;

			// Cut dest to be inside drawn picture
			Size size = GetZoomedSize(false);
			int x =	Math.Max(0, bounds.Width - size.Width) / 2;
			int y = Math.Max(0, bounds.Height - size.Height) / 2;
			dest = Rectangle.FromLTRB(Math.Max(dest.X, x), Math.Max(dest.Y, y), 
				Math.Min(dest.Right, bounds.Right - x), 
				Math.Min(dest.Bottom, bounds.Bottom - y));
			if ((dest.Width <= 0) || (dest.Height <= 0)) return;

			// Draw from screen buffer
			hbmScreen.DrawToDC(hdc, dest.X, dest.Y, dest.Width, dest.Height, 
				dest.X - x + view.X, dest.Y - y + view.Y);
		}

		public static Point ClientToDisplay(int x, int y) 
		{
			if (original == null) return new Point();
			Size size = GetZoomedSize(false);
			int x1 =	Math.Max(0, bounds.Width - size.Width) / 2;
			int y1 = Math.Max(0, bounds.Height - size.Height) / 2;
			return new Point(x - x1 + view.X, y - y1 + view.Y);
		}

	}
}
