using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Image_Gallery_Demo.Properties;
using C1.Win.C1Tile;

namespace Image_Gallery_Demo
{
    public partial class Image_Gallery_Demo : Form
    {

        public SplitContainer splitContainer1 = new SplitContainer();
        public TableLayoutPanel tableLayoutPanel1 = new TableLayoutPanel();
        public Panel panel1 = new Panel();
        public Panel panel2 = new Panel();
        public Panel panel3 = new Panel();
        public TextBox SearchBoxtxt = new TextBox();
        public PictureBox SearchImageIcon = new PictureBox();
        public PictureBox DownloadImageIcon = new PictureBox();
        public PictureBox ExporttoPdfIcon = new PictureBox();
        public C1.Win.C1Tile.C1TileControl tileControl1 = new C1.Win.C1Tile.C1TileControl();
        public C1.Win.C1Tile.Group group1 = new C1.Win.C1Tile.Group();
        public C1.C1Pdf.C1PdfDocument pdfDocument1 = new C1.C1Pdf.C1PdfDocument();
        public StatusStrip statusStrip1 = new System.Windows.Forms.StatusStrip();
        public ToolStripProgressBar progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
        public C1.Win.C1Tile.Tile img1 = new Tile();
        public C1.Win.C1Tile.Tile img2 = new Tile();


        //Instance of DataFechter class and a list of ImageItem class.
        DataFetcher datafetch1 = new DataFetcher();
        List<ImageItem> imagesList1 = new List<ImageItem>();
        int checkedItems = 0;

        private void ImageGallery_Load(object sender, EventArgs e)
        {
            void controls1()
            {
                this.Text = "Image Gallery";
                this.MaximizeBox = false;
                this.Size = new System.Drawing.Size(780, 780);
                this.MaximumSize = new System.Drawing.Size(810, 810);
                this.ShowIcon = false;
                this.Controls.Add(splitContainer1);

                this.splitContainer1.Dock = DockStyle.Fill;
                this.splitContainer1.Panel1.Show();
                this.splitContainer1.Panel2.Show();
                this.splitContainer1.SplitterDistance = 40;
                this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
                this.splitContainer1.Visible = true;
                this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
                this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
                this.splitContainer1.IsSplitterFixed = true;
                this.splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
                this.splitContainer1.Panel2.Controls.Add(tileControl1);
                this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);

                this.tableLayoutPanel1.ColumnCount = 3;
                this.tableLayoutPanel1.Dock = DockStyle.Fill;
                this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
                this.tableLayoutPanel1.RowCount = 1;
                this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 40);
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
                this.tableLayoutPanel1.Controls.Add(panel1, 1, 0);
                this.tableLayoutPanel1.Controls.Add(panel2, 2, 0);
                this.tableLayoutPanel1.Controls.Add(panel3, 0, 0);

                this.panel1.Location = new System.Drawing.Point(477, 0);
                this.panel1.Size = new System.Drawing.Size(287, 40);
                this.panel1.Dock = DockStyle.Fill;
                this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnSearchPanelPaint);
                this.panel1.Controls.Add(SearchBoxtxt);

                this.SearchBoxtxt.Name = "_searchBox";
                this.SearchBoxtxt.BorderStyle = 0;
                this.SearchBoxtxt.Dock = DockStyle.Fill;
                this.SearchBoxtxt.Location = new System.Drawing.Point(16, 9);
                this.SearchBoxtxt.Size = new System.Drawing.Size(244, 16);
                this.SearchBoxtxt.Text = "Search Image";
                this.SearchBoxtxt.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);


                this.panel2.Location = new System.Drawing.Point(479, 12);
                this.panel2.Margin = new System.Windows.Forms.Padding(2, 12, 45, 12);
                this.panel2.Size = new System.Drawing.Size(40, 16);
                this.panel2.TabIndex = 1;
                this.panel2.Controls.Add(SearchImageIcon);

                this.SearchImageIcon.Name = "_search";
                this.SearchImageIcon.Image = global::Image_Gallery_Demo.Properties.Resources.searchicon;
                this.SearchImageIcon.Dock = DockStyle.Left;
                this.SearchImageIcon.Location = new System.Drawing.Point(0, 0);
                this.SearchImageIcon.Margin = new System.Windows.Forms.Padding(0);
                this.SearchImageIcon.Size = new System.Drawing.Size(40, 16);
                this.SearchImageIcon.SizeMode = PictureBoxSizeMode.Zoom;
                this.SearchImageIcon.BorderStyle = BorderStyle.FixedSingle;
                this.SearchImageIcon.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
                this.SearchImageIcon.Click += new System.EventHandler(this.searchimg_Click);


                this.panel3.Dock = DockStyle.Fill;
                this.panel3.TabIndex = 1;
                this.panel3.Controls.Add(ExporttoPdfIcon);
                this.panel3.Controls.Add(DownloadImageIcon);
                this.panel3.TabIndex = 2;

                this.ExporttoPdfIcon.Name = "_exportImage";
                this.ExporttoPdfIcon.Image = global::Image_Gallery_Demo.Properties.Resources.expdf;
                this.ExporttoPdfIcon.Location = new System.Drawing.Point(0, 3);
                this.ExporttoPdfIcon.Size = new System.Drawing.Size(100, 30);
                this.ExporttoPdfIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                this.ExporttoPdfIcon.Click += new System.EventHandler(this.OnExportClick);
                this.ExporttoPdfIcon.Visible = false;
                this.ExporttoPdfIcon.BorderStyle = BorderStyle.FixedSingle;
                this.ExporttoPdfIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.OnExportImagePaint);

                this.DownloadImageIcon.Name = "_DownloadImage";
                this.DownloadImageIcon.Image = global::Image_Gallery_Demo.Properties.Resources.downbad;
                this.DownloadImageIcon.Location = new System.Drawing.Point(105, 3);
                this.DownloadImageIcon.Size = new System.Drawing.Size(100, 30);
                this.DownloadImageIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                this.DownloadImageIcon.Click += new System.EventHandler(this.DownloadImage_Click);
                this.DownloadImageIcon.Visible = false;
                this.DownloadImageIcon.BorderStyle = BorderStyle.FixedSingle;


                this.tileControl1.CellHeight = 78;
                this.tileControl1.CellSpacing = 11;
                this.tileControl1.CellWidth = 78;
                this.tileControl1.Name = "tileControl1";
                this.tileControl1.Dock = DockStyle.Fill;
                this.tileControl1.Size = new System.Drawing.Size(764, 718);
                this.tileControl1.SurfacePadding = new System.Windows.Forms.Padding(12, 4, 12, 4);
                this.tileControl1.SwipeDistance = 20;
                this.tileControl1.SwipeRearrangeDistance = 98;
                this.tileControl1.Groups.Add(this.group1);
                this.tileControl1.TileChecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this.OnTileChecked);
                this.tileControl1.TileUnchecked += new System.EventHandler<C1.Win.C1Tile.TileEventArgs>(this.OnTileUnchecked);
                this.tileControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnTileControlPaint);
                this.tileControl1.AllowChecking = true;
                this.tileControl1.Orientation = LayoutOrientation.Vertical;

                this.statusStrip1.Visible = false;
                this.statusStrip1.Dock = DockStyle.Bottom;
                this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.progressBar1 });
                this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;

                this.group1.Name = "Group1";
                this.group1.Tiles.Add(this.img1);
                this.group1.Tiles.Add(this.img2);
                this.img1.Image = global::Image_Gallery_Demo.Properties.Resources.whiteback;
                this.img2.Image = global::Image_Gallery_Demo.Properties.Resources.whiteback;

            }
            controls1();

        }

        public Image_Gallery_Demo()
        {

            InitializeComponent();
        }

        //To fetch the images using DataFetcher class.
        private async void searchimg_Click(object sender, EventArgs e)
        {

            statusStrip1.Visible = true;
            imagesList1 = await
            datafetch1.GetImageData(SearchBoxtxt.Text);
            AddTiles(imagesList1);
            statusStrip1.Visible = false;

        }
        //Loop through all the images and add it to the tile control
        private void AddTiles(List<ImageItem> imageList1)
        {
            tileControl1.Groups[0].Tiles.Clear();

            foreach (var imageitem in imageList1)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                tileControl1.Groups[0].Tiles.Add(tile);

                //Converts the base64 encoding to the corresponding image using MemoryStream class.
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }


        //Callback for click event for export button.
        private void OnExportClick(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in tileControl1.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {

                pdfDocument1.Save(saveFile.FileName);

            }

        }

        //Iterates through all the tiles, gets it’s image and save this list of images to disk with given ImageName and Location.
        private void DownloadImage_Click(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in tileControl1.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }

            SaveFileDialog s1 = new SaveFileDialog();
            s1.DefaultExt = "jpg";
            s1.Filter = "jpg files (*.jpg)|*.jpg";
            if (s1.ShowDialog() == DialogResult.OK)
            {
                int len;
                string s = s1.FileName;
                len = s.Length;

                //Store Filename to String.
                string fname = "";
                for (int t = 0; t < len - 4; t++)
                {
                    fname += s[t];
                }
                int count = 1;

                //Give unique FileName if there are Multiple Images. 
                foreach (var selectedimg in images)
                {
                    string temp = fname;
                    if (count != 1)
                    {
                        temp += count;
                    }
                        selectedimg.Save(temp + ".jpg");
                    count++;
                }
            }

        }

        //iterates through all the tiles, gets it’s image and convert this list of images to PDF.
        private void ConvertToPdf(List<Image> images)
        {
            RectangleF rect = pdfDocument1.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    pdfDocument1.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                // opens a SaveFileDialog to save the image.
                pdfDocument1.DrawImage(selectedimg, rect);
            }

        }
        
        //To add a grey border to the search box.
        public void OnSearchPanelPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle r = SearchBoxtxt.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
        }

        //used for drawing a grey border for export to pdf button.
        public void OnExportImagePaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle r = new Rectangle(ExporttoPdfIcon.Location.X, ExporttoPdfIcon.Location.Y, ExporttoPdfIcon.Width,
            ExporttoPdfIcon.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new
            Point(this.Width, 43));
        }

        //Change Visiblity of ExporttoPDF and DownloadImage Buttons and increment selected items.
        private void OnTileChecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems = checkedItems + 1;
            ExporttoPdfIcon.Visible = true;
            DownloadImageIcon.Visible = true;
        }

        ////Change Visiblity of ExporttoPDF and DownloadImage Buttons and decrement selected items.
        private void OnTileUnchecked(object sender, C1.Win.C1Tile.TileEventArgs e)
        {
            checkedItems = checkedItems - 1;
            ExporttoPdfIcon.Visible = (checkedItems > 0);
            DownloadImageIcon.Visible = (checkedItems > 0);
        }

        //used to draw a separator
        private void OnTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }


    }
}
