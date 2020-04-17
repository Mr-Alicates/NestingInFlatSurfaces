using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Interfaces;
using Nesting.Core.Classes.Classification;
using Nesting.Core.Interfaces;

namespace Nesting.ClassifierList
{
    public partial class ClassifierList : Form
    {
        public ClassifierList()
        {
            InitializeComponent();
        }

        public void LoadClassifierFactories(List<INestingClassifierFactory> factories)
        {
            lstClassifierList.Items.Clear();

            IEnumerable<ClassifierInformation> informations = factories.Select(x => x.ClassifierInformation);

            foreach (ClassifierInformation information in informations)
            {
                ListViewItem item = new ListViewItem(information.Name);

                item.SubItems.Add(information.Version);
                item.SubItems.Add(information.AssemblyName);
                item.SubItems.Add(information.Description);
                item.ToolTipText = information.Description;

                lstClassifierList.Items.Add(item);
            }

            lstClassifierList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
