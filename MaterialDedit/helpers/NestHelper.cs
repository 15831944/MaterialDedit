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
    class NestHelper
    {
        // load part object from dxf/dwg file.
        static public PartEx LoadPartFromDxfdwg(String strFilePath, ImpDataListEx impDataList)
        {
	        PartEx part = null;

	        // the file name.
	        int iDotIndex = strFilePath.LastIndexOf('.');
	        int iSlashIndex = strFilePath.LastIndexOf('\\');
	        String strFileName = strFilePath.Substring(iSlashIndex+1, iDotIndex-iSlashIndex-1);

	        // whether the file is dxf file or dwg file.
	        bool bDwg = true;
	        String strExt = strFilePath.Substring(iDotIndex, strFilePath.Length-iDotIndex);
            strExt = strExt.ToLower();
	        if (strExt == ".dxf")
		        bDwg = false;
	        else if (strExt == ".dwg")
		        bDwg = true;
	        else
		        return part;

	        // extract geometry items from dxf/dwg file.
	        ImpDataEx impData;
	        if (!bDwg)
	        {
                impData = NestFacadeEx.ExtractGeomItems(strFilePath);
	        }
	        else
	        {
                // the temp folder for dxf.
                String strDxfPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                strDxfPath += "\\";
                strDxfPath += new Random().Next(1,100000).ToString();
                strDxfPath += ".dxf";

		        // save dxf file in tmp path.
		        NestFacadeEx.Dwg2Dxf(strFilePath, strDxfPath);

		        // extract geometry items from dxf file.
                impData = NestFacadeEx.ExtractGeomItems(strDxfPath);

		        // delete the temp file.
		        File.Delete(strDxfPath);
	        }

	        // build part object.
            GeomItemListEx geomItemList = impData.GetAllGeomItem();
	        AncillaryDataEx ancillaryData = new AncillaryDataEx();
	        part = new PartEx(strFileName, geomItemList, ancillaryData);
            impData.SetPartID(part.GetID());
            impDataList.AddImpData(impData);

	        return part;
        }

        // get the rotate angle name.
        static public String GetRotateAngName(PART_ROT_STYLE_EX iRotateAng)
        {
            String strRotateAng = null;

            if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_FREE)
		        strRotateAng = "Free Rotate";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_PID2_INCREMENT)
		        strRotateAng = "90 Increment";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_PI_INCREMENT)
		        strRotateAng = "180 Increment";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_ZERO_FIXED)
		        strRotateAng = "0 Fixed";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_PID2_FIXED)
		        strRotateAng = "90 Fixed";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_PI_FIXED)
		        strRotateAng = "180 Fixed";
	        else if (iRotateAng == PART_ROT_STYLE_EX.PART_ROT_PID23_FIXED)
		        strRotateAng = "270 Fixed";

	        return strRotateAng;
        }

        // load boundary polygon from dxf/dwg file.
        static public MatEx LoadMatFromDxfdwg(String strFilePath, NestParamEx nestParam)
        {
	        MatEx mat = null;

	        /************************************************************************/
	        // load all geometry items from the dxf/dwg file.

	        // the file name.
	        int iDotIndex = strFilePath.LastIndexOf('.');
	        int iSlashIndex = strFilePath.LastIndexOf('\\');
	        String strFileName = strFilePath.Substring(iSlashIndex+1, iDotIndex-iSlashIndex-1);

	        // whether the file is dxf file or dwg file.
	        bool bDwg = true;
	        String strExt = strFilePath.Substring(iDotIndex, strFilePath.Length-iDotIndex);
            strExt = strExt.ToLower();
	        if (strExt == ".dxf")
		        bDwg = false;
	        else if (strExt == ".dwg")
		        bDwg = true;
	        else
		        return mat;

            // extract geometry items from dxf/dwg file.
	        ImpDataEx impData;
	        if (!bDwg)
	        {
                impData = NestFacadeEx.ExtractGeomItems(strFilePath);
	        }
	        else
	        {
                // the temp folder for dxf.
                String strDxfPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                strDxfPath += "\\";
                strDxfPath += new Random().Next(1,100000).ToString();
                strDxfPath += ".dxf";

		        // save dxf file in tmp path.
		        NestFacadeEx.Dwg2Dxf(strFilePath, strDxfPath);

		        // extract geometry items from dxf file.
                impData = NestFacadeEx.ExtractGeomItems(strDxfPath);

		        // delete the temp file.
		        File.Delete(strDxfPath);
	        }
	        /************************************************************************/

	        /************************************************************************/
	        // get the polygons from the dxf/dwg file.

	        // build the polygons of all the geometry items.
            GeomItemListEx geomItemList = impData.GetAllGeomItem();
	        Poly2DListEx polyList = NestFacadeEx.BuildPolyListFromLineArc(geomItemList, nestParam.GetConTol());

            // if no closed polygon found, return.
            if (polyList.Size() == 0)
            {
                MessageBox.Show("No closed boundary found.", "NestProfessor DEMO");
                return mat;
            }

	        // the outer boundary polygon.
	        int iMaxIndex = polyList.GetMaxAreaPoly();
	        Polygon2DEx outerPoly = polyList.GetPolygonByIndex(iMaxIndex);

	        // the hole polygons.
	        polyList.DelPolygonByIndex(iMaxIndex);
	        Poly2DListEx holePolyList = polyList;
	        /************************************************************************/

	        /************************************************************************/
	        // build the material object.

	        // whether the boundary polygon is a rectangle.
	        bool bRectMat = true;
	        if (outerPoly.GetPtCount() != 4)
		        bRectMat = false;
	        else if (holePolyList.Size() > 0)
		        bRectMat = false;
	        else
	        {
		        // line items in the polygon.
		        ArrayList lineItems = outerPoly.GetLineList();
                LineItemEx lineItem1 = (LineItemEx)lineItems[0];
                LineItemEx lineItem2 = (LineItemEx)lineItems[1];
                LineItemEx lineItem3 = (LineItemEx)lineItems[2];
                LineItemEx lineItem4 = (LineItemEx)lineItems[3];

		        if (Math.Abs(lineItem1.GetLength() - lineItem3.GetLength()) > 0.0001)
			        bRectMat = false;
		        if (Math.Abs(lineItem2.GetLength() - lineItem4.GetLength()) > 0.0001)
			        bRectMat = false;

		        Vector2DEx vect1 = lineItem1.GetBaseVector();
		        Vector2DEx vect2 = lineItem2.GetBaseVector();
		        if (!vect1.OrthogonalTo(vect2))
			        bRectMat = false;
	        }

	        // build the material object.
	        if (bRectMat)
	        {
		        mat = new RectMatEx(strFileName, outerPoly.GetBoundaryRect(), 1);
	        }
	        else
	        {
		        PolyMatEx polyMat = new PolyMatEx(strFileName, outerPoly, 1);
                polyMat.SetUselessHoleList(holePolyList);
                mat = polyMat;
	        }
	        /************************************************************************/

	        return mat;
        }

        // calc the utilization of the material.
        static public double CalcMatUtil(SheetListEx sheetList, NestParamEx nestParam)
        {
	        double dUtilization = .0;

	        // calculate the utilization of material.
	        double dMatArea = .0, dNestedArea = .0;
	        for (int i = 0; i < sheetList.Size(); i++)
	        {
		        SheetEx sheet = sheetList.GetSheetByIndex(i);
		        int iSheetCount = sheet.GetCount();
		        dMatArea += sheet.GetMat().GetArea()*iSheetCount;

		        // go through each part placement object in the sheet.
		        PartPmtListEx partPmtList = sheet.GetPartPmtList();
		        for (int j = 0; j < partPmtList.Size(); j++)
		        {
			        PartPmtEx partPmt = partPmtList.GetPartPmtByIndex(j);
			        PartEx part = partPmt.GetPart();

			        // the area of the part.
			        double dPartArea = NestFacadeEx.GetPartArea(part, nestParam.GetConTol());

			        // part count in the part placement object.
			        int iPartCount = 1;
			        if (partPmt.IsGrid())
				        iPartCount = partPmt.GetRowCount()*partPmt.GetColCount();
			        dNestedArea += iSheetCount*iPartCount*dPartArea;
		        }
	        }

	        // figure out the utilization.
	        if (dMatArea == 0 || dNestedArea == 0)
		        dUtilization = .0;
	        else
		        dUtilization = (dNestedArea/dMatArea)*100;

	        return dUtilization;
        }

        // pick the next color for the part.
        static public Color PickNextColor_4_part(ref int iCurrentColorIndex)
        {
            Color iColor_picked = Color.Empty;

            // the color pool hold 10 colors.
            List<Color> colorPool = new List<Color>();
            colorPool.Add(Color.Red);
            colorPool.Add(Color.Orange);
            colorPool.Add(Color.Yellow);
            colorPool.Add(Color.Green);
            colorPool.Add(Color.Cyan);
            colorPool.Add(Color.Indigo);
            colorPool.Add(Color.Blue);
            colorPool.Add(Color.Purple);
            colorPool.Add(Color.Magenta);
            colorPool.Add(Color.Fuchsia);

            if (iCurrentColorIndex < colorPool.Count - 1)
            {
                iCurrentColorIndex++;
                iColor_picked = colorPool[iCurrentColorIndex];
            }
            else
            {
                iCurrentColorIndex = 0; // reset to start from the beginning.
                iColor_picked = colorPool[iCurrentColorIndex];
            }

            return iColor_picked;
        }
    }
}
