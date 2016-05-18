using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Collections;

namespace Overlay
{
	public class TextArea : Area
	{
		public TextArea(Point p1, Point p2)
		{
			Points = new Point[2] {p1, p2};
			Recalc(false);
		}

		public override bool Contains(Point p) 
		{
			double x;
			double y;
			if (pts[1].X != pts[0].X) 
			{
				double k = ((double)pts[1].Y - pts[0].Y) / 
					((double)pts[1].X - pts[0].X);
				float xb = pts[0].X;
				float yb = pts[0].Y;
				y = (double)(p.X - xb) * k + yb;
				x = (double)(p.Y - yb) / k + xb;
			}
			else 
			{
				x = pts[0].X;
				y = p.Y;
			}
			RectangleF r = GetZoomedBounds();
			r = new RectangleF(r.X-4, r.Y-4, r.Right+4, r.Bottom+4);
			return ((Math.Min(Math.Abs(p.Y - y), Math.Abs(p.X - x)) < 8)
				&& (r.Contains(p)));
		}

		private PointF[] GetRotation(Graphics gr, Font font) 
		{
			if (!gr.Transform.IsIdentity) return pts;
			SizeF size = gr.MeasureString(Text, font);
			
			PointF[] pts1 = new PointF[pts.Length];
			for (int i = 0; i < pts.Length; i++)
				pts1[i] = new PointF(pts[i].X, pts[i].Y);

			if (pts[0].Y != pts[1].Y) 
			{
				double angle = Math.Atan(((double)pts[1].Y - pts[0].Y) / ((double)pts[1].X - pts[0].X));
				if (pts[0].X > pts[1].X) angle += Math.PI;
				gr.RotateTransform((float)(angle * 180 / Math.PI));
				gr.TransformPoints(System.Drawing.Drawing2D.CoordinateSpace.World, 
					System.Drawing.Drawing2D.CoordinateSpace.Page, pts1);
			}
			return pts1;
		}

		protected override RectangleF CalcLineBounds(TextLine ln) 
		{
			Graphics gr = Graphics.FromHwnd(Display.Handle);
			Font font = CalcZoomedFont();
			GetRotation(gr, font);
			float lineHeight = CalcLineHeight(font);
			PointF[] pts2 = new PointF[4] 
			{ 
				ln.Location, 
				new PointF(ln.Location.X, ln.Location.Y + lineHeight), 
				new PointF(ln.Location.X + ln.Width, ln.Location.Y), 
				new PointF(ln.Location.X + ln.Width, ln.Location.Y + lineHeight) 
			};
			gr.TransformPoints(System.Drawing.Drawing2D.CoordinateSpace.Page, 
				System.Drawing.Drawing2D.CoordinateSpace.World, pts2);
			return RectangleF.FromLTRB(
				Math.Min(Math.Min(pts2[1].X, pts2[0].X), Math.Min(pts2[3].X, pts2[2].X)),
				Math.Min(Math.Min(pts2[1].Y, pts2[0].Y), Math.Min(pts2[3].Y, pts2[2].Y)),
				Math.Max(Math.Max(pts2[1].X, pts2[0].X), Math.Max(pts2[3].X, pts2[2].X)),
				Math.Max(Math.Max(pts2[1].Y, pts2[0].Y), Math.Max(pts2[3].Y, pts2[2].Y))
				);
		}

		protected override void CalcTextLines(Graphics gr, Font font, float lineHeight) 
		{
			PointF[] pts1 = GetRotation(gr, font);
			double len = Math.Sqrt(Math.Pow(pts[1].X - pts[0].X, 2) + 
				Math.Pow(pts[1].Y - pts[0].Y, 2));

			// Break text to even lines
			int st = 0;
			lines = new ArrayList();
			while (st < Text.Length) 
				lines.Add(GetLine(gr, font, Text, ref st, (int)len, pts1[0].X, 0));

			// Determine line positions
			float y1 = (float)(pts1[0].Y - (lines.Count * lineHeight) / 2);
			for (int i = 0; i < lines.Count; i++) 
			{
				TextLine ln = (TextLine)lines[i];
				ln.Location.Y = y1 + i * lineHeight;
			}
		}

		protected override void DoDrawLine(Graphics gr, TextLine ln, Font font, 
			Color textColor, Point view) 
		{
			GetRotation(gr, font);
			PointF[] varr = new PointF[1] {view};
			gr.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, varr);
			float x = ln.Location.X - varr[0].X;
			float y = ln.Location.Y - varr[0].Y;
			if (Mode == AreaMode.View) 
			{
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x - 1, y - 1);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x + 1, y - 1);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x - 1, y + 1);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x + 1, y + 1);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x - 0, y - 2);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x + 2, y - 0);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x - 2, y + 0);
				gr.DrawString(ln.Text, font, new SolidBrush(FillColor),	x + 0, y + 2);
			}
			gr.DrawString(ln.Text, font, new SolidBrush(textColor),	x, y);
		}

		protected override void DoDraw(IntPtr hdc, Point view) 
		{
			// Shape
			PointF[] pts1 = new PointF[pts.Length];
			for (int i = 0; i < pts.Length; i++)
				pts1[i] = new PointF(pts[i].X - view.X, pts[i].Y - view.Y);
			
			if (Mode != AreaMode.View) 
			{
				if (Selected) 
					WinApi.Polygon(hdc, pts1, DesignColor, selWidth, ShapeDrawing.Outline);
				else
					WinApi.Polygon(hdc, pts1, DesignColor, 1, ShapeDrawing.Outline);
			}
		}

	}
}
