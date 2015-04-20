using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ConsoleApplication1.Manager.Models
{
    public partial class Users : EasyRepo.IEntity
    {
        public string Nominativo { get { return string.Format("{0} {1}", this.Surname, this.Firstname); } }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.Id, this.Nominativo);

            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                return textWriter.ToString();
            }
        }
    }
}
