using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace iGUIPro
{
    [Serializable, XmlRoot(ElementName = "PreviousPropertyValue")]
    [XmlType("PreviousPropertyValue")]

    public class PreviousPropertyValue
    {
        public string FileName { get; set; }
        public string ControllerName { get; set; }
        public string ControllerType { get; set; }
        public string Property { get; set; }
        public string DefaultValue { get; set; }
    }
}
