using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Login_Encrypter
{
    public partial class Login : Form
    {

        /// <summary>
        /// Variable koneksi database
        /// </summary>
        string connectionString;
        public Login()
        {
            InitializeComponent();

            /* Membuat relasi koneksi pada database */
            connectionString = @"Data Source = Account.db; Version = 3";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Proses button click login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {

            /* Semua bagian harus diisi untuk 
            melakukan proses login */
            if (txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty)
            {
                /* Melakukan proses enkripsi password menjadi Sandi Yurani Tistripnol */
                Encrypter(txtPassword.Text);
            }
            else
            {
                MessageBox.Show("Silahkan masukkan username dan password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Proses Enkripsi data password
        /// menjadi sebuah sandi Yurani Tistripnol.
        /// </summary>
        /// <param name="password"></param>
        private void Encrypter(string password)
        {
            /* Proses enkripsi password menjadi sandi kode */
            password = password.Replace("0", "-*- ").Replace("1", "*.- ").Replace("2", "-+* ").Replace("3", "--* ").Replace("4", "+.-* ").Replace("5", "++- ").Replace("6", "*--++ ").Replace("7", "*.-.* ").Replace("8", "***- ").Replace("9", "+**- ").Replace("a", "-0 ").Replace("b", "00-- ").Replace("c", "0.00- ").Replace("d", "-- ").Replace("e", "0.0 ").Replace("f", "0.0-.0 ").Replace("g", "0.0.- ").Replace("h", ".00-..0 ").Replace("i", "..- ").Replace("j", ".0- ").Replace("k", "0-.- ").Replace("l", "-..00. ").Replace("m", "000 ").Replace("n", "00..0.00 ").Replace("o", ".--0-. ").Replace("p", "00.-00 ").Replace("q", ".--0.- ").Replace("r", "0.. ").Replace("s", ".00.- ").Replace("t", ".--.0.- ").Replace("u", ".0.-.0 ").Replace("v", "0..0-. ").Replace("w", "00-.0 ").Replace("x", ".0.-.0-. ").Replace("y", "00--.. ").Replace("z", "---.0. ").Replace("A", "-0 ").Replace("B", "00-- ").Replace("C", "0.00- ").Replace("D", "-- ").Replace("E", "0.0 ").Replace("F", "0.0-.0 ").Replace("G", "0.0.- ").Replace("H", ".00-..0 ").Replace("I", "..- ").Replace("J", ".0- ").Replace("K", "0-.- ").Replace("L", "-..00. ").Replace("M", "000 ").Replace("N", "00..0.00 ").Replace("O", ".--0-. ").Replace("P", "00.-00 ").Replace("Q", ".--0.- ").Replace("R", "0.. ").Replace("S", ".00.- ").Replace("T", ".--.0.- ").Replace("U", ".0.-.0 ").Replace("V", "0..0-. ").Replace("W", "00-.0 ").Replace("X", ".0.-.0-. ").Replace("Y", "00--.. ").Replace("Z", "---.0. ");
            /* Melaksanakan method login */
            loginSystem(txtUsername.Text, password);
        }

        /// <summary>
        /// Proses login ke dalam sistem.
        /// Dan akan melakukan pengecekan valid username dan password
        /// terlebih dahulu. Jika memang username dan password benar,
        /// maka akan sukses login. Begitupun sebaliknya.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void loginSystem(string username, string password)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM Akun WHERE Username = '" + username + "' AND Password = '" + password + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("Berhasil Login!", "Sukses Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else if (count < 1)
                    {
                        MessageBox.Show("Username atau password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Membuka form register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register _register = new Register();
            _register.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// Mengaktifkan fungsi enter untuk
        /// mengakses button login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
 
        }

        /// <summary>
        /// Mengaktifkan fungsi enter untuk
        /// mengakses button login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(this, new EventArgs());
            }
        }
    }
}
