using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;

namespace Overlay
{
	public class RectangleArea : Area
	{
		public RectangleArea(Rectangle r)
		{
			Points = new Point[2] {new Point(r.Left, r.Top), 
															new Point(r.Right, r.Bottom)};
			Recalc(false);
		}

		public override bool Contains(Point p) 
		{
			return GetBounds().Contains(p);
		}

		protected override void CalcTextLines(Graphics gr, Font font, float lineHeight) 
		{
			RectangleF r = GetZoomedBounds();

			// Break text to even lines
			int st = 0;
			lines = new ArrayList();
			while (st < Text.Length) 
				lines.Add(GetLine(gr, font, Text, ref st, r.Width, r.X, 0));

			// Determine line positions
			float sy = (r.Height - (lines.Count * lineHeight)) / 2 + r.Y;
			for (int i = 0; i < lines.Count; i++) 
				((TextLine)lines[i]).Location.Y = sy + i * lineHeight;
		}

		protected override void DoDraw(IntPtr hdc, Point view) 
		{
			RectangleF r = GetZoomedBounds();
			r.Offset(-view.X, -view.Y);

			// Shape
			if (Mode == AreaMode.Text) 
				WinApi.Rectangle(hdc, r, DesignColor, DesignFillColor, ShapeDrawing.Both);
			if (Mode == AreaMode.View) 
				WinApi.Rectangle(hdc, r, FillColor, ShapeDrawing.Fill);
			if (Mode == AreaMode.Shapes)
				WinApi.Rectangle(hdc, r, DesignColor, ShapeDrawing.Outline);
			if ((Mode != AreaMode.View) && Selected) 
				WinApi.Rectangle(hdc, new RectangleF(r.X + 1, r.Y + 1, 
					r.Width - 2, r.Height - 2), DesignColor, ShapeDrawing.Outline);
		}

	}
}
