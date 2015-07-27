using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAF_Animal_Database
{
    public partial class Form4 : Form
    {
        public Form1 form1;
        public Animal chosen;
        public Form4(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            txtPrint.Text = "";
            txtPrint.Text += chosen.toPrinterString() + "\r\n";
            txtPrint.Select(txtPrint.Text.IndexOf("ID:", 0), 3);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("Exp. Date:", 0), 10);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("NAME:", 0), 5);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("REQ#:", 0), 5);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("AUP:", 0), 4);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("SOURCE:", 0), 7);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("REC'D:", 0), 6);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("SEX:", 0), 4);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("SPECIES:", 0), 8);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("AGE:", 0), 4);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("WT:", 0), 3);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 9.25f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("REQ#: ", 0) + "REQ#: ".Length, chosen.currentReqNumber().ToString().Length);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 14f, FontStyle.Bold);
            txtPrint.Select(txtPrint.Text.IndexOf("AUP: ", 0) + 5, chosen.aup.ToString().Length);
            txtPrint.SelectionFont = new Font("Lucida Sans Unicode", 14f, FontStyle.Bold);
            txtPrint.SelectAll();
        }
    }
}
