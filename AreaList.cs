using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Overlay
{
	public enum ShapeMode { None, Rectangle, Ellipse, Polygon, TextLine }

	public class AreaList : ArrayList
	{
		public ShapeMode ShapeMode = ShapeMode.None;
		private Point moveOrigin = new Point();
		private int moveShapePoint = -1;
		private Point[] origPoints = new Point[0];


		public new Area this[int index] 
		{
			get { return (Area)base[index]; } 
		}

		public Area Selected 
		{
			get 
			{
				foreach (Area a in this)
					if (a.Selected) return a;
				return null;
			}
		}

		public Point GetDisplacement(Point hit) 
		{
			return new Point(hit.X - moveOrigin.X, hit.Y - moveOrigin.Y);
		}

		public void DrawToDC(IntPtr hdc, Rectangle clip, Point view) 
		{
			Rectangle clip1 = new Rectangle(clip.X + view.X, clip.Y + view.Y, 
				clip.Width, clip.Height);
			foreach (Area a in this)
				if (clip1.IntersectsWith(a.GetBounds(true))) a.Draw(hdc, view);
		}

		public Area AreaPointAt(Point p, out int index) 
		{
			foreach (Area a in this) 
			{
				index = a.PointAtCoords(p);
				if (index >= 0) return a;
			}
			index = -1;
			return null;
		}

		public bool IsAreaPointAt(Point p) 
		{
			int i;
			return (AreaPointAt(p, out i) != null);
		}

		public Area AreaAt(Point p)
		{
			for (int i = Count-1; i >= 0; i--)
				if (this[i].Contains(p)) return this[i];
			return null;
		}

		public void ClearSelection() 
		{
			foreach (Area a in this) 
				if (a.Selected) 
				{
					a.Selected = false;
					Display.UpdateScreenBuffer(a.GetBounds(true));
				}
		}

		public void RecalcAll() 
		{
			foreach (Area a in this) a.Recalc(false);
		}

		public void ProcessMouseDown(Point hit) 
		{
			moveOrigin = hit;
			Point p = new Point(hit.X * 100 / Display.Zoom, hit.Y * 100 / Display.Zoom);

			Area area = null;
			switch (ShapeMode)
			{
				// Select an existing area
				case ShapeMode.None: 
					area = AreaPointAt(hit, out moveShapePoint);
					if (area == null) area = AreaAt(hit);
					if (area != Selected) ClearSelection();
					break;

				// Create new area
				case ShapeMode.Rectangle:
					ClearSelection();
					area = new RectangleArea(new Rectangle(p.X, p.Y, 0, 0));
					Add(area);
					moveShapePoint = 1;
					break;

				case ShapeMode.Ellipse:
					ClearSelection();
					area = new EllipseArea(new Rectangle(p.X, p.Y, 0, 0));
					Add(area);
					moveShapePoint = 1;
					break;

				case ShapeMode.Polygon:
					Area selArea = Selected;
					if ((selArea == null) || (selArea.GetType() != typeof(PolygonArea)) ||
						((PolygonArea)selArea).Closed)
					{
						ClearSelection();
						Point[] points = new Point[2] {p, p};
						area = new PolygonArea(points, false);
						Add(area);
						moveShapePoint = 1;
					}
					else 
					{
						area = selArea;
						if (selArea.PointAtCoords(hit) == 0) 
						{
							ShapeMode = ShapeMode.None;
							((PolygonArea)selArea).Closed = true;
							Point[] points = new Point[selArea.Points.Length - 1];
							for (int i = 0; i < points.Length; i++)
								points[i] = selArea.Points[i];
							selArea.Points = points;
							moveShapePoint = -1;
						}
						else 
						{
							Point[] points = new Point[selArea.Points.Length + 1];
							for (int i = 0; i < points.Length-1; i++)
								points[i] = selArea.Points[i];
							points[points.Length-1] = p;
							selArea.Points = points;
							moveShapePoint = points.Length-1;
						}
					}
					break;

				case ShapeMode.TextLine:
					ClearSelection();
					area = new TextArea(p, p);
					Add(area);
					moveShapePoint = 1;
					break;
			}

			if (area != null) 
			{
				area.Selected = true;
				origPoints = (Point[])area.Points.Clone();
				area.Recalc(true);
			}
		}

		public void ProcessMouseMove(Point hit) 
		{
			Area selArea = Selected;
			if (selArea == null) return;
			bool shift = ((WinApi.GetKeyState(WinApi.VK_SHIFT) & 0x80) != 0);

			int fx = (hit.X - moveOrigin.X) * 100 / Display.Zoom;
			int fy = (hit.Y - moveOrigin.Y) * 100 / Display.Zoom;
			
			if (moveShapePoint == -1) 
				for (int i = 0; i < selArea.Points.Length; i++)
					selArea.Points[i] = new Point(origPoints[i].X + fx,
						origPoints[i].Y + fy);
			else 
			{
				if (!shift)
					selArea.Points[moveShapePoint] = new Point(origPoints[moveShapePoint].X + fx,
						origPoints[moveShapePoint].Y + fy);
				else 
				{
					Point p;
					if (moveShapePoint > 0) 
						p = origPoints[moveShapePoint-1];
					else p = origPoints[origPoints.Length-1];
					PointF h = new PointF((float)hit.X * 100 / Display.Zoom, 
						(float)hit.Y * 100 / Display.Zoom);

					float k;
					if (h.Y != p.Y)
						k = (h.X - p.X) / (h.Y - p.Y);
					else k = 2;

					if (((Math.Abs(k) < 0.5) || (Math.Abs(k) > 1.5)) && 
						((selArea.GetType() == typeof(TextArea)) ||
						(selArea.GetType() == typeof(PolygonArea))))
					{
						if (Math.Abs(k) > 1.5)
							selArea.Points[moveShapePoint] = new Point(
								origPoints[moveShapePoint].X + fx, p.Y);
						else
							selArea.Points[moveShapePoint] = new Point(
								p.X, origPoints[moveShapePoint].Y + fy);
					}
					else 
					{
						int dist;
						if (Math.Abs(k) < 1)
							dist = origPoints[moveShapePoint].X - p.X + fx;
						else dist = origPoints[moveShapePoint].Y - p.Y + fy;
						int dx;
						if (h.X - p.X > 0) dx = Math.Abs(dist);
						else dx = -Math.Abs(dist);
						int dy;
						if (h.Y - p.Y > 0) dy = Math.Abs(dist);
						else dy = -Math.Abs(dist);
						selArea.Points[moveShapePoint] = new Point(p.X + dx, p.Y + dy);
					}
				}
			}
			selArea.Recalc(true);
			Display.DrawToDC();
		}

		public void ProcessMouseUp(Point hit) 
		{
		}

	}
}
