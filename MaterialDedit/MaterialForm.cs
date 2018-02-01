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
    public partial class MaterialForm : Form
    {
        public MaterialForm(MatEx mat)
        {
            m_mat = mat;

            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            // verify input.
            try
            {
                Convert.ToDouble(widthTextBox.Text);
                Convert.ToDouble(heightTextBox.Text);
                Convert.ToInt32(countTextBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input: " + exception.Message, "NestProfessor DEMO");
                return;
            }

            // update MatEx object.
            m_mat.SetName(nameTextBox.Text);
            m_mat.SetCount(Convert.ToInt32(countTextBox.Text));
            if (m_mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
            {
                RectMatEx rectMat = (RectMatEx)(m_mat);
                Rect2DEx rect = new Rect2DEx(0.0, Convert.ToDouble(widthTextBox.Text), 0.0, Convert.ToDouble(heightTextBox.Text));
                rectMat.SetMatRect(rect);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private MatEx m_mat;

        private void MaterialForm_Load(object sender, EventArgs e)
        {
            nameTextBox.Text = m_mat.GetName();
            countTextBox.Text = m_mat.GetCount().ToString();

            if (m_mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
            {
                RectMatEx rectMat = (RectMatEx)(m_mat);
                Rect2DEx rect = rectMat.GetBoundaryRect();
                widthTextBox.Enabled = true;
                widthTextBox.Text = rect.GetWidth().ToString("0.000");
                heightTextBox.Enabled = true;
                heightTextBox.Text = rect.GetHeight().ToString("0.000");
            }
            else if (m_mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
            {
                PolyMatEx polyMat = (PolyMatEx)(m_mat);
                Polygon2DEx poly = polyMat.GetMatPolygon();
                Rect2DEx rect = poly.GetBoundaryRect();
                widthTextBox.Enabled = false;
                widthTextBox.Text = rect.GetWidth().ToString("0.000");
                heightTextBox.Enabled = false;
                heightTextBox.Text = rect.GetHeight().ToString("0.000");
            }
        }
    }
}
