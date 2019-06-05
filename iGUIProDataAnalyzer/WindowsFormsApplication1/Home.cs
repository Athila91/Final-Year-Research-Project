using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using iGUIProDataAnalyzer.Model;
using iGUIProDataAnalyzer.ServiceClasses;
using System.Diagnostics;
namespace iGUIProDataAnalyzer
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

       

        private void Home_Load(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
            List<Controller> listPreferenceParameters = ControllerService.LoadData();
            foreach (Controller control in listPreferenceParameters)
            {
                if (!comboBoxRegion.Items.Contains(control.Region))
                {
                    comboBoxRegion.Items.Add(control.Region);
                }

                if (!comboBoxController.Items.Contains(control.ControllerName))
                {
                    comboBoxController.Items.Add(control.ControllerName);
                }

                if (!comboBoxField.Items.Contains(control.ProfessionalField))
                {
                    comboBoxField.Items.Add(control.ProfessionalField);
                }

                if (!comboBoxProperty.Items.Contains(control.PropertyName))
                {
                    comboBoxProperty.Items.Add(control.PropertyName);
                }

                if (!controllerComboBox.Items.Contains(control.ControllerName))
                {
                    controllerComboBox.Items.Add(control.ControllerName);
                }

                if (!propertyComboBox.Items.Contains(control.PropertyName))
                {
                    propertyComboBox.Items.Add(control.PropertyName);
                }

            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            List<iGUIProDataAnalyzer.Model.Controller> listController = new List<Model.Controller>();
            List<TextBox> listTextbox = new List<TextBox>();
            listTextbox.Add(textBox1);
            listTextbox.Add(textBox2);
            listTextbox.Add(textBox3);
            listTextbox.Add(textBox4);
            listTextbox.Add(textBox5);
            listTextbox.Add(textBox6);
            listTextbox.Add(textBox7);
            listTextbox.Add(textBox8);
            listTextbox.Add(textBox9);
            listTextbox.Add(textBox10);
            listTextbox.Add(textBox11);
            listTextbox.Add(textBox12);
            listTextbox.Add(textBox13);
            listTextbox.Add(textBox14);
            listTextbox.Add(textBox15);

            var emptyTextboxes = from tb in listTextbox.OfType<TextBox>()
                                 where string.IsNullOrEmpty(tb.Text)
                                 select tb;

            int textboxCount = emptyTextboxes.Count();
            if (textboxCount == 15 || comboBoxController.SelectedItem == null || comboBoxProperty.SelectedItem == null || textBoxRegion.Text == string.Empty || textBoxField.Text == string.Empty)
            {
                MessageBox.Show("Please select or fill the required parameters or preferred values.");

            }
            else
            {
                ControllerService.AddUserPreferredData(listTextbox, listController, textBoxField.Text.ToString(), textBoxRegion.Text.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());


                string path2 = @"D:\\s2.xml";






                if (!(File.Exists(path2)))
                {

                    ControllerService.CreateFile(path2, listController);


                }
                else
                {
                    ControllerService.SaveData(path2, listController);
                }
                MessageBox.Show("   Successfully saved!    ");
            }

        }

        private void buttonAnalyze_Click_1(object sender, EventArgs e)
        {
            var st = Stopwatch.StartNew();
            if (DataAnalyze.AnalyzeUserPreferredData())
            {
                var m = st.ElapsedMilliseconds;
                MessageBox.Show("Successfully analyzed!"+ m.ToString());
                
            }
            else
            {
                MessageBox.Show("File not found containing user preferrences to analyze.");
            }
            
        }

        private void buttonSaveMoreData_Click(object sender, EventArgs e)
        {
            List<iGUIProDataAnalyzer.Model.Controller> listController = new List<Model.Controller>();
            List<TextBox> listTextbox = new List<TextBox>();
            listTextbox.Add(textBox16);
            listTextbox.Add(textBox17);
            listTextbox.Add(textBox18);
            listTextbox.Add(textBox19);
            listTextbox.Add(textBox20);
            listTextbox.Add(textBox21);
            listTextbox.Add(textBox22);
            listTextbox.Add(textBox23);
            listTextbox.Add(textBox24);
            listTextbox.Add(textBox25);
            listTextbox.Add(textBox26);
            listTextbox.Add(textBox27);
            listTextbox.Add(textBox28);
            listTextbox.Add(textBox29);
            listTextbox.Add(textBox30);

            ControllerService.AddUserPreferredData(listTextbox, listController, comboBoxField.SelectedItem.ToString(), comboBoxRegion.SelectedItem.ToString(), controllerComboBox.SelectedItem.ToString(), propertyComboBox.SelectedItem.ToString());


            string path2 = @"D:\\s2.xml";






            if (!(File.Exists(path2)))
            {

                ControllerService.CreateFile(path2, listController);


            }
            else
            {
                ControllerService.SaveData(path2, listController);
            }

        }

        private void buttonAnalyzeMoreData_Click(object sender, EventArgs e)
        {
            DataAnalyze.AnalyzeUserPreferredData();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }





        
    }
}
