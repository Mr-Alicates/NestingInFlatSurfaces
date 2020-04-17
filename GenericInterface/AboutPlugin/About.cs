using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenericInterface.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            string versionNumber = version.ToString();

            lblBuildNumber.Text += versionNumber;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
