﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NTRDebuggerTool.Objects.Saving
{
    public class SaveManager
    {
        public String titleId;
        public List<SaveCode> codes;
        public List<GateShark> gscodes;

        public void Init()
        {
            titleId = null;
            codes = new List<SaveCode>();
            gscodes = new List<GateShark>();
        }

        public override string ToString()
        {
            return titleId + ",[" + codes.ToString() + "],[" + gscodes.ToString() + "]";
        }

        public static void SaveToXml(string filePath, SaveManager sourceObj)
        {
            if (sourceObj.titleId == null || sourceObj.codes.Count == 0)
            {
                return;
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(sourceObj.GetType());
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception saving codes [" + sourceObj + "] to XML file", ex);
                MessageBox.Show(ex.Message);
            }
        }

        public static SaveManager LoadFromXml(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(SaveManager));
                    return (SaveManager)xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception loading codes from XML file " + filePath, ex);
            }
            return new SaveManager();
        }
    }
}