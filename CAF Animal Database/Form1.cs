using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
namespace CAF_Animal_Database
{
    public partial class Form1 : Form
    {
        Form3 form3;
        Form4 form4;
        Form5 form5;
        Form2 form2;
        AboutBox1 about = new AboutBox1();

        public List<Animal> animalList = new List<Animal>();
        public string animalPath = Application.StartupPath + "/Animal Database Directory";
        int index = 0;
        int counter = 0;

        public Form1()
        {
            InitializeComponent();
            form3 = new Form3(this);
            form4 = new Form4(this);
            form5 = new Form5(this);
            form2 = new Form2();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            ListView1.MouseDown += new MouseEventHandler(ListView1_MouseDown);
            //ListView1.item
        }

        #region Methods
        public void updateAnimal()
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int updateIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[updateIndex].SubItems[0].Text))
                    {
                        foreach (Animal animal1 in animalList)
                        {
                            if (animal1.name.Equals(txtName.Text) && !animal1.Equals(animal))
                            {
                                MessageBox.Show("An animal already exists with the name '" + animal1.name + "'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtName.Text = animal.name;
                                break;
                            }
                        }
                        animal.getForm();
                        if (!animal.oldName.Equals(animal.name))
                        {
                            ListView1.Items[updateIndex].SubItems[0].Text = animal.name;
                            animal.updateFolder();
                            animal.oldName = txtName.Text;
                        }
                        ListView1.Items[updateIndex].SubItems[1].Text = animal.id.ToString();
                        ListView1.Items[updateIndex].BackColor = Color.White;
                        break;
                    }
                }
            }
        }
        public void updateDisplayArea()
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int updateIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[updateIndex].SubItems[0].Text))
                    {
                        animal.setForm();
                        //animal.highlightCurrentResearcher();
                        ListView1.Items[updateIndex].BackColor = Color.LightSteelBlue;
                        //ListView1.Items[updateIndex].Selected = true;
                        break;
                    }
                }
            }
        }

        public void createAnimal()
        {
            String tempName;
            Boolean repeat = false;
            do
            {
                foreach (Animal animal in animalList)
                {
                    tempName = "New " + index;
                    if (animal.name.Equals(tempName))
                    {
                        repeat = true;
                        index++;
                        break;
                    }
                    else
                    {
                        repeat = false;
                    }
                }
            } while (repeat);
            animalList.Add(new Animal("New " + index, index,this));
            ListView1.Items.Add(new ListViewItem(new[] { "New " + index, "" + index }));
            ListView1.Items[ListView1.Items.Count - 1].Selected = true;
            ListView1.Items[ListView1.Items.Count - 1].EnsureVisible();
            animalList[animalList.Count - 1].folderPath = animalPath;
            animalList[animalList.Count - 1].setupFolder();
        }
        public void createAnimal(String name, String id, Boolean loadInfo)
        {
            animalList.Add(new Animal(name, System.Convert.ToInt32(id),this));
            animalList[index].folderPath = animalPath;
            if (loadInfo)
            {
                animalList[index].loadInfo();
            }
            else
            {
                animalList[index].setupFolder();
            }
            ListView1.Items.Add(new ListViewItem(new[] { animalList[index].name, animalList[index].id.ToString() }));
            index++;
        }
        public void deleteAnimal()
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int delIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[delIndex].SubItems[0].Text))
                    {
                        ListView1.Items.RemoveAt(delIndex);
                        animalList.Remove(animal);
                        animal.deleteFolder();
                        index--;
                        if (delIndex-1 >= 0)
                        {
                            ListView1.Items[delIndex-1].Selected = true;
                        }
                        else if (delIndex < ListView1.Items.Count)
                        {
                            ListView1.Items[delIndex].Selected = true;
                        }
                        
                        break;
                    }
                }
            }
        }
        public void loadAnimals()
        {
            label3.Visible = false;
            pgbProgress.Visible = true;
            string[] animalDirectories = Directory.GetDirectories(animalPath);
            pgbProgress.Maximum = animalDirectories.Length;
            foreach (string path in animalDirectories)
            {
                string cleanPath = path.Substring(path.LastIndexOf('\\') + 1);
                if (!cleanPath.Equals("Deleted"))
                {
                    createAnimal(cleanPath, "999", true);
                }
                pgbProgress.Value++;
                double percent = Math.Round((double)pgbProgress.Value / (double)animalDirectories.Length * 100.0, 2);
                pgbProgress.Refresh();
                pgbProgress.CreateGraphics().DrawString("Loading: " + percent.ToString() + "%", new Font("Century Gothic", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pgbProgress.Width / 2 - 38, pgbProgress.Height / 2 - 7));
            }
            label3.Text = "Successfuly Loaded All Animals!";
            label3.Visible = true;
            pgbProgress.Visible = false;
            timer1.Start();
        }
        public void saveAnimals()
        {
            label3.Visible = false;
            pgbProgress.Visible = true;
            pgbProgress.Value = 0;
            pgbProgress.Maximum = animalList.Count;
            foreach (Animal animal in animalList)
            {
                animal.saveInfo();
                pgbProgress.Value++;
                double percent = Math.Round((double)pgbProgress.Value / (double)animalList.Count * 100.0, 2);
                pgbProgress.Refresh();
                pgbProgress.CreateGraphics().DrawString("Saving: " + percent.ToString() + "%", new Font("Century Gothic", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pgbProgress.Width / 2 - 38, pgbProgress.Height / 2 - 7));
            }
        }
        public void chooseAnimalPicture()
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int listIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[listIndex].SubItems[0].Text))
                    {
                        string fileName = null;

                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Open an Image";
                        openFileDialog.RestoreDirectory = true;

                        openFileDialog.Filter = "All picture files (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            fileName = openFileDialog.FileName;
                            pcbAnimal.Image = Image.FromFile(fileName);
                        }
                        break;
                    }
                }
            }
        }
        public void updateAnimalPoints()
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int updateIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[updateIndex].SubItems[0].Text))
                    {
                        animal.updatePoints();
                        lblPoints.Text = "Points = " + animal.points.ToString();
                    }
                }
            }
        }
        public Boolean checkDateFormat()
        {
            for (int a = 0; a < dgvResearchInformation.Rows.Count; a++)
            {
                DataGridViewRow row = dgvResearchInformation.Rows[a];
                DateTime temp;
                //Start Date
                if (row.Cells[1] != null)
                {
                    string date = row.Cells[1].Value.ToString();
                    if (!DateTime.TryParse(date, out temp))
                    {
                        MessageBox.Show("Please format the 'Start Date' cell at (" + (a + 1) + ",2) correctly, eg. '1/1/2001'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Please format the 'Start Date' cell at (" + (a + 1) + ",2) correctly, eg. '1/1/2001'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //Complete Date
                if (row.Cells[4] != null)
                {
                    string date = row.Cells[4].Value.ToString();
                    if (!DateTime.TryParse(date, out temp))
                    {
                        MessageBox.Show("Please format the 'Complete Date' cell at (" + (a + 1) + ",5) correctly, eg. '1/1/2001'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Please format the 'Complete Date' cell at (" + (a + 1) + ",5) correctly, eg. '1/1/2001'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        #endregion

        void ListView1_MouseDown(object sender, MouseEventArgs e)
        {
            updateAnimal();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateAnimal();
            saveAnimals();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbSex.SelectedIndex = 0;
            cmbSpayNeuter.SelectedIndex = 0;
            txtDispositionBy.Enabled = false;
            dtpDispositionWhen.Enabled = false;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(animalPath))
            {
                Directory.CreateDirectory(animalPath);
            }
            else
            {
                loadAnimals();
                index = animalList.Count;
                if (ListView1.Items.Count > 0)
                {
                    ListView1.Items[0].Selected = true;
                }
                else
                {
                    TabControl1.Enabled = false;
                }
            }

            if (!Directory.Exists(animalPath + "/Deleted"))
            {
                Directory.CreateDirectory(animalPath + "/Deleted");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateAnimal();
            createAnimal();
            if (ListView1.Items.Count == 1)
            {
                TabControl1.Enabled = true;
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDisplayArea();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteAnimal();
            if (ListView1.Items.Count == 0)
            {
                TabControl1.Enabled = false;
                index = 0;
                pcbAnimal.Image = null;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            chooseAnimalPicture();
            
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateAnimal();
            createAnimal();
            if (ListView1.Items.Count == 1)
            {
                TabControl1.Enabled = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteAnimal();
            if (ListView1.Items.Count == 0)
            {
                TabControl1.Enabled = false;
                index = 0;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about.ShowDialog();
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            updateAnimal();
        }

        private void dgvResearchInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            updateAnimalPoints();
        }

        private void dgvResearchInformation_Leave(object sender, EventArgs e)
        {
            updateAnimalPoints();
        }

        private void dgvResearchInformation_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgvResearchInformation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkDisposition_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisposition.Checked)
            {
                txtDispositionBy.Enabled = true;
                dtpDispositionWhen.Enabled = true;
            }
            else
            {
                txtDispositionBy.Enabled = false;
                dtpDispositionWhen.Enabled = false;
            }
        }

        private void batchOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form5.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ListView1.SelectedIndices.Count > 0)
            {
                int updateIndex = ListView1.SelectedIndices[0];
                foreach (Animal animal in animalList)
                {
                    if (animal.oldName.Equals(ListView1.Items[updateIndex].SubItems[0].Text))
                    {
                        form4.chosen = animal;
                        form4.ShowDialog();
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 2)
            {
                label3.Visible = false;
                timer1.Stop();
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void reportButSuggestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
    }
}
