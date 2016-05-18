using System;
using System.Drawing;
using System.Collections;

namespace Overlay
{
	public class EllipseArea : Area
	{
		public EllipseArea(Rectangle r)
		{
			Points = new Point[2] {new Point(r.Left, r.Top), 
															new Point(r.Right, r.Bottom)};
			Recalc(false);
		}

		private float getX(RectangleF r, float y) 
		{
			if (Math.Abs(y) > r.Height) return -1;
			float a = (float)r.Width / 2;
			float b = (float)r.Height / 2;
			float y1 = b - y;
			return (float)Math.Round(Math.Sqrt((1 - (y1*y1) / (b*b)) * (a*a)));
		}

		public override bool Contains(Point p) 
		{
			RectangleF r = GetZoomedBounds();
			float x = getX(r, (p.Y - r.Y));
			return ((x >= 0) && (x > Math.Abs(p.X - (r.X + r.Width / 2))));
		}

		private float countWidth(RectangleF r, float y) 
		{
			return getX(r, y) * 2;
		}

		private ArrayList placeLines(Graphics gr, Font font, int st_line, 
			RectangleF r, float lineHeight) 
		{
			ArrayList lines = new ArrayList();
			int st = 0;
			while (st < Text.Length) 
			{
				float y = (st_line + lines.Count) * lineHeight;
				float wid;
				if (y > (r.Height - lineHeight) / 2) 
					wid = countWidth(r, y + lineHeight);
				else wid = countWidth(r, y);
				lines.Add(GetLine(gr, font, Text, ref st, (int)wid, r.X + 
					(r.Width - wid) / 2, 0));
			}
			return lines;
		}

		protected override void CalcTextLines(Graphics gr, Font font, float lineHeight) 
		{
			RectangleF r = GetZoomedBounds();

			// Count amount of lines
			int amt = (int)Math.Round(r.Height / lineHeight);

			// Try to place text
			int best_pos = 0;
			float best_rate = -1;
			for (int i = 0; i < amt / 2; i++) 
			{
				lines = placeLines(gr, font, i, r, lineHeight);
				if (i + lines.Count <= amt) 
				{
					int rem_lines = amt - lines.Count - i;
					float rate = (float)(i+1) / (rem_lines + 1);
					if (rate > 1) rate = 1 / rate;
					if ((best_rate == -1) || (rate > best_rate))
					{
						best_rate = rate;
						best_pos = i;
					}
				}
			}

			// Rebuild lines for best pos
			lines = placeLines(gr, font, best_pos, r, lineHeight);

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
			int pen = 1;
			if (Selected) pen = selWidth;
			if (Mode == AreaMode.View)
				WinApi.Ellipse(hdc, r, FillColor, pen, ShapeDrawing.Fill);
			if (Mode == AreaMode.Text)
				WinApi.Ellipse(hdc, r, DesignColor, DesignFillColor, pen, 
					ShapeDrawing.Both);
			if (Mode == AreaMode.Shapes)
				WinApi.Ellipse(hdc, r, DesignColor, pen, ShapeDrawing.Outline);
		}

	}
}
