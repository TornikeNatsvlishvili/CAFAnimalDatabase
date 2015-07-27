using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAF_Animal_Database
{
    public partial class Form3 : Form
    {
        Form1 form1;
        List<Animal> sortedList = new List<Animal>();
        public Form3(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cmbSortBy.SelectedIndex = 0;
            cmbSearchBy.SelectedIndex = 0;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {

            listView1.Items.Clear();
            listView1.Columns.Clear();
            listView1.Groups.Clear();
            sortedList.Clear();
            if (cmbSortBy.SelectedIndex != 0)
            {
                if (cmbSortBy.SelectedIndex == 1)
                {
                    //sort by points
                    foreach (Animal animal in form1.animalList)
                    {
                        Animal temp = new Animal();
                        temp.import(animal);
                        sortedList.Add(temp);
                    }
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("Points"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "Points";
                    listView1.Columns[0].Width = listView1.Width / 2;
                    listView1.Columns[1].Width = listView1.Width / 2 - 10;
                    sortedList.Sort(delegate(Animal first, Animal second)
                    {
                        return second.points.CompareTo(first.points);
                    });
                    foreach (Animal animal in sortedList)
                    {
                        ListViewItem item = new ListViewItem(new string[] { animal.name, animal.points.ToString() });
                        if (chkHighlight.Checked == true && animal.points > nmbLimit.Value)
                        {
                            item.BackColor = Color.PaleVioletRed;
                        }
                        else
                        {
                            item.BackColor = Color.White;
                        }
                        listView1.Items.Add(item);
                    }
                }
                else if (cmbSortBy.SelectedIndex == 2)
                {
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("ID"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "ID";
                    listView1.Columns[0].Width = listView1.Width / 2;
                    listView1.Columns[1].Width = listView1.Width / 2 - 10;
                    foreach (Animal animal in form1.animalList)
                    {
                        Boolean added = false;
                        foreach (ListViewGroup group in listView1.Groups)
                        {
                            if (group.Header.Equals(animal.currentReseacher()))
                            {
                                ListViewItem item = new ListViewItem(new[] { animal.name, animal.id.ToString() });
                                item.Group = group;
                                listView1.Items.Add(item);
                                added = true;
                            }
                        }
                        if (!added)
                        {
                            listView1.Groups.Add(new ListViewGroup(animal.currentReseacher()));
                            foreach (ListViewGroup group in listView1.Groups)
                            {
                                if (group.Header.Equals(animal.currentReseacher()))
                                {
                                    ListViewItem item = new ListViewItem(new[] { animal.name, animal.id.ToString() });
                                    item.Group = group;
                                    listView1.Items.Add(item);
                                }
                            }
                        }
                    }
                }
                else if (cmbSortBy.SelectedIndex == 3)
                {
                    int minWeight = (int)nmbMinWeight.Value;
                    foreach (Animal animal in form1.animalList)
                    {
                        if (animal.weight > minWeight)
                        {
                            sortedList.Add(animal);
                        }
                    }
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("Weight"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "Weight";
                    listView1.Columns[0].Width = listView1.Width / 2;
                    listView1.Columns[1].Width = listView1.Width / 2 - 10;

                    sortedList.Sort(delegate(Animal first, Animal second)
                    {
                        return second.weight.CompareTo(first.weight);
                    });

                    foreach (Animal animal in sortedList)
                    {
                        ListViewItem item = new ListViewItem(new string[] { animal.name, animal.weight.ToString() });
                        listView1.Items.Add(item);
                    }
                }
                else if (cmbSortBy.SelectedIndex == 4)
                {
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("ID"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "ID";
                    listView1.Columns[0].Width = listView1.Width / 2;
                    listView1.Columns[1].Width = listView1.Width / 2 - 10;
                    foreach (Animal animal in form1.animalList)
                    {
                        if (animal.currentProjectDate().Year < int.Parse(nmbDateLimit.Value.ToString()))
                        {
                            continue;
                        }
                        Boolean added = false;
                        foreach (ListViewGroup group in listView1.Groups)
                        {
                            string phrase = animal.currentProjectDate().ToString("MMMM, dd, yyyy");
                            if (group.Header.Equals(phrase))
                            {
                                ListViewItem item = new ListViewItem(new[] { animal.name, animal.id.ToString() });
                                item.Group = group;
                                listView1.Items.Add(item);
                                added = true;
                            }
                        }
                        if (!added)
                        {
                            string phrase = animal.currentProjectDate().ToString("MMMM, dd, yyyy");
                            listView1.Groups.Add(new ListViewGroup(phrase));
                            foreach (ListViewGroup group in listView1.Groups)
                            {

                                if (group.Header.Equals(phrase))
                                {
                                    ListViewItem item = new ListViewItem(new[] { animal.name, animal.id.ToString() });
                                    item.Group = group;
                                    listView1.Items.Add(item);
                                }
                            }
                        }
                    }
                    List<DateTime> dateList = new List<DateTime>();
                    foreach (ListViewGroup group in listView1.Groups)
                    {
                        dateList.Add(DateTime.Parse(group.Header.ToString()));
                    }

                    dateList.Sort(delegate(DateTime first, DateTime second)
                    {
                        return DateTime.Compare(second, first);
                    });
                    List<ListViewGroup> groupList = new List<ListViewGroup>();
                    foreach (ListViewGroup group in listView1.Groups)
                    {
                        groupList.Add(group);
                    }
                    listView1.Groups.Clear();
                    foreach (DateTime time in dateList)
                    {
                        foreach (ListViewGroup group in groupList)
                        {
                            if (time.ToString("MMMM, dd, yyyy").Equals(group.Header))
                            {
                                listView1.Groups.Add(group);
                            }
                        }
                    }
                }
            }
            else if (cmbSearchBy.SelectedIndex != 0)
            {
                if (cmbSearchBy.SelectedIndex == 1)
                {
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("ID"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "ID";
                    listView1.Columns[0].Width = listView1.Width / 2;
                    listView1.Columns[1].Width = listView1.Width / 2 - 10;
                    foreach (Animal animal in form1.animalList)
                    {
                        if (animal.find(form1, txtSearch.Text))
                        {
                            ListViewItem item = new ListViewItem(new string[] { animal.name, animal.id.ToString() });
                            listView1.Items.Add(item);
                        }
                    }
                }
                else if (cmbSearchBy.SelectedIndex == 2)
                {
                    foreach (Animal animal in form1.animalList)
                    {
                        if (txtSearch.Text.Equals(animal.researchHistory.Rows[animal.researchHistory.Rows.Count-1].ItemArray[3].ToString()))
                        {
                            sortedList.Add(animal);
                        }
                    }
                    listView1.Columns.Add(new ColumnHeader("Name"));
                    listView1.Columns.Add(new ColumnHeader("ID"));
                    listView1.Columns.Add(new ColumnHeader("Start Date"));
                    listView1.Columns.Add(new ColumnHeader("Completion Date"));
                    listView1.Columns[0].Text = "Name";
                    listView1.Columns[1].Text = "ID";
                    listView1.Columns[2].Text = "Start Date";
                    listView1.Columns[3].Text = "Completion Date";
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    sortedList.Sort(delegate(Animal first, Animal second)
                    {
                        return first.name.CompareTo(second.name);
                    });

                    foreach (Animal animal in sortedList)
                    {
                        ListViewItem item = new ListViewItem(new string[] { animal.name, 
                                                    animal.id.ToString(),
                                                    animal.researchHistory.Rows[animal.researchHistory.Rows.Count-1].ItemArray[1].ToString(),
                                                    animal.researchHistory.Rows[animal.researchHistory.Rows.Count-1].ItemArray[4].ToString()});
                        listView1.Items.Add(item);
                    }
                }
            }           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (Animal animal in form1.animalList)
                {
                    if (listView1.SelectedItems[0].Text.Equals(animal.name))
                    {
                        for (int a = 0; a < form1.ListView1.Items.Count; a++)
                        {
                            if (form1.ListView1.Items[a].Text.Equals(animal.name))
                            {
                                form1.updateAnimal();
                                form1.ListView1.Items[a].Selected = true;
                                form1.ListView1.Items[a].BackColor = Color.LimeGreen;
                                form1.ListView1.Items[a].EnsureVisible();
                            }
                        }
                    }
                }
            }

        }

        private void chkHighlight_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHighlight.Checked == true)
            {
                nmbLimit.Enabled = true;
            }
            else
            {
                nmbLimit.Enabled = false;
            }
        }

        private void cmbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSortBy.SelectedIndex == 1)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
            }
            else if (cmbSortBy.SelectedIndex == 2)
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                groupBox3.Enabled = false;
            }
            else if (cmbSortBy.SelectedIndex == 3)
            {
                groupBox2.Enabled = true;
                groupBox1.Enabled = false;
                groupBox3.Enabled = false;
            }
            else if (cmbSortBy.SelectedIndex == 4)
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                groupBox3.Enabled = true;
            }
        }

        private void cmbSearchBy_Click(object sender, EventArgs e)
        {
            cmbSortBy.SelectedIndex = 0;
            txtSearch.Enabled = true;
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            groupBox3.Enabled = false;
        }

        private void cmbSortBy_Click(object sender, EventArgs e)
        {
            cmbSearchBy.SelectedIndex = 0;
            txtSearch.Enabled = false;
        }

        #region Methods
        #endregion
    }
}
