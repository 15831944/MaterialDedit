using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using NestBridge;

namespace MaterialDedit
{
    public partial class RemnantMatInfoForm : Form
    {
        private SheetEx m_sheet;
        private double m_dConnectTol;

        private PolyMatEx m_polyMat;
        private GlViewPortEx m_matViewPort = new GlViewPortEx();

        // the reference point for pan the view port.
        private Point m_referPt;

        // the selected polygon.
        private Polygon2DEx m_selPoly = null;

        public RemnantMatInfoForm(SheetEx sheet, double dConnectTol)
        {
            m_sheet = sheet;
            m_dConnectTol = dConnectTol;

            InitializeComponent();
        }

        private void RemnantMatInfoForm_Load(object sender, EventArgs e)
        {
        	// build the remnant material from the sheet.
            double dMergeDis = 10;
            m_polyMat = NestFacadeEx.BuildRemnantMat(m_sheet, m_dConnectTol, dMergeDis);

            // init the tree.
            UpdateTree();

            /************************************************************************/
            // init the view port.

            m_matViewPort.InitEnv(matViewWnd.Handle, 0.00001, 10000);

            // all polygons in material.
	        Poly2DListEx poly2DList = new Poly2DListEx();
            poly2DList.AddPoly(m_polyMat.GetMatPolygon());
            poly2DList.AddPolyList(m_polyMat.GetUselessHoleList());

            // set the drawing area.
            Int32 iWidth = matViewWnd.Right - matViewWnd.Left;
            Int32 iHeight = matViewWnd.Bottom - matViewWnd.Top;
            Rect2DEx geomRect = poly2DList.GetRectBox();
            Point2DEx leftBottomPt = new Point2DEx();
            double dXDirRange = m_matViewPort.GetFitAllParam(iWidth, iHeight, geomRect, 1.2, leftBottomPt);
            m_matViewPort.SetDrawingArea(1.1 * dXDirRange, iWidth, iHeight, leftBottomPt);
            /************************************************************************/

            mergeDisTextBox.Text = dMergeDis.ToString("0.000");
        }

        // update the tree.
        private void UpdateTree()
        {
            polyTreeView.Nodes.Clear();

            // append the root node.
            TreeNode rootNode = polyTreeView.Nodes.Add("Boundary Polygon");
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            rootNode.Tag = m_polyMat.GetMatPolygon().GetID();

            // append the inner polygons.
            Poly2DListEx uselessHoleList = m_polyMat.GetUselessHoleList();
            for (int i = 0; i < uselessHoleList.Size(); i++)
            {
                Polygon2DEx poly = uselessHoleList.GetPolygonByIndex(i);

                String strNodeName = "Inner Polygon ";
                strNodeName += (i + 1);
                TreeNode polyNode = rootNode.Nodes.Add(strNodeName);
                polyNode.ImageIndex = 0;
                polyNode.SelectedImageIndex = 0;
                polyNode.Tag = poly.GetID();
            }

            // expand the tree.
            polyTreeView.ExpandAll();
        }

        private void RemnantMatInfoForm_Paint(object sender, PaintEventArgs e)
        {
            if (m_polyMat != null)
            {
                // all polygons in material.
	            Poly2DListEx poly2DList = new Poly2DListEx();
                poly2DList.AddPoly(m_polyMat.GetMatPolygon());
                poly2DList.AddPolyList(m_polyMat.GetUselessHoleList());

                // clear screen and set the background color.
                m_matViewPort.BindRendContext();
                m_matViewPort.ClearScreen();
                m_matViewPort.SetBackgroundColor(Color.Black);

                // draw coordinate.
                m_matViewPort.SetDrawColor(Color.Blue);
                m_matViewPort.SetLineWidth(1);
                m_matViewPort.DrawCoordinate(0, 0, 0, false);

                // draw material polygons.
		        m_matViewPort.SetDrawColor(Color.White);
		        m_matViewPort.SetLineWidth(1);
		        for (int i = 0; i < poly2DList.Size(); i++)
		        {
			        Polygon2DEx poly = poly2DList.GetPolygonByIndex(i);
			        ArrayList lineItems = poly.GetLineList();
                    for (int j = 0; j < lineItems.Count; j++)
                        m_matViewPort.DrawLineItem((LineItemEx)lineItems[j]);
		        }

                // draw selected polygon.
                if (m_selPoly != null)
                {
                    // hold the current drawing mode.
                    ROP_MODE_EX iOldRopMode = ROP_MODE_EX.ROP_EX_NORMAL;
                    m_matViewPort.GetROP(ref iOldRopMode);

                    // hold the current drawing width.
                    int iOldLineWid = m_matViewPort.GetLineWidth();

                    // hold the current drawing color.
                    Color oldColor = new Color();
                    m_matViewPort.GetDrawColor(ref oldColor);

                    // get the stipple mode.
                    bool bOldStipple = false;
                    int iOldRepeat = 1;
                    ushort iOldPattern = 0xffff;
                    m_matViewPort.GetLineStipple(ref bOldStipple, ref iOldRepeat, ref iOldPattern);

                    // draw selected part placements.
                    m_matViewPort.SetROP(ROP_MODE_EX.ROP_EX_COPY);
                    m_matViewPort.SetLineWidth(2);
                    m_matViewPort.SetDrawColor(Color.Red);
                    m_matViewPort.SetLineStipple(true, 2, 0xcccc);
                    ArrayList lineItems = m_selPoly.GetLineList();
                    for (int j = 0; j < lineItems.Count; j++)
                        m_matViewPort.DrawLineItem((LineItemEx)lineItems[j]);

                    // restore the old drawer config.
                    m_matViewPort.SetROP(iOldRopMode);
                    m_matViewPort.SetLineWidth(iOldLineWid);
                    m_matViewPort.SetDrawColor(oldColor);
                    m_matViewPort.SetLineStipple(bOldStipple, iOldRepeat, iOldPattern);
                }

                // swap buffer to display the geometry.
                m_matViewPort.SwapBuffers();
            }
        }

        private void matViewWnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                m_referPt.X = e.X;
                m_referPt.Y = e.Y;
            }
        }

        private void matViewWnd_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double dRate = 1.25;
            if (e.Delta > 0)
                dRate = 1 / dRate;

            m_matViewPort.ZoomViewPort(dRate, e.X, e.Y);

            Invalidate();
        }

        private void matViewWnd_MouseEnter(object sender, EventArgs e)
        {
            matViewWnd.Focus();
        }

        private void matViewWnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Point pt = new Point(e.X, e.Y);
                m_matViewPort.PanViewPort(pt.X - m_referPt.X, pt.Y - m_referPt.Y);
                m_referPt = pt;

                Invalidate();
            }

            // display the current coordinate.
            Point2DEx currentPt = m_matViewPort.GetCursorPos();
            posTextBox.Text = "x= " + currentPt.X().ToString("0.000") + ",  y= " + currentPt.Y().ToString("0.000");
        }

        private void polyTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;

            // all polygons in material.
            Poly2DListEx poly2DList = new Poly2DListEx();
            poly2DList.AddPoly(m_polyMat.GetMatPolygon());
            poly2DList.AddPolyList(m_polyMat.GetUselessHoleList());

            // the selected polygon.
            long iPolygonID = (long)treeNode.Tag;
            m_selPoly = poly2DList.GetPolygonByID(iPolygonID);

            Invalidate();
        }

        private void reGenBtn_Click(object sender, EventArgs e)
        {
            // verify input.
            try
            {
                Convert.ToDouble(mergeDisTextBox.Text);
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Incorrect input: " + exception.Message, "NestProfessor DEMO");
                return;
            }

        	// re-generate the remnant material from the sheet.
            m_polyMat = NestFacadeEx.BuildRemnantMat(m_sheet, m_dConnectTol, Convert.ToDouble(mergeDisTextBox.Text));

            // update tree nodes.
            UpdateTree();

            /************************************************************************/
            // adjust the drawing area.

            // the boundary rect of all polygons in material.
            Poly2DListEx poly2DList = new Poly2DListEx();
            poly2DList.AddPoly(m_polyMat.GetMatPolygon());
            poly2DList.AddPolyList(m_polyMat.GetUselessHoleList());
            Rect2DEx geomRect = poly2DList.GetRectBox();

            // set the drawing area.
            Int32 iWidth = matViewWnd.Right - matViewWnd.Left;
            Int32 iHeight = matViewWnd.Bottom - matViewWnd.Top;
            Point2DEx leftBottomPt = new Point2DEx();
            double dXDirRange = m_matViewPort.GetFitAllParam(iWidth, iHeight, geomRect, 1.2, leftBottomPt);
            m_matViewPort.SetDrawingArea(1.1 * dXDirRange, iWidth, iHeight, leftBottomPt);

            m_selPoly = null;
            Invalidate();
            /************************************************************************/
        }

        private void saveDxfBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "DXF Files|*.dxf";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String strFilePath = saveFileDialog.FileName;

                // save the remnant material as dxf file.
		        PolyMatEx polyMat = NestFacadeEx.BuildRemnantMat(m_sheet, m_dConnectTol, Convert.ToDouble(mergeDisTextBox.Text));
		        GeomItemListEx geomItemList = polyMat.GetGeomItemList();
                NestFacadeEx.GeomItems2DxfDwg(geomItemList, strFilePath, false);
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void RemnantMatInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_matViewPort.ReleaseResource();
        }
    }
}
