using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iGUIProDataAnalyzer.Model;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace iGUIProDataAnalyzer.ServiceClasses
{
    class DataAnalyze
    {
        public static bool AnalyzeUserPreferredData()
        {
            int i = 0;
            int k = 1;
            string path = @"D:\\s2.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<Controller>));
            List<Controller> list = new List<Controller>();
            if(!(File.Exists(path)))
            {
                return false;
            }
            StreamReader reader = new StreamReader(path);
            list = (List<Controller>)(serializer.Deserialize(reader));
            List<Controller> list0 = new List<Controller>();
            List<List<Controller>> preferredValueLists = new List<List<Controller>>();
            foreach (Controller con in list)
            {
                if (i == 0)
                {
                    list0.Add(con);
                    preferredValueLists.Add(list0);
                    i++;
                }
                else
                {
                    for (int j = 0; j < preferredValueLists.Count; j++)
                    {
                        if (con.ControllerName == preferredValueLists[j][0].ControllerName && con.Region == preferredValueLists[j][0].Region && 
                            con.ProfessionalField == preferredValueLists[j][0].ProfessionalField && con.PropertyName == preferredValueLists[j][0].PropertyName)
                        {
                            preferredValueLists[j].Add(con);
                            break;
                        }
                        else if (j == preferredValueLists.Count - 1)
                        {
                            var dict = new Dictionary<string, List<Controller>>();
                            dict["list" + k] = new List<Controller>();
                            dict["list" + k].Add(con);
                            preferredValueLists.Add(dict["list" + k]);
                            k++;
                            break;
                        }
                    }
                    
                }
            }
            FindMostPreferredValueList(preferredValueLists);
            return true;
        }

        private static void FindMostPreferredValueList(List<List<Controller>> valueLists)
        {
            List<Controller> mostPreferredList = new List<Controller>();
            
            foreach (List<Controller> llist in valueLists)
            {
                int i = 0;
                var most = (from a in llist
                            group a by a.PropertyValue into grp
                            orderby grp.Count() descending
                            select grp.Key);
                foreach (var item in most)
                {
                    llist[i].PropertyValue = item.ToString();
                    mostPreferredList.Add(llist[i]);
                    i++;
                }
                //llist[0].PropertyValue = most;
                //mostPreferredList.Add(llist[0]);
            }
            
            string path = @"D:\\MostPreferredValues.xml";
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<Controller>));
            XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings
                {
                    Indent = true,
                    ConformanceLevel = ConformanceLevel.Auto,
                    OmitXmlDeclaration = true
                });

            serializer2.Serialize(writer, mostPreferredList);
            writer.Close();
        }
    }

}
    

