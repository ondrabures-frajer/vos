using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace userrhash
{
    public class UserManager
    {
        public List<User> Users { get; set; } = new List<User>();

        // Uložíme soubor vedle spustitelného souboru
        private readonly string file = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "users.xml");

        private XmlSerializer CreateSerializer()
        {
            // Musíme předat podtyp Admin, aby serializace fungovala správně s dědičností
            return new XmlSerializer(typeof(List<User>), new Type[] { typeof(Admin) });
        }

        public void Save()
        {
            XmlSerializer s = CreateSerializer();
            using (StreamWriter w = new StreamWriter(file, false, Encoding.UTF8))
            {
                s.Serialize(w, Users);
            }
        }

        public void Load()
        {
            if (!File.Exists(file))
                return;

            XmlSerializer s = CreateSerializer();
            using (StreamReader r = new StreamReader(file, Encoding.UTF8))
            {
                Users = (List<User>)s.Deserialize(r)!;
            }
        }

        /// <summary>
        /// Zahashuje heslo pomocí SHA-256 a vrátí hex řetězec.
        /// </summary>
        public static string Hash(string password)
        {
            byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder(64);
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Pokusí se přihlásit uživatele. Vrátí objekt User nebo null.
        /// </summary>
        public User? Login(string name, string password)
        {
            string hash = Hash(password);

            foreach (User u in Users)
            {
                if (u.Username == name && u.PasswordHash == hash)
                    return u;
            }

            return null;
        }

        /// <summary>
        /// Přidá nového uživatele s plaintext heslem (zahashuje ho).
        /// Vrátí false, pokud uživatelské jméno již existuje.
        /// </summary>
        public bool AddUser(string username, string password, bool isAdmin = false)
        {
            foreach (User u in Users)
            {
                if (u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            User newUser = isAdmin ? new Admin() : new User();
            newUser.Username = username;
            newUser.PasswordHash = Hash(password);

            Users.Add(newUser);
            Save();
            return true;
        }

        /// <summary>
        /// Odstraní uživatele ze seznamu.
        /// </summary>
        public bool RemoveUser(User user)
        {
            bool removed = Users.Remove(user);
            if (removed) Save();
            return removed;
        }
    }
}
