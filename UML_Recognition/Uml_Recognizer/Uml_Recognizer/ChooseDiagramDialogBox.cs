using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uml_Recognizer
{
    public partial class ChooseDiagramDialogBox : Form
    {
        Form parentForm;
        public string diagram;

        public ChooseDiagramDialogBox()
        {
            InitializeComponent();

            diagram = "Use Case Diagram";
        }

        public ChooseDiagramDialogBox(Form mainForm)
        {
            this.parentForm = mainForm;
            InitializeComponent();

            diagram = "Use Case Diagram";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                diagram = radioButton1.Text;
            else
                diagram = radioButton2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
