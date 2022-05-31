using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections;
using System.Reflection;

namespace Common
{
    public class XmlUtility
    {
        public static Hashtable LoadXml(string strPath, Type typParam)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strPath);
            Hashtable htParams = new Hashtable();
            XmlNodeList nodeList = doc.SelectNodes("/params/param");
            foreach (XmlElement node in nodeList)
            {
                object value = null;
                if (typParam == null)
                {
                    value = node.InnerText;
                }
                else
                {
                    value = Activator.CreateInstance(typParam);
                    XmlNodeList propertyList = node.SelectNodes("./property");
                    foreach (XmlElement propertyNode in propertyList)
                    {
                        string strPropertyName = propertyNode.GetAttribute("name");
                        string strPropertyValue = propertyNode.InnerText;
                        PropertyInfo pi = GetPropertyInfo(typParam, strPropertyName);
                        Type propertyType = pi.PropertyType;
                        object propertyValue = null;
                        if (propertyType.IsEnum)
                        {
                            propertyValue = Enum.Parse(propertyType, strPropertyValue, true);
                        }
                        else
                        {
                            propertyValue = Convert.ChangeType(strPropertyValue, propertyType);
                        }
                        pi.SetValue(value, propertyValue, null);
                    }
                }
                htParams.Add(node.GetAttribute("name"), value);
            }
            return htParams;
        }

        public static PropertyInfo GetPropertyInfo(Type target, string name)
        {
            PropertyInfo pi = target.GetProperty(name);
            if (pi == null)
            {
                throw new System.Exception(string.Format("property \"{0}\" is nothing at \"{1}\"", name, target.Name));
            }
            return pi;
        }
    }
}
