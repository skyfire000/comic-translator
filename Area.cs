using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;

namespace Overlay
{

	public class TextLine
	{
		public string Text;
		public PointF Location;
		public float Width;

		public TextLine(string text, float x, float y, float width)
		{
			Text = text;
			Location = new PointF(x, y);
			Width = width;
		}
	}

	public enum AreaMode { None, View, Shapes, Text };

	public class Area
	{
		protected const int selWidth = 2;
		
		public static Color DesignColor = Color.Blue;
		public static Color DesignFillColor = Color.White;

		public static Color DefaultTextColor = Color.Black;
		public static String DefaultFontFamily = "Microsoft Yahei";
		public static float DefaultFontSize = 16;
		public static bool DefaultFontBold = false;
		public static bool DefaultFontItalic = false;
		public static int DefaultLineHeight = 0;
		public static Color DefaultFillColor = Color.White;
		public static StringAlignment DefaultAlignment = StringAlignment.Center;

		public Color TextColor = DefaultTextColor;
		public Color FillColor = DefaultFillColor;
		public String FontFamily = DefaultFontFamily;
		public float FontSize = DefaultFontSize;
		public bool FontBold = DefaultFontBold;
		public bool FontItalic = DefaultFontItalic;
		public int LineHeight = DefaultLineHeight;
		public StringAlignment Alignment = DefaultAlignment;

		public Point[] Points;
		public string Text = "";
		public bool Selected = false;

		protected PointF[] pts;
		protected ArrayList lines;

		public static AreaMode Mode = AreaMode.Text;

		// Get point/pixel convertor
		private static Font tmpfont = new Font("Arial", 100, GraphicsUnit.Pixel);
		private static float pixelrate = tmpfont.Size / tmpfont.SizeInPoints;

		public static bool ReadOnlyMode 
		{
			get { return (Mode == AreaMode.None) || (Mode == AreaMode.View); }
		}

		protected static int Zoom 
		{
			get { return Display.Zoom; }
		}

		public void Move(int dx, int dy) 
		{
		}

		private static RectangleF ExtendRectangleF(RectangleF r, PointF p) 
		{
			RectangleF res = new RectangleF(r.Location, r.Size);
			if (p.X < r.X) 
			{
				res.Width += r.X - p.X;
				res.X = p.X;
			}
			if (p.Y < r.Y) 
			{
				res.Height += r.Y - p.Y;
				res.Y = p.Y;
			}
			if (p.X > r.Right) 
				res.Width = p.X - r.X;
			if (p.Y > r.Bottom)
				res.Height = p.Y - r.Y;
			return res;
		}
        private string GetSubstring(string s, int st, Graphics gr, Font font, float width)
        {
            string res = "";
            string res1 = "";
            while (true)
            {
                if ((st >= s.Length) || (s[st] == ' ') || (s[st] == '\n') || (s[st] == '\r'))
                {
                    if ((st < s.Length) && (s[st] == '\r'))
                        st++;
                    if (gr.MeasureString(res, font).Width <= width)
                        res1 = res;
                    else
                    {
                        if (res1 == "") return res;
                        return res1;
                    }
                    if (st >= s.Length)
                    {
                        return res;
                    }
                    if (s[st] == '\n')
                        return res1;
                }
                res += s[st];
                st++;
            }
        }
           // Textc T1 = new Textc(s, "");
           // s=T1.transform(1).text + T1.transform(2).text + T1.transform(3).text;
     

		protected TextLine GetLine(Graphics gr, Font font, string s, 
			ref int st, float width, float x, float y) 
		{
           // Textc T1 = new Textc(s, "");
          //  s = T1.transform(1).text + T1.transform(2).text + T1.transform(3).text;
			// Get part of text to fit in width
			string res = GetSubstring(s, st, gr, font, width);
			st += res.Length + 1;
			if ((st > 0) && (st < s.Length) && (s[st-1] == '\r')) st++;

			// Move spaces to beginning (to cheat width calculation)
			string res1 = res;
			if (res1.Trim().Length > 0) 
				while (res1[res1.Length-1] == ' ')
					res1 = " " + res1.Substring(0, res1.Length-1);
           
             Textc T1 = new Textc(res1, "");
             while (T1.transform(2).text != "")
             {
                 res1 = T1.transform(1).text + T1.transform(2).text + T1.transform(3).text;
                 T1.text = res1;
             }
        
            float wid = gr.MeasureString(res1, font).Width;

			float x1 = (float)(x + (width - wid) / 2);
			if (Alignment == StringAlignment.Near) 
				x1 = x;
			else if (Alignment == StringAlignment.Far) 
				x1 = x + width - wid;
			return new TextLine(res, x1, y, wid);
		}

		protected RectangleF GetZoomedBounds()
		{
			RectangleF edge = new Rectangle((int)pts[0].X, (int)pts[0].Y, 0, 0);
			for (int i = 1; i < pts.Length; i++) 
				edge = ExtendRectangleF(edge, pts[i]);
			return edge;
		}

		public Rectangle GetBounds() 
		{
			return GetBounds(false);
		}

		protected virtual RectangleF CalcLineBounds(TextLine ln) 
		{
			float lineHeight = CalcLineHeight(CalcZoomedFont());
			return new RectangleF(ln.Location.X, ln.Location.Y, ln.Width, lineHeight);
		}

		public Rectangle GetBounds(bool full)
		{
			RectangleF rf = GetZoomedBounds();
			if (!full)
				return new Rectangle((int)rf.X, (int)rf.Y, (int)rf.Width, (int)rf.Height);
			else 
			{
				Rectangle r = new Rectangle((int)rf.X - 3, (int)rf.Y - 3,
					(int)rf.Width + 7, (int)rf.Height + 7);
				foreach (TextLine ln in lines) 
				{
					RectangleF lb = CalcLineBounds(ln);
					r = Rectangle.FromLTRB((int)Math.Min(lb.Left, r.Left), 
						(int)Math.Min(lb.Top, r.Top), 
						(int)Math.Max(lb.Right, r.Right),
						(int)Math.Max(lb.Bottom, r.Bottom));
				}
				return r;
			}
		}

		public int PointAtCoords(Point p) 
		{
			for (int i = 0; i < pts.Length; i++) 
			{
				Rectangle r = new Rectangle((int)pts[i].X - 3, (int)pts[i].Y - 3, 7, 7);
				if (r.Contains(p)) return i;
			}
			return -1;
		}

		protected virtual void CalcTextLines(Graphics gr, Font font, float lineHeight) 
		{
		}

		protected Font CalcZoomedFont() 
		{
			FontStyle fs = new FontStyle();
			if (FontBold) fs = fs | FontStyle.Bold;
			if (FontItalic) fs = fs | FontStyle.Italic;
			return new Font(frmMain.GetFontFamily(FontFamily), (float)Math.Max(0.01, FontSize * Zoom / 
				100), fs);
		}

		protected float CalcLineHeight(Font font) 
		{
			float lineHeight;
			if (LineHeight > 0) 
				lineHeight = LineHeight * Zoom / 100;
			else 
			{
				FontFamily ff = frmMain.GetFontFamily(this.FontFamily);
				FontStyle fs = font.Style;
				lineHeight = (FontSize * ff.GetLineSpacing(fs) / 
					ff.GetEmHeight(fs)) * pixelrate * Zoom / 100;
			}
			return lineHeight;
		}

		public void Recalc(bool redraw) 
		{
			Rectangle r1 = new Rectangle();
			if (pts != null) r1 = GetBounds(true);

			pts = new PointF[Points.Length];
			for (int j = 0; j < Points.Length; j++)
				pts[j] = new PointF((float)(Points[j].X * Zoom) / 100, 
					(float)(Points[j].Y * Zoom) / 100);
			Font font = CalcZoomedFont();
			Graphics gr = Graphics.FromHwnd(Display.Handle);
			gr.TextRenderingHint = TextRenderingHint.AntiAlias;
			CalcTextLines(gr, font, CalcLineHeight(font));
			gr.Dispose();

			Rectangle r2 = GetBounds(true);
			if (r1.IsEmpty) r1 = r2;
			if (redraw)	Display.UpdateScreenBuffer(Rectangle.FromLTRB(
				Math.Min(r1.X, r2.X) - 4,
				Math.Min(r1.Y, r2.Y) - 4, 
				Math.Max(r1.Right, r2.Right) + 4,
				Math.Max(r1.Bottom, r2.Bottom) + 4));
		}

		public virtual bool Contains(Point p) 
		{
			return false;
		}

		protected virtual void DoDraw(IntPtr hdc, Point view)
		{
		}


		protected virtual void DoDrawLine(Graphics gr, TextLine ln, Font font, 	Color textColor, Point view) 
		{
            
            Textc T1 = new Textc(ln.Text, "");
            string s1 = T1.transform(1).text;
           
            string s2 = T1.transform(2).text;
            string s3 = T1.transform(3).text;
            if (s2 == "" )
                gr.DrawString(ln.Text, font, new SolidBrush(textColor), ln.Location.X - view.X, ln.Location.Y - view.Y);
            else
            {
                Textc T2 = new Textc(s1, "");
                while (T2.transform(2).text != "")
                {
                    T2.text = T2.transform(1).text + T2.transform(2).text + T2.transform(3).text;
                    
                }
                SizeF size1 = gr.MeasureString(T2.text, font);
                //if T1.transform(0)=b
                Font font2 = new Font(font.FontFamily, font.Size, FontStyle.Bold);
                //
                SizeF size2 = gr.MeasureString(s2, font2);
                if (s1 == "")
                {
                    gr.DrawString(s2, font2, new SolidBrush(textColor), ln.Location.X - view.X + size1.Width, ln.Location.Y - view.Y);
                    gr.DrawString(s3, font, new SolidBrush(textColor), ln.Location.X - view.X + size1.Width + size2.Width - font.Size/2, ln.Location.Y - view.Y);
                }
                else
                {
                    DoDrawLine(gr, new TextLine(s1, ln.Location.X, ln.Location.Y, ln.Width), font, textColor, view);
                    //gr.DrawString(s1, font, new SolidBrush(textColor), ln.Location.X - view.X, ln.Location.Y - view.Y);
                    gr.DrawString(s2, font2, new SolidBrush(textColor), ln.Location.X - view.X - font.Size / 2 + size1.Width, ln.Location.Y - view.Y);
                    gr.DrawString(s3, font, new SolidBrush(textColor), ln.Location.X - view.X + size1.Width + size2.Width - font.Size, ln.Location.Y - view.Y);
                }
            }

           // 	
		}

		public void Draw(IntPtr hdc, Point view) 
		{
			if (Mode == AreaMode.None) return;

			Graphics gr = Graphics.FromHdc(hdc);
			gr.TextRenderingHint = TextRenderingHint.AntiAlias;
			
			// Font color
			Color textColor = DesignColor;
			if (Mode == AreaMode.View) textColor = TextColor;

			// Call overriden drawing method
			DoDraw(hdc, view);

			// Draw lines
			if (Mode != AreaMode.Shapes) 
			{
				for (int i = 0; i < lines.Count; i++) 
					DoDrawLine(gr, (TextLine)lines[i], CalcZoomedFont(), textColor, view);
			}

			// Draw point handles
			if (Selected && (Mode != AreaMode.View))
			{
				for (int i = 0; i < pts.Length; i++) 
					WinApi.Rectangle(hdc, new Rectangle((int)pts[i].X - view.X - 3, 
						(int)pts[i].Y - view.Y - 3, 7, 7), DesignColor, ShapeDrawing.Outline);
			}

			gr.Dispose();

		}

	}
}
