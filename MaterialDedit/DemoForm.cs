using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NestBridge;
using OrderManager;
using System.Threading.Tasks;
using Entities;
using System.IO;

namespace MaterialDedit
{
    public partial class DemoForm : UserControl
    {
        /************************************************************************/
        // variables.

        /// <summary>
        /// 需要排版的零件集合
        /// </summary>
        NestPartListEx m_nestPartList = new NestPartListEx();
        List<KeyValuePair<long, string>> m_partDxfPath = new List<KeyValuePair<long, string>>();
        Dictionary<long, int> m_partColorConfig = new Dictionary<long, int>(); // part id and color.

        /// <summary>
        /// 需要排版的材料集合
        /// </summary>
        MatListEx m_matList = new MatListEx();
        List<KeyValuePair<long, string>> m_matDxfPath = new List<KeyValuePair<long, string>>(); 

        // the nesting params.
        NestParamEx m_nestParam = new NestParamEx();

        GlViewPortEx partViewPort = new GlViewPortEx();
        GlViewPortEx matViewPort = new GlViewPortEx();

        int m_iNestingTime = 60;

        private ConfigEntity CfgNormal;

        private ConfigHighEntity CfgHigh;

        // the dxf data.
        ImpDataListEx m_impDataList = new ImpDataListEx();

        // whether disable selection-change event.
        bool m_bDisableSelChgEvent = false;

        // the current color.
        int m_iCurrentColorIndex = -1;
        /************************************************************************/

        public DemoForm()
        {
            InitializeComponent();

            //设置评估因子 1 => 10
            m_nestParam.SetEvalFactor(10);
        }

        private void DemoForm_Load(object sender, EventArgs e)
        {
            /************************************************************************/
            // init the license.

            // init the license which is bound to local computer.
            ReturnInfoEx retInfo = NestFacadeEx.InitLicense("d:\\nest.NPLic");

            // init license if the license is bound to SafeNet hardware key.
            //ReturnInfoEx retInfo = NestFacadeEx.InitLicense(iDevelopID, softwareKey, iLicenseID, iFlag);
            /************************************************************************/

            // init the view port.
            partViewPort.InitEnv(partPreviewWnd.Handle, 0.00001, 10000);
            matViewPort.InitEnv(matPreviewWnd.Handle, 0.00001, 10000);
            // display the edition of the nest kernel on the dialog title.
            int iLicType = NestFacadeEx.GetLicenseType();
            if (iLicType == 0)
                this.Text = "NestProfessor DEMO  (License type of the nest kernel: Trial Edition)";
            else if (iLicType == 1)
                this.Text = "NestProfessor DEMO  (License type of the nest kernel: Standard Edition)";
            else if (iLicType == 2)
                this.Text = "NestProfessor DEMO  (License type of the nest kernel: Professional Edition)";

            SetConfig();
        }

        private void addPartBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DXF Files|*.dxf|DWG Files|*.dwg";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // disable select-change event.
                m_bDisableSelChgEvent = true;
                lblLoadPart.Text = "零件加载中……";

                Task tk = new Task(new Action(() =>
                {
                    for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                    {
                        String strFilePath = openFileDialog.FileNames[i];

                        //获取文件所在的文件夹名，用做订单号
                        var view = strFilePath.Split('\\');
                        var dir = view[view.Length - 2];

                        // 从Dxf文件中加载零件对象
                        PartEx part = NestHelper.LoadPartFromDxfdwg(strFilePath, m_impDataList);

                        // 判断零件是否是闭合的
                        bool bClosedBoundary = NestFacadeEx.HasClosedBoundary(part, m_nestParam.GetConTol());
                        if (!bClosedBoundary)
                        {
                            MessageBox.Show("零件没有闭合!!!");
                            continue;
                        }

                        // build NestPartEx object.
                        NestPartEx nestPart = new NestPartEx(part, NestPriorityEx.MaxPriority(), 1, PART_ROT_STYLE_EX.PART_ROT_PID2_INCREMENT, false);
                        m_nestPartList.AddNestPart(nestPart);
                        m_partDxfPath.Add(new KeyValuePair<long, string>(nestPart.GetID(), strFilePath));
                        m_partColorConfig[nestPart.GetPart().GetID()] = ColorTranslator.ToOle(NestHelper.PickNextColor_4_part(ref m_iCurrentColorIndex));

                        // add part to list control.
                        this.Invoke(new Action(() =>
                        {
                            AddPart_to_listCtrl(nestPart, dir);
                        }));
                    }
                }));
                tk.Start();
                tk.ContinueWith(t => this.Invoke(new Action(() => { lblLoadPart.Text = string.Empty; })));

                // disable select-change event.
                m_bDisableSelChgEvent = false;

                // select the last row.
                if (partListView.Items.Count > 0)
                {
                    partListView.SelectedItems.Clear();
                    partListView.Items[partListView.Items.Count - 1].Selected = true;
                    partListView.Items[partListView.Items.Count - 1].Focused = true;
                    partListView.Items[partListView.Items.Count - 1].EnsureVisible();
                }
            }
        }

        // add part to list control.
        private void AddPart_to_listCtrl(NestPartEx nestPart, string order)
        {
            // insert a row.
            int iCount = partListView.Items.Count + 1;
            ListViewItem item = partListView.Items.Add(iCount.ToString());

            // part name column.
            item.SubItems.Add(nestPart.GetPart().GetName());

            // priority column.
            item.SubItems.Add(nestPart.GetPriority().GetVal().ToString());

            // nest count column.
            item.SubItems.Add(nestPart.GetNestCount().ToString());

            // rotate angle column.
            String strRotateAng = NestHelper.GetRotateAngName(nestPart.GetRotStyle());

            string temp = string.Empty;
            switch(strRotateAng.Trim())
            {
                case "Free Rotate": temp = "自由旋转";break;
                case "90 Increment": temp = "90度增量"; break;
                case "180 Increment": temp = "180度增量"; break;
                case "0 Fixed": temp = "0度固定"; break;
                case "90 Fixed": temp = "90度固定"; break;
                case "180 Fixed": temp = "180度固定"; break;
                case "270 Fixed": temp = "270度固定"; break;
            }

            item.SubItems.Add(temp);

            // "Part Size" column.
            Rect2DEx partRect = nestPart.GetPart().GetGeomItemList().GetRectBox();
            StringBuilder sb = new StringBuilder();
            sb.Append(partRect.GetWidth().ToString("0.00")).Append("(W) * ").Append(partRect.GetHeight().ToString("0.00")).Append("(H)");
            item.SubItems.Add(sb.ToString());

            //添加订单号
            item.SubItems.Add(order);

            // hold the ID.
            item.Tag = nestPart.GetID();

            OrderManagerDal.Instance.AddOrder(nestPart.GetPart().GetID(), order);
        }

        private void partListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_bDisableSelChgEvent)
                return;

            Preview_selected_part();
        }

        private void matListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (m_bDisableSelChgEvent)
                return;

            Preview_selected_material();
        }

        private void newMatBtn_Click(object sender, EventArgs e)
        {
            // create a RectMatEx object.
            Rect2DEx rect = new Rect2DEx(0, 1200, 0, 2400);
            RectMatEx rectMat = new RectMatEx("新材料", rect, 1);

            MaterialForm matForm = new MaterialForm(rectMat);
            if (matForm.ShowDialog() == DialogResult.OK)
            {
                m_matList.AddMat(rectMat);

                // add material to list control.
                AddMat(rectMat);
            }
        }

        private void loadMatBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DXF Files|*.dxf|DWG Files|*.dwg";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String strFilePath = openFileDialog.FileName;
                MatEx mat = NestHelper.LoadMatFromDxfdwg(strFilePath, m_nestParam);
                if (mat != null)
                {
                    m_matList.AddMat(mat);

                    // add material to list control.
                    AddMat(mat);
                }
            }
        }

        // add material to list control.
        private void AddMat(MatEx mat)
        {
            // the boundary rect of the material.
            Rect2DEx boundaryRect = null;
            if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
            {
                RectMatEx rectMat = (RectMatEx)mat;
                boundaryRect = rectMat.GetBoundaryRect();
            }
            else if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
            {
                PolyMatEx polyMat = (PolyMatEx)mat;
                Polygon2DEx polygon = polyMat.GetMatPolygon();
                boundaryRect = polygon.GetBoundaryRect();
            }

            /************************************************************************/
            // add a row to list control.

            // insert a row.
            int iCount = matListView.Items.Count + 1;
            ListViewItem item = matListView.Items.Add(iCount.ToString());

            // name column.
            item.SubItems.Add(mat.GetName());

            // material type column.
            if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
                item.SubItems.Add("矩形材料");
            else if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
                item.SubItems.Add("不规则材料");

            // the material height.
            item.SubItems.Add(boundaryRect.GetHeight().ToString("0.000"));

            // the material width.
            item.SubItems.Add(boundaryRect.GetWidth().ToString("0.000"));

            // the material count.
            item.SubItems.Add(mat.GetCount().ToString());

            // hold the ID.
            item.Tag = mat.GetID();
            /************************************************************************/

            // select the last row.
            matListView.SelectedItems.Clear();
            matListView.Items[matListView.Items.Count - 1].Selected = true;
            matListView.Items[matListView.Items.Count - 1].Focused = true;
            matListView.Items[matListView.Items.Count - 1].EnsureVisible();
        }

        private void editPartBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = partListView.SelectedItems;
            if (selItems.Count != 1)
            {
                MessageBox.Show("请选择要编辑的零件！！");
                return;
            }
            else
            {
                ListViewItem item = selItems[0];
                long iNestPartID = (long)item.Tag;
                NestPartEx nestPart = m_nestPartList.GetNestPartByID(iNestPartID);
                if (nestPart != null)
                {
                    NestPartForm nestPartForm = new NestPartForm(nestPart);
                    if (nestPartForm.ShowDialog() == DialogResult.OK)
                    {
                        /************************************************************************/
                        // update the list control.

                        // priority column.
                        item.SubItems[2].Text = nestPart.GetPriority().GetVal().ToString();

                        // nest count column.
                        item.SubItems[3].Text = nestPart.GetNestCount().ToString();

                        // priority column.
                        item.SubItems[4].Text = NestHelper.GetRotateAngName(nestPart.GetRotStyle());
                        /************************************************************************/
                    }
                }
            }
        }

        private void delPartBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要移除零件！！") == DialogResult.No)
                return;

            m_bDisableSelChgEvent = true;

            foreach (ListViewItem item in partListView.SelectedItems)
            {
                if (item.Selected)
                {
                    m_nestPartList.DelNestPartByID((long)item.Tag);
                    item.Remove();
                }
            }

            // enable select-change event.
            m_bDisableSelChgEvent = false;

            // select the last row.
            if (partListView.Items.Count > 0)
            {
                partListView.SelectedItems.Clear();
                partListView.Items[partListView.Items.Count - 1].Selected = true;
                partListView.Items[partListView.Items.Count - 1].Focused = true;
                partListView.Items[partListView.Items.Count - 1].EnsureVisible();
            }
        }

        private void editMatBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = matListView.SelectedItems;
            if (selItems.Count != 1)
            {
                MessageBox.Show("Please select one row to edit.", "NestProfessor DEMO");
                return;
            }
            else
            {
                ListViewItem item = selItems[0];
                long iMatID = (long)item.Tag;
                MatEx mat = m_matList.GetMatByID(iMatID);
                if (mat != null)
                {
                    if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
                        MessageBox.Show("For irregular material, its width and height can be edited here.", "NestProfessor DEMO");

                    MaterialForm matForm = new MaterialForm(mat);
                    if (matForm.ShowDialog() == DialogResult.OK)
                    {
                        /************************************************************************/
                        // update the list control.

                        // name column.
                        item.SubItems[1].Text = mat.GetName();

                        if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
                        {
                            RectMatEx rectMat = (RectMatEx)(mat);
                            Rect2DEx rect = rectMat.GetBoundaryRect();

                            // the material width.
                            item.SubItems[3].Text = rect.GetWidth().ToString("0.000");

                            // the material height.
                            item.SubItems[4].Text = rect.GetHeight().ToString("0.000");
                        }

                        // count.
                        item.SubItems[5].Text = mat.GetCount().ToString();
                        /************************************************************************/

                        matPreviewWnd.Invalidate();
                    }
                }
            }
        }

        private void delMatBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in matListView.SelectedItems)
            {
                if (item.Selected)
                {
                    m_matList.DelMatByID((long)item.Tag);
                    item.Remove();
                }
            }

            // select the last row.
            if (matListView.Items.Count > 0)
            {
                matListView.SelectedItems.Clear();
                matListView.Items[matListView.Items.Count - 1].Selected = true;
                matListView.Items[matListView.Items.Count - 1].Focused = true;
                matListView.Items[matListView.Items.Count - 1].EnsureVisible();
            }
        }

        private void executeBtn_Click(object sender, EventArgs e)
        {
            // check
            if (m_nestPartList.Size() == 0)
            {
                MessageBox.Show("没有需要排版的零件！！！");
                return;
            }
            if (m_matList.Size() == 0)
            {
                MessageBox.Show("没有需要排版的材料！！！");
                return;
            }

            // create nest task object and execute it.
            NestTaskEx nestTask = new NestTaskEx(m_nestPartList, m_matList, m_nestParam);
            NestProcessorEx nestProcessor = NestFacadeEx.StartNest(nestTask);
            if (nestProcessor == null)
            {
                //MessageBox.Show("Cannot start the nesting task, please contact TAOSoft and check your license.");
                MessageBox.Show("没有排版的结果，请联系技术人员！！！");
                return;
            }

            // display the nesting result.
            NestResultForm form = new NestResultForm(nestTask, m_nestParam, nestProcessor, m_iNestingTime, m_impDataList, m_partColorConfig);
            form.ShowDialog();
        }

        private void configBtn_Click(object sender, EventArgs e)
        {
            NestParamForm form = new NestParamForm(m_nestParam, m_iNestingTime);
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_iNestingTime = form.GetNestingTime();
            }
        }

        private void partListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = partListView.SelectedItems;
            if (selItems.Count == 1)
            {
                ListViewItem item = selItems[0];
                long iNestPartID = (long)item.Tag;
                NestPartEx nestPart = m_nestPartList.GetNestPartByID(iNestPartID);
                if (nestPart != null)
                {
                    NestPartForm nestPartForm = new NestPartForm(nestPart);
                    if (nestPartForm.ShowDialog() == DialogResult.OK)
                    {
                        /************************************************************************/
                        // update the list control.

                        // priority column.
                        item.SubItems[2].Text = nestPart.GetPriority().GetVal().ToString();

                        // nest count column.
                        item.SubItems[3].Text = nestPart.GetNestCount().ToString();

                        // priority column.
                        item.SubItems[4].Text = NestHelper.GetRotateAngName(nestPart.GetRotStyle());
                        /************************************************************************/
                    }
                }
            }
        }

        private void matListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = matListView.SelectedItems;
            if (selItems.Count == 1)
            {
                ListViewItem item = selItems[0];
                long iMatID = (long)item.Tag;
                MatEx mat = m_matList.GetMatByID(iMatID);
                if (mat != null)
                {
                    if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
                        MessageBox.Show("For irregular material, its width and height can be edited here.", "NestProfessor DEMO");

                    MaterialForm matForm = new MaterialForm(mat);
                    if (matForm.ShowDialog() == DialogResult.OK)
                    {
                        /************************************************************************/
                        // update the list control.

                        // name column.
                        item.SubItems[1].Text = mat.GetName();

                        if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
                        {
                            RectMatEx rectMat = (RectMatEx)(mat);
                            Rect2DEx rect = rectMat.GetBoundaryRect();

                            // the material width.
                            item.SubItems[3].Text = rect.GetWidth().ToString("0.000");

                            // the material height.
                            item.SubItems[4].Text = rect.GetHeight().ToString("0.000");
                        }

                        // count.
                        item.SubItems[5].Text = mat.GetCount().ToString();
                        /************************************************************************/

                        matPreviewWnd.Invalidate();
                    }
                }
            }
        }

        private void DemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            partViewPort.ReleaseResource();
            matViewPort.ReleaseResource();

            // unload the nesting kernel before the client app will quit.
            NestFacadeEx.UnloadNestKernel();
        }

        private void loadTaskBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Task Files|*.xml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                String strFilePath = openFileDialog.FileName;
                NestTaskEx nestTask = TaskStorage.LoadNestTask_from_taskFile(strFilePath, m_partDxfPath, m_matDxfPath, m_partColorConfig, m_impDataList, ref m_iNestingTime);
                m_nestParam = nestTask.GetNestParam();

                // disable select-change event.
                m_bDisableSelChgEvent = true;

                // clean list.
                partListView.Items.Clear();
                matListView.Items.Clear();

                // init part list.
                {
                    m_nestPartList = nestTask.GetNestPartList();
                    for (int i = 0; i < m_nestPartList.Size(); i++)
                    {
                        AddPart_to_listCtrl(m_nestPartList.GetNestPartByIndex(i), "");
                    }

                    // select the last row.
                    if (partListView.Items.Count > 0)
                    {
                        partListView.SelectedItems.Clear();
                        partListView.Items[partListView.Items.Count - 1].Selected = true;
                        partListView.Items[partListView.Items.Count - 1].Focused = true;
                        partListView.Items[partListView.Items.Count - 1].EnsureVisible();
                    }
                }

                // init material.
                {
                    m_matList = nestTask.GetMatList();
                    for (int i = 0; i < m_matList.Size(); i++)
                    {
                        AddMat(m_matList.GetMatByIndex(i));
                    }
                }

                // enable select-change event.
                m_bDisableSelChgEvent = false;

                Preview_selected_part();
                Preview_selected_material();
            }
        }

        private void saveTaskBtn_Click(object sender, EventArgs e)
        {
            // check.
            if (m_nestPartList.Size() == 0)
            {
                MessageBox.Show("No part will be nested.", "NestProfessor DEMO");
                return;
            }
            if (m_matList.Size() == 0)
            {
                MessageBox.Show("No material will be used for nesting.", "NestProfessor DEMO");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Task File|*.xml";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                String strFilePath = saveFileDialog.FileName;
                NestTaskEx nestTask = new NestTaskEx(m_nestPartList, m_matList, m_nestParam);
                TaskStorage.SaveNestTask(strFilePath, nestTask, m_partDxfPath, m_partColorConfig, m_matDxfPath, m_iNestingTime);
            }
        }

        private void DemoForm_Paint(object sender, PaintEventArgs e)
        {
            Preview_selected_part();
            Preview_selected_material();
        }

        private void Preview_selected_part()
        {
            // get the select NestPartEx object.
            ListView.SelectedListViewItemCollection selItems = partListView.SelectedItems;
            if (selItems.Count == 0)
                return;
            ListViewItem item = selItems[0];
            long iNestPartID = (long)item.Tag;
            NestPartEx nestPart = m_nestPartList.GetNestPartByID(iNestPartID);
            if (nestPart != null)
            {
                PartEx part = nestPart.GetPart();
                GeomItemListEx partGeomItems = part.GetGeomItemList();

                // fit the geom to window.
                DrawHelper.FitWindow(partGeomItems.GetRectBox(), partViewPort, partPreviewWnd);

                // clear screen and set the background color.
                partViewPort.BindRendContext();
                partViewPort.ClearScreen();
                partViewPort.SetBackgroundColor(Color.Black);

                // set the draw params.
                partViewPort.SetDrawColor(ColorTranslator.FromOle(m_partColorConfig[nestPart.GetPart().GetID()]));
                partViewPort.SetLineWidth(1);

                partViewPort.DrawGeomItemList(partGeomItems);

                // swap buffer to display the geometry.
                partViewPort.SwapBuffers();
                //Graphics g = partPreviewWnd.Graphics;
            }
        }

        private void Preview_selected_material()
        {
            // get the select material object.
            ListView.SelectedListViewItemCollection selItems = matListView.SelectedItems;
            if (selItems.Count == 0)
                return;
            ListViewItem item = selItems[0];
            long iMatID = (long)item.Tag;
            MatEx mat = m_matList.GetMatByID(iMatID);
            if (mat != null)
            {
                GeomItemListEx matGeomItems = mat.GetGeomItemList();

                // fit the geom to window.
                DrawHelper.FitWindow(matGeomItems.GetRectBox(), matViewPort, matPreviewWnd);

                // clear screen and set the background color.
                matViewPort.BindRendContext();
                matViewPort.ClearScreen();
                matViewPort.SetBackgroundColor(Color.Black);

                // set the draw params.
                matViewPort.SetDrawColor(Color.White);
                matViewPort.SetLineWidth(1);

                matViewPort.DrawGeomItemList(matGeomItems);

                // swap buffer to display the geometry.
                matViewPort.SwapBuffers();
            }
        }

        private void DemoForm_SizeChanged(object sender, EventArgs e)
        {
            //零件控件自适应
            int width = partListView.Width;
            int temp = 0;
            int count = 0;
            foreach (ColumnHeader item in partListView.Columns)
            {
                if (count == partListView.Columns.Count - 1)
                {
                    item.Width = width - temp - 200;
                    break;
                }

                if (item.Text == "零件名称" ||
                    item.Text == "零件尺寸")
                {
                    item.Width = (partListView.Width / 100) * 24;
                    temp += item.Width;
                }
                else
                    item.Width = (partListView.Width / 100) * 10;

                count++;
            }

            //材料控件自适应
            width = matListView.Width;
            temp = 0;
            count = 0;
            foreach (ColumnHeader item in matListView.Columns)
            {
                if (count == matListView.Columns.Count - 1)
                {
                    item.Width = width - temp - 300;
                    break;
                }

                //if (item.Text == "材料名称" ||
                //    item.Text == "材料尺寸")
                //{
                //    item.Width = (partListView.Width / 100) * 40;
                //    temp += item.Width;
                //}
                //else
                item.Width = (partListView.Width / 100) * 17;

                count++;
            }
        }

        private void btnRemoveMaterial_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要移除材料！！！") == DialogResult.No)
                return;

            m_bDisableSelChgEvent = true;

            foreach (ListViewItem item in matListView.SelectedItems)
            {
                if (item.Selected)
                {
                    m_matList.DelMatByID((long)item.Tag);
                    //m_nestPartList.DelNestPartByID((long)item.Tag);
                    item.Remove();
                }
            }

            // enable select-change event.
            m_bDisableSelChgEvent = false;

            // select the last row.
            if (matListView.Items.Count > 0)
            {
                matListView.SelectedItems.Clear();
                matListView.Items[partListView.Items.Count - 1].Selected = true;
                matListView.Items[partListView.Items.Count - 1].Focused = true;
                matListView.Items[partListView.Items.Count - 1].EnsureVisible();
            }
        }

        private void partPreviewWnd_Click(object sender, EventArgs e)
        {
            //Form f = FrmMainWindow;
            Control de = this;
            Panel p = sender as Panel;
            Point PTS = PointToScreen(p.Location);
            Padding ParentFormPadding = this.Parent.Padding;
            Padding ParentFormMargin = this.Parent.Margin;
            Padding ParentPadding = this.Padding;
            Padding ParentMargin = this.Margin;
            Point objP = new Point(
                PTS.X + ParentFormPadding.Size.Width + ParentFormMargin.Size.Width + ParentPadding.Size.Width + ParentMargin.Size.Width,
                PTS.Y + ParentFormPadding.Size.Height + ParentFormMargin.Size.Height + ParentPadding.Size.Height + ParentMargin.Size.Width);
            //DrawHelper.GetAndSaveControllerScreen(objP, p.Size);
            //截图
            Bitmap bmp = new Bitmap(p.Size.Width, p.Size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(objP, new Point(0, 0), p.Size);
            //matPreviewWnd.Size = new Size(p.Size.Width, p.Size.Height+30);
            //matPreviewWnd.BackgroundImage = bmp;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bmp|*.bmp|jpg|*.jpg|gif|*.gif";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                bmp.Save(saveFileDialog.FileName);
            }
            g.Dispose();
        }

        private void SetConfig()
        {
            string file = System.IO.Directory.GetCurrentDirectory() + "\\ConfigHigh.txt";

            if (File.Exists(file) == true)
            {
                CfgHigh = Tool.Tools.JsonStingToObj<ConfigHighEntity>(File.ReadAllText(file));

                if (CfgHigh != null && CfgHigh.NestTime > 0)
                    m_iNestingTime = CfgHigh.NestTime;
            }

            string path = Directory.GetCurrentDirectory() + "\\ConfigInfo";
            if (Directory.Exists(path) == true)
            {
                var dir = new DirectoryInfo(path);
                var files = dir.GetFiles().OrderBy(q => q.CreationTime).ToList();
                if (files.Count > 0)
                {
                    var config = Tool.Tools.JsonStingToObj<ConfigEntity>(File.ReadAllText(files.Last().FullName));
                    if (config != null)
                    {
                        m_nestParam.SetMatLeftMargin(config.MatLeftMargin);
                        m_nestParam.SetMatRightMargin(config.MatRightMargin);
                        m_nestParam.SetMatTopMargin(config.MatTopMargin);
                        m_nestParam.SetMatBottomMargin(config.MatBottomMargin);
                        m_nestParam.SetMatMargin(config.MatMargin);
                        // the part spacing.
                        m_nestParam.SetPartDis(config.PartDis);

                        // the start nesting corner.
                        if (config.StartCorner == Corner.LeftTop)
                            m_nestParam.SetStartCorner(RECT_CORNER_EX.LEFT_TOP);
                        else if (config.StartCorner == Corner.LeftBottom)
                            m_nestParam.SetStartCorner(RECT_CORNER_EX.LEFT_BOTTOM);
                        else if (config.StartCorner == Corner.RightTop)
                            m_nestParam.SetStartCorner(RECT_CORNER_EX.RIGHT_TOP);
                        else if (config.StartCorner == Corner.RightBottom)
                            m_nestParam.SetStartCorner(RECT_CORNER_EX.RIGHT_BOTTOM);

                        // the nesting direction.
                        if (config.DirValue == Dir.XDir)
                            m_nestParam.SetNestDir(NEST_DIRECTION_EX.NEST_DIR_X);
                        else if (config.DirValue == Dir.YDir)
                            m_nestParam.SetNestDir(NEST_DIRECTION_EX.NEST_DIR_Y);
                    }
                }
            }
        }
    }
}
