using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFMPEG2
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFile1;
        private SaveFileDialog saveFile1;
        public Form1()
        {
            openFile1 = new OpenFileDialog();
            saveFile1 = new SaveFileDialog {
                AddExtension = true,
                DefaultExt = ".mp4",
                Filter = "Video Files | *.mp4;*.mkv;*.wmv;*.webm",
                FilterIndex = 1,
            };
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e){ //select file
            if (openFile1.ShowDialog() == DialogResult.OK){
                try{
                    var sr = openFile1.FileName;
                    SetText(sr);
                    button3.Enabled = true;
                }
                catch (SecurityException ex){
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e){ //convert file
            if (textFile.Text != "" && openFile1.FileName != ""){

                string command1 = "/C ffmpeg " + openFile1.FileName;

                if (textStartT.Text != "") {
                    command1 += "-ss " + textStartT.Text;
                }
                if(textEndT.Text != "") {
                    command1 += " -to " + textEndT.Text;
                }


                command1 += " -i ";

                if (textResX.Text != "" && textResY.Text != "") {
                    command1 += " -s " + textResX.Text + "x" + textResY.Text;
                }
                command1 += " " + saveFile1.FileName;

                System.Diagnostics.Process.Start("CMD.exe",command1);

            } else {
                SetText("No file selected!");
            }
        }

        private void button3_Click(object sender, EventArgs e) { //select save file
            if (saveFile1.ShowDialog() == DialogResult.OK) {
                SetText("Save ok: " + saveFile1.FileName);
                button2.Enabled = true;
            }
        }




        private void number_filter(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;
            }
        }
        private void SetText(string text) {
            textFile.Text = text;
        }


        private void Form1_Load(object sender, EventArgs e){

        }

        private void textFile_TextChanged(object sender, EventArgs e){

        }

        private void textBox5_TextChanged(object sender, EventArgs e) {

        }
    }

    
}
