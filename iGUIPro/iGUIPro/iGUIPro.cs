using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iGUIPro.UserPreferenceService;
using System.Diagnostics;

namespace iGUIPro
{
    public partial class iGUIPro : Form
    {
        public iGUIPro()
        {
            InitializeComponent();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            var st = Stopwatch.StartNew();
            if (Connect._applicationObject.ActiveDocument == null)
            {
                MessageBox.Show("You have not opened a solution. Please open a solution solution to continue.");
            }

            else if (comboBoxRegion.SelectedItem == null || comboBoxController.SelectedItem == null || comboBoxField.SelectedItem == null || comboBoxProperty.SelectedItem == null)
            {
                MessageBox.Show("Please select the parameters to display the user preference priority list.");
            }

            else if(comboBoxPrefereneValues.SelectedItem == null)
            {
                MessageBox.Show("Please select an value from user preference priority list.");
            }
            else
            {
                SetUserPreferences.ApplyPrefeence(Connect._applicationObject, comboBoxRegion.SelectedItem.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxField.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString(), comboBoxPrefereneValues.SelectedItem.ToString());
                var m = st.ElapsedMilliseconds;
                MessageBox.Show("User preference applied successfully" + m.ToString());
            }

        }
        private void iGUIPro_Load(object sender, EventArgs e)
        {

            pictureBox1.BorderStyle = BorderStyle.None;
            List<Controller> listPreferenceParameters = SetUserPreferences.LoadPreferenceValues();
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
            }
           
        }

        private void buttonRevert_Click(object sender, EventArgs e)
        {
            if (Connect._applicationObject.ActiveDocument == null)
            {
                MessageBox.Show("You have not opened a solution. Please open a solution solution to continue.");
            }

            else if (SetUserPreferences.ChangeToPreveiousValue(Connect._applicationObject, comboBoxController.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString()) == false)
            {
                MessageBox.Show("No previous property values file found.");
            }

            

            else if (comboBoxController.SelectedItem == null || comboBoxProperty.SelectedItem == null)
            {
                MessageBox.Show("You have not selected the Ui controller or property. Please select both.");
            }
            else
            {
                SetUserPreferences.ChangeToPreveiousValue(Connect._applicationObject, comboBoxController.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());
                MessageBox.Show("Successfully changed to previously set values.");           
            }
            
        }

        private void comboBoxController_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField.SelectedItem != null && comboBoxProperty.SelectedItem != null && comboBoxRegion.SelectedItem != null)
            {
                string[] preferredValues = SetUserPreferences.GetMostPreferredValue(comboBoxRegion.SelectedItem.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxField.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());
                if (preferredValues.Length != 0)
                {
                    comboBoxPrefereneValues.Items.Clear();
                    foreach (string value in preferredValues)
                    {
                        comboBoxPrefereneValues.Items.Add(value);
                    }

                    comboBoxPrefereneValues.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Most preferred values xml file doesn't exist");
                }
            }
        }

        private void comboBoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField.SelectedItem != null && comboBoxProperty.SelectedItem != null && comboBoxController.SelectedItem != null)
            {
                string[] preferredValues = SetUserPreferences.GetMostPreferredValue(comboBoxRegion.SelectedItem.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxField.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());
                if (preferredValues.Length != 0)
                {
                    comboBoxPrefereneValues.Items.Clear();
                    foreach (string value in preferredValues)
                    {
                        comboBoxPrefereneValues.Items.Add(value);
                    }

                    comboBoxPrefereneValues.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Most preffered values xml file doesnt exist");
                }
            }
        }

        private void comboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxController.SelectedItem != null && comboBoxProperty.SelectedItem != null && comboBoxRegion.SelectedItem != null)
            {
                string[] preferredValues = SetUserPreferences.GetMostPreferredValue(comboBoxRegion.SelectedItem.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxField.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());
                if (preferredValues.Length  != 0)
                {
                    comboBoxPrefereneValues.Items.Clear();
                    foreach (string value in preferredValues)
                    {
                        comboBoxPrefereneValues.Items.Add(value);
                    }
                    
                    comboBoxPrefereneValues.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Most preffered values xml file doesn't exist. Please analyze data using data analyzer.");
                }
            }
        }

        private void comboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxField.SelectedItem != null && comboBoxController.SelectedItem != null && comboBoxRegion.SelectedItem != null)
            {
                string[] preferredValues = SetUserPreferences.GetMostPreferredValue(comboBoxRegion.SelectedItem.ToString(), comboBoxController.SelectedItem.ToString(), comboBoxField.SelectedItem.ToString(), comboBoxProperty.SelectedItem.ToString());
                if (preferredValues.Length != 0)
                {
                    comboBoxPrefereneValues.Items.Clear();
                    foreach (string value in preferredValues)
                    {
                        comboBoxPrefereneValues.Items.Add(value);
                    }

                    comboBoxPrefereneValues.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Most preffered values xml file doesnt exist");
                }
            }
        }


    } 
}
