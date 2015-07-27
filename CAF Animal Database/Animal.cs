using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;  // Does XML serializing for a class.
using System.Drawing;            // Required for storing a Bitmap.
using System.IO;			     // Required for using Memory stream objects.
using System.ComponentModel;// Required for conversion of Bitmap objects.
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

/*  aup
    breed
    colour
    description
    id
    name
    request
    tattoo
    weight
    sex
    source
    spayNeuter
    comments
    arrivalDat
    birthDate
    spayNeuterDate
    fecalHistory
    researchHistory
    vaccinationHistory
*/

namespace CAF_Animal_Database
{
    [XmlRootAttribute("Animal", Namespace = "", IsNullable = false)]
    public class Animal
    {
        #region Declaration
            Form1 form1;
            private Bitmap picture;

            // Set this 'DateTimeValue' field to be an attribute of the root node.
            [XmlAttributeAttribute(DataType = "date")]
            public DateTime DateTimeValue= System.DateTime.Now;

            public string name,
                oldName,
                breed,
                colour,
                tattoo,
                description,
                comments,
                folderPath,
                source,
                dispositionBy;
            public int id,
                aup,
                sex,
                spayNeuter,
                points;
            public Boolean disposition;
            public decimal weight;
            public DateTime birthDate = System.DateTime.Now,
                spayNeuterDate= System.DateTime.Now,
                arrivalDate= System.DateTime.Now,
                dispositionDate = DateTime.Now;

            [XmlIgnoreAttribute()]
            public DataTable fecalHistory = new DataTable("FecalHistory");
            [XmlIgnoreAttribute()]
            public DataTable vaccinationHistory = new DataTable("VaccinationHistory");
            [XmlIgnoreAttribute()]
            public DataTable researchHistory = new DataTable("ResearchHistory");

            // Set serialization to IGNORE this property because the 'PictureByteArray' property
            // is used instead to serialize the 'Picture' Bitmap as an array of bytes.
            [XmlIgnoreAttribute()]
            public Bitmap Picture
            {
                get { return picture; }
                set { picture = value; }
            }
            // Serializes the 'Picture' Bitmap to XML.
            [XmlElementAttribute("Picture")]
            public byte[] PictureByteArray
            {
                get
                {
                    if (picture != null)
                    {
                        TypeConverter BitmapConverter = TypeDescriptor.GetConverter(picture.GetType());
                        return (byte[])BitmapConverter.ConvertTo(picture, typeof(byte[]));
                    }
                    else
                        return null;
                }

                set
                {
                    if (value != null)
                        picture = new Bitmap(new MemoryStream(value));
                    else
                        picture = null;
                }
            }
        #endregion
        #region Constructors
            public Animal()
            {
                initializeAnimal();
            }

            public Animal(String Name,Form1 form)
            {
                name = Name;
                oldName = Name;
                form1 = form;
                fecalHistory.Columns.AddRange(new[] { new DataColumn("Date (M/D/Y)"), new DataColumn("Result"), new DataColumn("Treatment") });

                vaccinationHistory.Columns.AddRange(new[] { new DataColumn("Date (M/D/Y)"), new DataColumn("Vaccination") });

                researchHistory.Columns.AddRange(new DataColumn[] { new DataColumn("Researcher"), 
                    new DataColumn("Start Date (M/D/Y)"),
                    new DataColumn("Request Number"), 
                    new DataColumn("AUP"), 
                    new DataColumn("Completion Date (M/D/Y)"), 
                    new DataColumn("Comment"),
                    new DataColumn("Points")});
                initializeAnimal();
            }

            public Animal(String Name, int Id,Form1 form)
            {
                name = Name;
                oldName = Name;
                id = Id;
                form1 = form;
                fecalHistory.Columns.AddRange(new[] { new DataColumn("Date (M/D/Y)"), new DataColumn("Result"), new DataColumn("Treatment") });

                vaccinationHistory.Columns.AddRange(new[] { new DataColumn("Date (M/D/Y)"), new DataColumn("Vaccination") });

                researchHistory.Columns.AddRange(new DataColumn[] { new DataColumn("Researcher"), 
                    new DataColumn("Start Date (M/D/Y)"),
                    new DataColumn("Request Number"), 
                    new DataColumn("AUP"), 
                    new DataColumn("Completion Date (M/D/Y)"), 
                    new DataColumn("Comment"),
                    new DataColumn("Points")});
                initializeAnimal();
            }
            public void initializeAnimal()
            {
                aup = 0;
                breed = "";
                colour = ""; 
                description = ""; 
                disposition = false;
                dispositionBy = "";
                tattoo = "";
                weight = 0;
                sex = 0;
                source = "";
                spayNeuter = 0;
                comments = "";
                points = 0;
                Picture = CAF_Animal_Database.Properties.Resources.noimageavailable;
            }
        #endregion
        #region Methods
            public void setForm()
            {
                form1.txtAUP.Text = aup+"";
                form1.txtBreed.Text = breed;
                form1.txtColour.Text = colour;
                form1.txtDescription.Text = description;
                form1.txtId.Text = id + "";
                form1.txtName.Text = name;
                form1.txtTattoo.Text = tattoo;
                form1.nmbWeight.Value = weight;
                form1.txtComment.Text = comments;
                form1.cmbSex.SelectedIndex = sex;
                form1.txtSource.Text = source;
                form1.cmbSpayNeuter.SelectedIndex = spayNeuter;
                form1.dtpArrival.Value = arrivalDate;
                form1.dtpBirth.Value = birthDate;
                form1.dtpSpayNeuter.Value = spayNeuterDate;
                form1.dgvFecalHistory.DataSource = fecalHistory;
                form1.dgvResearchInformation.DataSource = researchHistory;
                form1.dgvVaccinationHistory.DataSource = vaccinationHistory;
                form1.pcbAnimal.Image = (System.Drawing.Image)picture;
                form1.lblPoints.Text = "Points = " + points;
                form1.chkDisposition.Checked = disposition;
                form1.txtDispositionBy.Text = dispositionBy;
                form1.dtpDispositionWhen.Value = dispositionDate;
            }
            public void getForm()
            {
                try
                {
                    aup = System.Convert.ToInt32(form1.txtAUP.Text);
                    breed=form1.txtBreed.Text;
                    colour=form1.txtColour.Text;
                    description=form1.txtDescription.Text;
                    id=System.Convert.ToInt32(form1.txtId.Text);
                    name=form1.txtName.Text;
                    tattoo=form1.txtTattoo.Text;
                    weight=form1.nmbWeight.Value;
                    sex = form1.cmbSex.SelectedIndex;
                    source = form1.txtSource.Text;
                    spayNeuter=form1.cmbSpayNeuter.SelectedIndex;
                    comments = form1.txtComment.Text;
                    arrivalDate=form1.dtpArrival.Value;
                    birthDate=form1.dtpBirth.Value;
                    spayNeuterDate=form1.dtpSpayNeuter.Value;
                    fecalHistory=(DataTable)form1.dgvFecalHistory.DataSource;
                    researchHistory=(DataTable)form1.dgvResearchInformation.DataSource;
                    vaccinationHistory = (DataTable)form1.dgvVaccinationHistory.DataSource;
                    Picture = resizeImage(form1.pcbAnimal.Image,new Size(new Point(form1.pcbAnimal.Size.Height+5,form1.pcbAnimal.Size.Width+5)));
                    disposition = form1.chkDisposition.Checked;
                    dispositionBy = form1.txtDispositionBy.Text;
                    dispositionDate = form1.dtpDispositionWhen.Value;
                    updatePoints();
                }
                catch (Exception e) 
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public void setupFolder()
            {
                try
                {
                    Directory.CreateDirectory(folderPath+"/"+name);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            public void deleteFolder()
            {
                try
                {
                    int counter = 0;
                    Boolean repeat = false;
                    string newName = name;
                    if (Directory.Exists(folderPath + "/Deleted/" + name))
                    {
                        do
                        {
                            newName = name + "(" + counter + ")";
                            if (!Directory.Exists(folderPath + "/Deleted/" + newName))
                            {
                                repeat = false;
                            }
                            else
                            {
                                repeat = true;
                                counter++;

                            }
                        } while (repeat);
                    }

                    Directory.Move(folderPath + "/" + name, folderPath + "/Deleted/" + newName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public void updateFolder()
            {
                try
                {
                    Directory.Move(folderPath + "/" + oldName, folderPath + "/" + name);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public void saveInfo()
            {
                try
                {
                    ObjectXMLSerializer<Animal>.Save(this, folderPath + "/" + name  + "/info.xml");
                    fecalHistory.WriteXml(folderPath + "/" + name + "/fecalHistory.xml", XmlWriteMode.WriteSchema);
                    researchHistory.WriteXml(folderPath + "/" + name  + "/researchHistory.xml", XmlWriteMode.WriteSchema);
                    vaccinationHistory.WriteXml(folderPath + "/" + name + "/vaccinationHistory.xml", XmlWriteMode.WriteSchema);
                }
                catch (Exception e) 
                {
                    MessageBox.Show(e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public void loadInfo()
            {
                try
                {
                    import(ObjectXMLSerializer<Animal>.Load(folderPath + "/" + name  + "/info.xml"));
                    fecalHistory.ReadXml(folderPath + "/" + name + "/fecalHistory.xml");
                    researchHistory.ReadXml(folderPath + "/" + name  + "/researchHistory.xml");
                    vaccinationHistory.ReadXml(folderPath + "/" + name + "/vaccinationHistory.xml");
                }
                catch (Exception e)
                {
                    if (!e.GetType().ToString().Equals("System.IO.FileNotFoundException")) 
                    {
                        MessageBox.Show(e.GetType().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
                    }
                }
            }
            public Boolean find(Form1 form1,string phrase)
            {
                if (aup.ToString().Equals(phrase) ||
                    breed.Equals(phrase) ||
                    colour.Equals(phrase) ||
                    description.Equals(phrase) ||
                    id.ToString().Equals(phrase) ||
                    name.Equals(phrase) ||
                    form1.cmbSex.Items[sex].Equals(phrase) ||
                    source.Equals(phrase) ||
                    form1.cmbSpayNeuter.Items[sex].Equals(phrase) ||
                    comments.Equals(phrase) ||
                    fecalHistory.Equals(phrase) ||
                    researchHistory.Equals(phrase) ||
                    vaccinationHistory.Equals(phrase) ||
                    points.ToString().Equals(phrase))
                {
                    return true;
                }
                return false;
            }

            public void import(Animal animal)
            {
                aup = animal.aup; 
                breed = animal.breed; 
                colour = animal.colour;
                description = animal.description;
                id = animal.id;
                name = animal.name;
                oldName = animal.oldName;
                tattoo =animal.tattoo; 
                weight =animal.weight; 
                sex = animal.sex;
                source =animal.source; 
                spayNeuter =animal.spayNeuter; 
                comments =animal.comments; 
                arrivalDate = animal.arrivalDate;
                birthDate = animal.birthDate; 
                spayNeuterDate = animal.spayNeuterDate;
                fecalHistory = animal.fecalHistory;
                researchHistory = animal.researchHistory;
                vaccinationHistory = animal.vaccinationHistory;
                picture = animal.Picture;
                points = animal.points;
                disposition = animal.disposition;
                dispositionBy = animal.dispositionBy;
                dispositionDate = animal.dispositionDate;
            }
            public void updatePoints()
            {
                points = 0;
                for (int a = 0; a < researchHistory.Rows.Count; a++)
                {
                    int temp;
                    if (!researchHistory.Rows[a].ItemArray.ElementAt(6).Equals("") && int.TryParse(researchHistory.Rows[a].ItemArray.ElementAt(6).ToString(), out temp))
                    {
                        points += temp;
                    }
                }
            }
            public Bitmap resizeImage(Image imgToResize, Size size)
            {
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                return b;
            }
            public void highlightCurrentResearcher()
            {
                foreach (DataGridViewRow row in form1.dgvResearchInformation.Rows)
                {
                    string date = row.Cells[4].Value.ToString();
                    string[] devided = date.Split('/');
                    DateTime formattedDate;
                    if(DateTime.TryParse(date,out formattedDate))
                    {
                        MessageBox.Show(formattedDate.ToString());
                    }
                }
            }
            public string currentReseacher()
            {
                for(int a = researchHistory.Rows.Count-1; a>-1;a--)
                {
                    if(researchHistory.Rows[a] != null && !researchHistory.Rows[a].ItemArray[0].ToString().Equals(""))
                    {
                        return researchHistory.Rows[a].ItemArray[0].ToString();
                    }
                }
                return "Error";
            }
            public DateTime currentProjectDate()
            {
                for (int a = researchHistory.Rows.Count - 1; a > -1; a--)
                {
                    if (researchHistory.Rows[a] != null && !researchHistory.Rows[a].ItemArray[1].ToString().Equals(""))
                    {
                        DateTime temp;
                        if(DateTime.TryParse(researchHistory.Rows[a].ItemArray[1].ToString(),out temp))
                        {
                            return temp;
                        }
                    }
                }
                return new DateTime();
            }
            public int currentReqNumber()
            {
                for (int a = researchHistory.Rows.Count - 1; a > -1; a--)
                {
                    if (researchHistory.Rows[a] != null && !researchHistory.Rows[a].ItemArray[2].ToString().Equals(""))
                    {
                        int num;
                        if (int.TryParse(researchHistory.Rows[a].ItemArray[2].ToString(), out num))
                        {
                            return num;
                        }
                    }
                }
                return -999;
            }
            public DateTime currentProjectEndDate()
            {
                for (int a = researchHistory.Rows.Count - 1; a > -1; a--)
                {
                    if (researchHistory.Rows[a] != null && !researchHistory.Rows[a].ItemArray[4].ToString().Equals(""))
                    {
                        DateTime temp;
                        if(DateTime.TryParse(researchHistory.Rows[a].ItemArray[4].ToString(),out temp))
                        {
                            return temp;
                        }
                    }
                }
                return new DateTime();
            }
            public string toPrinterString()
            {
                return "ID: " + id + "    Exp. Date: " + currentProjectEndDate().ToString("MMM, dd, yyyy") + "\r\n"
                    + "NAME: " + name + "\r\n"
                    + "REQ#: " + currentReqNumber() + "    AUP: " + aup + "\r\n"
                    + "SOURCE: " + source + "\r\n"
                    + "REC'D: " + arrivalDate.ToString("MMM, dd, yyyy") + "\r\n"
                    + "SEX: " + form1.cmbSex.Items[sex] + "    AGE: " + (DateTime.Now.Year - arrivalDate.Year) + "\r\n"
                    + "SPECIES: " + breed + "\r\n"
                    + "WT: " + weight + " kg\r\n";
            }
        #endregion
    }
}
