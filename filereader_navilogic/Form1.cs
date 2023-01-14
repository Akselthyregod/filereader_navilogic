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
using System.Collections;

namespace filereader_navilogic
{
    public partial class Form1 : Form
    {

        //private int count = 0;
        private ListViewColumnSorter lvwColumnSorter;

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
                // clear old data
                this.listView1.ListViewItemSorter = null;
                this.listView1.Items.Clear();


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
               
                lvwColumnSorter = new ListViewColumnSorter();
                this.listView1.ListViewItemSorter = lvwColumnSorter;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
            
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            
            // Call FindItemWithText with the contents of the textbox.
            ListViewItem foundItem =
                listView1.FindItemWithText(searchBox.Text, true, 0, true);
            
            if (foundItem != null)
            {
                listView1.TopItem = foundItem;
            }
            
        }

    }
}
