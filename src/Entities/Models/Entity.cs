using Entities.LinkModels;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Entities.Models
{
    public class Entity : Dictionary<string, object>
    {
        public Entity() : base() { }
        public void WriteToXml(XmlWriter writer)
        {
            foreach (var kv in this)
            {
                WriteLinksToXml(kv.Key, kv.Value, writer);
            }
        }

        private void WriteLinksToXml(string key, object value, XmlWriter writer)
        {
            writer.WriteStartElement(key);

            if (value == null)
            {
                writer.WriteEndElement();
                return;
            }

            if (value is List<Link> links)
            {
                foreach (var link in links)
                {
                    writer.WriteStartElement(nameof(Link));
                    writer.WriteElementString(nameof(link.Href), link.Href ?? string.Empty);
                    writer.WriteElementString(nameof(link.Method), link.Method ?? string.Empty);
                    writer.WriteElementString(nameof(link.Rel), link.Rel ?? string.Empty);
                    writer.WriteEndElement();
                }
            }
            else
            {
             
                writer.WriteString(value.ToString());
            }

            writer.WriteEndElement();
        }
    }
}
