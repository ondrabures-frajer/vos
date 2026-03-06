using System;
using System.Windows.Forms;

namespace userrhash
{
    public partial class MainForm : Form
    {
        User user;
        UserManager manager;

        public MainForm(User u, UserManager m)
        {
            InitializeComponent();

            user = u;
            manager = m;

            // Zobrazíme jméno přihlášeného uživatele v titulku
            this.Text = $"Hlavní menu – {user.Username}" + (user.IsAdmin() ? " (Admin)" : "");

            // Tlačítko pro admin panel schováme normálním uživatelům
            if (!user.IsAdmin())
                btnAdmin.Visible = false;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string newPass = Microsoft.VisualBasic.Interaction.InputBox(
                "Zadejte nové heslo:", "Změna hesla");

            if (string.IsNullOrEmpty(newPass))
                return;

            if (newPass.Length < 4)
            {
                MessageBox.Show("Heslo musí mít alespoň 4 znaky.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            user.PasswordHash = UserManager.Hash(newPass);
            manager.Save();

            MessageBox.Show("Heslo bylo úspěšně změněno.", "Hotovo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            AdminForm f = new AdminForm(manager);
            f.ShowDialog(); // modální – MainForm zůstane blokovaný
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // LoginForm se zobrazí díky FormClosed eventu
        }
    }
}
