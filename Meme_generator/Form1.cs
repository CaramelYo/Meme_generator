using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meme_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            total_rbs = new List<List<RadioButton>>();

            foreach (TabPage tp in tabControl1.TabPages)
            {
                List<RadioButton> rbs = new List<RadioButton>();

                foreach (Control c in tp.Controls)
                    if (c.GetType() == typeof(RadioButton))
                    {
                        RadioButton rb = (RadioButton)c;
                        rb.Click += radio_btn_click;

                        rbs.Add(rb);
                    }
                
                total_rbs.Add(rbs);
            }

            r = new Random();

            img_dir = "imgs/";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // check the first radiobutton in every tabpage
            foreach (List<RadioButton> rbs in total_rbs)
                foreach (RadioButton rb in rbs)
                    rb.Checked = false;
            
            foreach (List<RadioButton> rbs in total_rbs)
                rbs[0].Checked = true;
        }

        private void generate()
        {
            // 3 tabpages
            int[] selections = new int[3];

            // record which radiobutton is checked
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 4; ++j)
                    if (total_rbs[i][j].Checked)
                    {
                        selections[i] = j;
                        break;
                    }
            
            picturebox_0.Image = Image.FromFile(img_dir + total_rbs[0][selections[0]].Text + ".png");
            
            FontStyle new_fs;

            switch (selections[1])
            {
                case 0:
                    new_fs = FontStyle.Regular;
                    break;
                case 1:
                    new_fs = FontStyle.Bold;
                    break;
                case 2:
                    new_fs = FontStyle.Italic;
                    break;
                case 3:
                    new_fs = FontStyle.Underline;
                    break;
                default:
                    new_fs = FontStyle.Regular;
                    break;
            }

            sentence_label.Font = new Font(
                        sentence_label.Font.Name,
                        sentence_label.Font.Size,
                        new_fs
                    );
            
            sentence_label.Text = total_rbs[2][selections[2]].Text;

            sentence_label.Left = (Width - sentence_label.Width) / 2;
        }

        private void radio_btn_click(object sender, EventArgs e)
        {
            generate();
        }

        private void random_btn_Click(object sender, EventArgs e)
        {
            foreach (List<RadioButton> rbs in total_rbs)
            {
                int idx = r.Next(rbs.Count);
                rbs[idx].Checked = true;
            }

            generate();
        }

        private void change_font_size(float size)
        {
            sentence_label.Font = new Font(
                sentence_label.Font.Name,
                size,
                sentence_label.Font.Style
            );

            generate();
        }

        private void font_size_plus_btn_Click(object sender, EventArgs e)
        {
            change_font_size(sentence_label.Font.Size + 1);
        }

        private void font_size_minus_btn_Click(object sender, EventArgs e)
        {
            change_font_size(sentence_label.Font.Size - 1);
        }

        List<List<RadioButton>> total_rbs;
        Random r;

        string img_dir;
    }
}
