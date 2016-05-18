using System;
using System.Drawing;
using System.Collections;

namespace Overlay
{
	public class PolygonArea : Area
	{
		private enum Side { Left, Right, None };

		public bool Closed;

		public PolygonArea(Point[] points, bool closed)
		{
			Points = points;
			Closed = closed;
			Recalc(false);
		}

		public override bool Contains(Point p) 
		{
			if (!Closed) return false;
			if (pts.Length < 3) return false;
			return WinApi.PtInRegion(pts, p);
		}

		private object IntersectionX(float y, PointF[] points, int index) 
		{
			// Get two points of line
			PointF p1 = points[index];
			PointF p2 = points[0];
			if (index < points.Length-1) p2 = points[index+1];

			// Return if line cut not intersects this Y
			if (p1.Y == p2.Y) return null;
			if (Math.Max(p1.Y, p2.Y) < y) return null;
			if (Math.Min(p1.Y, p2.Y) > y) return null;

			// Calculate intersection
			if (p1.Y == y) return p1.X;
			if (p2.Y == y) return p2.X;

			if (p1.X == p2.X) 
				return (float)p1.X;
			else 
			{
				float k = (float)(p2.Y - p1.Y) / (float)(p2.X - p1.X);
				return (float)((y - p1.Y) / k) + p1.X;
			}
		}

		private Side FilledSide(PointF[] points, float x, float y, int skipLine) 
		{
			int iright = 0;
			int ileft = 0;
			for (int j = 0; j < points.Length; j++) 
			{
				if (j == skipLine) continue;
				object ix = IntersectionX(y, points, j);
				if (ix == null) continue;
				if ((float)ix > x) iright++;
				else if ((float)ix < x) ileft++;
			}

			if (iright % 2 == 1) return Side.Right;
			else if (ileft % 2 == 1) return Side.Left;
			else return Side.None;
		}

		private RectangleF chopByLine(RectangleF rect, int i) 
		{
			// Get two pts of line
			PointF p1 = pts[i];
			PointF p2 = pts[0];
			if (i < pts.Length-1) p2 = pts[i+1];

			// Get intersection with rectangle top and bottom lines
			object xt = IntersectionX(rect.Top, pts, i);
			object xb = IntersectionX(rect.Bottom, pts, i);

			Side side;

			if ((xt == null) && (xb == null)) 
			{
				if ((p1.Y >= rect.Top) && (p1.Y <= rect.Bottom)) 
				{
					xt = (float)p1.X;
					xb = (float)p2.X;
					side = FilledSide(pts, p1.X, p1.Y, i);
				}
				else return rect;
			}
			else if (xt == null) 
			{
				if (p1.Y < p2.Y) xt = (float)p1.X;
				else xt = (float)p2.X;
				side = FilledSide(pts, (float)xb, (float)rect.Bottom, i);
			}
			else if (xb == null) 
			{
				if (p1.Y > p2.Y) xb = (float)p1.X;
				else xb = (float)p2.X;
				side = FilledSide(pts, (float)xt, (float)rect.Top, i);
			}
			else
				side = FilledSide(pts, (float)xt, (float)rect.Top, i);

			// Cut rectangle
			if (side == Side.Right)
			{
				float left = rect.Left;
				if ((float)xt <= rect.Right) left = Math.Max(left, (float)xt);
				if ((float)xb <= rect.Right) left = Math.Max(left, (float)xb);
				rect = RectangleF.FromLTRB(left, rect.Top, rect.Right,	rect.Bottom);
			}
			else if (side == Side.Left)
			{
				float right = rect.Right;
				if ((float)xt >= rect.Left) right = Math.Min(right, (float)xt);
				if ((float)xb >= rect.Left) right = Math.Min(right, (float)xb);
				rect = RectangleF.FromLTRB(rect.Left, rect.Top, right,	rect.Bottom);
			}
			else rect.Width = 0;

			return rect;
		}

		private RectangleF chopRect(RectangleF rect, bool dir) 
		{
			if (dir)
				for (int i = 0; i < pts.Length; i++) 
					rect = chopByLine(rect, i);
			else
				for (int i = pts.Length-1; i >= 0; i--) 
					rect = chopByLine(rect, i);
			return rect;
		}

		private ArrayList placeLines(Graphics gr, Font font, 
			float st_pos, RectangleF r, float lineHeight, out int errors) 
		{
			errors = 0;
			ArrayList lines = new ArrayList();
			int st = 0;
			float y = (float)Math.Round(r.Y + st_pos);
			while (st < Text.Length) 
			{
				RectangleF place = new RectangleF(r.X, y, r.Width, lineHeight);
				RectangleF chop = chopRect(place, true);
				RectangleF chop1 = chopRect(place, false);
				if (chop1.Width > chop.Width) chop = chop1;
				if (chop.Width > 0) 
				{
					lines.Add(GetLine(gr, font, Text, ref st, chop.Width, chop.X, chop.Y));
					if (((TextLine)lines[lines.Count-1]).Location.X < chop.X)
						errors++;
				}
				else errors++;
				y += lineHeight;
			}
			return lines;
		}

		private void UpdateRate(float pos, Graphics gr, Font font, 
			PointF[] pts, RectangleF r, float lineHeight, ref float best_pos, 
			ref float best_rate, ref int best_errors) 
		{
			int errors;
			ArrayList lines = placeLines(gr, font, pos, r, lineHeight, out errors);
			float btm_space = r.Height - (lines.Count * lineHeight) - pos;
			if (btm_space > 0) 
			{
				float rate = pos / btm_space;
				if (rate > 1) rate = 1 / rate;
				if ((best_errors == -1) || (errors <= best_errors)) 
				{
					if (errors < best_errors) best_rate = -1;
					if ((best_rate == -1) || (rate > best_rate))
					{
						best_rate = rate;
						best_pos = pos;
						best_errors = errors;
					}
				}
			}
		}

		protected override void CalcTextLines(Graphics gr, Font font, float lineHeight) 
		{
			if (pts.Length < 2) return;
			RectangleF r = GetZoomedBounds();

			// Try to place text
			float best_pos = 0;
			float best_rate = -1;
			int best_errors = -1;
	
			for (float f1 = 0; f1 < r.Height - lineHeight; f1 += lineHeight) 
			{
				UpdateRate(f1, gr, font, pts, r, lineHeight, ref best_pos, 
					ref best_rate, ref best_errors);
				if ((best_rate == 1) && (best_errors == 0)) break;
			}
			for (float f2 = Math.Max(0, best_pos - lineHeight); f2 < best_pos + lineHeight; 
				f2 += Math.Min(1, lineHeight / 5)) 
			{
				if ((best_rate == 1) && (best_errors == 0)) break;
				UpdateRate(f2, gr, font, pts, r, lineHeight, ref best_pos, 
					ref best_rate, ref best_errors);
			}
	
			// Rebuild lines for best pos
			int errors;
			lines = placeLines(gr, font, best_pos, r, lineHeight, out errors);
		}

		protected override void DoDrawLine(Graphics gr, TextLine ln, Font font, 
			Color textColor, Point view) 
		{
			if (!Closed) return;
			base.DoDrawLine(gr, ln, font, textColor, view);
		}

		protected override void DoDraw(IntPtr hdc, Point view) 
		{
			if (pts.Length < 2) return;
			RectangleF r = GetZoomedBounds();

			// Shape
			PointF[] pts1 = new PointF[pts.Length];
			for (int i = 0; i < pts.Length; i++)
				pts1[i] = new PointF(pts[i].X - view.X, pts[i].Y - view.Y);

			int pen = 1;
			if (Selected) pen = selWidth;
			if (!Closed) 
				WinApi.Polygon(hdc, pts1, DesignColor, pen, ShapeDrawing.Outline);
			else 
			{
				if (Mode == AreaMode.Shapes)
				{
					PointF[] points = new PointF[pts1.Length + 1];
					for (int i = 0; i < pts1.Length; i++)
						points[i] = pts1[i];
					points[points.Length-1] = pts1[0];
					WinApi.Polygon(hdc, points, DesignColor, pen, ShapeDrawing.Outline);
				}
				if (Mode == AreaMode.Text)
					WinApi.Polygon(hdc, pts1, DesignColor, DesignFillColor, pen,
						ShapeDrawing.Both);
				if (Mode == AreaMode.View)
					WinApi.Polygon(hdc, pts1, FillColor, pen, ShapeDrawing.Fill);
			}
		}

	}
}
