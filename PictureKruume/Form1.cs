using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureKruume
{

    public partial class Form1 : Form
    {
        Image imgOriginal;
        int historyCounter;
        bool drawing;
        GraphicsPath currentPath;
        Point oldLocation;
        Pen currentPen;
        SolidBrush solidBrush;


        List<Image> History;
        int figuri = 0;
        int localX = 0;
        int localY = 0;
        int locallXO = 0;
        int locallYO = 0;

        

        


        public Form1()
        {
            
            Point[] points = new Point[8];
            InitializeComponent();
            drawing = false;
            historyColor.historycolor = Color.Black;
            currentPen = new Pen(Color.Black);
            solidBrush = new SolidBrush(Color.Black);

            currentPen.Width = trackBar1.Value;
            History = new List<Image>();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            OpenToolStripMenuItem_Click(sender, e);
        }
        

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {                             
            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Вы желаете сохранить текущий рисунок?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: SaveToolStripMenuItem_Click(sender, e); break;
                    case DialogResult.Cancel: return;
                }
            }
            else { 
            History.Clear();
            historyCounter = 0;
            Bitmap pic = new Bitmap(750, 500);
                pic.MakeTransparent(Color.White);
                picDrawingSurface.Image = pic;
            History.Add(new Bitmap(picDrawingSurface.Image));
            }
            Graphics graphic = Graphics.FromImage(picDrawingSurface.Image);

            graphic.Clear(Color.White);
            picDrawingSurface.Invalidate();
            imgOriginal = picDrawingSurface.Image;
        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog SaveDlg = new SaveFileDialog();
            
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4;
            SaveDlg.ShowDialog();
            if(SaveDlg.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                         this.picDrawingSurface.Image.Save(fs, ImageFormat.Jpeg); break;
                    case 2:
                         this.picDrawingSurface.Image.Save(fs, ImageFormat.Bmp); break;
                    case 3:
                         this.picDrawingSurface.Image.Save(fs, ImageFormat.Gif); break;
                    case 4:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Png); break;
                }
                fs.Close();
            }
            imgOriginal = picDrawingSurface.Image;
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1;
            
            if (OP.ShowDialog() != DialogResult.Cancel)
            
                picDrawingSurface.Load(OP.FileName);

                picDrawingSurface.AutoSize = true;

            imgOriginal = picDrawingSurface.Image;
            }
        

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                var result = MessageBox.Show("Вы желаете сохранить текущий рисунок?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                


                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: Application.Exit(); break;
                    case DialogResult.Cancel: return;
                }
            imgOriginal = picDrawingSurface.Image;

        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            NewToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            SaveToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            ExitToolStripMenuItem_Click(sender, e);
        }

        private void PicDrawingSurface_Click(object sender, EventArgs e)
        {

        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                picDrawingSurface.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count  - 1)
            {
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void SolidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;

            solidToolStripMenuItem.Checked = true;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;

        }

        private void DotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = true;
            dashDotDotToolStripMenuItem.Checked = false;

        }

        private void DashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;


        }

        private void BlackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Black;
            historyColor.historycolor = Color.Black;

            blackToolStripMenuItem.Checked = true;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            purpleToolStripMenuItem.Checked = false;
        }

        private void RedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Red;
            historyColor.historycolor = Color.Red;

            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = true;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            purpleToolStripMenuItem.Checked = false;
        }

        

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            Graphics graphic = Graphics.FromImage(picDrawingSurface.Image);
            
            graphic.Clear(Color.White);
            picDrawingSurface.Invalidate();
        }

        private void ToolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        

        private void StyleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void BoxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;


            figuri = 1;
        }

        private void CircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;


            figuri = 2;
        }

        private void LineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;


            figuri = 3;
        }

        private void PenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = false;
                       

            figuri = 0;
        }

        private void GreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Green;
            historyColor.historycolor = Color.Green;

            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = true;
            blueToolStripMenuItem.Checked = false;
            purpleToolStripMenuItem.Checked = false;
        }

        private void BlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Blue;
            historyColor.historycolor = Color.Blue;

            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = true;
            purpleToolStripMenuItem.Checked = false;
        }

        private void PurpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Purple;
            historyColor.historycolor = Color.Purple;

            blackToolStripMenuItem.Checked = false;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            purpleToolStripMenuItem.Checked = true;
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            picDrawingSurface.Image = Zoom(imgOriginal, trackBar2.Value);


        }
        Image Zoom(Image img, int size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size / 10), img.Height + (img.Height * size / 10));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bmp;

        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PicDrawingSurface_Click_1(object sender, EventArgs e)
        {

        }

        private void ToolStripButton6_Click_1(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Owner = this;
            f.ShowDialog();

        }

        private void ToolStripButton7_Click(object sender, EventArgs e)
        {
            figuri = 2;
        }

        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            figuri = 4;
        }
    }
}
