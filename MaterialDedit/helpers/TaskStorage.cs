using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

using NestBridge;

namespace MaterialDedit
{
    class TaskStorage
    {
        // the version of the task file.
        public const int TASK_VERSION_1 = 1;

        // load nest task from task file.
        static public NestTaskEx LoadNestTask_from_taskFile(String strFilePath, List<KeyValuePair<long, string>> partDxfPath, List<KeyValuePair<long, string>> matDxfPath,
									                        Dictionary<long, int> partColorConfig, ImpDataListEx impDataList, ref int iNestingTime)
        {
            NestTaskEx nestTask = new NestTaskEx();

            // load the task file.
            XmlDocument xmlDocument = new XmlDocument();
            StreamReader reader = new StreamReader(strFilePath, Encoding.UTF8);
            xmlDocument.Load(reader);

            // the version.
            XmlNode taskNode = xmlDocument.SelectSingleNode("NestTask");
            int iTaskVersion = Convert.ToInt32(taskNode.Attributes["taskVersion"].Value);

            if (iTaskVersion == TASK_VERSION_1)
            {
                // load param.
                XmlNode paramNode = taskNode.SelectSingleNode("Param");
                NestParamEx nestParam = LoadNestParam_V1(paramNode, ref iNestingTime);
                nestTask.SetNestParam(nestParam);

                // load nest part info.
                XmlNode partListNode = taskNode.SelectSingleNode("PartList");
                NestPartListEx nestParts = LoadNestParts_V1(strFilePath, partListNode, partDxfPath, impDataList, partColorConfig);
                nestTask.SetNestPartList(nestParts);

                // load material info.
                XmlNode matListNode = taskNode.SelectSingleNode("MaterialList");
                MatListEx mats = LoadMats_V1(strFilePath, matListNode, matDxfPath, nestParam);
                nestTask.SetMatList(mats);
            }

            return nestTask;
        }

        // save the nest task to file.
        static public void SaveNestTask(String strFilePath, NestTaskEx nestTask, List<KeyValuePair<long, string>> partDxfPath,
							            Dictionary<long, int> partColorConfig, List<KeyValuePair<long, string>> matDxfPath, int iNestingTime)
        {
            XmlDocument xmlDoc = new XmlDocument();

	        // the task node.
            XmlElement taskNode = xmlDoc.CreateElement("NestTask");
            XmlAttribute versionAttribute = xmlDoc.CreateAttribute("taskVersion");
            versionAttribute.Value = TASK_VERSION_1.ToString();
            taskNode.Attributes.Append(versionAttribute);
            xmlDoc.AppendChild(taskNode);

	        // save part info.
            XmlElement partListNode = xmlDoc.CreateElement("PartList");
            taskNode.AppendChild(partListNode);
            SaveNestParts(xmlDoc, partListNode, nestTask.GetNestPartList(), partDxfPath, partColorConfig);

	        // save material info.
            XmlElement matListNode = xmlDoc.CreateElement("MaterialList");
            taskNode.AppendChild(matListNode);
            SaveMats(xmlDoc, matListNode, nestTask.GetMatList(), matDxfPath);

	        // save param info.
            XmlElement paramNode = xmlDoc.CreateElement("Param");
            taskNode.AppendChild(paramNode);
            SaveNestParam(xmlDoc, paramNode, nestTask.GetNestParam(), iNestingTime);

	        xmlDoc.Save(strFilePath);
        }

        static private NestParamEx LoadNestParam_V1(XmlNode paramNode, ref int iNestingTime)
        {
	        NestParamEx nestParam = new NestParamEx();

	        // MatLeftMargin.
	        nestParam.SetMatLeftMargin(Convert.ToDouble(paramNode.SelectSingleNode("MatLeftMargin").InnerText));

	        // MatRightMargin.
            nestParam.SetMatRightMargin(Convert.ToDouble(paramNode.SelectSingleNode("MatRightMargin").InnerText));

	        // MatTopMargin.
            nestParam.SetMatTopMargin(Convert.ToDouble(paramNode.SelectSingleNode("MatTopMargin").InnerText));

	        // MatBottomMargin.
            nestParam.SetMatBottomMargin(Convert.ToDouble(paramNode.SelectSingleNode("MatBottomMargin").InnerText));

	        // MatBottomMargin.
            nestParam.SetMatMargin(Convert.ToDouble(paramNode.SelectSingleNode("MatMargin").InnerText));

	        // PartDis.
            nestParam.SetPartDis(Convert.ToDouble(paramNode.SelectSingleNode("PartDis").InnerText));

	        // ConTol.
            nestParam.SetConTol(Convert.ToDouble(paramNode.SelectSingleNode("ConTol").InnerText));

	        // PartRotStep.
            nestParam.SetPartRotStep(Convert.ToDouble(paramNode.SelectSingleNode("PartRotStep").InnerText));

	        // StartCorner.
            nestParam.SetStartCorner((RECT_CORNER_EX)Convert.ToInt32(paramNode.SelectSingleNode("StartCorner").InnerText));

	        // NestDir.
            nestParam.SetNestDir((NEST_DIRECTION_EX)Convert.ToInt32(paramNode.SelectSingleNode("NestDir").InnerText));

	        // PartInPart.
            if (Convert.ToInt32(paramNode.SelectSingleNode("PartInPart").InnerText) == 1)
            {
                nestParam.IsPartInPart(true);
            }
            else if (Convert.ToInt32(paramNode.SelectSingleNode("PartInPart").InnerText) == 0)
            {
                nestParam.IsPartInPart(false);
            }

	        // EvalFactor.
            nestParam.SetEvalFactor(Convert.ToInt32(paramNode.SelectSingleNode("EvalFactor").InnerText));

	        // nesting time.
            iNestingTime = Convert.ToInt32(paramNode.SelectSingleNode("NestingTime").InnerText);

	        return nestParam;
        }

        static private NestPartListEx LoadNestParts_V1(string strTaskFilePath, XmlNode partListNode, List<KeyValuePair<long, string>> partDxfPath, 
											           ImpDataListEx impDataList, Dictionary<long, int> partColorConfig)
        {
	        NestPartListEx nestParts = new NestPartListEx();

	        for (int i = 0; i < partListNode.ChildNodes.Count; i++)
	        {
                XmlNode partNode = partListNode.ChildNodes.Item(i);
		        NestPartEx nestPart = new NestPartEx();

		        // part Path.
		        string strPartFileFullPath = partNode.SelectSingleNode("Path").InnerText;

		        // load part.
                if (!File.Exists(strPartFileFullPath))
		        {
                    // the new file path.
                    string strTaskFileFolder = strTaskFilePath.Substring(0, strTaskFilePath.LastIndexOf("\\"));
                    string strPartFileName = strPartFileFullPath.Substring(strPartFileFullPath.LastIndexOf("\\") + 1, strPartFileFullPath.Length - strPartFileFullPath.LastIndexOf("\\") - 1);
                    string strNewPartFileFullPath = strTaskFileFolder + "\\" + strPartFileName;

                    // try again.
                    if (!File.Exists(strNewPartFileFullPath))
                    {
                        string strMessage = "Cannot find part file: ";
                        MessageBox.Show(strMessage + strPartFileFullPath, "NestProfessor DEMO");
                        continue;
                    }
                    else
                    {
                        strPartFileFullPath = strNewPartFileFullPath;
                    }
		        }
                PartEx part = NestHelper.LoadPartFromDxfdwg(strPartFileFullPath, impDataList);
		        nestPart.SetPart(part);

		        // nest count.
		        nestPart.SetNestCount(Convert.ToInt32(partNode.SelectSingleNode("Count").InnerText));

		        // rotate.
		        nestPart.SetRotStyle((PART_ROT_STYLE_EX)Convert.ToInt32(partNode.SelectSingleNode("Rotate").InnerText));

		        // custom angle.
		        if (nestPart.GetRotStyle() == PART_ROT_STYLE_EX.PART_ROT_CUSTOM_ANG)
		        {
			        ArrayList customRotAngs = new ArrayList();

			        string strAngles = partNode.SelectSingleNode("CustomAng").InnerText;
                    string[] strArray = strAngles.Split(',');
                    foreach (string strAngle in strArray)  
                    {
                        double dAngle = Convert.ToDouble(strAngle);
                        customRotAngs.Add(dAngle);
                    }
			        nestPart.SetCustomRotAng(customRotAngs);
		        }

		        // color.
		        int iColor = Convert.ToInt32(partNode.SelectSingleNode("Color").InnerText);

		        nestParts.AddNestPart(nestPart);
                partDxfPath.Add(new KeyValuePair<long, string>(nestPart.GetID(), strPartFileFullPath));
		        partColorConfig[nestPart.GetPart().GetID()] = iColor;
	        }

	        return nestParts;
        }

        static private MatListEx LoadMats_V1(string strTaskFilePath, XmlNode matListNode, List<KeyValuePair<long, string>> matDxfPath, NestParamEx nestParam)
        {
	        MatListEx mats = new MatListEx();

	        for (int i = 0; i < matListNode.ChildNodes.Count; i++)
	        {
                XmlNode matNode = matListNode.ChildNodes.Item(i);

		        // whether load material from file.
		        XmlNode pathNode = matNode.SelectSingleNode("MatPath");
		        if (pathNode != null)
		        {
                    string strMaterialFileFullPath = pathNode.InnerText;
                    if (!File.Exists(strMaterialFileFullPath))
			        {
                        // the new file path.
                        string strTaskFileFolder = strTaskFilePath.Substring(0, strTaskFilePath.LastIndexOf("\\"));
                        string strMaterialFileName = strMaterialFileFullPath.Substring(strMaterialFileFullPath.LastIndexOf("\\") + 1, strMaterialFileFullPath.Length - strMaterialFileFullPath.LastIndexOf("\\") - 1);
                        string strNewMaterialFileFullPath = strTaskFileFolder + "\\" + strMaterialFileName;

                        // try again.
                        if (!File.Exists(strNewMaterialFileFullPath))
                        {
                            string strMessage = "Cannot find material file: ";
                            MessageBox.Show(strMessage + strMaterialFileFullPath, "NestProfessor DEMO");
                            continue;
                        }
                        else
                        {
                            strMaterialFileFullPath = strNewMaterialFileFullPath;
                        }
			        }

			        MatEx mat = NestHelper.LoadMatFromDxfdwg(strMaterialFileFullPath, nestParam);
			        mats.AddMat(mat);
			        matDxfPath.Add(new KeyValuePair<long, string>(mat.GetID(), strMaterialFileFullPath));

			        // count.
			        mat.SetCount(Convert.ToInt32(matNode.SelectSingleNode("Count").InnerText));
		        }
		        else
		        {
			        RectMatEx rectMat = new RectMatEx();
			        mats.AddMat(rectMat);

			        // name
			        rectMat.SetName(matNode.SelectSingleNode("Name").InnerText);

			        // width.
			        double dWidth = Convert.ToDouble(matNode.SelectSingleNode("Width").InnerText);

			        // height.
			        double dHeight = Convert.ToDouble(matNode.SelectSingleNode("Height").InnerText);

			        // the material rect.
			        Rect2DEx matRect = new Rect2DEx(0,dWidth,0,dHeight);
			        rectMat.SetMatRect(matRect);

			        // count.
                    rectMat.SetCount(Convert.ToInt32(matNode.SelectSingleNode("Count").InnerText));
		        }
	        }

	        return mats;
        }

        static private void SaveNestParts(XmlDocument xmlDoc, XmlNode partListNode, NestPartListEx nestParts, List<KeyValuePair<long, string>> partDxfPath, Dictionary<long, int> partColorConfig)
        {
            for (int i = 0; i < nestParts.Size(); i++)
	        {
		        NestPartEx nestPart = nestParts.GetNestPartByIndex(i);

                XmlElement partNode = xmlDoc.CreateElement("Part");
                partListNode.AppendChild(partNode);

		        // path node.
		        {
			        for (int j = 0; i < partDxfPath.Count; j++)
			        {
				        if (partDxfPath[j].Key == nestPart.GetID())
				        {
                            XmlElement pathNode = xmlDoc.CreateElement("Path");
                            pathNode.InnerText = partDxfPath[j].Value;
                            partNode.AppendChild(pathNode);
					        break;
				        }
			        }
		        }

		        // Count
		        {
                    XmlElement countNode = xmlDoc.CreateElement("Count");
                    countNode.InnerText = nestPart.GetNestCount().ToString();
                    partNode.AppendChild(countNode);
		        }

		        // Rotate.
		        {
                    XmlElement rotateNode = xmlDoc.CreateElement("Rotate");
                    rotateNode.InnerText = ((int)nestPart.GetRotStyle()).ToString();
                    partNode.AppendChild(rotateNode);

			        // custom angles.
                    if (nestPart.GetRotStyle() == PART_ROT_STYLE_EX.PART_ROT_CUSTOM_ANG)
			        {
				        // the angle-string.
                        string strAngs = "";
				        ArrayList angles = nestPart.GetCustomRotAng();
				        for (int k = 0; k < angles.Count; k++)
				        {
                            double dAngle = (double)angles[k];
                            strAngs += dAngle.ToString("0.00000000");
                            strAngs += ",";
				        }

				        // append the node.
                        XmlElement customAngleNode = xmlDoc.CreateElement("CustomAng");
                        customAngleNode.InnerText = strAngs;
                        partNode.AppendChild(customAngleNode);
			        }
		        }

		        // part color.
		        {
                    XmlElement colorNode = xmlDoc.CreateElement("Color");
                    colorNode.InnerText = partColorConfig[nestPart.GetPart().GetID()].ToString();
                    partNode.AppendChild(colorNode);
		        }
	        }
        }

        static private void SaveMats(XmlDocument xmlDoc, XmlNode matListNode, MatListEx mats, List<KeyValuePair<long, string>> matDxfPath)
        {
            for (int i = 0; i < mats.Size(); i++)
	        {
		        MatEx mat = mats.GetMatByIndex(i);

                XmlElement materialNode = xmlDoc.CreateElement("Material");
                matListNode.AppendChild(materialNode);

		        bool bFromDxf = false;
		        for (int j = 0; i < matDxfPath.Count; j++)
		        {
			        if (matDxfPath[j].Key == mat.GetID())
			        {
                        XmlElement matPathNode = xmlDoc.CreateElement("MatPath");
                        matPathNode.InnerText = matDxfPath[j].Value;
                        materialNode.AppendChild(matPathNode);

				        // Count
				        {
                            XmlElement countNode = xmlDoc.CreateElement("Count");
                            countNode.InnerText = mat.GetCount().ToString();
                            materialNode.AppendChild(countNode);
				        }

				        bFromDxf = true;
				        break;
			        }
		        }

		        // material is not from dxf.
		        if (!bFromDxf)
		        {
			        RectMatEx rectMat = (RectMatEx)(mat);

			        // name
			        {
                        XmlElement nameNode = xmlDoc.CreateElement("Name");
                        nameNode.InnerText = mat.GetName();
                        materialNode.AppendChild(nameNode);
			        }

			        // Width
			        {
                        XmlElement widthNode = xmlDoc.CreateElement("Width");
                        widthNode.InnerText = rectMat.GetBoundaryRect().GetWidth().ToString("0.000000");
                        materialNode.AppendChild(widthNode);
			        }

			        // Height
			        {
                        XmlElement heightNode = xmlDoc.CreateElement("Height");
                        heightNode.InnerText = rectMat.GetBoundaryRect().GetHeight().ToString("0.000000");
                        materialNode.AppendChild(heightNode);
			        }

			        // Count
			        {
                        XmlElement countNode = xmlDoc.CreateElement("Count");
                        countNode.InnerText = rectMat.GetCount().ToString();
                        materialNode.AppendChild(countNode);
			        }
		        }
	        }
        }

        static private void SaveNestParam(XmlDocument xmlDoc, XmlNode paramNode, NestParamEx nestParam, int iNestingTime)
        {
            // MatLeftMargin.
            {
                XmlElement node = xmlDoc.CreateElement("MatLeftMargin");
                node.InnerText = nestParam.GetMatLeftMargin().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // MatRightMargin.
            {
                XmlElement node = xmlDoc.CreateElement("MatRightMargin");
                node.InnerText = nestParam.GetMatRightMargin().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // MatTopMargin.
            {
                XmlElement node = xmlDoc.CreateElement("MatTopMargin");
                node.InnerText = nestParam.GetMatTopMargin().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // MatBottomMargin.
            {
                XmlElement node = xmlDoc.CreateElement("MatBottomMargin");
                node.InnerText = nestParam.GetMatBottomMargin().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // MatMargin.
            {
                XmlElement node = xmlDoc.CreateElement("MatMargin");
                node.InnerText = nestParam.GetMatMargin().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // PartDis.
            {
                XmlElement node = xmlDoc.CreateElement("PartDis");
                node.InnerText = nestParam.GetPartDis().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // ConTol.
            {
                XmlElement node = xmlDoc.CreateElement("ConTol");
                node.InnerText = nestParam.GetConTol().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // PartRotStep.
            {
                XmlElement node = xmlDoc.CreateElement("PartRotStep");
                node.InnerText = nestParam.GetPartRotStep().ToString("0.0000000");
                paramNode.AppendChild(node);
            }

            // StartCorner.
            {
                XmlElement node = xmlDoc.CreateElement("StartCorner");
                node.InnerText = ((int)nestParam.GetStartCorner()).ToString();
                paramNode.AppendChild(node);
            }

            // NestDir.
            {
                XmlElement node = xmlDoc.CreateElement("NestDir");
                node.InnerText = ((int)nestParam.GetNestDir()).ToString();
                paramNode.AppendChild(node);
            }

            // PartInPart.
            {
                XmlElement node = xmlDoc.CreateElement("PartInPart");
                if (nestParam.IsPartInPart())
                {
                    node.InnerText = "1";
                }
                else
                {
                    node.InnerText = "0";
                }
                paramNode.AppendChild(node);
            }

            // EvalFactor.
            {
                XmlElement node = xmlDoc.CreateElement("EvalFactor");
                node.InnerText = nestParam.GetEvalFactor().ToString();
                paramNode.AppendChild(node);
            }

            // nest time.
            {
                XmlElement node = xmlDoc.CreateElement("NestingTime");
                node.InnerText = iNestingTime.ToString();
                paramNode.AppendChild(node);
            }
        }
    }
}
