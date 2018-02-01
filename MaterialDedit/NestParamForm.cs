using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NestBridge;
using Entities;
using System.IO;
using Tool;

namespace MaterialDedit
{
    public partial class NestParamForm : Form
    {
        // the nesting param.
        private NestParamEx m_nestParam;

        private int m_iNestingTime;

        private List<ConfigEntity> lstConfig;

        public NestParamForm(NestParamEx nestParam, int iNestingTime)
        {
            m_nestParam = nestParam;
            m_iNestingTime = iNestingTime;

            InitializeComponent();
        }

        public int GetNestingTime() { return m_iNestingTime; }

        private void advBtn_Click(object sender, EventArgs e)
        {
            AdvParamForm form = new AdvParamForm(m_nestParam, m_iNestingTime);
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_iNestingTime = form.GetNestingTime();
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            // verify input.
            try
            {
                Convert.ToDouble(topTextBox.Text);
                Convert.ToDouble(bottomTextBox.Text);
                Convert.ToDouble(leftTextBox.Text);
                Convert.ToDouble(rightTextBox.Text);
                Convert.ToDouble(marginTextBox.Text);
                Convert.ToDouble(spaceTextBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("输入的参数不合法: " + exception.Message, "排版参数");
                return;
            }

            ConfigEntity config = new ConfigEntity();
            config.ConfigName = tbConfigName.Text;

            /************************************************************************/
            // update the nesting param.
            // the border margin of the material.
            m_nestParam.SetMatLeftMargin(Convert.ToDouble(leftTextBox.Text));
            m_nestParam.SetMatRightMargin(Convert.ToDouble(rightTextBox.Text));
            m_nestParam.SetMatTopMargin(Convert.ToDouble(topTextBox.Text));
            m_nestParam.SetMatBottomMargin(Convert.ToDouble(bottomTextBox.Text));
            m_nestParam.SetMatMargin(Convert.ToDouble(marginTextBox.Text));

            config.MatLeftMargin = Convert.ToDouble(leftTextBox.Text);
            config.MatRightMargin = Convert.ToDouble(rightTextBox.Text);
            config.MatTopMargin = Convert.ToDouble(topTextBox.Text);
            config.MatBottomMargin = Convert.ToDouble(bottomTextBox.Text);
            config.MatMargin = Convert.ToDouble(marginTextBox.Text);

            // the part spacing.
            m_nestParam.SetPartDis(Convert.ToDouble(spaceTextBox.Text));

            config.PartDis = Convert.ToDouble(spaceTextBox.Text);

            // the start nesting corner.
            if (ltRadioBtn.Checked)
            {
                m_nestParam.SetStartCorner(RECT_CORNER_EX.LEFT_TOP);
                config.StartCorner = Corner.LeftTop;
            }
            else if (lbRadioBtn.Checked)
            {
                m_nestParam.SetStartCorner(RECT_CORNER_EX.LEFT_BOTTOM);
                config.StartCorner = Corner.LeftBottom;
            }
            else if (rtRadioBtn.Checked)
            {
                m_nestParam.SetStartCorner(RECT_CORNER_EX.RIGHT_TOP);
                config.StartCorner = Corner.RightTop;
            }
            else if (rbRadioBtn.Checked)
            {
                m_nestParam.SetStartCorner(RECT_CORNER_EX.RIGHT_BOTTOM);
                config.StartCorner = Corner.RightBottom;
            }

            // the nesting direction.
            if (xRadioBtn.Checked)
            {
                m_nestParam.SetNestDir(NEST_DIRECTION_EX.NEST_DIR_X);
                config.DirValue = Dir.XDir;
            }
            else if (yRadioBtn.Checked)
            {
                m_nestParam.SetNestDir(NEST_DIRECTION_EX.NEST_DIR_Y);
                config.DirValue = Dir.YDir;
            }
            /************************************************************************/

            //保存参数到本地
            SaveConfig(config);

            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void NestParamForm_Load(object sender, EventArgs e)
        {
            // the border margin of the material.
            topTextBox.Text = m_nestParam.GetMatTopMargin().ToString("0.000");
            bottomTextBox.Text = m_nestParam.GetMatBottomMargin().ToString("0.000");
            leftTextBox.Text = m_nestParam.GetMatLeftMargin().ToString("0.000");
            rightTextBox.Text = m_nestParam.GetMatRightMargin().ToString("0.000");
            marginTextBox.Text = m_nestParam.GetMatMargin().ToString("0.000");

            // the part spacing.
            spaceTextBox.Text = m_nestParam.GetPartDis().ToString("0.000");

            // the start nesting corner.
            if (m_nestParam.GetStartCorner() == RECT_CORNER_EX.LEFT_TOP)
                ltRadioBtn.Checked = true;
            else if (m_nestParam.GetStartCorner() == RECT_CORNER_EX.LEFT_BOTTOM)
                lbRadioBtn.Checked = true;
            else if (m_nestParam.GetStartCorner() == RECT_CORNER_EX.RIGHT_TOP)
                rtRadioBtn.Checked = true;
            else if (m_nestParam.GetStartCorner() == RECT_CORNER_EX.RIGHT_BOTTOM)
                rbRadioBtn.Checked = true;

            // the nesting direction.
            if (m_nestParam.GetNestDir() == NEST_DIRECTION_EX.NEST_DIR_X)
                xRadioBtn.Checked = true;
            else if (m_nestParam.GetNestDir() == NEST_DIRECTION_EX.NEST_DIR_Y)
                yRadioBtn.Checked = true;

            lstConfig = GetConfig();
            int iCount = 1;
            foreach (var cfg in lstConfig)
            {
                ListViewItem item = lvConfigList.Items.Add(iCount.ToString());

                // part name column.
                item.SubItems.Add(cfg.ConfigName);
                item.Tag = cfg;
                iCount++;
            }
        }

        private void SaveConfig(ConfigEntity config)
        {
            lstConfig.Add(config);

            string path = System.IO.Directory.GetCurrentDirectory() + "\\ConfigInfo";
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            if (string.IsNullOrEmpty(config.ConfigName) == true || 
                config.ConfigName == "名称默认为当前时间")
                config.ConfigName = DateTime.Now.ToString("yyyyMMdd HHmmss");

            using (StreamWriter sw = new StreamWriter(path + "\\" + config.ConfigName + ".txt", false))
            {
                sw.Write(Tools.ObjToJsonString(config));
                sw.Flush();
                sw.Close();
            }
        }

        private void lvConfigList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = lvConfigList.SelectedItems;
            if (selItems.Count == 0)
                return;

            ListViewItem item = selItems[0];
            ConfigEntity cfg = (ConfigEntity)item.Tag;

            leftTextBox.Text = cfg.MatLeftMargin.ToString();
            topTextBox.Text = cfg.MatTopMargin.ToString();
            rightTextBox.Text = cfg.MatRightMargin.ToString();
            bottomTextBox.Text = cfg.MatBottomMargin.ToString();

            marginTextBox.Text = cfg.MatMargin.ToString();

            spaceTextBox.Text = cfg.PartDis.ToString();

            tbConfigName.Text = cfg.ConfigName;

            switch (cfg.StartCorner)
            {
                case Corner.LeftTop: ltRadioBtn.Checked = true; break;
                case Corner.LeftBottom: lbRadioBtn.Checked = true; break;
                case Corner.RightTop: rtRadioBtn.Checked = true; break;
                case Corner.RightBottom: rbRadioBtn.Checked = true; break;
            }

            if (cfg.DirValue == Dir.XDir)
                xRadioBtn.Checked = true;
            else
                yRadioBtn.Checked = true;
        }

        private List<ConfigEntity> GetConfig()
        {
            List<ConfigEntity> lstResult = new List<ConfigEntity>();

            string path = System.IO.Directory.GetCurrentDirectory() + "\\ConfigInfo";
            if (Directory.Exists(path) == false)
                return lstResult;

            var files = Directory.GetFiles(path, "*.txt");

            foreach (var file in files)
                lstResult.Add(Tool.Tools.JsonStingToObj<ConfigEntity>(File.ReadAllText(file)));

            return lstResult;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            leftTextBox.Text = "0.00";
            topTextBox.Text = "0.00";
            rightTextBox.Text = "0.00";
            bottomTextBox.Text = "0.00";
            marginTextBox.Text = "0.00";
            spaceTextBox.Text = "0.00";
            tbConfigName.Text = string.Empty;

            lbRadioBtn.Checked = true;
            yRadioBtn.Checked = true;
        }

        private void tbConfigName_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tbConfigName.Text) == true)
            //    tbConfigName.Text = "名称默认为当前时间";
        }

        private void tbConfigName_MouseEnter(object sender, EventArgs e)
        {
            if (tbConfigName.Text == "名称默认为当前时间")
                tbConfigName.Clear();
        }

        private void tbConfigName_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbConfigName.Text) == true)
                tbConfigName.Text = "名称默认为当前时间";
        }
    }
}
