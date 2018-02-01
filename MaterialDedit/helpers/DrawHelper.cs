using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

using NestBridge;

namespace MaterialDedit
{
    class DrawHelper
    {
        /// <summary>
        /// 截图
        /// </summary>
        /// <returns></returns>
        static public Bitmap GetControllerScreen(Control baseWindow, Control control)
        {
            //截图
            Panel ww = new Panel();
            Bitmap bmp = new Bitmap(control.Size.Width, control.Size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(new Point(baseWindow.Location.X, baseWindow.Location.Y), new Point(0, 0), control.Size);
            //g.
            return bmp;
        }
        /// <summary>
        /// 截图
        /// </summary>
        /// <returns></returns>
        static public void GetAndSaveControllerScreen(Point upperLeftSource, Size blockRegionSize)
        {
            //截图
            Panel ww = new Panel();
            Bitmap bmp = new Bitmap(blockRegionSize.Width, blockRegionSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(upperLeftSource, new Point(0, 0), blockRegionSize);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "bmp|*.bmp|jpg|*.jpg|gif|*.gif";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                bmp.Save(saveFileDialog.FileName);
            }
            g.Dispose();
        }
        // fit the view port to the rect.
        static public void FitWindow(Rect2DEx geomRect, GlViewPortEx viewPort, Control window)
        {
            Int32 iWidth = window.Right - window.Left;
            Int32 iHeight = window.Bottom - window.Top;
            Point2DEx leftBottomPt = new Point2DEx();
            double dXDirRange = viewPort.GetFitAllParam(iWidth, iHeight, geomRect, 1.2, leftBottomPt);
            viewPort.SetDrawingArea(1.1 * dXDirRange, iWidth, iHeight, leftBottomPt);
        }

        // draw the material.
        static public void DrawMat(MatEx mat, GlViewPortEx viewPort)
        {
            viewPort.SetDrawColor(Color.White);
            viewPort.SetLineWidth(1);

            if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_RECT)
            {
                RectMatEx rectMat = (RectMatEx)mat;
                Rect2DEx rect2D = rectMat.GetBoundaryRect();
                Polygon2DEx poly = new Polygon2DEx();
                poly.AddPoint(new Point2DEx(rect2D.GetXMin(), rect2D.GetYMin()));
                poly.AddPoint(new Point2DEx(rect2D.GetXMax(), rect2D.GetYMin()));
                poly.AddPoint(new Point2DEx(rect2D.GetXMax(), rect2D.GetYMax()));
                poly.AddPoint(new Point2DEx(rect2D.GetXMin(), rect2D.GetYMax()));
                viewPort.DrawPolygon(poly);
            }
            else if (mat.GetMatType() == MAT_TYPE_EX.MAT_EX_POLY)
            {
                PolyMatEx polyMat = (PolyMatEx)mat;
                Polygon2DEx poly = polyMat.GetMatPolygon();
                viewPort.DrawPolygon(poly);

                // draw the useless holes.
                Poly2DListEx uselessHoles = polyMat.GetUselessHoleList();
                for (int i = 0; i < uselessHoles.Size(); i++)
                {
                    viewPort.DrawPolygon(uselessHoles.GetPolygonByIndex(i));
                }
            }
        }

        // draw the part placements.
        static public void DrawPartPmts(PartPmtListEx partPmts, PartPmtListEx selected_partPmts, GlViewPortEx viewPort, Dictionary<long, int> partColorConfig, ImpDataListEx impDataList)
        {
            for (int k = 0; k < partPmts.Size(); k++)
            {
                PartPmtEx partPmt = partPmts.GetPartPmtByIndex(k);
                PartEx part = partPmt.GetPart();

                // set the color and line width.
                if (selected_partPmts != null && selected_partPmts.GetPartPmtByID(partPmt.GetID()) != null)
                {
                    viewPort.SetLineWidth(6);
                    viewPort.SetDrawColor(Color.Red);
                }
                else
                {
                    viewPort.SetLineWidth(2);
                    int iColor = partColorConfig[part.GetID()];
                    viewPort.SetDrawColor(ColorTranslator.FromOle(iColor));
                }

                // get the ImpData object.
                ImpDataEx impData = null;
                for (int m = 0; m < impDataList.Size(); m++)
                {
                    ImpDataEx tmpImpData = impDataList.GetImpDataByIndex(m);
                    if (tmpImpData.GetPartID() == part.GetID())
                    {
                        impData = tmpImpData;
                        break;
                    }
                }

                // draw the first instance in the grid.
                {
                    // draw base geom.
                    GeomItemListEx geomItems_not_poly = impData.GetBaseGeomList();
                    if (geomItems_not_poly != null)
                    {
                        viewPort.DrawGeomItemList_With_Mat(geomItems_not_poly, partPmt.GetMatrix());
                    }

                    // draw poly data.
                    PolyDataListEx polyDataList = impData.GetPolyDataList();
                    if (polyDataList != null)
                    {
                        for (int m = 0; m < polyDataList.Size(); m++)
                        {
                            PolyDataEx polyData = polyDataList.GetPolyDataByIndex(m);
                            viewPort.DrawLoop_With_Mat(polyData, partPmt.GetMatrix());
                        }
                    }
                }


                // draw other parts in the grid.
                if (partPmt.IsGrid())
                {
                    int iColCount = partPmt.GetColCount();
                    int iRowCount = partPmt.GetRowCount();
                    double dSpacingX = partPmt.GetSpacingX();
                    double dSpacingY = partPmt.GetSpacingY();
                    for (int i = 0; i < iColCount; i++)
                    {
                        for (int j = 0; j < iRowCount; j++)
                        {
                            if (i == 0 && j == 0)
                                continue;

                            // prepare the transform matrix.
                            double dOffsetX = dSpacingX * i;
                            double dOffsetY = dSpacingY * j;
                            Vector2DEx offsetVect = new Vector2DEx(dOffsetX, dOffsetY);
                            Matrix2DEx mat = partPmt.GetMatrix();
                            mat.Transfer(offsetVect.X(), offsetVect.Y());

                            // draw base geom.
                            GeomItemListEx geomItems_not_poly = impData.GetBaseGeomList();
                            if (geomItems_not_poly != null)
                            {
                                viewPort.DrawGeomItemList_With_Mat(geomItems_not_poly, mat);
                            }

                            // draw poly data.
                            PolyDataListEx polyDataList = impData.GetPolyDataList();
                            if (polyDataList != null)
                            {
                                for (int m = 0; m < polyDataList.Size(); m++)
                                {
                                    PolyDataEx polyData = polyDataList.GetPolyDataByIndex(m);
                                    viewPort.DrawLoop_With_Mat(polyData, mat);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
