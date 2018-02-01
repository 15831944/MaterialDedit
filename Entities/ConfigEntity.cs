using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ConfigEntity
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 矩形材料 左边距
        /// </summary>
        public double MatLeftMargin { get; set; }

        /// <summary>
        /// 矩形材料 上边距
        /// </summary>
        public double MatTopMargin { get; set; }

        /// <summary>
        /// 矩形材料 右边距
        /// </summary>
        public double MatRightMargin { get; set; }

        /// <summary>
        /// 矩形材料 下边距
        /// </summary>
        public double MatBottomMargin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double PartDis { get; set; }

        /// <summary>
        /// 不规则 边距
        /// </summary>
        public double MatMargin { get; set; }

        /// <summary>
        /// 开始排版的点
        /// </summary>
        public Corner StartCorner { get; set; }

        /// <summary>
        /// 排版的方向
        /// 0 X轴
        /// 1 Y轴
        /// </summary>
        public Dir DirValue { get; set; }
    }

    public enum Corner
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop = 0,

        /// <summary>
        /// 左下
        /// </summary>
        LeftBottom = 1,

        /// <summary>
        /// 右上
        /// </summary>
        RightTop = 2,

        /// <summary>
        /// 右下
        /// </summary>
        RightBottom = 3
    }

    public enum Dir
    {
        /// <summary>
        /// X轴
        /// </summary>
        XDir = 1,

        /// <summary>
        /// Y轴
        /// </summary>
        YDir = 2
    }
}
