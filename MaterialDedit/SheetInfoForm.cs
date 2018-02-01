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
    public partial class SheetInfoForm : Form
    {
        private ImpDataListEx m_impDataList;
        private Dictionary<long, int> m_partColorConfig;
        private SheetEx m_sheet;
        private GlViewPortEx m_shtViewPort = new GlViewPortEx();

        // the reference point for pan the view port.
        private Point m_referPt;

        // the select part placements.
        PartPmtListEx m_selPartPmtList = new PartPmtListEx();

        public SheetInfoForm(ImpDataListEx impDataList, Dictionary<long, int> partColorConfig, SheetEx sheet)
        {
            m_impDataList = impDataList;
            m_partColorConfig = partColorConfig;
            m_sheet = sheet;

            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void SheetInfoForm_Load(object sender, EventArgs e)
        {
            viewTypeComboBox.Items.Add("View By Part Name");
            viewTypeComboBox.Items.Add("View By Part Topology");
            viewTypeComboBox.SelectedIndex = 0;

            // init the tree by part name.
            InitTreeByPart();
     
            // init the view port.
            m_shtViewPort.InitEnv(shtViewWnd.Handle, 0.00001, 10000);
            DrawHelper.FitWindow(m_sheet.GetMat().GetBoundaryRect(), m_shtViewPort, shtViewWnd);
        }

        // init the tree by part name.
        private void InitTreeByPart()
        {
            partPmtTreeView.Nodes.Clear();

            PartPmtListEx partPmtList = m_sheet.GetPartPmtList();
            PartListEx partList = partPmtList.GetPartList();
            for (int i = 0; i < partList.Size(); i++)
            {
                PartEx part = partList.GetPartByIndex(i);

                // append the part node.
                TreeNode partNode = partPmtTreeView.Nodes.Add(part.GetName());
                partNode.ImageIndex = 0;
                partNode.SelectedImageIndex = 0;
                partNode.Tag = part.GetID();

                // insert each part pmt node.
                PartPmtListEx partPmtList1 = partPmtList.GetAllPmtOfPart(part.GetID());
                for (int j = 0; j < partPmtList1.Size(); j++)
                {
                    PartPmtEx partPmt = partPmtList1.GetPartPmtByIndex(j);

                    // append the part placement node.
                    String strNodeName = "Placement ";
                    strNodeName += (j + 1);
                    TreeNode pmtNode = partNode.Nodes.Add(strNodeName);
                    pmtNode.ImageIndex = 1;
                    pmtNode.SelectedImageIndex = 1;
                    pmtNode.Tag = partPmt.GetID();
                }
            }
        }

        // init the tree by part name.
        private void InitTreeByTopology(TreeNodeCollection nodes, PartTopItemListEx partTopItemList)
        {
            for (int i = 0; i < partTopItemList.Size(); i++)
            {
                PartTopItemEx partTopItem = partTopItemList.GetPartPmtByIndex(i);
                PartPmtEx partPmt = partTopItem.GetPartPmt();
                PartEx part = partPmt.GetPart();

                // append a node.
                TreeNode pmtNode = nodes.Add(part.GetName());
                pmtNode.ImageIndex = 1;
                pmtNode.SelectedImageIndex = 1;
                pmtNode.Tag = partPmt.GetID();

                // append the child node.
                PartTopItemListEx subTopItemList = partTopItem.GetSubPartTopItem();
                if (subTopItemList.Size() > 0)
                    InitTreeByTopology(pmtNode.Nodes, subTopItemList);
            }
        }

        private void SheetInfoForm_Paint(object sender, PaintEventArgs e)
        {
            if (m_sheet != null)
            {
                // clear screen and set the background color.
                m_shtViewPort.BindRendContext();
                m_shtViewPort.ClearScreen();
                m_shtViewPort.SetBackgroundColor(Color.Black);

                // draw coordinate.
                m_shtViewPort.SetDrawColor(Color.Blue);
                m_shtViewPort.SetLineWidth(1);
                m_shtViewPort.DrawCoordinate(0, 0, 0, false);

                // draw material.
                DrawHelper.DrawMat(m_sheet.GetMat(), m_shtViewPort);

                // draw parts.
                DrawHelper.DrawPartPmts(m_sheet.GetPartPmtList(), m_selPartPmtList, m_shtViewPort, m_partColorConfig, m_impDataList);

                // swap buffer to display the geometry.
                m_shtViewPort.SwapBuffers();
            }
        }

        private void shtViewWnd_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double dRate = 1.25;
            if (e.Delta > 0)
                dRate = 1 / dRate;

            m_shtViewPort.ZoomViewPort(dRate, e.X, e.Y);

            Invalidate();
        }

        private void shtViewWnd_MouseEnter(object sender, EventArgs e)
        {
            shtViewWnd.Focus();
        }

        private void shtViewWnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Point pt = new Point(e.X, e.Y);
			    m_shtViewPort.PanViewPort(pt.X - m_referPt.X, pt.Y - m_referPt.Y);
                m_referPt = pt;

                Invalidate();
            }

            // display the current coordinate.
            Point2DEx currentPt = m_shtViewPort.GetCursorPos();
            posTextBox.Text = "x= " + currentPt.X().ToString("0.000") + ",  y= " + currentPt.Y().ToString("0.000");
        }

        private void shtViewWnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                m_referPt.X = e.X;
                m_referPt.Y = e.Y;
            }
        }

        private void partPmtTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;

            // clear the selection.
            m_selPartPmtList.Clear();

            if (viewTypeComboBox.SelectedIndex == 0)
            {
                if (treeNode.Nodes.Count > 0)
                {
                    // selected the part node.
                    for (int i = 0; i < treeNode.Nodes.Count; i++)
                    {
                        TreeNode partPmtNode = treeNode.Nodes[i];
                        long iPartPmtID = (long)partPmtNode.Tag;
                        PartPmtEx partPmt = m_sheet.GetPartPmtList().GetPartPmtByID(iPartPmtID);
                        m_selPartPmtList.AddPartPmt(partPmt);
                    }
                }
                else
                {
                    // selected the part pmt node.
                    long iPartPmtID = (long)treeNode.Tag;
                    PartPmtEx partPmt = m_sheet.GetPartPmtList().GetPartPmtByID(iPartPmtID);
                    m_selPartPmtList.AddPartPmt(partPmt);
                }
            }
            else if (viewTypeComboBox.SelectedIndex == 1)
            {
                // selected the part pmt node.
                long iPartPmtID = (long)treeNode.Tag;
                PartPmtEx partPmt = m_sheet.GetPartPmtList().GetPartPmtByID(iPartPmtID);
                m_selPartPmtList.AddPartPmt(partPmt);
            }

            Invalidate();
        }

        private void viewTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (viewTypeComboBox.SelectedIndex == 0)
            {
                // init the tree by part name.
                InitTreeByPart();
            }
            else if (viewTypeComboBox.SelectedIndex == 1)
            {
                partPmtTreeView.Nodes.Clear();

                // init the tree by part name.
                InitTreeByTopology(partPmtTreeView.Nodes, m_sheet.GetPartTopItemList());
            }
        }

        private void SheetInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_shtViewPort.ReleaseResource();
        }
    }
}
