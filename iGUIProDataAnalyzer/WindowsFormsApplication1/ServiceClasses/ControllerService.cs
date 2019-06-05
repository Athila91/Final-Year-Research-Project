using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using iGUIProDataAnalyzer.Model;
using System.Xml.Serialization;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace iGUIProDataAnalyzer.ServiceClasses
{
    class ControllerService
    {
        public static void CreateFile(string path, List<Controller> listController)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Controller>));
            XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto,
                OmitXmlDeclaration = true
            });
            serializer.Serialize(writer, listController);

            writer.Close();
        }

        public static void AddUserPreferredData(List<TextBox> listOfTextbox, List<Controller> list, string field, string region, string controller, string property)
        {
           

            
                for (int i = 0; i < 15; i++)
                {
                    iGUIProDataAnalyzer.Model.Controller c = new Model.Controller();
                    if (listOfTextbox[i].Text != "")
                    {
                        c.ProfessionalField = field;
                        c.Region = region;
                        c.ControllerName = controller;
                        c.PropertyName = property;
                        c.PropertyValue = listOfTextbox[i].Text;
                        listOfTextbox[i].Text = "";
                        list.Add(c);
                    }

                }
            
        }

        public static void SaveData(string path2, List<Controller> list)
        {
            
            string path = @"D:\\s.xml";
            string path1 = @"D:\\s1.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Controller>));
            using (FileStream file = File.OpenWrite(path1))
            {
                TextWriter writer = new StreamWriter(file);
                serializer.Serialize(writer, list);
            }

            File.Copy(path2, path);
            XmlTextReader xmlreader1 = new XmlTextReader(path);
            XmlTextReader xmlreader2 = new XmlTextReader(path1);

            DataSet ds = new DataSet();
            ds.ReadXml(xmlreader1);
            DataSet ds2 = new DataSet();
            ds2.ReadXml(xmlreader2);
            ds.Merge(ds2);
            ds.WriteXml(path2);
            xmlreader2.Close();
            xmlreader1.Close();
            File.Delete(path);
            File.Delete(path1);
        }

        public static List<Controller> LoadData()
        {
            string path2 = @"D:\\MostPreferredValues.xml";
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Controller>));
            List<Controller> preferredValuesList = new List<Controller>();
            StreamReader reader = new StreamReader(path2);
            preferredValuesList = (List<Controller>)(serializer2.Deserialize(reader));
            reader.Close();

            return preferredValuesList;
        }
    }



}

