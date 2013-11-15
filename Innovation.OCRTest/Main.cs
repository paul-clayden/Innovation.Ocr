using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IaG.State.Innovation.Ocr.DtkAnpr;

namespace Iag.State.Innovation.OCRTest
{
    public partial class Main : Form
    {
        private Bitmap _theImage;
        private string _dataFolder = @"tessdata";
        public Main()
        {
            InitializeComponent();
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            _theImage = OpenImageFile();
            OcrProcessImage();
        }


        private Bitmap OpenImageFile()
        {
            try
            {
                openFileDialog1.FileName = null;
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    
                    openFileDialog1.RestoreDirectory = true;
                    ImageFilePathTextBox.Text = openFileDialog1.FileName;
                    return new Bitmap(openFileDialog1.OpenFile());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());
            }
            return null;
        }

        private void ImageFilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(ImageFilePathTextBox.Text))
            {
                _theImage = new Bitmap(ImageFilePathTextBox.Text);
                OcrProcessImage();
            }
                
        }


        private void OcrProcessImage()
        {
            try
            {
                if (_theImage != null)
                {
                    pictureBox1.SuspendLayout();
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = _theImage;
                    pictureBox1.ResumeLayout();

                    const string language = "eng";

                    IOcrProcessor processor = new DtkOcrProcessor();

                    using (var bmp = _theImage)
                    {
                        var result = processor.ReadText(_theImage);
                        if (string.IsNullOrEmpty(result))
                        {
                            ResultsTextBox.Text = "Failed to read plate.";
                        }
                        else
                        {
                            ResultsTextBox.Text = "";
                            var sb = new StringBuilder();
                            string text = result;
                            sb.AppendLine("Text:");
                            sb.AppendLine("*****************************");
                            sb.AppendLine(text);
                            sb.AppendLine("*****************************");
                            ResultsTextBox.Text = sb.ToString();
                        }
                    }

                }
                else
                {
                    MessageBox.Show(this, "Please select an image");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());
            }
        }

    }
}
