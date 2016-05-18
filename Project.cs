using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.Drawing.Imaging;
using SevenZip;
using System.Collections.Generic;


namespace Overlay
{
	public class Scan 
	{
		public string Filename;
		public Size PicSize = new Size(1, 1);
		public AreaList Areas = new AreaList();

		private Project project;

		public Scan(Project project) 
		{
			this.project = project;
		}

		public Image GetImage() 
		{
			Image img;
            if (project.ArchiveName != "")
            {
                using (SevenZipExtractor extr = new SevenZipExtractor(project.Folder+project.ArchiveName))
                {


                    MemoryStream memStream = new MemoryStream(100);
                    try
                    {
                        extr.ExtractFile(Filename, memStream);
                    }
                    catch
                    {
                    }
                    try
                    {
                        img = Bitmap.FromStream(memStream);
                        return img;
                    }
                    catch
                    {
                    }
                    
                }
                return null;
            }
            else
            {

                if (File.Exists(project.Folder + Filename))
                    img = Bitmap.FromFile(project.Folder + Filename);
                else
                {
                    img = new Bitmap(PicSize.Width, PicSize.Height);
                    Graphics gr = Graphics.FromImage(img);
                    gr.FillRectangle(new SolidBrush(Color.White), 0, 0, img.Width, img.Height);
                }
                return img;
            }
		}

		private ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			int j;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			for(j = 0; j < encoders.Length; ++j)
			{
				if(encoders[j].MimeType == mimeType)
					return encoders[j];
			} return null;
		}

		public void Export(string filename, bool overwrite, object quality) 
		{
			while (!overwrite && File.Exists(filename))
				filename = Path.GetDirectoryName(filename) + "\\" +
					Path.GetFileNameWithoutExtension(filename) + "_" +
					Path.GetExtension(filename);

			AreaMode mode = Area.Mode;
			Area.Mode = AreaMode.View;
			Bitmap img = (Bitmap)GetImage();
			WinApi.GdiBitmap bmp = WinApi.GdiBitmap.FromBitmap(Display.Handle, img);
			Areas.DrawToDC(bmp.Hdc, new Rectangle(0, 0, img.Width, img.Height), 
				new Point(0, 0));
			img = Bitmap.FromHbitmap(bmp.Hbitmap);
			bmp.Dispose();

			EncoderParameters EncParms = new EncoderParameters(1);
			long q = 100;
			if (quality != null) q = Convert.ToInt32(((string)quality).Substring(0, 
														 ((string)quality).Length-1));
			EncoderParameter EncParm = new EncoderParameter(Encoder.Quality, q);
			EncParms.Param[0] = EncParm;
			ImageCodecInfo CodecInfo = GetEncoderInfo("image/jpeg");

			img.Save(filename, CodecInfo, EncParms);
			Area.Mode = mode;
		}
	}


	public class Project
	{
		private StreamWriter sw;

		public string Name = "untitled";
		public string Filename = "";
		public string ScansUrl = "";
		public string Description = "";
        public string ArchiveName = "";
		public ArrayList Scans = new ArrayList();
		public static Project Current = null;

        public MemoryStream memStream = new MemoryStream(100);
        public List<MemoryStream> memStreams = new List<MemoryStream>();
        
		public Project() 
		{
			Current = this;
		}

		public string Folder 
		{
			get 
			{ 
				try 
				{
					if (Path.GetDirectoryName(Filename).EndsWith("\\"))
						return Path.GetDirectoryName(Filename); 
					else
						return Path.GetDirectoryName(Filename) + "\\"; 
				}
				catch (ArgumentException) 
				{
					return "";
				}
			}
		}

		public Project(string projfile)
		{
			Current = this;
			Filename = projfile;
			Name = Path.GetFileName(projfile);
 

			// Read from file
			string s;
			StreamReader sr = new StreamReader(projfile, System.Text.Encoding.Default);
			if (sr.ReadLine() != "[Overlay Project]")
				throw new Exception("This file is not a valid project.");
			
			// Project properties
			do 
			{
				s = sr.ReadLine();
				if (Key(s) == "Name") Name = Value(s);
				if (Key(s) == "ScansUrl") ScansUrl = Value(s);
				if (Key(s) == "Description") Description = Value(s).Replace("\\n", "\r\n");
                if (Key(s) == "ArchiveName") ArchiveName = Value(s);
				if (Key(s) == "DefaultTextColor") 
					Area.DefaultTextColor = Color.FromArgb(Convert.ToInt32(Value(s)));
				if (Key(s) == "DefaultFontFamily") 
					Area.DefaultFontFamily = Value(s);
				if (Key(s) == "DefaultFontSize") 
					Area.DefaultFontSize = (float)Convert.ToDouble(RdDecimal(Value(s)));
				if (Key(s) == "DefaultFontBold") 
					Area.DefaultFontBold = Convert.ToBoolean(Value(s));
				if (Key(s) == "DefaultFontItalic") 
					Area.DefaultFontItalic = Convert.ToBoolean(Value(s));
				if (Key(s) == "DefaultLineHeight") 
					Area.DefaultLineHeight = Convert.ToInt32(Value(s));
				if (Key(s) == "DefaultFillColor") 
					Area.DefaultFillColor = Color.FromArgb(Convert.ToInt32(Value(s)));
				if (Key(s) == "Alignment") 
					Area.DefaultAlignment = RdAlignment(Value(s));
			} 
			while ((sr.Peek() > -1) && (s.IndexOf('[') != 0));

			// Scans
			while (sr.Peek() > -1) 
			{
				Scan sc = new Scan(this);
				if (s.Length >= 2) 
					sc.Filename = s.Substring(1, s.Length-2);
				Scans.Add(sc);
				Area a = null;
				do
				{
					s = sr.ReadLine();
					
					if (a == null) 
					{
						if (Key(s) == "Size") 
						{
							string[] ss1 = Value(s).Split(new char[1] {'x'});
							if (ss1.Length == 2) 
								sc.PicSize = new Size(Convert.ToInt32(ss1[0]),
									Convert.ToInt32(ss1[1]));
						}
					}

					// Determine area type
					if (Key(s).IndexOf(".") == 0) 
					{
						string[] ss = Value(s).Split(new char[2] {' ', ','});
						Point[] pts = new Point[ss.Length / 2];
						for (int i = 0; i < ss.Length / 2; i++)
							pts[i] = new Point(Convert.ToInt32(ss[i * 2]), 
								Convert.ToInt32(ss[i * 2 + 1]));

						if ((Key(s) == ".rectangle") && (pts.Length >= 2)) 
							a = new RectangleArea(new Rectangle(pts[0].X, pts[0].Y, 
								pts[1].X - pts[0].X, pts[1].Y - pts[0].Y));

						if ((Key(s) == ".ellipse") && (pts.Length >= 2)) 
							a = new EllipseArea(new Rectangle(pts[0].X, pts[0].Y, 
								pts[1].X - pts[0].X, pts[1].Y - pts[0].Y));

						if ((Key(s) == ".polygon") && (pts.Length >= 3)) 
							a = new PolygonArea(pts, true);

						if ((Key(s) == ".text") && (pts.Length == 2)) 
							a = new TextArea(pts[0], pts[1]);

						sc.Areas.Add(a);
					}

					if (a == null) continue;

					// Set area parameters
					if (Key(s) == "Text") 
						a.Text = Value(s).Replace("\\n", "\r\n");
					if (Key(s) == "TextColor") 
						a.TextColor = Color.FromArgb(Convert.ToInt32(Value(s)));
					if (Key(s) == "FontFamily") 
						a.FontFamily = Value(s);
					if (Key(s) == "FontSize") 
						a.FontSize = (float)Convert.ToDouble(RdDecimal(Value(s)));
					if (Key(s) == "FontBold") 
						a.FontBold = Convert.ToBoolean(Value(s));
					if (Key(s) == "FontItalic") 
						a.FontItalic = Convert.ToBoolean(Value(s));
					if (Key(s) == "LineHeight") 
						a.LineHeight = Convert.ToInt32(Value(s));
					if (Key(s) == "FillColor") 
						a.FillColor = Color.FromArgb(Convert.ToInt32(Value(s)));
					if (Key(s) == "Alignment")
						a.Alignment = RdAlignment(Value(s));
				} 
				while ((sr.Peek() > -1) && (s.IndexOf('[') != 0));
			}

			sr.Close();
		}

		private string Key(string s) 
		{
			string[] ss = s.Split(new char[1] {'='}, 2);
			return ss[0];
		}	

		private string Value(string s)
		{
			string[] ss = s.Split(new char[1] {'='}, 2);
			if (ss.Length > 1) return ss[1].ToString();
			else return "";
		}

		private void Write(string s) 
		{
			string s1 = "";
			for (int i = 0; i < s.Length; i++) 
				if (s[i] == '\n') s1 += "\\n";
				else if (s[i] != '\r') s1 += s[i];
			sw.Write(s1 + "\n");
		}

		private string CnvDecimal(string s) 
		{
			return s.Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator,
				".");
		}

		private string RdDecimal(string s) 
		{
			return s.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
		}

		private StringAlignment RdAlignment(string s) 
		{
			if (s == "Far") return StringAlignment.Far;
			else if (s == "Near") return StringAlignment.Near;
			else return StringAlignment.Center;
		}
        public int getindex(string filename)
        {
            int index = 0;
                using (SevenZipExtractor extr = new SevenZipExtractor(ArchiveName))
                { 
                    foreach (string sc_name in extr.ArchiveFileNames)
                    {
                        if (filename == sc_name)
                            return index;
                        else index++;
 
                    }
                }
                return -1;

        }

		public void Save(string filename) 
		{
			Filename = filename;
			sw = new StreamWriter(filename, false, System.Text.Encoding.Default);
			Write("[Overlay Project]");
			Write("Version=1.0");
			Write("Name=" + Name);
			Write("ScansUrl=" + ScansUrl);
			Write("Description=" + Description);
            Write("ArchiveName=" + ArchiveName);
			Write("DefaultTextColor=" + Area.DefaultTextColor.ToArgb().ToString());
			Write("DefaultFontFamily=" + Area.DefaultFontFamily);
			Write("DefaultFontSize=" + CnvDecimal(Area.DefaultFontSize.ToString()));
			Write("DefaultFontBold=" + Area.DefaultFontBold.ToString());
			Write("DefaultFontItalic=" + Area.DefaultFontItalic.ToString());
			Write("DefaultLineHeight=" + Area.DefaultLineHeight.ToString());
			Write("DefaultFillColor=" + Area.DefaultFillColor.ToArgb().ToString());
			Write("DefaultAlignment=" + Area.DefaultAlignment.ToString());
			Write("");
      
			for (int i = 0; i < Scans.Count; i++) 
			{
				Scan sc = (Scan)Scans[i];
				Write("[" + sc.Filename + "]");
				Write("Size=" + sc.PicSize.Width.ToString() + "x" + 
					sc.PicSize.Height.ToString());
				for (int j = 0; j < sc.Areas.Count; j++) 
				{
					Area a = (Area)sc.Areas[j];
					string s = "";
					for (int k = 0; k < a.Points.Length; k++)
						s += a.Points[k].X.ToString() + "," + a.Points[k].Y.ToString() + " ";
					if (a.GetType() == typeof(RectangleArea))
						Write(".rectangle=" + s);
					if (a.GetType() == typeof(EllipseArea))
						Write(".ellipse=" + s);
					if (a.GetType() == typeof(PolygonArea))
						Write(".polygon=" + s);
					if (a.GetType() == typeof(TextArea))
						Write(".text=" + s);
					Write("Text=" + a.Text);

					if (a.TextColor != Area.DefaultTextColor)
						Write("TextColor=" + a.TextColor.ToArgb().ToString());
					if (a.FontFamily != Area.DefaultFontFamily)
						Write("FontFamily=" + a.FontFamily);
					if (a.FontSize != Area.DefaultFontSize)
						Write("FontSize=" + CnvDecimal(a.FontSize.ToString()));
					if (a.FontBold != Area.DefaultFontBold)
						Write("FontBold=" + a.FontBold.ToString());
					if (a.FontItalic != Area.DefaultFontItalic) 
						Write("FontItalic=" + a.FontItalic.ToString());
					if (a.LineHeight != Area.DefaultLineHeight)
						Write("LineHeight=" + a.LineHeight.ToString());
					if (a.FillColor != Area.DefaultFillColor)
						Write("FillColor=" + a.FillColor.ToArgb().ToString());
					if (a.Alignment != Area.DefaultAlignment)
						Write("Alignment=" + a.Alignment.ToString());
				}
				Write("");
			}
			sw.Close();
		}
	}
}
