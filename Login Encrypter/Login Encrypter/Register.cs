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
    public partial class Register : Form
    {

        /// <summary>
        /// Variable koneksi database
        /// </summary>
        string connectionString;
        public Register()
        {
            InitializeComponent();
            /* Membuat relasi koneksi pada database */
            connectionString = @"Data Source = Account.db; Version = 3";
        }

        /// <summary>
        /// Proses execute button untuk registrasi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {

            /* Memberikan pendapat apakah user ingin
            melanjutkan daftar atau tidak */
            DialogResult _dialog = MessageBox.Show("Anda yakin ingin mendaftar?", "Register", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (_dialog == DialogResult.Yes)
            {

                /* Jika semua bagian belum diisi, maka 
                pendaftaran tidak bisa dilakukan */
                if (txtUsername.Text != string.Empty
                    && txtPassword.Text != string.Empty
                    && txtConfirmPassword.Text != string.Empty
                    && txtEmail.Text != string.Empty)
                {

                    /* Pengecekan password, jika tidak sama maka
                    proses register batal */
                    if (txtPassword.Text == txtConfirmPassword.Text)
                    {
                        /* Pengecekan duplikasi username */
                        checkSameUsername(txtUsername.Text);
                  
                    }
                    else
                    {
                        MessageBox.Show("Password tidak sama!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Silahkan isi semua bagian!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        /// <summary>
        /// Proses pengecekan duplikasi username
        /// pada database. Proses register akan gagal jika
        /// terdapat username yang telah sama dan sudah
        /// pernah terdaftar sebelumnya.
        /// </summary>
        /// <param name="username"></param>
        private void checkSameUsername(string username)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM Akun WHERE Username = '" + username + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }

                    /* Jika username yang terbaca lebih dari 1, maka 
                    proses register batal */
                    if (count > 0)
                    {
                        MessageBox.Show("Username sudah ada!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtConfirmPassword.Text = string.Empty;
                        txtEmail.Text = string.Empty;
                    }
                    else if (count < 1)
                    {
                        /* Melakukan proses enkripsi password menjadi Sandi Yurani Tistripnol */
                        Encrypter(txtPassword.Text);
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Proses Enkripsi data password
        /// menjadi sebuah sandi Yurani Tistripnol.
        /// </summary>
        /// <param name="password"></param>
        private void Encrypter(string password)
        {
            try
            {
                /* Proses enkripsi password menjadi sandi kode */
                password = password.Replace("0", "-*- ").Replace("1", "*.- ").Replace("2", "-+* ").Replace("3", "--* ").Replace("4", "+.-* ").Replace("5", "++- ").Replace("6", "*--++ ").Replace("7", "*.-.* ").Replace("8", "***- ").Replace("9", "+**- ").Replace("a", "-0 ").Replace("b", "00-- ").Replace("c", "0.00- ").Replace("d", "-- ").Replace("e", "0.0 ").Replace("f", "0.0-.0 ").Replace("g", "0.0.- ").Replace("h", ".00-..0 ").Replace("i", "..- ").Replace("j", ".0- ").Replace("k", "0-.- ").Replace("l", "-..00. ").Replace("m", "000 ").Replace("n", "00..0.00 ").Replace("o", ".--0-. ").Replace("p", "00.-00 ").Replace("q", ".--0.- ").Replace("r", "0.. ").Replace("s", ".00.- ").Replace("t", ".--.0.- ").Replace("u", ".0.-.0 ").Replace("v", "0..0-. ").Replace("w", "00-.0 ").Replace("x", ".0.-.0-. ").Replace("y", "00--.. ").Replace("z", "---.0. ").Replace("A", "-0 ").Replace("B", "00-- ").Replace("C", "0.00- ").Replace("D", "-- ").Replace("E", "0.0 ").Replace("F", "0.0-.0 ").Replace("G", "0.0.- ").Replace("H", ".00-..0 ").Replace("I", "..- ").Replace("J", ".0- ").Replace("K", "0-.- ").Replace("L", "-..00. ").Replace("M", "000 ").Replace("N", "00..0.00 ").Replace("O", ".--0-. ").Replace("P", "00.-00 ").Replace("Q", ".--0.- ").Replace("R", "0.. ").Replace("S", ".00.- ").Replace("T", ".--.0.- ").Replace("U", ".0.-.0 ").Replace("V", "0..0-. ").Replace("W", "00-.0 ").Replace("X", ".0.-.0-. ").Replace("Y", "00--.. ").Replace("Z", "---.0. ");
                /* Melakukan penginputan data ke database */
                insertData(txtUsername.Text, password, txtEmail.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Proses penginputan data hasil register ke sistem database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        private void insertData(string username, string password, string email)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO Akun (Username, Password, Email) VALUES(@username, @password, @email)";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@username", username));
                    cmd.Parameters.Add(new SQLiteParameter("@password", password));
                    cmd.Parameters.Add(new SQLiteParameter("@email", email));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Proses pendaftaran sukses!", "Sukses Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
