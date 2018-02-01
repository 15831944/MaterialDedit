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
        private bool isDebug = false;
        private SheetListEx m_sheetList = null;
        public List<SLTQrCodeItem> _SLTQrCodeList = new List<SLTQrCodeItem>();
        private NestParamEx m_nestParam = null;
        public QrCodeForm(ListView shtListView, SheetListEx m_sheetList, NestParamEx nestParam)
        {
            this.shtListView = shtListView;
            m_nestParam = nestParam;
            this.m_sheetList = m_sheetList;
            InitializeComponent();
            cbShadowBmp.Visible = isDebug;
            Paneltext.Visible = isDebug;
            panelQr.Visible = isDebug;
            panelQrCode.Visible = isDebug;
            this.Size = new Size(450, 670);
        }

        private void QrCodeForm_Load(object sender, EventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 1);
            Pen blackPen2 = new Pen(Color.Black, 2);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.White);
            //外边距
            int space = 2;

            for (int length = 0; length < shtListView.Items.Count; length++)
            {
                SLTQrCodeItem SLTQrCodeItemTemp = new SLTQrCodeItem()
                {
                    SLTBmpShadowList = new List<SLTShadowBmp>()
                };
                ListViewItem item = shtListView.Items[length];
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
                    string OrderNo = OrderManagerDal.Instance.GetOrder(partEx.GetID());
                    string partNameIndex = partEx.GetName().Substring(1, 1);
                    string partName = partEx.GetName();
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].QrCodeText = OrderNo + "-" + partNameIndex;
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].QrCodeBmp = GenerateQRCode(partNameIndex);
                    //字体
                    Font font = new Font("微软雅黑", 60f);//8号
                    //设定字体格式  
                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                    };
                    string text1 = OrderNo + "-" + partNameIndex;
                    string text2 = partName;
                    string text3 = Shapeitem.SLTPartInfos.PartWidth + "*" + Shapeitem.SLTPartInfos.PartHeight;
                    string text4 = Shapeitem.SLTPartInfos.MatName;
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTText = text1 + "\r\n" + text2 + "\r\n" + text3 + "\r\n" + text4;
                    Size SLTTextSize = TextRenderer.MeasureText(SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTText, font);
                    Bitmap textBmp = new Bitmap(SLTTextSize.Width + space + 5, SLTTextSize.Width + space + 5);
                    using (Graphics gTextTemp = Graphics.FromImage(textBmp))
                    {
                        gTextTemp.Clear(Color.White);
                        gTextTemp.SmoothingMode = SmoothingMode.HighQuality;
                        gTextTemp.DrawString(SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTText, font, blackBrush,
                            new RectangleF(space, (textBmp.Height - SLTTextSize.Height - (2 * space)) / 2 + space, textBmp.Width - space, textBmp.Height - space));
                    }
                    SLTQrCodeItemTemp.SLTBmpShadowList[i].SLTTextBmp = textBmp;
                }
                _SLTQrCodeList.Add(SLTQrCodeItemTemp);
                //g.DrawString(partName, font, blackBrush, new RectangleF(0, 0, 100, 100), format);
                //g.DrawImage(qrBmp, 150, 150, 50, 50);
            }

            //
            int bmpPrintWidth = 500;
            int Colunm = 2;
            List<Shape> positionBase = new List<Shape>();
            int count = 0;
            for (int i = 0; i < _SLTQrCodeList.Count; i++)
            {
                for (int j = 0; j < _SLTQrCodeList[i].SLTBmpShadowList.Count; j++)
                {
                    count++;
                }
            }
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < Colunm; j++)
                {
                    if (i * j + j >= count)
                    {
                        break;
                    }
                    Shape itemTemp = new Shape()
                    {
                        PoX = j * (bmpPrintWidth / Colunm),
                        PoY = i * (bmpPrintWidth / Colunm),
                        PartWidth = bmpPrintWidth / Colunm,
                        PartHeight = bmpPrintWidth / Colunm,
                    };
                    positionBase.Add(itemTemp);
                }
            }

            int bmpPrintHeight = ((positionBase.Count / Colunm) + (positionBase.Count % Colunm)) * (bmpPrintWidth / Colunm);
            Bitmap PrintBmp = new Bitmap(bmpPrintWidth, bmpPrintHeight);
            using (Graphics gPrint = Graphics.FromImage(PrintBmp))
            {
                gPrint.Clear(Color.White);
                count = 0;
                for (int i = 0; i < _SLTQrCodeList.Count; i++)
                {
                    for (int j = 0; j < _SLTQrCodeList[i].SLTBmpShadowList.Count; j++)
                    {

                        float TextPx = positionBase[count].PoX;
                        float TextPy = positionBase[count].PoY + positionBase[count].PartHeight * 0.1f;
                        float QrCodePx = TextPx;
                        float QrCodePy = positionBase[count].PoY + positionBase[count].PartHeight * 0.5f;

                        float SLTPx = positionBase[count].PoX + positionBase[count].PartWidth * 0.4f;
                        float SLTPy = positionBase[count].PoY;
                        Size SLTSize = _SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp.Size;
                        float SLTWidth = (int)(positionBase[count].PartHeight * SLTSize.Width / SLTSize.Height);
                        float SLTHeight = (int)(positionBase[count].PartHeight);
                        Bitmap SLTBmpTemp = _SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp;
                        float scale = (float)SLTSize.Width / (float)SLTSize.Height;
                        if (scale < 0.6f)//固定高度
                        {
                            SLTWidth = (int)(positionBase[count].PartHeight * SLTSize.Width / SLTSize.Height);
                            SLTHeight = positionBase[count].PartHeight;
                        }
                        else if (0.6f < scale && scale < 1)//固定宽度
                        {
                            SLTWidth = positionBase[count].PartWidth * 0.6f;
                            SLTHeight = (int)(positionBase[count].PartWidth * 0.6f * SLTSize.Height / SLTSize.Width);
                        }
                        else if (scale > 1)//旋转
                        {
                            SLTWidth = positionBase[count].PartWidth * 0.6f;
                            SLTHeight = (int)(positionBase[count].PartWidth * 0.6f * SLTSize.Height / SLTSize.Width);
                            //SLTBmpTemp = new Bitmap(SLTSize.Width, SLTSize.Height);
                            //using (Graphics gCcopy = Graphics.FromImage(SLTBmpTemp))
                            //{
                            //    gCcopy.Clear(Color.White);
                            //    gCcopy.TranslateTransform(0, 0); //源点移动到旋转中心
                            //    gCcopy.RotateTransform(90f); //旋转
                            //    Rectangle imageRectangle = new Rectangle(0, 0, SLTSize.Width, SLTSize.Height);
                            //    gCcopy.DrawImage(_SLTQrCodeList[i].SLTBmpShadowList[j].ShadowBmp, imageRectangle, imageRectangle, GraphicsUnit.Pixel);
                            //}
                            //scale = SLTSize.Height / SLTSize.Width;
                            //if (0.6f < scale && scale < 1)//固定高度
                            //{
                            //    SLTWidth = (int)(positionBase[count].PartHeight * SLTSize.Width / SLTSize.Height);
                            //    SLTHeight = positionBase[count].PartHeight;
                            //}
                            //else if (0.6f < scale && scale < 1)//固定宽度
                            //{
                            //    SLTWidth = positionBase[count].PartWidth * 0.6f;
                            //    SLTHeight = (int)(positionBase[count].PartWidth * 0.6f * SLTSize.Height / SLTSize.Width);
                            //}
                        }
                        //else//固定高度
                        //{
                        //    SLTWidth = (int)(positionBase[count].PartHeight * SLTSize.Width / SLTSize.Height);
                        //    SLTHeight = positionBase[count].PartHeight;
                        //}
                        gPrint.DrawImage(_SLTQrCodeList[i].SLTBmpShadowList[j].SLTTextBmp,
                            TextPx, TextPy,
                            (int)(positionBase[count].PartWidth * 0.4),
                            (int)(positionBase[count].PartHeight * 0.4));
                        gPrint.DrawImage(_SLTQrCodeList[i].SLTBmpShadowList[j].QrCodeBmp,
                           QrCodePx, QrCodePy,
                           (int)(positionBase[count].PartWidth * 0.4),
                           (int)(positionBase[count].PartHeight * 0.4));
                        gPrint.DrawImage(SLTBmpTemp, SLTPx, SLTPy, SLTWidth, SLTHeight);
                        count++;
                    }
                }

            }
            panelQrCodePrint.BackgroundImage = PrintBmp;
            panelQrCodePrint.BackgroundImageLayout = ImageLayout.Stretch;

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
