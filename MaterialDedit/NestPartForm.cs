using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NestBridge;

namespace MaterialDedit
{
    public partial class NestPartForm : Form
    {
        public NestPartForm(NestPartEx nestPart)
        {
            m_nestPart = nestPart;
            InitializeComponent();
        }

        private NestPartEx m_nestPart;

        private void NestPartForm_Load(object sender, EventArgs e)
        {
            // the nesting priority.
            priorComboBox.Items.Add("1");
            priorComboBox.Items.Add("2");
            priorComboBox.Items.Add("3");
            priorComboBox.Items.Add("4");
            priorComboBox.Items.Add("5");
            priorComboBox.Items.Add("6");
            priorComboBox.Items.Add("7");
            priorComboBox.Items.Add("8");
            priorComboBox.Items.Add("9");
            priorComboBox.Items.Add("10");
            priorComboBox.SelectedIndex = m_nestPart.GetPriority().GetVal() - 1;

            // the nesting count.
            countTextBox.Text = m_nestPart.GetNestCount().ToString();

            // the part rotate angle.
            angleComboBox.Items.Add("自由旋转");
            angleComboBox.Items.Add("90度增量");
            angleComboBox.Items.Add("180度增量");
            angleComboBox.Items.Add("0度固定");
            angleComboBox.Items.Add("90度固定");
            angleComboBox.Items.Add("180度固定");
            angleComboBox.Items.Add("270度固定");
            angleComboBox.SelectedIndex = (int)m_nestPart.GetRotStyle();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            // verify input.
            try
            {
                Convert.ToInt32(countTextBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input: " + exception.Message, "NestProfessor DEMO");
                return;
            }

            m_nestPart.SetPriority(new NestPriorityEx(priorComboBox.SelectedIndex + 1));
            m_nestPart.SetNestCount(Convert.ToInt32(countTextBox.Text));
            m_nestPart.SetRotStyle((PART_ROT_STYLE_EX)angleComboBox.SelectedIndex);

            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
