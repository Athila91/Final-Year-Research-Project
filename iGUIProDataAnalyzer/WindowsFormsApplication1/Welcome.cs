using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iGUIProDataAnalyzer
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Home home = new Home();
            //Application.Run(new Home());
            this.Close();
            home.ShowDialog();
           
            
            
        }
    }
}
