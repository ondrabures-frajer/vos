using System;
using System.Windows.Forms;

namespace userrhash
{
    public partial class AdminForm : Form
    {
        UserManager manager;

        public AdminForm(UserManager m)
        {
            InitializeComponent();

            manager = m;
            RefreshList();
        }

        /// <summary>Znovu načte seznam uživatelů do ListBoxu.</summary>
        private void RefreshList()
        {
            // Uložíme aktuálně vybraný index
            int selectedIndex = listUsers.SelectedIndex;

            listUsers.DataSource = null;
            listUsers.DataSource = new System.ComponentModel.BindingList<User>(manager.Users);

            // Obnovíme výběr, pokud to jde
            if (selectedIndex >= 0 && selectedIndex < listUsers.Items.Count)
                listUsers.SelectedIndex = selectedIndex;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedItem is not User u)
            {
                MessageBox.Show("Vyberte uživatele.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pass = Microsoft.VisualBasic.Interaction.InputBox(
                $"Nové heslo pro uživatele '{u.Username}':", "Reset hesla");

            if (string.IsNullOrEmpty(pass))
                return;

            if (pass.Length < 4)
            {
                MessageBox.Show("Heslo musí mít alespoň 4 znaky.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            u.PasswordHash = UserManager.Hash(pass);
            manager.Save();

            MessageBox.Show($"Heslo uživatele '{u.Username}' bylo resetováno.", "Hotovo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox(
                "Uživatelské jméno nového uživatele:", "Nový uživatel");

            if (string.IsNullOrWhiteSpace(name))
                return;

            string pass = Microsoft.VisualBasic.Interaction.InputBox(
                "Heslo nového uživatele (min. 4 znaky):", "Nový uživatel");

            if (string.IsNullOrEmpty(pass) || pass.Length < 4)
            {
                MessageBox.Show("Heslo musí mít alespoň 4 znaky.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = manager.AddUser(name, pass);

            if (!ok)
            {
                MessageBox.Show($"Uživatel '{name}' již existuje.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RefreshList();
            MessageBox.Show($"Uživatel '{name}' byl přidán.", "Hotovo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedItem is not User u)
            {
                MessageBox.Show("Vyberte uživatele.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (u.IsAdmin())
            {
                MessageBox.Show("Admina nelze smazat.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult res = MessageBox.Show(
                $"Opravdu smazat uživatele '{u.Username}'?", "Potvrzení",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res != DialogResult.Yes)
                return;

            manager.RemoveUser(u);
            RefreshList();
        }
    }
}
