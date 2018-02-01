using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NestBridge;
using Entities;

namespace MaterialDedit
{
    public partial class AdvParamForm : Form
    {
        // the nesting param.
        private NestParamEx m_nestParam;

        private int m_iNestingTime;

        public AdvParamForm(NestParamEx nestParam, int iNestingTime)
        {
            m_nestParam = nestParam;
            m_iNestingTime = iNestingTime;

            InitializeComponent();
        }

        private void AdvParamForm_Load(object sender, EventArgs e)
        {
            tolTextBox.Text = m_nestParam.GetConTol().ToString("0.000");
            rotTextBox.Text = m_nestParam.GetPartRotStep().ToString("0.000");
            evalTextBox.Text = m_nestParam.GetEvalFactor().ToString();
            timeTextBox.Text = m_iNestingTime.ToString();

            if (m_nestParam.IsPartInPart())
            {
                partInPartCheckBox.Checked = true;
            }
            else
            {
                partInPartCheckBox.Checked = false;
            }

            if (m_nestParam.EnableOptimization())
            {
                optimizationCheckBox.Checked = true;
            }
            else
            {
                optimizationCheckBox.Checked = false;
            }

            GetConfigHigh();
        }

        public int GetNestingTime() { return m_iNestingTime; }

        private void okBtn_Click(object sender, EventArgs e)
        {
            // verify input.
            try
            {
                Convert.ToDouble(tolTextBox.Text);
                Convert.ToDouble(rotTextBox.Text);
                Convert.ToInt32(evalTextBox.Text);
                Convert.ToInt32(timeTextBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input: " + exception.Message, "NestProfessor DEMO");
                return;
            }

            // updating the nest param.
            m_nestParam.SetConTol(Convert.ToDouble(tolTextBox.Text));
            m_nestParam.SetPartRotStep(Convert.ToDouble(rotTextBox.Text));
            m_nestParam.SetEvalFactor(Convert.ToInt32(evalTextBox.Text));
            m_iNestingTime = Convert.ToInt32(timeTextBox.Text);

            ConfigHighEntity cfg = new ConfigHighEntity();
            cfg.ConnectTolerance = Convert.ToDouble(tolTextBox.Text);
            cfg.PartRotateStep = Convert.ToDouble(rotTextBox.Text);
            cfg.EvaluationFactor = Convert.ToInt32(evalTextBox.Text);
            cfg.NestTime = Convert.ToInt32(timeTextBox.Text);

            if (partInPartCheckBox.Checked)
            {
                m_nestParam.IsPartInPart(true);
                cfg.PartInPart = true;
            }
            else
            {
                m_nestParam.IsPartInPart(false);
                cfg.PartInPart = false;
            }
            if (optimizationCheckBox.Checked)
            {
                m_nestParam.EnableOptimization(true);
                cfg.Optimization = true;
            }
            else
            {
                m_nestParam.EnableOptimization(false);
                cfg.Optimization = false;
            }

            SaveConfig(cfg);
            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SaveConfig(ConfigHighEntity config)
        {
            string file = System.IO.Directory.GetCurrentDirectory() + "\\ConfigHigh.txt";

            using(StreamWriter sw = new StreamWriter(file, false))
            {
                sw.Write(Tool.Tools.ObjToJsonString(config));
                sw.Flush();
                sw.Close();
            }
        }

        private void GetConfigHigh()
        {
            string file = System.IO.Directory.GetCurrentDirectory() + "\\ConfigHigh.txt";

            if (File.Exists(file) == false)
                return;

            ConfigHighEntity cfg = Tool.Tools.JsonStingToObj<ConfigHighEntity>(File.ReadAllText(file));
            tolTextBox.Text = cfg.ConnectTolerance.ToString();
            rotTextBox.Text = cfg.PartRotateStep.ToString();
            evalTextBox.Text = cfg.EvaluationFactor.ToString();
            timeTextBox.Text = cfg.NestTime.ToString();

            if (cfg.PartInPart == true)
                partInPartCheckBox.Checked = true;
            else
                partInPartCheckBox.Checked = false;

            if (cfg.Optimization == true)
                optimizationCheckBox.Checked = true;
            else
                optimizationCheckBox.Checked = false;
        }
    }
}
