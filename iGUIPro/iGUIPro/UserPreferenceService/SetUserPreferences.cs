using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Reflection;
using System.Globalization;
using iGUIPro;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;
namespace iGUIPro.UserPreferenceService
{
    class SetUserPreferences
    {
        public static void ApplyPrefeence(DTE2 dte, string region, string controller, string field, string property, string preferredValue)
        {
            //string preferredValue = GetMostPreferredValue(region, controller, field, property);
            List<PreviousPropertyValue> listDefault = new List<PreviousPropertyValue>();
             foreach (ProjectItem pi in dte.Solution.Projects.Item(1).ProjectItems)
                        {
                            if (pi.ProjectItems != null)
                            {
                                foreach (ProjectItem p in pi.ProjectItems)
                                {
                                    if (p.Name.EndsWith(".Designer.cs"))
                                    {
                                        p.Open(EnvDTE.Constants.vsViewKindCode);
                                        p.Document.Activate();
                                        TextSelection ts = (TextSelection)dte.ActiveDocument.Selection;
                                        TextSelection ts2 = (TextSelection)dte.ActiveDocument.Selection;
                                        string srchPattern1 = "new System.Windows.Forms.Button();";
                                        EnvDTE.TextRanges textRanges = null;
                                       
                                        ts.StartOfDocument(false);
                                        
                                        int count = 0;
                                        
                                        string nameLine = "";
                                        string name = "";
                                        string[] np = new string[50];
                                        
                                        while (ts.FindPattern(srchPattern1, 0, ref textRanges))
                                        {
                                            ts.SelectLine();
                                            nameLine = ts.Text;
                                            count++;
                                            string[] sp = nameLine.Split('.');
                                            string spi = sp[1];
                                            string[] sp2 = spi.Split('=');
                                            name = sp2[0];
                                            np[count] = name;
                                        }

                                        int i = 1;
                                        while (ts2.FindPattern(".BackColor = System.Drawing.Color", 0, ref textRanges))
                                        {
                                            PreviousPropertyValue def = new PreviousPropertyValue();
                                            
                                            ts2.SelectLine();
                                            string codeLine = ts2.Text;
                                            codeLine = codeLine.Trim();
                                          foreach(string s in np)
                                          {
                                                string ss = s;
                                                if (ss != null)
                                                {
                                                    ss = ss.Trim();
                                                    if (codeLine.Contains(ss) == true)
                                                    {
                                                        ts2.ReplacePattern(codeLine, "this." + s + ".BackColor = System.Drawing.Color." + preferredValue + ";", 0, ref textRanges);
                                                        np = np.Where(w => w != s).ToArray();
                                                        def.FileName = p.Name;
                                                        def.ControllerType = controller;
                                                        def.Property = property;
                                                        def.ControllerName = ss;
                                                        def.DefaultValue = codeLine;
                                                        listDefault.Add(def);
                                                    }
                                                    //else
                                                    //{
                                                    //    ts2.LineDown();
                                                    //    ts2.NewLine();
                                                    //    ts2.Insert("this." + np[i] + ".BackColor = System.Drawing.Color." + preferredValue + ";");
                                                    //}
                                                    //def.FileName = p.Name;
                                                    //def.ControllerType = controller;
                                                    //def.Property = property;
                                                    //def.ControllerName = ss;
                                                    //def.DefaultValue = codeLine;
                                                    //listDefault.Add(def);
                                                }
                                                
                                            }
                                         
                                            //i++;

                                        }
                                        if (np != null)
                                        {
                                            foreach(string s in np)
                                            {
                                                if (s != null)
                                                {
                                                    ts2.EndOfLine();

                                                    ts2.NewLine();
                                                    ts2.Insert("this."+np[i]+".BackColor = System.Drawing.Color." + preferredValue + ";");
                                                    np = np.Where(w => w != s).ToArray();
                                                }
                                            }
                                    }
                                        SaveDefaultValues(listDefault);
                                        dte.ActiveDocument.Save(p.Document.FullName);
                                        dte.ActiveDocument.Close(vsSaveChanges.vsSaveChangesNo);
                                    }
                                }
                            }
                        }
                    
        }

        public static List<Controller> LoadPreferenceValues()
        {
            string path2 = @"D:\\MostPreferredValues.xml";
            List<Controller> preferredValuesList = new List<Controller>();
            if (!File.Exists(path2))
            {
                return preferredValuesList;
            }

            else
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(List<Controller>));
                
                StreamReader reader = new StreamReader(path2);
                preferredValuesList = (List<Controller>)(serializer2.Deserialize(reader));
                reader.Close();
                return preferredValuesList;
            }

            
        }

        public static string[] GetMostPreferredValue(string region, string controller, string field, string property)
        {
            List<Controller> PreferredValuesList = LoadPreferenceValues();
            if (PreferredValuesList != null)
            {
                var most = PreferredValuesList.FindAll(p => p.ProfessionalField == field && p.ControllerName == controller && p.Region == region && p.PropertyName == property);
                string[] preferredValues = new string[most.Count];
                for (int i = 0; i < most.Count; i++)
                {
                    preferredValues[i] = most[i].PropertyValue;
                }
                return preferredValues;
            }
            else
            {
                string[] preferredValues = new string[3];
                return preferredValues;
            }

        }

        private static void SaveDefaultValues(List<PreviousPropertyValue> list)
        {
            string path = @"D:\\PreviousPropertyValues.xml";
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<PreviousPropertyValue>));
            XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings
            {
                Indent = true,
                ConformanceLevel = ConformanceLevel.Auto,
                OmitXmlDeclaration = true
            });
            serializer2.Serialize(writer, list);

            writer.Close();
        }

        public static bool ChangeToPreveiousValue(DTE2 dte, string controller, string property)
        {
            List<PreviousPropertyValue> listDefault = new List<PreviousPropertyValue>();
            List<PreviousPropertyValue> list = LoadDefaultValues();
            if (list == null)
            {
                return false;
            }
            else
            {
                foreach (ProjectItem pi in
                             dte.Solution.Projects.Item(1).ProjectItems)
                {
                    if (pi.ProjectItems != null)
                    {
                        foreach (ProjectItem p in pi.ProjectItems)
                        {

                            if (p.Name.EndsWith(".Designer.cs"))
                            {
                                p.Open(EnvDTE.Constants.vsViewKindCode);
                                p.Document.Activate();
                                TextSelection ts2 = (TextSelection)dte.ActiveDocument.Selection;

                                EnvDTE.TextRanges textRanges = null;

                                ts2.StartOfDocument(false);
                                //Find2 findWin = (Find2)dte.Find;
                                int count = 0;
                                //c = findWin.FindReplace(vsFindAction.vsFindActionFindAll, "button1", 0);
                                string s = "";
                                string name = "";
                                string[] np = new string[50];
                                // Advance to the next Visual Basic function beginning or end by 
                                // searching for  "Sub" with white space before and after it.
                                //while
                                //while (ts.FindPattern(srchPattern1, 0, ref textRanges))
                                //{

                                //    //    //  Select the entire line.

                                //    count++;
                                //    ts.SelectLine();
                                //    s = ts.Text;

                                //    string[] sp = s.Split('.');
                                //    string spi = sp[1];


                                //    string[] sp2 = spi.Split('=');
                                //    name = sp2[0];

                                //    np[count] = name;
                                //    //ts.FindPattern("this." + name + ".BackColor = System.Drawing.Color", 0, ref textRanges);
                                //    //ts.SelectLine();
                                //    //s = ts.Text;
                                //    //ts2.StartOfDocument(false);
                                //    //while (ts.FindText("this." + name + ".BackColor = System.Drawing.Color", 0))
                                //    //{

                                //    //    ts.SelectLine();
                                //    //    string sd = ts.Text;
                                //    //    bool t = ts.ReplacePattern(sd, "this.button1.BackColor = System.Drawing.Color.Red;", 0, ref textRanges);
                                //    //}

                                //}

                                int i = 1;
                                //for(int i=1; i<=5;i++)
                                //{
                                //ts2 = null;


                                while (ts2.FindPattern(".BackColor = System.Drawing.Color", 0, ref textRanges))
                                {
                                    ts2.SelectLine();
                                    string sd = ts2.Text;
                                    sd = sd.Trim();
                                    foreach (PreviousPropertyValue dfcon in list)
                                    {

                                        if (dfcon.FileName == p.Name && sd.Contains(dfcon.ControllerName) && dfcon.Property == property && dfcon.ControllerType == controller)
                                        {
                                            ts2.ReplacePattern(sd, dfcon.DefaultValue, 0, ref textRanges);

                                        }
                                        i++;
                                        //}


                                    }
                                }
                                //ts.NewLine(1);


                                dte.ActiveDocument.Save(p.Document.FullName);
                                dte.ActiveDocument.Close(vsSaveChanges.vsSaveChangesNo);
                            }
                        }
                    }
                }
                return true;
            }
        }

        public static List<PreviousPropertyValue> LoadDefaultValues()
        {
            string path2 = @"D:\\PreviousPropertyValues.xml";
            XmlSerializer serializer2 = new XmlSerializer(typeof(List<PreviousPropertyValue>));
            List<PreviousPropertyValue> defaultValuesList = new List<PreviousPropertyValue>();
            StreamReader reader = new StreamReader(path2);
            defaultValuesList = (List<PreviousPropertyValue>)(serializer2.Deserialize(reader));
            reader.Close();

            return defaultValuesList;
        }
    }
}
