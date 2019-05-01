using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Encrypter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Variable username yang akan mengambil
        /// value dari form Login
        /// </summary>
        public string usernames = string.Empty;

        /// <summary>
        /// Pengecekan username akun yang login
        /// adalah admin.
        /// </summary>
        string adminUsername = "admin";

        private void Main_Load(object sender, EventArgs e)
        {
            /* Membuka form login ketika
            program pertama kali dibuka */
            Login login = new Login();
            login.ShowDialog();

            /* Mengambil value username
            dari form Login dan akan ditampilkan
            pada label selamat datang */
            usernames = login.txtUsername.Text;

            /* Jika username yang login sama
            dengan variable adminUsername,
            maka akan membuka Admin Form ketika
            seusai login.
            Namun jika bukan, ia akan membuka
            halaman utama */
            if (usernames == adminUsername)
            {
                Admin _admin = new Admin();
                _admin.ShowDialog();
            }
            lblUsername.Text = usernames;

            /* Pencegahan terjadinya bug saat
            close form tetapi belum login */
            if (usernames == string.Empty)
            {
                this.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
