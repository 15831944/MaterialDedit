using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// 缩略图 二维码
    /// </summary>
    public class SLTQrCodeItem
    {
        /// <summary>
        /// 未加阴影图像
        /// </summary>
        public Bitmap BlankBmp { get; set; }
        /// <summary>
        /// 加阴影图像列表
        /// </summary>
        public List<SLTShadowBmp> SLTBmpShadowList { get; set; }

    }

    public class SLTShadowBmp
    {
        /// <summary>
        /// 阴影图像
        /// </summary>
        public Bitmap ShadowBmp { get; set; }
        /// <summary>
        /// 二维码文本
        /// </summary>
        public string QrCodeText { get; set; }
        /// <summary>
        /// 二维码图像
        /// </summary>
        public Bitmap QrCodeBmp { get; set; }
        /// <summary>
        /// 缩略图文本信息
        /// </summary>
        public string SLTText { get; set; }
        /// <summary>
        /// 缩略图图像信息
        /// </summary>
        public Bitmap SLTTextBmp { get; set; }
        /// <summary>
        /// 阴影图像零件
        /// </summary>
        public SLTPartShape SLTPartInfos { get; set; }
    }
    public class Shape
    {
        public int PoX { get; set; }
        public int PoY { get; set; }
        public int PartWidth { get; set; }
        public int PartHeight { get; set; }
        public string NestDir { get; set; }
    }
    public class SLTPartShape : Shape
    {
        public int Angle { get; set; }
        public string MatName { get; set; }
    }
}
