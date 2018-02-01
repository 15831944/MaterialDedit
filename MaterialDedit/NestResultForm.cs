using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using NestBridge;
using Gma.QrCodeNet.Encoding;
using MaterialDedit;

namespace MaterialDedit
{
    public partial class NestResultForm : Form
    {
        private NestTaskEx m_nestTask = null;
        private NestProcessorEx m_nestProcessor = null;
        private SheetListEx m_sheetList = null;
        private NestParamEx m_nestParam = null;

        // the count of the returned nesting result.
        private int m_iRetRstNum = 0;

        // allowed nesting time(s).
        private int m_iNestingTime;

        ImpDataListEx m_impDataList;
        Dictionary<long, int> m_partColorConfig; // part id and color.

        // draw the selected sheet.
        private GlViewPortEx m_shtViewPort = new GlViewPortEx(); // the view port.
        private Point m_referPt; // the reference point for pan the view port.

        // sometimes we need to disable some event.
        private bool m_bDisableEvent = false;

        // the nest result.
        private NestResult m_nestResult = new NestResult();

        public NestResultForm(NestTaskEx nestTask, NestParamEx nestParam, NestProcessorEx nestProcessor, int iNestingTime, ImpDataListEx impDataList, Dictionary<long, int> partColorConfig)
        {
            m_nestTask = nestTask;
            m_nestParam = nestParam;
            m_nestProcessor = nestProcessor;
            m_iNestingTime = iNestingTime;
            m_impDataList = impDataList;
            m_partColorConfig = partColorConfig;

            InitializeComponent();
        }

        private void NestResultForm_Load(object sender, EventArgs e)
        {
            statusTextBox.Text = "Running";

            // init the view port.
            m_shtViewPort.InitEnv(shtPreViewWnd.Handle, 0.00001, 10000);

            // start the watcher.
            NestRstWatcher nestRstWatcher = new NestRstWatcher(m_nestProcessor, m_nestResult, m_iNestingTime);
            Thread thread = new Thread(nestRstWatcher.Run);
            thread.Start();
        }

        private void NestResultForm_Paint(object sender, PaintEventArgs e)
        {
            // preview the selected sheet.
            PreviewSheet();
        }

        private void viewShtBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count != 1)
            {
                MessageBox.Show("Please select one sheet to view.", "NestProfessor DEMO");
                return;
            }
            else
            {
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);
                string M_Name = sheet.GetMat().GetName();
                PartPmtListEx partPmtList = sheet.GetPartPmtList();
                int partsCount = partPmtList.GetPartList().Size();
                Rect2DEx sheetRec = sheet.GetMat().GetGeomItemList().GetItemByIndex(0).GetRectBox();
                string info = "SheetName:" + M_Name + "\r\nWidth:" + sheetRec.GetWidth().ToString() + "--Height:" + sheetRec.GetHeight().ToString();
                for (int i = 0; i < partsCount; i++)
                {
                    PartEx partEx = partPmtList.GetPartList().GetPartByIndex(i);
                    PartPmtEx part = sheet.GetPartTopItemList().GetPartPmtByIndex(i).GetPartPmt();
                    Rect2DEx re2 = part.GetRectBox();
                    Matrix2DEx Matrix = part.GetMatrix();
                    info += "\r\nPartName:" + partEx.GetName()
                        + "\r\n角度:" + Math.Round(Math.Asin(Matrix.GetMatVal(0, 1)) * 180 / Math.PI).ToString() + "度"
                        + "\r\nX:" + Math.Round(Matrix.GetMatVal(2, 0)).ToString()
                        + "\tY:" + Math.Round(Matrix.GetMatVal(2, 1)).ToString()
                        + "\r\n长:" + Math.Round(re2.GetWidth()).ToString()
                        + "\t宽:" + Math.Round(re2.GetHeight()).ToString()
                        + "\t面积:" + Math.Round(re2.GetWidth() * re2.GetHeight()).ToString();
                }

                Bitmap bmp = new Bitmap(300, 300);
                Graphics GraphicsObj = Graphics.FromImage(bmp);
                GraphicsObj.Clear(Color.White);
                //写字
                string str = "OrderNo:0025";
                Font font = new Font("微软雅黑", 15f);
                double strWidth = TextRenderer.MeasureText(str, font).Width;
                Brush brush = Brushes.Red;
                PointF point = new PointF(10f, 10f);
                GraphicsObj.DrawString(str, font, brush, 10, 10);
                //画图形
                Pen myPen = new Pen(Color.Red, 1);
                GraphicsObj.DrawRectangle(myPen, 50, 50, 30, 30);//画矩形
                SolidBrush myBrush = new SolidBrush(Color.Red);
                GraphicsObj.FillRectangle(myBrush, 70, 70, 30, 30);
                //添加图形
                Bitmap tBtm = new Bitmap(1000, 1000);
                Brush blackrush = Brushes.Black;
                Graphics gTest = Graphics.FromImage(tBtm);
                gTest.FillRectangle(blackrush, 0, 0, 1000, 1000);
                GraphicsObj.DrawImage(tBtm, 100, 100, 50, 50);





                MessageBox.Show(info);

                if (sheet != null)
                {
                    SheetInfoForm form = new SheetInfoForm(m_impDataList, m_partColorConfig, sheet);
                    form.ShowDialog();
                }
            }
        }


        private void remMatBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count != 1)
            {
                MessageBox.Show("Please select one sheet.", "NestProfessor DEMO");
                return;
            }
            else
            {
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);
                if (sheet != null)
                {
                    RemnantMatInfoForm form = new RemnantMatInfoForm(sheet, m_nestTask.GetNestParam().GetConTol());
                    form.ShowDialog();
                }
            }
        }

        private void saveShtBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count != 1)
            {
                MessageBox.Show("Please select one sheet to save.", "NestProfessor DEMO");
                return;
            }
            else
            {
                // get the selected sheet object.
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);

                // save sheet as dxf file.
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "DXF Files|*.dxf";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String strFilePath = saveFileDialog.FileName;
                    GeomItemListEx geomItemList = sheet.GetGeomItems(true);
                    //NestFacadeEx.GeomItems2DxfDwg(geomItemList, strFilePath, false); // save line/arc to dxf/dwg instead of saving polylines.
                    NestFacadeEx.Sheet2DxfDwg(sheet, m_impDataList, strFilePath, false);
                }
            }
        }

        private void shtListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count == 1)
            {
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);
                if (sheet != null)
                {
                    SheetInfoForm form = new SheetInfoForm(m_impDataList, m_partColorConfig, sheet);
                    form.ShowDialog();
                }
            }
        }

        private void shtListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bDisableEvent)
            {
                PreviewSheet();
            }
        }

        private void checkTimer_Tick(object sender, EventArgs e)
        {
            // update the elapsed time.
            costTimeTextBox.Text = m_nestResult.GetElapsedTime().ToString();

            // whether we displayed all results and kill the timer.
            bool bFinished = false;
            if (m_nestResult.TaskFinished() && m_iRetRstNum == m_nestResult.GetNestResultCount())
            {
                bFinished = true;
            }

            if (bFinished)
            {
                statusTextBox.Text = "Stopped";
                checkTimer.Enabled = false; // stop the timer.
                stopBtn.Enabled = false; // disable the "stop" button.
                allResultBtn.Enabled = true; // disable the "all result" button.

                // 停止进度条动画。
                nestProgressBar.Hide();
            }
            else
            {
                if (m_iRetRstNum < m_nestResult.GetNestResultCount())
                {
                    // get the next result.
                    m_iRetRstNum++;
                    m_sheetList = m_nestResult.GetNestResultByIndex(m_iRetRstNum - 1);

                    // display the nest result.
                    {
                        countTextBox.Text = m_iRetRstNum.ToString();
                        DisplayNestResult();
                    }
                }
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要停止排版吗？", "排版结果", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            m_nestProcessor.StopNest();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (m_nestProcessor.IsStopped())
            {
                checkTimer.Enabled = false;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Nesting is still running, please stop it and then click quit button!", "NestProfessor DEMO");
            }
        }

        // display the nesting result.
        private void DisplayNestResult()
        {
            if (m_sheetList == null)
                return;

            m_bDisableEvent = true;

            // display detail info of each sheet.
            shtListView.Items.Clear();
            for (int i = 0; i < m_sheetList.Size(); i++)
            {
                SheetEx sheet = m_sheetList.GetSheetByIndex(i);

                // insert a row.
                int iCount = shtListView.Items.Count + 1;
                ListViewItem item = shtListView.Items.Add(iCount.ToString());

                // "name" column.
                item.SubItems.Add(sheet.GetName());

                // "sheet count" column.
                item.SubItems.Add(sheet.GetCount().ToString());

                // "material name" column.
                item.SubItems.Add(sheet.GetMat().GetName());

                // hold the sheet ID.
                item.Tag = sheet.GetID();
            }

            /************************************************************************/
            // part group.

            NestPartListEx nestPartList = m_nestTask.GetNestPartList();

            // submitted part count.
            int iSubmitPartCount = 0;
            for (int i = 0; i < nestPartList.Size(); i++)
                iSubmitPartCount += nestPartList.GetNestPartByIndex(i).GetNestCount();
            subPartTextBox.Text = iSubmitPartCount.ToString();

            // the count of the nested parts.
            int iNestedPartCount = m_sheetList.GetPartInstTotalCount();
            nestPartTextBox.Text = iNestedPartCount.ToString();

            // display detailed info of each part.
            partListView.Items.Clear();
            for (int i = 0; i < nestPartList.Size(); i++)
            {
                NestPartEx nestPart = nestPartList.GetNestPartByIndex(i);
                PartEx part = nestPart.GetPart();

                // insert a row.
                int iCount = partListView.Items.Count + 1;
                ListViewItem item = partListView.Items.Add(iCount.ToString());

                // "name" column.
                item.SubItems.Add(part.GetName());

                // "submitted count" column.
                item.SubItems.Add(nestPart.GetNestCount().ToString());

                // "nested count" column.
                int iNestedCount = m_sheetList.GetPartInstCount(part.GetID());
                item.SubItems.Add(iNestedCount.ToString());
            }
            /************************************************************************/

            /************************************************************************/
            // material group.

            MatListEx matList = m_nestTask.GetMatList();

            // the utilization of material.
            double dUtilization = NestHelper.CalcMatUtil(m_sheetList, m_nestTask.GetNestParam());
            utilTextBox.Text = dUtilization.ToString("0.00");

            matListView.Items.Clear();
            for (int i = 0; i < matList.Size(); i++)
            {
                MatEx mat = matList.GetMatByIndex(i);

                // insert a row.
                int iCount = matListView.Items.Count + 1;
                ListViewItem item = matListView.Items.Add(iCount.ToString());

                // "name" column.
                item.SubItems.Add(mat.GetName());

                // "submitted count" column.
                item.SubItems.Add(mat.GetCount().ToString());

                // "consumed count" column.
                int iConsumedCount = m_sheetList.GetSheetCount(mat.GetID());
                item.SubItems.Add(iConsumedCount.ToString());
            }
            /************************************************************************/

            // preview the first sheet.
            if (shtListView.Items.Count > 0)
            {
                shtListView.Items[0].Selected = true;

                // get the select sheet.
                ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx selectedSheet = m_sheetList.GetSheetByID(iSheetID);

                // fit the window.
                DrawHelper.FitWindow(selectedSheet.GetMat().GetBoundaryRect(), m_shtViewPort, shtPreViewWnd);

                PreviewSheet();
            }

            m_bDisableEvent = false;
        }

        private void shtViewWnd_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            double dRate = 1.25;
            if (e.Delta > 0)
                dRate = 1 / dRate;

            m_shtViewPort.ZoomViewPort(dRate, e.X, e.Y);
            Invalidate();
        }

        private void shtViewWnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                m_referPt.X = e.X;
                m_referPt.Y = e.Y;
            }
        }

        private void shtViewWnd_MouseEnter(object sender, EventArgs e)
        {
            shtPreViewWnd.Focus();
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
        }

        private void NestResultForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_shtViewPort.ReleaseResource();
        }

        // preview the selected sheet.
        private void PreviewSheet()
        {
            // get the select sheet.
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count == 0)
                return;
            ListViewItem item = selItems[0];
            long iSheetID = (long)item.Tag;
            SheetEx selectedSheet = m_sheetList.GetSheetByID(iSheetID);

            // clear screen and set the background color.
            m_shtViewPort.BindRendContext();
            m_shtViewPort.ClearScreen();
            m_shtViewPort.SetBackgroundColor(Color.Black);

            // draw coordinate.
            m_shtViewPort.SetDrawColor(Color.Blue);
            m_shtViewPort.SetLineWidth(1);
            m_shtViewPort.DrawCoordinate(0, 0, 0, false);

            // draw material.
            DrawHelper.DrawMat(selectedSheet.GetMat(), m_shtViewPort);

            // draw parts.
            DrawHelper.DrawPartPmts(selectedSheet.GetPartPmtList(), null, m_shtViewPort, m_partColorConfig, m_impDataList);

            // swap buffer to display the geometry.
            m_shtViewPort.SwapBuffers();

            // the rect of part region.
            Rect2DEx partsRect = selectedSheet.GetPartPmtList().GetRectBox();
            partRegionTextBox.Text = partsRect.GetWidth().ToString("0.000") + " * " + partsRect.GetHeight().ToString("0.000");
        }

        private void allResultBtn_Click(object sender, EventArgs e)
        {
            AllResultsForm dlg = new AllResultsForm(m_nestResult.GetNestResultCount());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int iSelectedResultIndex = dlg.GetSelectedResultIndex();
                m_sheetList = m_nestResult.GetNestResultByIndex(iSelectedResultIndex);
                DisplayNestResult();
            }
        }

        private void shtListView_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = shtListView.SelectedItems;
            if (selItems.Count == 1)
            {
                ListViewItem item = selItems[0];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);
                if (sheet != null)
                {
                    SheetInfoForm form = new SheetInfoForm(m_impDataList, m_partColorConfig, sheet);
                    form.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 生成二维码及缩略图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQrCode_Click(object sender, EventArgs e)
        {
            ListView listView = shtListView;
            if (shtListView.Items.Count <= 0)
            {
                MessageBox.Show("集合为空");
                return;
            }
            QrCodeForm qrCodeForm = new QrCodeForm(listView, m_sheetList, m_nestParam);
            qrCodeForm.ShowDialog();
        }
    }
}
