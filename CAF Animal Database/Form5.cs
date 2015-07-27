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
    public partial class Form5 : Form
    {
        Form1 form1;

        public Form5(Form1 form)
        {
            form1 = form;
            InitializeComponent();
            dataGridView1.Columns.AddRange(new[] { new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn() });
            dataGridView1.Columns[0].HeaderText = "Date (M/D/Y)";
            dataGridView1.Columns[1].HeaderText = "Result";
            dataGridView1.Columns[2].HeaderText = "Treatment";
            dataGridView2.Columns.AddRange(new[] { new DataGridViewTextBoxColumn(), new DataGridViewTextBoxColumn() });
            dataGridView2.Columns[0].HeaderText = "Date (M/D/Y)";
            dataGridView2.Columns[1].HeaderText = "Vaccination";
            dataGridView3.Columns.AddRange(new[] { new DataGridViewTextBoxColumn(), 
                    new DataGridViewTextBoxColumn(),
                    new DataGridViewTextBoxColumn(), 
                    new DataGridViewTextBoxColumn(), 
                    new DataGridViewTextBoxColumn(), 
                    new DataGridViewTextBoxColumn(),
                    new DataGridViewTextBoxColumn()});
            dataGridView3.Columns[0].HeaderText = "Researcher";
            dataGridView3.Columns[1].HeaderText = "Start Date (M/D/Y)";
            dataGridView3.Columns[2].HeaderText = "Request Number";
            dataGridView3.Columns[3].HeaderText = "AUP";
            dataGridView3.Columns[4].HeaderText = "Completion Date (M/D/Y)";
            dataGridView3.Columns[5].HeaderText = "Comment";
            dataGridView3.Columns[6].HeaderText = "Points";
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            clbAnimalList.Items.Clear();
            foreach (Animal animal in form1.animalList)
            {
                clbAnimalList.Items.Add(animal.name);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            for (int a = 0; a < clbAnimalList.Items.Count; a++)
            {
                foreach (Animal animal in form1.animalList)
                {
                    if(animal.name.Equals(clbAnimalList.Items[a]))
                    {
                        foreach(DataGridViewRow gridRow in dataGridView1.Rows)
                        {
                            DataRow row = animal.fecalHistory.NewRow();
                            int counter = 0;
                            Boolean add = true;
                            foreach(DataGridViewCell gridCell in gridRow.Cells)
                            {
                                if (gridCell.Value != null && !gridCell.Value.ToString().Equals(""))
                                {
                                    row[counter] = gridCell.Value.ToString();
                                    counter++;
                                }
                                else
                                {
                                    add = false;
                                }
                            }
                            if (add)
                            {
                                animal.fecalHistory.Rows.InsertAt(row, animal.fecalHistory.Rows.Count);
                            }
                        }
                        foreach (DataGridViewRow gridRow in dataGridView2.Rows)
                        {
                            DataRow row = animal.vaccinationHistory.NewRow();
                            int counter = 0;
                            Boolean add = true;
                            foreach (DataGridViewCell gridCell in gridRow.Cells)
                            {
                                if (gridCell.Value != null && !gridCell.Value.ToString().Equals(""))
                                {
                                    row[counter] = gridCell.Value.ToString();
                                    counter++;
                                }
                                else
                                {
                                    add = false;
                                }
                            }
                            if (add)
                            {
                                animal.vaccinationHistory.Rows.InsertAt(row, animal.vaccinationHistory.Rows.Count);
                            }
                        }
                        foreach (DataGridViewRow gridRow in dataGridView3.Rows)
                        {
                            DataRow row = animal.researchHistory.NewRow();
                            int counter = 0;
                            Boolean add = true;
                            foreach (DataGridViewCell gridCell in gridRow.Cells)
                            {
                                if (gridCell.Value != null && !gridCell.Value.ToString().Equals(""))
                                {
                                    row[counter] = gridCell.Value.ToString();
                                    counter++;
                                }
                                else
                                {
                                    add = false;
                                }
                            }
                            if (add)
                            {
                                animal.researchHistory.Rows.InsertAt(row, animal.researchHistory.Rows.Count);
                            }
                        }
                    }
                }
            }
        }
    }
}
