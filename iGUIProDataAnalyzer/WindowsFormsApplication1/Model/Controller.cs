using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iGUIProDataAnalyzer.Model
   
{
    [Serializable, XmlRoot(ElementName = "Controller")]
    [XmlType("Controller")]
    
    public class Controller 
    {
         public string Region { get; set; } 
         public string ControllerName { get; set; }
         public string PropertyName { get; set; }
         public string ProfessionalField { get; set; }
         public string PropertyValue { get; set; }
    }
}
