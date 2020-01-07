using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureKruume
{
    public partial class Form2 : Form
    {
        Color colorResult;
        Color color;
        public Form2()
        {
            InitializeComponent();
            Scroll_Red.Tag = numeric_Red;
            Scroll_Green.Tag = numeric_Green;
            Scroll_Blue.Tag = numeric_Blue;

            numeric_Red.Tag = Scroll_Red;
            numeric_Blue.Tag = Scroll_Green;
            numeric_Green.Tag = Scroll_Green;

            numeric_Red.Value = color.R;
            numeric_Blue.Value = color.G;
            numeric_Green.Value = color.B;
            Form1 main = this.Owner as Form1;

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Scroll_Red_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            UpdateColor();

        }

        private void Numeric_Red_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
            UpdateColor();
        }

        private void Scroll_Green_Scroll(object sender, ScrollEventArgs e)
        {
            Scroll_Red_Scroll(sender,e);
        }

        private void Scroll_Blue_Scroll(object sender, ScrollEventArgs e)
        {
            Scroll_Red_Scroll(sender, e);
        }

        private void Numeric_Green_ValueChanged(object sender, EventArgs e)
        {
            Numeric_Red_ValueChanged( sender, e);
        }

        private void Numeric_Blue_ValueChanged(object sender, EventArgs e)
        {
            Numeric_Red_ValueChanged(sender, e);
        }
        private void UpdateColor()
        {
            colorResult = Color.FromArgb(Scroll_Red.Value, Scroll_Green.Value, Scroll_Blue.Value);
            
            picResultColor.BackColor = colorResult;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Scroll_Red.Value = colorDialog.Color.R;
                Scroll_Green.Value = colorDialog.Color.G;
                Scroll_Blue.Value = colorDialog.Color.B;
                
                colorResult = colorDialog.Color;
                
                UpdateColor();




            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            historyColor.historycolor = colorResult;
            this.Close();
        }
    }
}
