using Entities;
using Gma.QrCodeNet.Encoding;
using NestBridge;
using OrderManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialDedit
{
    public partial class QrCodeForm : Form
    {
        private ListView shtListView = null;
        private SheetListEx m_sheetList = null;
        public List<SLTQrCodeItem> _SLTQrCodeList = new List<SLTQrCodeItem>();
        private NestParamEx m_nestParam = null;
        public QrCodeForm(ListView shtListView, SheetListEx m_sheetList, NestParamEx nestParam)
        {
            this.Size = new Size(400, 600);
            this.shtListView = shtListView;
            m_nestParam = nestParam;
            this.m_sheetList = m_sheetList;
            InitializeComponent();
        }

        private void QrCodeForm_Load(object sender, EventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 1);
            Pen blackPen2 = new Pen(Color.Black, 2);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.White);
            //外边距
            int space = 5;

            for (int count = 0; count < shtListView.Items.Count; count++)
            {
                SLTQrCodeItem SLTQrCodeItemTemp = new SLTQrCodeItem()
                {
                    SLTBmpShadowList = new List<SLTShadowBmp>()
                };
                ListViewItem item = shtListView.Items[count];
                long iSheetID = (long)item.Tag;
                SheetEx sheet = m_sheetList.GetSheetByID(iSheetID);
                string M_Name = sheet.GetMat().GetName();
                PartPmtListEx partPmtList = sheet.GetPartPmtList();
                int partsCount = partPmtList.GetPartList().Size();
                Rect2DEx sheetRec = sheet.GetMat().GetBoundaryRect();
                int MatWidth = (int)sheetRec.GetWidth();
                int MatHeight = (int)sheetRec.GetHeight();
                SLTQrCodeItemTemp.BlankBmp = new Bitmap(MatWidth + 1, MatHeight + 1);
                Graphics gBlank = Graphics.FromImage(SLTQrCodeItemTemp.BlankBmp);
                gBlank.Clear(Color.FromArgb(255, 255, 255, 255));
                //边框
                gBlank.DrawRectangle(blackPen, 0, 0, MatWidth, MatHeight);
                //缩略图
                for (int i = 0; i < partsCount; i++)
                {
                    SLTShadowBmp SLTShadowBmpItem = new SLTShadowBmp();
                    PartPmtEx partPmt = sheet.GetPartTopItemList().GetPartPmtByIndex(i).GetPartPmt();
                    Rect2DEx partRe2 = partPmt.GetRectBox();
                    Matrix2DEx Matrix = partPmt.GetMatrix();
                    int PartWidth = (int)partRe2.GetWidth();
                    int PartHeight = (int)partRe2.GetHeight();
                    int PoX = (int)Matrix.GetMatVal(2, 0);
                    int PoY = (int)Matrix.GetMatVal(2, 1);
                    int Angle = (int)(Math.Asin(Matrix.GetMatVal(0, 1)) * 180 / Math.PI);
                    SLTShadowBmpItem.SLTPartInfos = new SLTPartShape()
                    {
                        PoX = m_nestParam.GetNestDir() == NEST_DIRECTION_EX.NEST_DIR_X ? PoX : PoX - PartWidth,
                        PoY = MatHeight - PoY - PartHeight,
                        PartWidth = PartWidth,
                        PartHeight = PartHeight,
                        NestDir = m_nestParam.GetNestDir() == NEST_DIRECTION_EX.NEST_DIR_X ? "X" : "Y",
                        MatName = M_Name,
                    };
                    SLTQrCodeItemTemp.SLTBmpShadowList.Add(SLTShadowBmpItem);
                    gBlank.DrawRectangle(blackPen, SLTShadowBmpItem.SLTPartInfos.PoX, SLTShadowBmpItem.SLTPartInfos.PoY, PartWidth, PartHeight);
                }
                gBlank.Dispose();
                //加阴影缩略图
                for (int i = 0; i < SLTQrCodeItemTemp.SLTBmpShadowList.Count; i++)
                {
                    Bitmap original = SLTQrCodeItemTemp.BlankBmp;
                    Bitmap copy = new Bitmap(original.Width, original.Height);
                    SLTShadowBmp Shapeitem = SLTQrCodeItemTemp.SLTBmpShadowList[i];
                    using (Graphics gTemp = Graphics.FromImage(copy))
                    {
                        gTemp.Clear(Color.White);
                        Rectangle imageRectangle = new Rectangle(0, 0, copy.Width, copy.Height);
                        gTemp.DrawImage(original, imageRectangle, imageRectangle, GraphicsUnit.Pixel);
                        gTemp.FillRectangle(hatchBrush, Shapeitem.SLTPartInfos.PoX,
                            Shapeitem.SLTPartInfos.PoY, Shapeitem.SLTPartInfos.PartWidth, Shapeitem.SLTPartInfos.PartHeight);
                        gTemp.DrawRectangle(blackPen2, Shapeitem.SLTPartInfos.PoX,
                            Shapeitem.SLTPartInfos.PoY, Shapeitem.SLTPartInfos.PartWidth, Shapeitem.SLTPartInfos.PartHeight);//画矩形
                    }
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].ShadowBmp = copy;
                    PartEx partEx = partPmtList.GetPartList().GetPartByIndex(i);

                    var view = partPmtList.GetPartList();

                    string OrderNo = OrderManagerDal.Instance.GetOrder(partEx.GetID());
                    string partName = partEx.GetName().Substring(1, 1);
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].QrCodeText = OrderNo + "-" + partName;
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].QrCodeBmp = GenerateQRCode(partName);
                    //字体
                    Font font = new Font("Arial Bold", 6f);//粗体 10号
                    //设定字体格式  
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    };
                    string text1 = OrderNo + "-" + partName + "\r\n" + partName;
                    string text2 = partName;
                    string text3 = Shapeitem.SLTPartInfos.PartWidth + "*" + Shapeitem.SLTPartInfos.PartHeight;
                    string text4 = Shapeitem.SLTPartInfos.MatName;
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTText = text1 + "\r\n" + text2 + "\r\n" + text3 + "\r\n" + text4;
                    List<int> widthTemp = new List<int>();
                    widthTemp.Add(TextRenderer.MeasureText(text1, font).Width);
                    widthTemp.Add(TextRenderer.MeasureText(text2, font).Width);
                    widthTemp.Add(TextRenderer.MeasureText(text3, font).Width);
                    widthTemp.Add(TextRenderer.MeasureText(text4, font).Width);
                    widthTemp.Sort();
                    Bitmap textBmp = new Bitmap(widthTemp[0], widthTemp[0]);
                    using (Graphics gTextTemp = Graphics.FromImage(textBmp))
                    {
                        gTextTemp.DrawString(SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTText
                            , font, blackBrush, new RectangleF(space, space, textBmp.Width - space, textBmp.Height - space));
                    }
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTTextBmp = textBmp;
                }
                _SLTQrCodeList.Add(SLTQrCodeItemTemp);
                //g.DrawString(partName, font, blackBrush, new RectangleF(0, 0, 100, 100), format);
                //g.DrawImage(qrBmp, 150, 150, 50, 50);
            }
            for (int i = 0; i < _SLTQrCodeList.Count; i++)
            {
                for (int j = 0; j < _SLTQrCodeList[i].SLTBmpShadowList.Count; j++)
                {
                    cbShadowBmp.Items.Add(_SLTQrCodeList[i].SLTBmpShadowList[j].QrCodeText);
                }
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="DarkColor">前景色</param>
        /// <param name="LightColor">背景色</param>
        /// <param name="Scale">放大比例大于1</param>
        /// <returns></returns>
        private Bitmap GenerateQRCode(string text)
        {
            QrEncoder Encoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode Code = Encoder.Encode(text);
            int Scale = 1;
            int bmpWidth = 200;
            if (Code.Matrix.Width < bmpWidth)
            {
                Scale = (int)(bmpWidth / Code.Matrix.Width);
            }
            Bitmap TempBMP = new Bitmap(Code.Matrix.Width * Scale, Code.Matrix.Height * Scale);
            for (int X = 0; X <= Code.Matrix.Width - 1; X++)
            {
                for (int Y = 0; Y <= Code.Matrix.Height - 1; Y++)
                {
                    if (Code.Matrix.InternalArray[X, Y])
                    {
                        for (int sX = 0; sX < Scale; sX++)
                        {
                            for (int sY = 0; sY < Scale; sY++)
                            {
                                TempBMP.SetPixel(Scale * X + sX, Scale * Y + sY, Color.Black);
                            }
                        }
                    }
                    else
                    {
                        for (int sX = 0; sX < Scale; sX++)
                        {
                            for (int sY = 0; sY < Scale; sY++)
                            {
                                TempBMP.SetPixel(Scale * X + sX, Scale * Y + sY, Color.White);
                            }
                        }
                    }
                }
            }
            ///压缩为200*200Bmp
            int newW = bmpWidth, newH = bmpWidth;
            Bitmap bmpOut = new Bitmap(newW, newW);
            Graphics g = Graphics.FromImage(bmpOut);
            // 插值算法的质量
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(TempBMP, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, TempBMP.Width, TempBMP.Height), GraphicsUnit.Pixel);
            g.Dispose();
            return bmpOut;
        }

        private void cbShadowBmp_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _SLTQrCodeList.Count; i++)
            {
                for (int j = 0; j < _SLTQrCodeList[i].SLTBmpShadowList.Count; j++)
                {
                    if (cbShadowBmp.Text == _SLTQrCodeList[i].SLTBmpShadowList[j].QrCodeText)
                    {
                        int MatWidth = _SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp.Size.Width;
                        int MatHeight = _SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp.Size.Height;
                        if (MatWidth < MatHeight)
                        {
                            panelQrCode.Size = new Size(400 * MatWidth / MatHeight, 400);
                        }
                        else
                        {
                            panelQrCode.Size = new Size(400, 400 * MatHeight / MatWidth);
                        }
                        Paneltext.BackgroundImage = _SLTQrCodeList[i].SLTBmpShadowList[j].SLTTextBmp;
                        Paneltext.BackgroundImageLayout = ImageLayout.Stretch;
                        panelQrCode.BackgroundImage = _SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp;
                        panelQrCode.BackgroundImageLayout = ImageLayout.Stretch;
                        panelQr.BackgroundImage = _SLTQrCodeList[i].SLTBmpShadowList[j].QrCodeBmp;
                        panelQr.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }
    }
}
