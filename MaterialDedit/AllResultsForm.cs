using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialDedit
{
    public partial class AllResultsForm : Form
    {
        public AllResultsForm(int iNestResultCount)
        {
            m_iNestResultCount = iNestResultCount;
            InitializeComponent();
        }

        public int GetSelectedResultIndex() { return m_iSelectedResultIndex; }

        private void okBtn_Click(object sender, EventArgs e)
        {
            m_iSelectedResultIndex = allResultListBox.SelectedIndex;
            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void AllResultsForm_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;

            for (int i = 0; i < m_iNestResultCount; i++)
            {
                String str = "Nest Result ";
                str += (i + 1);
                allResultListBox.Items.Add(str);
            }
            if (m_iNestResultCount > 0)
            {
                allResultListBox.SelectedIndex = 0;
            }
        }

        private int m_iNestResultCount = 0;
        private int m_iSelectedResultIndex = -1;
    }
}
