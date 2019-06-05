using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            WindowsFormsApplication1.Model.Controller c = new Model.Controller();
            c.BackgroundColor = "Red";
            c.Culture = comboBox1.SelectedItem.ToString();
            List<WindowsFormsApplication1.Model.Controller> l = new List<Model.Controller>();
            l.Add(c);
            string path = @"D:\\s.xml";
            string path1 = @"D:\\s1.xml";
            string path2 = @"D:\\s2.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<WindowsFormsApplication1.Model.Controller>));

           
           
              

                     if (!(File.Exists(path2)))
                        {
                           
                            XmlWriter writer = XmlWriter.Create(path2, new XmlWriterSettings
                            {
                                Indent = true,
                                ConformanceLevel = ConformanceLevel.Auto,
                                OmitXmlDeclaration = true
                            });
                            serializer.Serialize(writer, l);
                          
                            writer.Close();
                            
                   
                     }
                     else if (!(File.Exists(path1)))
                     {
                         using (FileStream file = File.OpenWrite(path1))
                         {

                             //BinaryWriter b = new BinaryWriter(file);
                             //for(int i = 0; i<l.Count;i++)
                             //{
                             //    b.Write(l[i].ToString());
                             //}
                             TextWriter writer = new StreamWriter(file);
                             serializer.Serialize(writer, l);
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
                         Console.WriteLine("Completed merging XML documents");
                         xmlreader2.Close();
                         xmlreader1.Close();
                         File.Delete(path);
                         File.Delete(path1);
                         //string path2 = @"D:\\s2.xml";
                         XmlSerializer serializer2 = new XmlSerializer(typeof(List<WindowsFormsApplication1.Model.Controller>));
                         List<WindowsFormsApplication1.Model.Controller> l2 = new List<Model.Controller>();
                         StreamReader reader = new StreamReader(path2);
                         l2 = (List<WindowsFormsApplication1.Model.Controller>)(serializer2.Deserialize(reader));
                         reader.Close();
                     }
                     
                        else
                        {
//                           
                            try
                            {
                                using (FileStream file = File.OpenWrite(path1))
                                {


                                    TextWriter writer = new StreamWriter(file);
                                    serializer.Serialize(writer, l);
                                    writer.Close();

                                }
                                File.Copy(path, path1);
                                XmlTextReader xmlreader1 = new XmlTextReader(path1);
                                XmlTextReader xmlreader2 = new XmlTextReader(path2);

                                DataSet ds = new DataSet();
                                ds.ReadXml(xmlreader1);

                                DataSet ds2 = new DataSet();
                                ds2.ReadXml(xmlreader2);
                               
                                ds.Merge(ds2);
                                ds.WriteXml(path2);
                                Console.WriteLine("Completed merging XML documents");
                            }
                            catch (System.Exception ex)
                            {
                                Console.Write(ex.Message);
                            }
                            Console.Read();	
                       
                        }
                    
                    

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            comboBox1.Items.Add("South Asia");
            comboBox1.Items.Add("East Asia");
            comboBox1.Items.Add("Europe");

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
              string path2 = @"D:\\s2.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(List<WindowsFormsApplication1.Model.Controller>));
            List<WindowsFormsApplication1.Model.Controller> l = new List<Model.Controller>();
            StreamReader reader = new StreamReader(path2);
            l = (List<WindowsFormsApplication1.Model.Controller>)(serializer.Deserialize(reader));
            DataSet ds = new DataSet();
            ds.ReadXml(path2);
        

        }
    }
}
