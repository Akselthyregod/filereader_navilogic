using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace filereader_navilogic
{
    public partial class Form1 : Form
    {

        private int count = 0;
        public Form1()
        {
            InitializeComponent();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private ListViewItem Line_To_Ttem(string[] line)
        {
            ListViewItem item = new ListViewItem(line[0]);

            for (int i = 1; i < line.Length; i++)
            {
                item.SubItems.Add(line[i]);

            }

            return item;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "text files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;

                // read file chosen
                string[] lines = File.ReadAllLines(selectedFileName);

                string sep = "\t";

                // populate with data
                foreach (string line in lines)
                {
                    if( line == lines[0])
                    {
                        string[] column_headers = line.Split(sep.ToCharArray());

                        for (int i = 0; i < column_headers.Length; i++)
                        {
                            listView1.Columns[i].Text = column_headers[i];
                        }

                        continue;

                    }

                    string[] split_Line = line.Split(sep.ToCharArray());
                    listView1.Items.Add(Line_To_Ttem(split_Line));

                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                label1.Text = "Number of lines loaded:" + lines.Length.ToString();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
          
            MessageBox.Show("click");

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
