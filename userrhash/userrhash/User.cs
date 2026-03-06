using System;
using System.Xml.Serialization;

namespace userrhash
{
    [Serializable]
    [XmlInclude(typeof(Admin))]
    public class User
    {
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";

        public virtual bool IsAdmin()
        {
            return false;
        }

        public override string ToString()
        {
            return Username + (IsAdmin() ? " [admin]" : "");
        }
    }
}
