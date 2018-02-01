using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaterialDedit;

namespace MaterialDedit
{
    public partial class FrmMainWindow : Form
    {
        public FrmMainWindow()
        {
            InitializeComponent();
        }

        private void FrmMainWindow_Load(object sender, EventArgs e)
        {
            DemoForm frm = new DemoForm();
            frm.Dock = DockStyle.Fill;

            TabPage tp = new TabPage(TabName.CreateCompose);
            tp.Name = TabName.CreateCompose;
            tp.Controls.Add(frm);

            tcMain.Controls.Add(tp);
        }

        private void 配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    static class TabName
    {
        public static string CreateCompose = "新建排版";
    }
}
