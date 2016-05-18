using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Win32;
using SevenZip;
using ColorPicker;

namespace Overlay
{
	public class frmMain : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code

		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.PictureBox pbMain;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton btnRectangle;
		private System.Windows.Forms.ToolBarButton btnEllipse;
		private System.Windows.Forms.ToolBarButton btnPolygon;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem itmZoom100;
		private System.Windows.Forms.MenuItem itmZoomFit;
		private System.Windows.Forms.MenuItem itmZoomIn;
		private System.Windows.Forms.MenuItem itmZoomOut;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem itmShapes;
		private System.Windows.Forms.MenuItem itmText;
		private System.Windows.Forms.MenuItem itmView;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem itmRectangle;
		private System.Windows.Forms.MenuItem itmEllipse;
		private System.Windows.Forms.MenuItem itmPolygon;
		private System.Windows.Forms.MenuItem itmSaveAs;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private System.Windows.Forms.MenuItem itmOpen;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.MenuItem itmSave;
		private System.Windows.Forms.Panel pText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pLeft;
		private System.Windows.Forms.Panel pRight;
		private System.Windows.Forms.MenuItem itmPrevPic;
		private System.Windows.Forms.MenuItem itmNextPic;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.TextBox tbText;
		private System.Windows.Forms.ComboBox cmFontFamily;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolBarButton btnBold;
		private System.Windows.Forms.ToolBarButton btnItalic;
		private System.Windows.Forms.ToolBarButton btnTextColor;
		private System.Windows.Forms.ToolBarButton btnFillColor;
        private System.Windows.Forms.ToolBar tbarText;
		private System.Windows.Forms.ToolBarButton btnSave;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.ToolBarButton btnNew;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.MenuItem itmNewProject;
		private System.Windows.Forms.ToolBarButton toolBTest;
		private System.Windows.Forms.ToolBarButton btnPrev;
		private System.Windows.Forms.ToolBarButton btnNext;
		private System.Windows.Forms.ToolBarButton toolBarButton6;
		private System.Windows.Forms.ToolBarButton btnZoom100;
		private System.Windows.Forms.ToolBarButton btnZoomFit;
		private System.Windows.Forms.ToolBarButton btnZoomIn;
		private System.Windows.Forms.ToolBarButton btnZoomOut;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton btnShapes;
		private System.Windows.Forms.ToolBarButton btnText;
		private System.Windows.Forms.ToolBarButton btnView;
		private System.Windows.Forms.ToolBarButton toolBarButton5;
		private System.Windows.Forms.ToolBarButton toolBarButton7;
		private System.Windows.Forms.ToolBarButton toolBarButton8;
		private System.Windows.Forms.ToolBarButton toolBarButton9;
		private System.Windows.Forms.MenuItem itmDelShape;
		private System.Windows.Forms.ToolBarButton btnDelShape;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem itmAddPic;
		private System.Windows.Forms.MenuItem itmDelPic;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem itmExportPic;
		private System.Windows.Forms.MenuItem itmExportAll;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem itmToolbar;
		private System.Windows.Forms.MenuItem itmDesignPanel;
		private System.Windows.Forms.ToolBarButton btnExportAll;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem itmProjectProperties;
		private System.Windows.Forms.ToolBarButton btnProjectProperties;
		private System.Windows.Forms.MenuItem itmBringToFront;
		private System.Windows.Forms.MenuItem itmSendToBack;
		private System.Windows.Forms.MenuItem itmNoShapes;
		private System.Windows.Forms.ToolBarButton btnNoShapes;
		private System.Windows.Forms.ToolBarButton btnTextLine;
		private System.Windows.Forms.MenuItem itmTextLine;
		private System.Windows.Forms.TextBox tbLineHeight;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmFontSize;
		private System.Windows.Forms.TextBox tbFontSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolBarButton btnSetDefault;
		private System.Windows.Forms.ListBox lbPictures;
		private System.Windows.Forms.MenuItem itmLastProj;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem itmUppercase;
		private System.Windows.Forms.MenuItem itmLowercase;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.ToolBarButton btnAlignment;
		private System.Windows.Forms.MenuItem itmHideUnusedFonts;
		private System.Windows.Forms.MenuItem itmAutosave;
		private System.Windows.Forms.MenuItem itmInvert;
		private System.Windows.Forms.MenuItem itmBuffer;
		private System.Windows.Forms.MenuItem itmBufferNext;
		private System.Windows.Forms.MenuItem itmBufferPrev;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem itmQuality10;
		private System.Windows.Forms.MenuItem itmQuality20;
		private System.Windows.Forms.MenuItem itmQuality30;
		private System.Windows.Forms.MenuItem itmQuality40;
		private System.Windows.Forms.MenuItem itmQuality50;
		private System.Windows.Forms.MenuItem itmQuality60;
		private System.Windows.Forms.MenuItem itmQuality70;
		private System.Windows.Forms.MenuItem itmQuality80;
		private System.Windows.Forms.MenuItem itmQuality90;
		private System.Windows.Forms.MenuItem itmQuality100;
		private System.Windows.Forms.MenuItem itmExportQuality;
        private MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem1;
        private ColorPickerDialog colorDialog=new ColorPickerDialog();

		[STAThread]
		static void Main(string[] args) 
		{
			if ((args.Length == 0) || (args[0][0] != '-')) 
			{
				if (args.Length > 0) startupProject = args[0];
				Application.Run(new frmMain());
			}
			else 
				frmMain.ProcessCommandLine(args);
		}

		public frmMain()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if (disposing && (components != null)) components.Dispose();
			base.Dispose( disposing );
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.itmNewProject = new System.Windows.Forms.MenuItem();
            this.itmOpen = new System.Windows.Forms.MenuItem();
            this.itmSave = new System.Windows.Forms.MenuItem();
            this.itmSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.itmLastProj = new System.Windows.Forms.MenuItem();
            this.itmAutosave = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.itmAddPic = new System.Windows.Forms.MenuItem();
            this.itmDelPic = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.itmExportPic = new System.Windows.Forms.MenuItem();
            this.itmExportAll = new System.Windows.Forms.MenuItem();
            this.itmExportQuality = new System.Windows.Forms.MenuItem();
            this.itmQuality10 = new System.Windows.Forms.MenuItem();
            this.itmQuality20 = new System.Windows.Forms.MenuItem();
            this.itmQuality30 = new System.Windows.Forms.MenuItem();
            this.itmQuality40 = new System.Windows.Forms.MenuItem();
            this.itmQuality50 = new System.Windows.Forms.MenuItem();
            this.itmQuality60 = new System.Windows.Forms.MenuItem();
            this.itmQuality70 = new System.Windows.Forms.MenuItem();
            this.itmQuality80 = new System.Windows.Forms.MenuItem();
            this.itmQuality90 = new System.Windows.Forms.MenuItem();
            this.itmQuality100 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.itmRectangle = new System.Windows.Forms.MenuItem();
            this.itmEllipse = new System.Windows.Forms.MenuItem();
            this.itmPolygon = new System.Windows.Forms.MenuItem();
            this.itmTextLine = new System.Windows.Forms.MenuItem();
            this.itmDelShape = new System.Windows.Forms.MenuItem();
            this.itmBringToFront = new System.Windows.Forms.MenuItem();
            this.itmSendToBack = new System.Windows.Forms.MenuItem();
            this.itmInvert = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.itmUppercase = new System.Windows.Forms.MenuItem();
            this.itmLowercase = new System.Windows.Forms.MenuItem();
            this.itmHideUnusedFonts = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.itmBuffer = new System.Windows.Forms.MenuItem();
            this.itmBufferNext = new System.Windows.Forms.MenuItem();
            this.itmBufferPrev = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.itmProjectProperties = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.itmToolbar = new System.Windows.Forms.MenuItem();
            this.itmDesignPanel = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.itmPrevPic = new System.Windows.Forms.MenuItem();
            this.itmNextPic = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.itmZoom100 = new System.Windows.Forms.MenuItem();
            this.itmZoomFit = new System.Windows.Forms.MenuItem();
            this.itmZoomIn = new System.Windows.Forms.MenuItem();
            this.itmZoomOut = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.itmNoShapes = new System.Windows.Forms.MenuItem();
            this.itmShapes = new System.Windows.Forms.MenuItem();
            this.itmText = new System.Windows.Forms.MenuItem();
            this.itmView = new System.Windows.Forms.MenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.pLeft = new System.Windows.Forms.Panel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.btnNew = new System.Windows.Forms.ToolBarButton();
            this.btnSave = new System.Windows.Forms.ToolBarButton();
            this.btnOpen = new System.Windows.Forms.ToolBarButton();
            this.btnExportAll = new System.Windows.Forms.ToolBarButton();
            this.btnProjectProperties = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton9 = new System.Windows.Forms.ToolBarButton();
            this.btnPrev = new System.Windows.Forms.ToolBarButton();
            this.btnNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
            this.btnZoom100 = new System.Windows.Forms.ToolBarButton();
            this.btnZoomFit = new System.Windows.Forms.ToolBarButton();
            this.btnZoomIn = new System.Windows.Forms.ToolBarButton();
            this.btnZoomOut = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.toolBTest = new System.Windows.Forms.ToolBarButton();
            this.btnRectangle = new System.Windows.Forms.ToolBarButton();
            this.btnEllipse = new System.Windows.Forms.ToolBarButton();
            this.btnPolygon = new System.Windows.Forms.ToolBarButton();
            this.btnTextLine = new System.Windows.Forms.ToolBarButton();
            this.btnDelShape = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.btnNoShapes = new System.Windows.Forms.ToolBarButton();
            this.btnShapes = new System.Windows.Forms.ToolBarButton();
            this.btnText = new System.Windows.Forms.ToolBarButton();
            this.btnView = new System.Windows.Forms.ToolBarButton();
            this.pRight = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbPictures = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pText = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbarText = new System.Windows.Forms.ToolBar();
            this.btnBold = new System.Windows.Forms.ToolBarButton();
            this.btnItalic = new System.Windows.Forms.ToolBarButton();
            this.btnAlignment = new System.Windows.Forms.ToolBarButton();
            this.btnTextColor = new System.Windows.Forms.ToolBarButton();
            this.btnFillColor = new System.Windows.Forms.ToolBarButton();
            this.btnSetDefault = new System.Windows.Forms.ToolBarButton();
            this.tbLineHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.cmFontFamily = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFontSize = new System.Windows.Forms.TextBox();
            this.cmFontSize = new System.Windows.Forms.ComboBox();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.pLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.pRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pText.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem2});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmNewProject,
            this.itmOpen,
            this.itmSave,
            this.itmSaveAs,
            this.menuItem12,
            this.itmLastProj,
            this.itmAutosave,
            this.menuItem3,
            this.menuItem11,
            this.itmAddPic,
            this.itmDelPic,
            this.menuItem9,
            this.itmExportPic,
            this.itmExportAll,
            this.itmExportQuality});
            this.menuItem1.Text = "File";
            // 
            // itmNewProject
            // 
            this.itmNewProject.Index = 0;
            this.itmNewProject.Text = "New Project";
            this.itmNewProject.Click += new System.EventHandler(this.itmNewProject_Click);
            // 
            // itmOpen
            // 
            this.itmOpen.Index = 1;
            this.itmOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.itmOpen.Text = "Open...";
            this.itmOpen.Click += new System.EventHandler(this.itmOpen_Click);
            // 
            // itmSave
            // 
            this.itmSave.Index = 2;
            this.itmSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.itmSave.Text = "Save";
            this.itmSave.Click += new System.EventHandler(this.itmSave_Click);
            // 
            // itmSaveAs
            // 
            this.itmSaveAs.Index = 3;
            this.itmSaveAs.Text = "Save As...";
            this.itmSaveAs.Click += new System.EventHandler(this.itmSaveAs_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 4;
            this.menuItem12.Text = "-";
            // 
            // itmLastProj
            // 
            this.itmLastProj.Index = 5;
            this.itmLastProj.Text = "Open last project on startup";
            this.itmLastProj.Click += new System.EventHandler(this.itmLastProj_Click);
            // 
            // itmAutosave
            // 
            this.itmAutosave.Index = 6;
            this.itmAutosave.Text = "Autosave on picture change";
            this.itmAutosave.Click += new System.EventHandler(this.itmAutosave_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 7;
            this.menuItem3.Text = "-";
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 8;
            this.menuItem11.Text = "Add arc...";
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click_1);
            // 
            // itmAddPic
            // 
            this.itmAddPic.Index = 9;
            this.itmAddPic.Text = "Add Pictures...";
            this.itmAddPic.Click += new System.EventHandler(this.itmAddPic_Click);
            // 
            // itmDelPic
            // 
            this.itmDelPic.Index = 10;
            this.itmDelPic.Text = "Delete Picture";
            this.itmDelPic.Click += new System.EventHandler(this.itmDelPic_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 11;
            this.menuItem9.Text = "-";
            // 
            // itmExportPic
            // 
            this.itmExportPic.Index = 12;
            this.itmExportPic.Text = "Export Picture...";
            this.itmExportPic.Click += new System.EventHandler(this.itmExportPic_Click);
            // 
            // itmExportAll
            // 
            this.itmExportAll.Index = 13;
            this.itmExportAll.Text = "Export All...";
            this.itmExportAll.Click += new System.EventHandler(this.itmExportAll_Click);
            // 
            // itmExportQuality
            // 
            this.itmExportQuality.Index = 14;
            this.itmExportQuality.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmQuality10,
            this.itmQuality20,
            this.itmQuality30,
            this.itmQuality40,
            this.itmQuality50,
            this.itmQuality60,
            this.itmQuality70,
            this.itmQuality80,
            this.itmQuality90,
            this.itmQuality100});
            this.itmExportQuality.Text = "Export Quality";
            // 
            // itmQuality10
            // 
            this.itmQuality10.Index = 0;
            this.itmQuality10.RadioCheck = true;
            this.itmQuality10.Text = "10%";
            this.itmQuality10.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality20
            // 
            this.itmQuality20.Index = 1;
            this.itmQuality20.RadioCheck = true;
            this.itmQuality20.Text = "20%";
            this.itmQuality20.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality30
            // 
            this.itmQuality30.Index = 2;
            this.itmQuality30.RadioCheck = true;
            this.itmQuality30.Text = "30%";
            this.itmQuality30.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality40
            // 
            this.itmQuality40.Index = 3;
            this.itmQuality40.RadioCheck = true;
            this.itmQuality40.Text = "40%";
            this.itmQuality40.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality50
            // 
            this.itmQuality50.Index = 4;
            this.itmQuality50.RadioCheck = true;
            this.itmQuality50.Text = "50%";
            this.itmQuality50.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality60
            // 
            this.itmQuality60.Index = 5;
            this.itmQuality60.RadioCheck = true;
            this.itmQuality60.Text = "60%";
            this.itmQuality60.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality70
            // 
            this.itmQuality70.Index = 6;
            this.itmQuality70.RadioCheck = true;
            this.itmQuality70.Text = "70%";
            this.itmQuality70.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality80
            // 
            this.itmQuality80.Index = 7;
            this.itmQuality80.RadioCheck = true;
            this.itmQuality80.Text = "80%";
            this.itmQuality80.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality90
            // 
            this.itmQuality90.Index = 8;
            this.itmQuality90.RadioCheck = true;
            this.itmQuality90.Text = "90%";
            this.itmQuality90.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // itmQuality100
            // 
            this.itmQuality100.Index = 9;
            this.itmQuality100.RadioCheck = true;
            this.itmQuality100.Text = "100%";
            this.itmQuality100.Click += new System.EventHandler(this.itmQuality_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmRectangle,
            this.itmEllipse,
            this.itmPolygon,
            this.itmTextLine,
            this.itmDelShape,
            this.itmBringToFront,
            this.itmSendToBack,
            this.itmInvert,
            this.menuItem14,
            this.itmUppercase,
            this.itmLowercase,
            this.itmHideUnusedFonts,
            this.menuItem16,
            this.itmBuffer,
            this.itmBufferNext,
            this.itmBufferPrev,
            this.menuItem10,
            this.itmProjectProperties});
            this.menuItem5.Text = "Edit";
            // 
            // itmRectangle
            // 
            this.itmRectangle.Index = 0;
            this.itmRectangle.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.itmRectangle.Text = "New Rectangle";
            this.itmRectangle.Click += new System.EventHandler(this.itmNewShape_Click);
            // 
            // itmEllipse
            // 
            this.itmEllipse.Index = 1;
            this.itmEllipse.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.itmEllipse.Text = "New Ellipse";
            this.itmEllipse.Click += new System.EventHandler(this.itmNewShape_Click);
            // 
            // itmPolygon
            // 
            this.itmPolygon.Index = 2;
            this.itmPolygon.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.itmPolygon.Text = "New Polygon";
            this.itmPolygon.Click += new System.EventHandler(this.itmNewShape_Click);
            // 
            // itmTextLine
            // 
            this.itmTextLine.Index = 3;
            this.itmTextLine.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.itmTextLine.Text = "New Text Line";
            this.itmTextLine.Click += new System.EventHandler(this.itmNewShape_Click);
            // 
            // itmDelShape
            // 
            this.itmDelShape.Index = 4;
            this.itmDelShape.Shortcut = System.Windows.Forms.Shortcut.CtrlDel;
            this.itmDelShape.Text = "Delete Shape";
            this.itmDelShape.Click += new System.EventHandler(this.itmDelShape_Click);
            // 
            // itmBringToFront
            // 
            this.itmBringToFront.Index = 5;
            this.itmBringToFront.Text = "Bring to Front";
            this.itmBringToFront.Click += new System.EventHandler(this.itmBringToFront_Click);
            // 
            // itmSendToBack
            // 
            this.itmSendToBack.Index = 6;
            this.itmSendToBack.Text = "Send to Back";
            this.itmSendToBack.Click += new System.EventHandler(this.itmSendToBack_Click);
            // 
            // itmInvert
            // 
            this.itmInvert.Index = 7;
            this.itmInvert.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.itmInvert.Text = "Invert Colors";
            this.itmInvert.Click += new System.EventHandler(this.itmInvert_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 8;
            this.menuItem14.Text = "-";
            // 
            // itmUppercase
            // 
            this.itmUppercase.Index = 9;
            this.itmUppercase.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.itmUppercase.Text = "Text to Uppercase";
            this.itmUppercase.Click += new System.EventHandler(this.itmUppercase_Click);
            // 
            // itmLowercase
            // 
            this.itmLowercase.Index = 10;
            this.itmLowercase.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.itmLowercase.Text = "Text to Lowercase";
            this.itmLowercase.Click += new System.EventHandler(this.itmLowercase_Click);
            // 
            // itmHideUnusedFonts
            // 
            this.itmHideUnusedFonts.Index = 11;
            this.itmHideUnusedFonts.Text = "Hide unused fonts";
            this.itmHideUnusedFonts.Click += new System.EventHandler(this.itmHideUnusedFonts_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 12;
            this.menuItem16.Text = "-";
            // 
            // itmBuffer
            // 
            this.itmBuffer.Index = 13;
            this.itmBuffer.Text = "Text Buffer...";
            this.itmBuffer.Click += new System.EventHandler(this.itmBuffer_Click);
            // 
            // itmBufferNext
            // 
            this.itmBufferNext.Index = 14;
            this.itmBufferNext.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
            this.itmBufferNext.Text = "Next from Buffer";
            this.itmBufferNext.Click += new System.EventHandler(this.itmBufferNext_Click);
            // 
            // itmBufferPrev
            // 
            this.itmBufferPrev.Index = 15;
            this.itmBufferPrev.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
            this.itmBufferPrev.Text = "Previous from Buffer";
            this.itmBufferPrev.Click += new System.EventHandler(this.itmBufferPrev_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 16;
            this.menuItem10.Text = "-";
            // 
            // itmProjectProperties
            // 
            this.itmProjectProperties.Index = 17;
            this.itmProjectProperties.Text = "Project Properties...";
            this.itmProjectProperties.Click += new System.EventHandler(this.itmProjectProperties_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem8,
            this.itmPrevPic,
            this.itmNextPic,
            this.menuItem7,
            this.itmZoom100,
            this.itmZoomFit,
            this.itmZoomIn,
            this.itmZoomOut,
            this.menuItem4,
            this.itmNoShapes,
            this.itmShapes,
            this.itmText,
            this.itmView});
            this.menuItem2.Text = "View";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.itmToolbar,
            this.itmDesignPanel});
            this.menuItem6.Text = "Controls";
            // 
            // itmToolbar
            // 
            this.itmToolbar.Index = 0;
            this.itmToolbar.Text = "Toolbar";
            this.itmToolbar.Click += new System.EventHandler(this.itmToolbar_Click);
            // 
            // itmDesignPanel
            // 
            this.itmDesignPanel.Index = 1;
            this.itmDesignPanel.Text = "Design Panel";
            this.itmDesignPanel.Click += new System.EventHandler(this.itmDesignPanel_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "-";
            // 
            // itmPrevPic
            // 
            this.itmPrevPic.Index = 2;
            this.itmPrevPic.Text = "Previous Picture";
            this.itmPrevPic.Click += new System.EventHandler(this.itmPrevPic_Click);
            // 
            // itmNextPic
            // 
            this.itmNextPic.Index = 3;
            this.itmNextPic.Text = "Next Picture";
            this.itmNextPic.Click += new System.EventHandler(this.itmNextPic_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 4;
            this.menuItem7.Text = "-";
            // 
            // itmZoom100
            // 
            this.itmZoom100.Index = 5;
            this.itmZoom100.Text = "Actual Size";
            this.itmZoom100.Click += new System.EventHandler(this.itmZoom100_Click);
            // 
            // itmZoomFit
            // 
            this.itmZoomFit.Index = 6;
            this.itmZoomFit.Text = "Best Fit";
            this.itmZoomFit.Click += new System.EventHandler(this.itmZoomFit_Click);
            // 
            // itmZoomIn
            // 
            this.itmZoomIn.Index = 7;
            this.itmZoomIn.Text = "Zoom In";
            this.itmZoomIn.Click += new System.EventHandler(this.itmZoomIn_Click);
            // 
            // itmZoomOut
            // 
            this.itmZoomOut.Index = 8;
            this.itmZoomOut.Text = "Zoom Out";
            this.itmZoomOut.Click += new System.EventHandler(this.itmZoomOut_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 9;
            this.menuItem4.Text = "-";
            // 
            // itmNoShapes
            // 
            this.itmNoShapes.Index = 10;
            this.itmNoShapes.RadioCheck = true;
            this.itmNoShapes.Text = "No Shapes";
            this.itmNoShapes.Click += new System.EventHandler(this.itmMode_Click);
            // 
            // itmShapes
            // 
            this.itmShapes.Index = 11;
            this.itmShapes.RadioCheck = true;
            this.itmShapes.Shortcut = System.Windows.Forms.Shortcut.F10;
            this.itmShapes.Text = "Shapes Mode";
            this.itmShapes.Click += new System.EventHandler(this.itmMode_Click);
            // 
            // itmText
            // 
            this.itmText.Index = 12;
            this.itmText.RadioCheck = true;
            this.itmText.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.itmText.Text = "Text Mode";
            this.itmText.Click += new System.EventHandler(this.itmMode_Click);
            // 
            // itmView
            // 
            this.itmView.Index = 13;
            this.itmView.RadioCheck = true;
            this.itmView.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.itmView.Text = "View Mode";
            this.itmView.Click += new System.EventHandler(this.itmMode_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "");
            this.imageList.Images.SetKeyName(7, "");
            this.imageList.Images.SetKeyName(8, "");
            this.imageList.Images.SetKeyName(9, "");
            this.imageList.Images.SetKeyName(10, "");
            this.imageList.Images.SetKeyName(11, "");
            this.imageList.Images.SetKeyName(12, "");
            this.imageList.Images.SetKeyName(13, "");
            this.imageList.Images.SetKeyName(14, "");
            this.imageList.Images.SetKeyName(15, "");
            this.imageList.Images.SetKeyName(16, "");
            this.imageList.Images.SetKeyName(17, "");
            this.imageList.Images.SetKeyName(18, "");
            this.imageList.Images.SetKeyName(19, "");
            this.imageList.Images.SetKeyName(20, "");
            this.imageList.Images.SetKeyName(21, "");
            this.imageList.Images.SetKeyName(22, "");
            this.imageList.Images.SetKeyName(23, "");
            this.imageList.Images.SetKeyName(24, "");
            this.imageList.Images.SetKeyName(25, "");
            this.imageList.Images.SetKeyName(26, "");
            this.imageList.Images.SetKeyName(27, "");
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.pbMain);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pLeft.Location = new System.Drawing.Point(0, 32);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(314, 385);
            this.pLeft.TabIndex = 3;
            // 
            // pbMain
            // 
            this.pbMain.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(0, 0);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(314, 385);
            this.pbMain.TabIndex = 1;
            this.pbMain.TabStop = false;
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
            this.pbMain.Resize += new System.EventHandler(this.pbMain_Resize);
            // 
            // toolBar
            // 
            this.toolBar.AutoSize = false;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButton1,
            this.btnNew,
            this.btnSave,
            this.btnOpen,
            this.btnExportAll,
            this.btnProjectProperties,
            this.toolBarButton2,
            this.toolBarButton9,
            this.btnPrev,
            this.btnNext,
            this.toolBarButton6,
            this.toolBarButton8,
            this.btnZoom100,
            this.btnZoomFit,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolBarButton7,
            this.toolBTest,
            this.btnRectangle,
            this.btnEllipse,
            this.btnPolygon,
            this.btnTextLine,
            this.btnDelShape,
            this.toolBarButton4,
            this.toolBarButton5,
            this.btnNoShapes,
            this.btnShapes,
            this.btnText,
            this.btnView});
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(536, 32);
            this.toolBar.TabIndex = 5;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnNew
            // 
            this.btnNew.ImageIndex = 0;
            this.btnNew.Name = "btnNew";
            this.btnNew.ToolTipText = "New Project";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 9;
            this.btnSave.Name = "btnSave";
            this.btnSave.ToolTipText = "Save";
            // 
            // btnOpen
            // 
            this.btnOpen.ImageIndex = 8;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ToolTipText = "Open...";
            // 
            // btnExportAll
            // 
            this.btnExportAll.ImageIndex = 20;
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.ToolTipText = "Export All...";
            // 
            // btnProjectProperties
            // 
            this.btnProjectProperties.ImageIndex = 21;
            this.btnProjectProperties.Name = "btnProjectProperties";
            this.btnProjectProperties.ToolTipText = "Project Properties...";
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton9
            // 
            this.toolBarButton9.Name = "toolBarButton9";
            this.toolBarButton9.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnPrev
            // 
            this.btnPrev.ImageIndex = 11;
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.ToolTipText = "Previous Picture";
            // 
            // btnNext
            // 
            this.btnNext.ImageIndex = 12;
            this.btnNext.Name = "btnNext";
            this.btnNext.ToolTipText = "Next Picture";
            // 
            // toolBarButton6
            // 
            this.toolBarButton6.Name = "toolBarButton6";
            this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton8
            // 
            this.toolBarButton8.Name = "toolBarButton8";
            this.toolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnZoom100
            // 
            this.btnZoom100.ImageIndex = 15;
            this.btnZoom100.Name = "btnZoom100";
            this.btnZoom100.ToolTipText = "Actual Size";
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.ImageIndex = 16;
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.ToolTipText = "Best Fit";
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 13;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "Zoom In";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 14;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            // 
            // toolBarButton7
            // 
            this.toolBarButton7.Name = "toolBarButton7";
            this.toolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBTest
            // 
            this.toolBTest.Name = "toolBarButton3";
            // 
            // btnRectangle
            // 
            this.btnRectangle.ImageIndex = 1;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnRectangle.Tag = "1";
            this.btnRectangle.ToolTipText = "New Rectangle";
            // 
            // btnEllipse
            // 
            this.btnEllipse.ImageIndex = 2;
            this.btnEllipse.Name = "btnEllipse";
            this.btnEllipse.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnEllipse.Tag = "2";
            this.btnEllipse.ToolTipText = "New Ellipse";
            // 
            // btnPolygon
            // 
            this.btnPolygon.ImageIndex = 3;
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnPolygon.Tag = "3";
            this.btnPolygon.ToolTipText = "New Polygon";
            // 
            // btnTextLine
            // 
            this.btnTextLine.ImageIndex = 23;
            this.btnTextLine.Name = "btnTextLine";
            this.btnTextLine.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnTextLine.ToolTipText = "New Text Line";
            // 
            // btnDelShape
            // 
            this.btnDelShape.ImageIndex = 10;
            this.btnDelShape.Name = "btnDelShape";
            this.btnDelShape.ToolTipText = "Delete Shape";
            // 
            // toolBarButton4
            // 
            this.toolBarButton4.Name = "toolBarButton4";
            this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnNoShapes
            // 
            this.btnNoShapes.ImageIndex = 22;
            this.btnNoShapes.Name = "btnNoShapes";
            this.btnNoShapes.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnNoShapes.ToolTipText = "No Shapes";
            // 
            // btnShapes
            // 
            this.btnShapes.ImageIndex = 17;
            this.btnShapes.Name = "btnShapes";
            this.btnShapes.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnShapes.ToolTipText = "Shapes Mode";
            // 
            // btnText
            // 
            this.btnText.ImageIndex = 18;
            this.btnText.Name = "btnText";
            this.btnText.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnText.ToolTipText = "Text Mode";
            // 
            // btnView
            // 
            this.btnView.ImageIndex = 19;
            this.btnView.Name = "btnView";
            this.btnView.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnView.ToolTipText = "View Mode";
            // 
            // pRight
            // 
            this.pRight.Controls.Add(this.panel1);
            this.pRight.Controls.Add(this.pText);
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(314, 32);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(222, 385);
            this.pRight.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbPictures);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 215);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 170);
            this.panel1.TabIndex = 9;
            // 
            // lbPictures
            // 
            this.lbPictures.AllowDrop = true;
            this.lbPictures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPictures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbPictures.ItemHeight = 17;
            this.lbPictures.Location = new System.Drawing.Point(0, 17);
            this.lbPictures.Name = "lbPictures";
            this.lbPictures.Size = new System.Drawing.Size(222, 153);
            this.lbPictures.TabIndex = 10;
            this.lbPictures.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbPictures_DrawItem);
            this.lbPictures.SelectedIndexChanged += new System.EventHandler(this.lbPictures_SelectedIndexChanged);
            this.lbPictures.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbPictures_DragDrop);
            this.lbPictures.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbPictures_DragEnter);
            this.lbPictures.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbPictures_KeyDown);
            this.lbPictures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.lbPictures.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbPictures_MouseMove);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Pictures";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pText
            // 
            this.pText.Controls.Add(this.label3);
            this.pText.Controls.Add(this.tbarText);
            this.pText.Controls.Add(this.tbLineHeight);
            this.pText.Controls.Add(this.label4);
            this.pText.Controls.Add(this.tbText);
            this.pText.Controls.Add(this.cmFontFamily);
            this.pText.Controls.Add(this.label1);
            this.pText.Controls.Add(this.tbFontSize);
            this.pText.Controls.Add(this.cmFontSize);
            this.pText.Dock = System.Windows.Forms.DockStyle.Top;
            this.pText.Location = new System.Drawing.Point(0, 0);
            this.pText.Name = "pText";
            this.pText.Size = new System.Drawing.Size(222, 215);
            this.pText.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(10, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sz.";
            // 
            // tbarText
            // 
            this.tbarText.AutoSize = false;
            this.tbarText.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnBold,
            this.btnItalic,
            this.btnAlignment,
            this.btnTextColor,
            this.btnFillColor,
            this.btnSetDefault});
            this.tbarText.Divider = false;
            this.tbarText.Dock = System.Windows.Forms.DockStyle.None;
            this.tbarText.DropDownArrows = true;
            this.tbarText.ImageList = this.imageList;
            this.tbarText.Location = new System.Drawing.Point(121, 57);
            this.tbarText.Name = "tbarText";
            this.tbarText.ShowToolTips = true;
            this.tbarText.Size = new System.Drawing.Size(87, 57);
            this.tbarText.TabIndex = 4;
            this.tbarText.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbarText_ButtonClick);
            // 
            // btnBold
            // 
            this.btnBold.ImageIndex = 6;
            this.btnBold.Name = "btnBold";
            this.btnBold.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnBold.ToolTipText = "Bold";
            // 
            // btnItalic
            // 
            this.btnItalic.ImageIndex = 7;
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnItalic.ToolTipText = "Italic";
            // 
            // btnAlignment
            // 
            this.btnAlignment.ImageIndex = 25;
            this.btnAlignment.Name = "btnAlignment";
            this.btnAlignment.ToolTipText = "Alignment";
            // 
            // btnTextColor
            // 
            this.btnTextColor.ImageIndex = 4;
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.ToolTipText = "Text Color";
            // 
            // btnFillColor
            // 
            this.btnFillColor.ImageIndex = 5;
            this.btnFillColor.Name = "btnFillColor";
            this.btnFillColor.ToolTipText = "Fill Color";
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.ImageIndex = 24;
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.ToolTipText = "Set as Default";
            // 
            // tbLineHeight
            // 
            this.tbLineHeight.Location = new System.Drawing.Point(46, 84);
            this.tbLineHeight.Name = "tbLineHeight";
            this.tbLineHeight.Size = new System.Drawing.Size(40, 21);
            this.tbLineHeight.TabIndex = 2;
            this.tbLineHeight.TextChanged += new System.EventHandler(this.SelAreaChanged);
            this.tbLineHeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            this.tbLineHeight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyUp);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "H.";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(10, 121);
            this.tbText.Multiline = true;
            this.tbText.Name = "tbText";
            this.tbText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbText.Size = new System.Drawing.Size(201, 86);
            this.tbText.TabIndex = 0;
            this.tbText.TextChanged += new System.EventHandler(this.SelAreaChanged);
            this.tbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            this.tbText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyUp);
            // 
            // cmFontFamily
            // 
            this.cmFontFamily.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmFontFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmFontFamily.ItemHeight = 17;
            this.cmFontFamily.Location = new System.Drawing.Point(10, 26);
            this.cmFontFamily.Name = "cmFontFamily";
            this.cmFontFamily.Size = new System.Drawing.Size(201, 23);
            this.cmFontFamily.TabIndex = 1;
            this.cmFontFamily.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmFontFamily_DrawItem);
            this.cmFontFamily.SelectedIndexChanged += new System.EventHandler(this.SelAreaChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shape Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbFontSize
            // 
            this.tbFontSize.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbFontSize.Location = new System.Drawing.Point(46, 58);
            this.tbFontSize.Name = "tbFontSize";
            this.tbFontSize.Size = new System.Drawing.Size(27, 21);
            this.tbFontSize.TabIndex = 6;
            this.tbFontSize.TextChanged += new System.EventHandler(this.SelAreaChanged);
            this.tbFontSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyDown);
            this.tbFontSize.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbText_KeyUp);
            // 
            // cmFontSize
            // 
            this.cmFontSize.DropDownWidth = 48;
            this.cmFontSize.Items.AddRange(new object[] {
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30",
            "32"});
            this.cmFontSize.Location = new System.Drawing.Point(46, 58);
            this.cmFontSize.Name = "cmFontSize";
            this.cmFontSize.Size = new System.Drawing.Size(48, 20);
            this.cmFontSize.TabIndex = 5;
            this.cmFontSize.SelectedIndexChanged += new System.EventHandler(this.cmFontSize_SelectedIndexChanged);
            // 
            // saveDialog
            // 
            this.saveDialog.FileName = "doc1";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(536, 417);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.toolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Overlay";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.pLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.pRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pText.ResumeLayout(false);
            this.pText.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		// Public declarations

		public const string Version = "1.5.2";

		public RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\" +
			"Wredosoft\\Overlay");
		Cursor curHand = new Cursor(typeof(frmMain), "grip.cur");
		Cursor curPoint = new Cursor(typeof(frmMain), "point.cur");
		public static PrivateFontCollection PrivateFonts;
		public static string[] TextBuffer = new string[0];
		public static int BufferPos = 0;

		// Private declarations

		private static string startupProject = "";

		private const int bmpDoc = 0;
		private const int bmpTextColor = 4;
		private const int bmpFillColor = 5;
		private const int bmpCenterAlign = 25;
		private const int bmpLeftAlign = 26;
		private const int bmpRightAlign = 27;

		private int currentScan = -1;
		private bool changingImage = false;

		private Project project;
		private AreaList Areas = new AreaList();
    
		bool filling = false;
		bool keyLock = false;
		bool modified = false;
        

		// Form handling methods

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			Display.Handle = pbMain.Handle;

			// Restore window position
			if (reg.GetValue("Top") != null)
				Top = Convert.ToInt32(reg.GetValue("Top"));
			if (reg.GetValue("Left") != null)
				Left = Convert.ToInt32(reg.GetValue("Left"));
			if (reg.GetValue("Width") != null)
				Width = Convert.ToInt32(reg.GetValue("Width"));
			if (reg.GetValue("Height") != null)
				Height = Convert.ToInt32(reg.GetValue("Height"));
			if ((reg.GetValue("Maximized") != null) && Convert.ToBoolean(reg.GetValue("Maximized"))) 
				WindowState = FormWindowState.Maximized;

			// Load private fonts
			PrivateFonts = new PrivateFontCollection();
			string fontdir = Path.GetDirectoryName(Application.ExecutablePath) + 
				@"\fonts\";
			if (Directory.Exists(fontdir)) 
			{
				string[] fontfiles = Directory.GetFiles(fontdir, "*.ttf");
				for (int i = 0; i < fontfiles.Length; i++)
					PrivateFonts.AddFontFile(fontfiles[i]);
			}

			if (Convert.ToString(reg.GetValue("Mode")) == "None")
			{
				Area.Mode = AreaMode.None;
				itmNoShapes.PerformClick();
			}
			if (Convert.ToString(reg.GetValue("Mode")) == "Shapes")
			{
				Area.Mode = AreaMode.Shapes;
				itmShapes.PerformClick();
			}
			else if (Convert.ToString(reg.GetValue("Mode")) == "Text")
			{
				Area.Mode = AreaMode.Text;
				itmText.PerformClick();
			}
			else if (Convert.ToString(reg.GetValue("Mode")) == "View")
			{
				Area.Mode = AreaMode.View;
				itmView.PerformClick();
			}

			for (int i = 0; i < itmExportQuality.MenuItems.Count; i++) 
				itmExportQuality.MenuItems[i].Checked = 
					((string)reg.GetValue("ExportQuality") == itmExportQuality.MenuItems[i].Text);
			if (reg.GetValue("ExportQuality") == null)
				itmQuality100.Checked = true;

			toolBar.Visible = (Convert.ToString(reg.GetValue("Toolbar")) != "False");
			itmToolbar.Checked = toolBar.Visible;
			pRight.Visible = (Convert.ToString(reg.GetValue("DesignPanel")) != "False");
			itmDesignPanel.Checked = pRight.Visible;
			itmHideUnusedFonts.Checked = Convert.ToBoolean(reg.GetValue("HideUnusedFonts"));
			itmAutosave.Checked = Convert.ToBoolean(reg.GetValue("Autosave"));

			FillSelArea();

			itmLastProj.Checked = Convert.ToBoolean(reg.GetValue("OpenLastProj"));

			if (startupProject != "")
				OpenProject(startupProject);
			else if (itmLastProj.Checked)
				OpenProject(Convert.ToString(reg.GetValue("LastProject")));
			else 
				OpenProject("");

			FillFontsCombo();
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !ConfirmSaveModifications();
			
			// Save form position
			if (WindowState != FormWindowState.Maximized) 
			{
				reg.SetValue("Top", Top);
				reg.SetValue("Left", Left);
				reg.SetValue("Width", Width);
				reg.SetValue("Height", Height);
			}
			reg.SetValue("Maximized", (WindowState == FormWindowState.Maximized));
		}

		private bool ConfirmSaveModifications() 
		{
			if (!modified) return true;
			return (MessageBox.Show(this, "You have unsaved changes. Lose them?", 
				"Confirmation", MessageBoxButtons.OKCancel, 
				MessageBoxIcon.Information) == DialogResult.OK);
		}


		// Project handling methods

		private void OpenProject(string filename) 
		{
			if (!ConfirmSaveModifications()) return;

			// Load project file
			if ((filename != "") && File.Exists(filename))
			{
				try 
				{
					project = new Project(filename);
				}
				catch (Exception ex) 
				{
                    MessageBox.Show(this, ex.Message, "File open error:" + filename, 
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					project = new Project();
				}
			}
			else project = new Project();

			SetCaption();

			// Open first picture
			if (project.Scans.Count > 0) LoadScan(0);
			else LoadScan(-1);

			FillScansBox();
			reg.SetValue("LastProject", filename);
		}

		private void SetCaption() 
		{
            Text = "Overlay " + Version + " - "  + project.Name + ", " + 
				Display.Zoom.ToString() + "%";
		}

		private void FillScansBox() 
		{
			lbPictures.Items.Clear();
			for (int i = 0; i < project.Scans.Count; i++) 
			{
				Scan sc = (Scan)project.Scans[i];
				if (sc.Filename == "") lbPictures.Items.Add("no image");
				else lbPictures.Items.Add(sc.Filename);
			}
			lbPictures.SelectedIndex = currentScan;
		}

		private void itmNewProject_Click(object sender, System.EventArgs e)
		{
      OpenProject("");		
		}

		private void itmOpen_Click(object sender, System.EventArgs e)
		{
			if (!ConfirmSaveModifications()) return;
			
			openDialog.Filter = "Overlay projects (*.lay)|*.lay|All files|*.*";
			openDialog.Multiselect = false;
			if (openDialog.ShowDialog() == DialogResult.OK) 
			{
				modified = false;
				OpenProject(openDialog.FileName);
			}
		}

		private void itmSave_Click(object sender, System.EventArgs e)
		{
			if (project.Filename != "") 
			{
				project.Save(project.Filename);
				modified = false;
			}
			else 
				itmSaveAs.PerformClick();
		}

		private void itmSaveAs_Click(object sender, System.EventArgs e)
		{
			saveDialog.FileName = "doc.lay";
			saveDialog.DefaultExt = ".lay";
			saveDialog.Filter = "Overlay projects (*.lay)|*.lay|All files|*.*";
			if (saveDialog.ShowDialog() == DialogResult.OK) 
			{
				project.Save(saveDialog.FileName);
				modified = false;
			}
		}

		private void lbPictures_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
            try
            {
                if (e.Index == -1) return;
                e.DrawBackground();
                imageList.Draw(e.Graphics, e.Bounds.X, e.Bounds.Y, bmpDoc);
                e.Graphics.DrawString(lbPictures.Items[e.Index].ToString(), e.Font,
                    new SolidBrush(e.ForeColor), e.Bounds.X + 18, e.Bounds.Y + 2);
            }
            catch
            {
            }
		}

		private void lbPictures_SelectedIndexChanged(object sender, System.EventArgs e)
		{
      if (!changingImage) LoadScan(lbPictures.SelectedIndex);		
		}

		private void itmPrevPic_Click(object sender, System.EventArgs e)
		{
      if (currentScan > 0)
				LoadScan(currentScan - 1);
		}

		private void itmNextPic_Click(object sender, System.EventArgs e)
		{
			if (currentScan < lbPictures.Items.Count-1)
				LoadScan(currentScan + 1);
		}

		public static void ProcessCommandLine(string[] args) 
		{
			try 
			{
				if (args[0] == "-e") 
				{
					Project project = new Project(args[1]);
					frmExport frm = new frmExport(project, args[2], "100%");
					frm.ShowInTaskbar = true;
					frm.ShowDialog();
					frm.Dispose();
				}
			}
			catch {}
		}

		// Picture handling methods

		private void LoadScan(int index)
		{
			Cursor = Cursors.WaitCursor;
			if (modified && itmAutosave.Checked)
				itmSave.PerformClick();
			changingImage = true;
			currentScan = index;
			
			// Update interface
			itmPrevPic.Enabled = (index > 0);
			btnPrev.Enabled = itmPrevPic.Enabled;
			itmNextPic.Enabled = (index < lbPictures.Items.Count-1);
			btnNext.Enabled = itmNextPic.Enabled;
			
			// Load picture
			Areas = new AreaList();
			Image img = null;
			if (index >= 0) 
			{
				Scan sc = (Scan)project.Scans[index];
				Areas = sc.Areas;
				img = sc.GetImage();
			}
			try 
			{
				Display.SetScan(img, Areas);
			}
			catch (Exception ex) 
			{
				MessageBox.Show("Can't show picture: " + ex.Message, "Error", 
					MessageBoxButtons.OK,	MessageBoxIcon.Error);
				Areas = new AreaList();
				Display.SetScan(null, Areas);
			}

			FillSelArea();
			SetNewShapeMode(ShapeMode.None);
			pbMain.Invalidate();
			changingImage = false;
			Cursor = Cursors.Default;
		}

		private void SetPbCursor(Point hit) 
		{
			if (Area.ReadOnlyMode)
				pbMain.Cursor = Cursors.Default;
			else if (Areas.ShapeMode != ShapeMode.None) 
				pbMain.Cursor = Cursors.Cross;
			else if (Areas.IsAreaPointAt(hit))
				pbMain.Cursor = curPoint;
			else if (Areas.AreaAt(hit) != null)
				pbMain.Cursor = Cursors.SizeAll;
			else 
				pbMain.Cursor = Cursors.Default;
		}

		private void pbMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			IntPtr hdc = e.Graphics.GetHdc();
			Display.DrawToDC(hdc, e.ClipRectangle);
			e.Graphics.ReleaseHdc(hdc);
		}

		private void pbMain_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point hit = Display.ClientToDisplay(e.X, e.Y);

			// Pan
			if (e.Button == MouseButtons.Right) 
			{
				pbMain.Cursor = curHand;
				Display.SetPanStart(e.X, e.Y);
			}
  
			// Area operations
            if ((e.Button == MouseButtons.Left) && (!Area.ReadOnlyMode))
            {
                Areas.ProcessMouseDown(hit);
                FillSelArea();
                pbMain.Invalidate();
            }



		}

		private void pbMain_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point hit = Display.ClientToDisplay(e.X, e.Y);

			// Pan 
			if (e.Button == MouseButtons.Right) 
			{
				Display.PanTo(e.X, e.Y);
				Display.DrawToDC();
				return;
			}

			if (Area.ReadOnlyMode) return;

			// Move areas or points
			if ((e.Button == MouseButtons.Left) || (Areas.ShapeMode == ShapeMode.Polygon)) 
			{
				Areas.ProcessMouseMove(hit);
				modified = (Areas.Selected != null);
			}

			SetPbCursor(hit);
		}

		private void pbMain_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point hit = Display.ClientToDisplay(e.X, e.Y);


			if ((Areas.ShapeMode == ShapeMode.Polygon) || 
				((Areas.ShapeMode != ShapeMode.None) && (e.Button == MouseButtons.Right))) 
				pbMain.Cursor = Cursors.Cross;
			else
			{
				SetNewShapeMode(0);
				SetPbCursor(hit);
			}

			if (e.Button == MouseButtons.Left)
				Areas.ProcessMouseUp(hit);
		}

		private void pbMain_Resize(object sender, System.EventArgs e)
		{
			Display.Bounds = pbMain.ClientRectangle;
			pbMain.Invalidate();
		}

		private void itmZoom100_Click(object sender, System.EventArgs e)
		{
			Display.ZoomActual();
			SetCaption();
			pbMain.Invalidate();
		}

		private void itmZoomFit_Click(object sender, System.EventArgs e)
		{
			Display.ZoomToFit();
			SetCaption();
			pbMain.Invalidate();
		}

		private void itmZoomIn_Click(object sender, System.EventArgs e)
	{	
			Display.ZoomIn();
			SetCaption();
			pbMain.Invalidate();
		}

		private void itmZoomOut_Click(object sender, System.EventArgs e)
		{
			Display.ZoomOut();
			SetCaption();
			pbMain.Invalidate();
		}

		private void itmMode_Click(object sender, System.EventArgs e)
		{
			if (sender == itmShapes) Area.Mode = AreaMode.Shapes;
			else if (sender == itmText) Area.Mode = AreaMode.Text;
			else if (sender == itmView) Area.Mode = AreaMode.View;
			else if (sender == itmNoShapes) Area.Mode = AreaMode.None;
			itmShapes.Checked = (Area.Mode == AreaMode.Shapes);
			itmText.Checked = (Area.Mode == AreaMode.Text);
			itmView.Checked = (Area.Mode == AreaMode.View);
			itmNoShapes.Checked = (Area.Mode == AreaMode.None);
			btnShapes.Pushed = (Area.Mode == AreaMode.Shapes);
			btnText.Pushed = (Area.Mode == AreaMode.Text);
			btnView.Pushed = (Area.Mode == AreaMode.View);
			btnNoShapes.Pushed = (Area.Mode == AreaMode.None);
			reg.SetValue("Mode", Area.Mode);
			pbMain.Invalidate();
			if (Area.Mode == AreaMode.View) 
			{
				Areas.ClearSelection();
				FillSelArea();
			}
			
			bool rdonly = (Area.Mode == AreaMode.None) || (Area.Mode == AreaMode.View);
			itmRectangle.Enabled = !rdonly;
			itmEllipse.Enabled = !rdonly;
			itmPolygon.Enabled = !rdonly;
			itmTextLine.Enabled = !rdonly;
			btnRectangle.Enabled = !rdonly;
			btnEllipse.Enabled = !rdonly;
			btnTextLine.Enabled = !rdonly;
			btnPolygon.Enabled = !rdonly;
			
			Display.UpdateScreenBuffer();
		}

		private void SetNewShapeMode(ShapeMode mode) 
		{
			if (currentScan == -1) mode = ShapeMode.None;
			if ((Areas.ShapeMode == ShapeMode.Polygon) && (Areas.Selected != null) 
				&&(Areas.Selected.GetType() == typeof(PolygonArea))) 
			{
				if (Areas.Selected.Points.Length >= 3)
					((PolygonArea)Areas.Selected).Closed = true;
				else Areas.Remove(Areas.Selected);
			}
			Areas.ShapeMode = mode;
			btnRectangle.Pushed = (mode == ShapeMode.Rectangle);
			btnEllipse.Pushed = (mode == ShapeMode.Ellipse);
			btnPolygon.Pushed = (mode == ShapeMode.Polygon);
			btnTextLine.Pushed = (mode == ShapeMode.TextLine);
			if (mode > 0) 
			{
				pbMain.Cursor = Cursors.Cross;
				Areas.ClearSelection();
				for (int i = 0; i < Areas.Count; i++) 
					((Area)Areas[i]).Selected = false;
			}
			else pbMain.Cursor = Cursors.Default;
			pbMain.Invalidate();
		}

		private MenuItem FindMenuItem(System.Windows.Forms.Menu.MenuItemCollection 
			Items, string Text) 
		{
			if (Text == null) return null;
			for (int i = 0; i < Items.Count; i++) 
			{
				if (Items[i].Text == Text) return Items[i];
				MenuItem res = FindMenuItem(Items[i].MenuItems, Text);
				if (res != null) return res;
			}
			return null;
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			MenuItem item = FindMenuItem(mainMenu.MenuItems, e.Button.ToolTipText);
			if (item != null) item.PerformClick();
		}

		private void itmNewShape_Click(object sender, System.EventArgs e)
		{
			if (sender == itmRectangle) SetNewShapeMode(ShapeMode.Rectangle);
			else if (sender == itmEllipse) SetNewShapeMode(ShapeMode.Ellipse);
			else if (sender == itmPolygon) SetNewShapeMode(ShapeMode.Polygon);
			else if (sender == itmTextLine) SetNewShapeMode(ShapeMode.TextLine);
		}

		private void FillSelArea() 
		{
			filling = true;
			Area selArea = Areas.Selected;
			for (int i = 0; i < tbarText.Buttons.Count; i++)
				tbarText.Buttons[i].Enabled = (selArea != null);
			cmFontFamily.Enabled = (selArea != null);
			tbFontSize.Enabled = (selArea != null);
			tbLineHeight.Enabled = (selArea != null);
			tbText.Enabled = (selArea != null);
			itmDelShape.Enabled = (selArea != null);
			btnDelShape.Enabled = (selArea != null);
			itmBringToFront.Enabled = (selArea != null);
			itmSendToBack.Enabled = (selArea != null);
			itmUppercase.Enabled = (selArea != null);
			itmLowercase.Enabled = (selArea != null);
			itmInvert.Enabled = (selArea != null);
			itmBufferNext.Enabled = (selArea != null);
			itmBufferPrev.Enabled = (selArea != null);

			if (selArea != null) 
			{
				cmFontFamily.SelectedIndex = cmFontFamily.Items.IndexOf(selArea.FontFamily);
				btnBold.Pushed = selArea.FontBold;
				btnItalic.Pushed = selArea.FontItalic;
				DrawButtonColor(bmpTextColor, selArea.TextColor);
				DrawButtonColor(bmpFillColor, selArea.FillColor);
				tbFontSize.Text = selArea.FontSize.ToString();
				cmFontSize.Tag = 1;
				cmFontSize.SelectedIndex = cmFontSize.Items.IndexOf(tbFontSize.Text);
				tbLineHeight.Text = selArea.LineHeight.ToString();
				tbText.Text = selArea.Text;
				switch (selArea.Alignment)
				{
					case StringAlignment.Center:
						btnAlignment.ImageIndex = bmpCenterAlign;
						break;
					case StringAlignment.Near:
						btnAlignment.ImageIndex = bmpLeftAlign;
						break;
					case StringAlignment.Far:
						btnAlignment.ImageIndex = bmpRightAlign;
						break;
				}
				if (pRight.Visible)	tbText.Focus();
			}
			else 
			{
				cmFontFamily.SelectedIndex = -1;
				for (int i = 0; i < tbarText.Buttons.Count; i++)
					tbarText.Buttons[i].Pushed = false;
				tbFontSize.Text = "";
				tbLineHeight.Text = "";
				tbText.Text = "";
				for (int i = 0; i < Areas.Count; i++)
					((Area)Areas[i]).Selected = false;
				if (!lbPictures.Focused) pbMain.Focus();
			}
			cmFontFamily.Refresh();
			filling = false;
		}

		private void DrawButtonColor(int image, Color color) 
		{
			Image img = imageList.Images[image];
			Graphics gr = Graphics.FromImage(img);
			gr.FillRectangle(new SolidBrush(color), 0, 13, 16, 3);
			imageList.Images[image] = img;
			tbarText.Refresh();
		}

		private void tbarText_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (Areas.Selected == null) return;
			if (e.Button == btnTextColor) 
			{
                colorDialog.m_colorPicker.SelectedColor=Areas.Selected.TextColor;
                colorDialog.ShowDialog(this);
				{
					Areas.Selected.TextColor = colorDialog.m_colorPicker.SelectedColor;
					DrawButtonColor(bmpTextColor, colorDialog.m_colorPicker.SelectedColor);
				}
			}
			if (e.Button == btnFillColor)
			{
                colorDialog.m_colorPicker.SelectedColor = Areas.Selected.FillColor;
                colorDialog.ShowDialog(this);
				{
					Areas.Selected.FillColor = colorDialog.m_colorPicker.SelectedColor;
					DrawButtonColor(bmpFillColor, colorDialog.m_colorPicker.SelectedColor);
				}
			}

			if ((e.Button == btnBold) || (e.Button == btnItalic)) 
			{
				Areas.Selected.FontBold = btnBold.Pushed;
				Areas.Selected.FontItalic = btnItalic.Pushed;
				cmFontFamily.Refresh();
			}
			if (e.Button == btnAlignment) 
			{
				if (btnAlignment.ImageIndex == bmpRightAlign)
					btnAlignment.ImageIndex = bmpCenterAlign;
				else btnAlignment.ImageIndex++;
				switch (btnAlignment.ImageIndex) 
				{
					case bmpCenterAlign:
						Areas.Selected.Alignment = StringAlignment.Center;
						break;
					case bmpLeftAlign:
						Areas.Selected.Alignment = StringAlignment.Near;
						break;
					case bmpRightAlign:
						Areas.Selected.Alignment = StringAlignment.Far;
						break;
				}
			}
			if (e.Button == btnSetDefault) 
			{
				Area.DefaultFillColor = Areas.Selected.FillColor;
				Area.DefaultFontBold = Areas.Selected.FontBold;
				Area.DefaultFontFamily = Areas.Selected.FontFamily;
				Area.DefaultFontItalic = Areas.Selected.FontItalic;
				Area.DefaultFontSize = Areas.Selected.FontSize;
				Area.DefaultLineHeight = Areas.Selected.LineHeight;
				Area.DefaultTextColor = Areas.Selected.TextColor;
				Area.DefaultAlignment = Areas.Selected.Alignment;
			}

			Areas.Selected.Recalc(true);
			pbMain.Invalidate();
			modified = true;
		}

		private void cmFontFamily_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if (e.Index < 0) return;
      e.DrawBackground();
			string family = cmFontFamily.Items[e.Index].ToString();
			try 
			{
				FontStyle fs = new FontStyle();
				if (btnBold.Pushed) fs = fs | FontStyle.Bold;
				if (btnItalic.Pushed) fs = fs | FontStyle.Italic;
				Font font = new Font(GetFontFamily(family), 10, fs);
				int hei = (int)e.Graphics.MeasureString(family, font).Height;
				Color color = e.ForeColor;
				foreach (FontFamily ff in PrivateFonts.Families)
					if (family == ff.Name) 
						color = Color.FromKnownColor(KnownColor.GrayText);
				e.Graphics.DrawString(family,	font, 
					new SolidBrush(color), e.Bounds.X, e.Bounds.Y + 
					(e.Bounds.Height - hei) / 2);
			}
			catch 
			{
				e.Graphics.DrawString(family,	e.Font, 
					new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y+2);
			}
		}

		private void SelAreaChanged(object sender, System.EventArgs e)
		{
			Area selArea = Areas.Selected;
			if (filling || (selArea == null)) return;
			selArea.FontFamily = cmFontFamily.Text;
			try 
			{
				selArea.FontSize = (float)Convert.ToDouble(tbFontSize.Text);
				cmFontSize.Tag = 1;
				cmFontSize.SelectedIndex = cmFontSize.Items.IndexOf(tbFontSize.Text);
			}
			catch (FormatException) {}
			try 
			{
				if (tbLineHeight.Text == "") tbLineHeight.Text = "0";
				selArea.LineHeight = Convert.ToInt32(tbLineHeight.Text);
			}
			catch (FormatException) {}
			selArea.Text = tbText.Text;
			selArea.Recalc(true);
			modified = true;
			pbMain.Invalidate();
		}

		private void itmDelShape_Click(object sender, System.EventArgs e)
		{
			Area a = Areas.Selected;
			if (a == null) return;
			Areas.Remove(a);
			SetNewShapeMode(ShapeMode.None);
			FillSelArea();
			Display.UpdateScreenBuffer(a.GetBounds(true));
			pbMain.Invalidate();
			modified = true;
		}

		private void itmAddPic_Click(object sender, System.EventArgs e)
		{
			if (project.Folder == "") 
			{
				MessageBox.Show(this, "Please save project before adding pictures", 
					"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			openDialog.Filter = "Pictures (*.jpg;*.gif)|*.jpg;*.gif|All files|*.*";
			openDialog.InitialDirectory = project.Folder;
			openDialog.Multiselect = true;
			if (openDialog.ShowDialog() == DialogResult.OK) 
			{
				string[] files = openDialog.FileNames;
				for (int i = 0; i < files.Length; i++)
					for (int j = i+1; j < files.Length; j++)
						if (Comparer.Default.Compare(files[i], files[j]) > 0) 
						{
							string s = files[i];
							files[i] = files[j];
							files[j] = s;
						}
				for (int i = 0; i < files.Length; i++) 
				{
					// Find if scan exists
					string path = files[i];
					string sc_name = Path.GetFileName(path);
					for (int j = 0; j < project.Scans.Count; j++)
						if (((Scan)project.Scans[j]).Filename == sc_name) 
						{
							MessageBox.Show(this, sc_name + " already added, skipping",
								"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							sc_name = "";
						}
					if (sc_name == "") continue;

					// Copy picture to project dir
					FileInfo fi1 = new FileInfo(path);
					FileInfo fi2 = new FileInfo(project.Folder + sc_name);
					if (!fi2.Exists || (fi1.Name != fi2.Name) || (fi1.Length != fi2.Length)) 
						try 
						{
							File.Copy(path, project.Folder + sc_name, false);
						}
						catch (Exception ex) 
						{
							MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, 
								MessageBoxIcon.Error);
						}

					Image img;
					try 
					{
						img = Image.FromFile(path);
					}
					catch (OutOfMemoryException) 
					{
						MessageBox.Show(this, "Format not supported", 
							"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					Scan sc = new Scan(project);
					sc.Filename = sc_name;
					sc.PicSize = img.Size;
					project.Scans.Add(sc);
					lbPictures.Items.Add(sc.Filename);
					modified = true;
				}
			}
		}

        private void menuItem11_Click_1(object sender, System.EventArgs e)
        {
            if (project.Folder == "")
            {
                MessageBox.Show(this, "Please save project before adding pictures",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            openDialog.Filter = "arc (*.cbr;*.cbz)|*.rar;*.zip;*.cbr;*.cbz|All files|*.*";
            openDialog.InitialDirectory = project.Folder;
            openDialog.Multiselect = false;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {

                // handle arc

                using (SevenZipExtractor extr = new SevenZipExtractor(openDialog.FileName))
                {
                     
                    foreach (string name in extr.ArchiveFileNames)
                    {
                        if (name.Contains("jpg"))
                        {
                        MemoryStream memStream = new MemoryStream(100);
                        string sc_name = Path.GetFileName(name);
                        try
                        {
                            extr.ExtractFile(name, memStream);

                        }
                        catch
                        {
                        }
                        Image img;
                        try
                        {
                            img = Image.FromStream(memStream);
                        }
                        catch (OutOfMemoryException)
                        {
                            MessageBox.Show(this, "Format not supported",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    Scan sc = new Scan(project);
                    sc.Filename = name;
                    sc.PicSize = img.Size;
                    project.Scans.Add(sc);
                    lbPictures.Items.Add(sc.Filename);
                    modified = true;
                    }
                    }
                }
                project.ArchiveName =Path.GetFileName(openDialog.FileName);
            }
        }

		private void itmDelPic_Click(object sender, System.EventArgs e)
		{
			if (currentScan == -1) return;
			if (MessageBox.Show(this, "All defined shapes will be lost. Are you sure?",
				"Delete picture", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
				== DialogResult.Cancel) return;
			project.Scans.RemoveAt(currentScan);
			lbPictures.Items.RemoveAt(currentScan);
			LoadScan(-1);
			modified = true;
		}

		private void itmExportPic_Click(object sender, System.EventArgs e)
		{
			if (currentScan == -1) return;
			Scan sc = (Scan)project.Scans[currentScan];
			saveDialog.Filter = "Pictures (*.jpg;*.gif)|*.jpg;*.gif|All files|*.*";
			saveDialog.DefaultExt = Path.GetExtension(sc.Filename);
			saveDialog.FileName = "Edited " + sc.Filename;
			if (saveDialog.ShowDialog() == DialogResult.OK) 
			{
				int j = -1;
				if (Path.GetDirectoryName(saveDialog.FileName) == project.Folder) 
				{
					j = project.Scans.Count-1;
					while (j >= 0 && ((Scan)project.Scans[j]).Filename != 
						Path.GetFileName(saveDialog.FileName))
						j--;
				}
				if (j >= 0)
					MessageBox.Show("Can't overwrite picture used in project", "Error",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				else
					((Scan)project.Scans[currentScan]).Export(saveDialog.FileName, true,
						reg.GetValue("ExportQuality"));
			}
		}

		private void itmExportAll_Click(object sender, System.EventArgs e)
		{
			if (project.Scans.Count == 0) return;
			saveDialog.Filter = "Pictures (*.jpg;*.gif)|*.jpg;*.gif|All files|*.*";
			saveDialog.FileName = "All Pictures";
			if (saveDialog.ShowDialog() == DialogResult.OK) 
			{
				frmExport frm = new frmExport(project, 
					Path.GetDirectoryName(saveDialog.FileName) + "\\",
					reg.GetValue("ExportQuality"));
				frm.ShowDialog();
			}
		}

		private void itmToolbar_Click(object sender, System.EventArgs e)
		{
			itmToolbar.Checked = !itmToolbar.Checked;
			toolBar.Visible = itmToolbar.Checked;
			reg.SetValue("Toolbar", toolBar.Visible);
		}

		private void itmDesignPanel_Click(object sender, System.EventArgs e)
		{
			itmDesignPanel.Checked = !itmDesignPanel.Checked;
			pRight.Visible = itmDesignPanel.Checked;
			pbMain.Focus();
			reg.SetValue("DesignPanel", pRight.Visible);
		}

		private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			keyLock = true;
			if (e.KeyCode == Keys.PageDown) itmNextPic.PerformClick();
			else if (e.KeyCode == Keys.PageUp) itmPrevPic.PerformClick();
			else if (e.KeyCode == Keys.Multiply) itmZoomFit.PerformClick();
			else if (e.KeyCode == Keys.Divide) itmZoom100.PerformClick();
			else if (e.KeyCode == Keys.Add) itmZoomIn.PerformClick();
			else if (e.KeyCode == Keys.Subtract) itmZoomOut.PerformClick();
			else if (e.KeyCode == Keys.Escape) 
			{
				if (Areas.ShapeMode != ShapeMode.None)
					itmDelShape.PerformClick();
			}
			else keyLock = false;
			e.Handled = false;
		}

		private void tbText_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (keyLock) ((TextBox)sender).ReadOnly = true;
			((TextBox)sender).BackColor = Color.FromKnownColor(KnownColor.Window);
		}

		private void tbText_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			((TextBox)sender).ReadOnly = false;
		}

		private void itmProjectProperties_Click(object sender, System.EventArgs e)
		{
			frmProject frm = new frmProject();
			if (frm.ShowDialog(this) == DialogResult.OK) 
			{
				modified = true;
				Text = "Overlay " + Version + " - " + project.Name;
			}
			frm.Dispose();
		}

		private void lbPictures_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(currentScan.GetType())) 
			{
				Point p = lbPictures.PointToClient(new Point(e.X, e.Y));
				int index1 = lbPictures.SelectedIndex;
				int index2 = lbPictures.IndexFromPoint(p);
				if (index1 == index2) 
				{
					lbPictures.SelectedIndex = index2;
					if (index2 != currentScan) LoadScan(index2);
					return;
				}
				if ((index1 == -1) || (index2 == -1)) return;
				Scan sc = (Scan)project.Scans[index1];
				project.Scans.Remove(sc);
				project.Scans.Insert(index2, sc);
				FillScansBox();
				lbPictures.Refresh();
				lbPictures.SelectedIndex = index2;
				modified = true;
			}
		}

		private void lbPictures_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				lbPictures.DoDragDrop(currentScan, DragDropEffects.All);
		}

		private void lbPictures_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetDataPresent(currentScan.GetType()))
				e.Effect = DragDropEffects.Move;
		}

		private void itmBringToFront_Click(object sender, System.EventArgs e)
		{
			Area selArea = Areas.Selected;
			if (selArea == null) return;
      Areas.Remove(selArea);
			Areas.Add(selArea);
			Display.UpdateScreenBuffer(selArea.GetBounds(true));
			pbMain.Invalidate();
			modified = true;
		}

		private void itmSendToBack_Click(object sender, System.EventArgs e)
		{
			Area selArea = Areas.Selected;
			if (selArea == null) return;
			Areas.Remove(selArea);
			Areas.Insert(0, selArea);
			Display.UpdateScreenBuffer(selArea.GetBounds(true));
			pbMain.Invalidate();
			modified = true;
		}

		private void lbPictures_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete) 
				itmDelPic.PerformClick();
			e.Handled = keyLock;
		}

		private void cmFontSize_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cmFontSize.Text != "") 
				tbFontSize.Text = cmFontSize.Text;
			if (cmFontSize.Focused && (cmFontSize.Tag.ToString() != "1")) 
				tbFontSize.Focus();
			cmFontSize.Tag = 0;
		}

		private void itmLastProj_Click(object sender, System.EventArgs e)
		{
			itmLastProj.Checked = !itmLastProj.Checked;
			reg.SetValue("OpenLastProj", itmLastProj.Checked);
		}

		private void itmUppercase_Click(object sender, System.EventArgs e)
		{
			if (Areas.Selected == null) return;
			Areas.Selected.Text = Areas.Selected.Text.ToUpper();
			Areas.Selected.Recalc(true);
			FillSelArea();
			pbMain.Invalidate();
			modified = true;
		}

		private void itmLowercase_Click(object sender, System.EventArgs e)
		{
			if (Areas.Selected == null) return;
			Areas.Selected.Text = Areas.Selected.Text.ToLower();
			Areas.Selected.Recalc(true);
			FillSelArea();
			pbMain.Invalidate();
			modified = true;
		}

		public void FillFontsCombo() 
		{
			string text = cmFontFamily.Text;
			cmFontFamily.Items.Clear();
			if (!Convert.ToBoolean(reg.GetValue("HideUnusedFonts"))) 
			{
				FontFamily[] fams = PrivateFonts.Families;
				for (int i = 0; i < fams.Length; i++) 
					cmFontFamily.Items.Add(fams[i].Name);
				InstalledFontCollection ifc = new InstalledFontCollection();
				fams = ifc.Families;
				for (int i = 0; i < fams.Length; i++) 
					if (!cmFontFamily.Items.Contains(fams[i].Name))
						cmFontFamily.Items.Add(fams[i].Name);
			}
			else 
			{
				foreach (Scan sc in project.Scans)
					foreach (Area a in sc.Areas)
						if (!cmFontFamily.Items.Contains(a.FontFamily))
							cmFontFamily.Items.Add(a.FontFamily);
			}
			for (int i = 0; i < cmFontFamily.Items.Count; i++)
				for (int j = i+1; j < cmFontFamily.Items.Count; j++)
					if (Comparer.Default.Compare(cmFontFamily.Items[j], 
						cmFontFamily.Items[i]) < 0)
					{
						object s = cmFontFamily.Items[j];
						cmFontFamily.Items[j] = cmFontFamily.Items[i];
						cmFontFamily.Items[i] = s;
					}
			cmFontFamily.Text = text;
		}

		public static FontFamily GetFontFamily(string name) 
		{
			foreach (FontFamily ff in PrivateFonts.Families)
				if (ff.Name == name) return ff;
			try 
			{
				return new FontFamily(name);
			}
			catch (ArgumentException) 
			{
				return new FontFamily("Arial");
			}
		}

		private void itmHideUnusedFonts_Click(object sender, System.EventArgs e)
		{
			itmHideUnusedFonts.Checked = !itmHideUnusedFonts.Checked;
			reg.SetValue("HideUnusedFonts", itmHideUnusedFonts.Checked);
			FillFontsCombo();
		}

		private void itmAutosave_Click(object sender, System.EventArgs e)
		{
			itmAutosave.Checked = !itmAutosave.Checked;
			reg.SetValue("Autosave", itmAutosave.Checked);
		}

		private void itmInvert_Click(object sender, System.EventArgs e)
		{
			Area sel = Areas.Selected;
			if (sel == null) return;
			Color c = sel.FillColor;
			sel.FillColor = sel.TextColor;
			sel.TextColor = c;
			FillSelArea();
		}

		private void itmBuffer_Click(object sender, System.EventArgs e)
		{
			frmBuffer frm = new frmBuffer();
			frm.ShowDialog();
			frm.Dispose();
		}

		private void itmBufferNext_Click(object sender, System.EventArgs e)
		{
			Area a = Areas.Selected;
			if ((a == null) || (TextBuffer.Length == 0)) return;
			if (BufferPos >= TextBuffer.Length)	BufferPos = 0;
			if (BufferPos < 0) BufferPos = TextBuffer.Length-1;
			a.Text = TextBuffer[BufferPos];
			a.Recalc(true);
			FillSelArea();
			pbMain.Invalidate();
			modified = true;
			BufferPos++;
		}

		private void itmBufferPrev_Click(object sender, System.EventArgs e)
		{
			Area a = Areas.Selected;
			if ((a == null) || (TextBuffer.Length == 0)) return;
			if (BufferPos >= TextBuffer.Length)	BufferPos = 0;
			if (BufferPos < 0) BufferPos = TextBuffer.Length-1;
			a.Text = TextBuffer[BufferPos];
			a.Recalc(true);
			FillSelArea();
			pbMain.Invalidate();
			modified = true;
			BufferPos--;
		}

		private void itmQuality_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < itmExportQuality.MenuItems.Count; i++) 
				itmExportQuality.MenuItems[i].Checked = false;
			reg.SetValue("ExportQuality", ((MenuItem)sender).Text);
			((MenuItem)sender).Checked = true;
		}




	}
}
