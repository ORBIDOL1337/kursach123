using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;

namespace Тестирование
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TradeEntities1 tradeEntities = new TradeEntities1();
        DataTable dt = new DataTable();
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
        }

        DataTable dataTable = new DataTable("dataBase");

        public DataTable Select(string selectSQL)
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["WpfApp1.Properties.Settings.TradeConnectionString1"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильно введены данные");
                return null;
            }
        }

        private void checkbox_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox.IsChecked == true)
            {
                tb_password.Text = passwordbox.Password;
                passwordbox.Visibility = Visibility.Collapsed;
                tb_password.Visibility = Visibility.Visible;
            }
            else
            {
                passwordbox.Password = tb_password.Text;
                passwordbox.Visibility = Visibility.Visible;
                tb_password.Visibility = Visibility.Collapsed;
            }
        }

        private void passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            tb_password.Text = passwordbox.Password;
        }

        private void tb_password_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordbox.Password = tb_password.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_login.Text.Length > 0 && (passwordbox.Password.Length > 0 || tb_password.Text.Length > 0))
            {
                try
                {
                    dt = Select("SELECT UserRole FROM [User] WHERE UserLogin = '" + tb_login.Text + "' AND UserPassword = '" + tb_password.Text + "'");
                    string role = dt.Rows[0][0].ToString();
                    MessageBox.Show(role);
                    if (Convert.ToInt32(role) == 1)
                    {
                        AdminWindow admin = new AdminWindow();
                        admin.Show();
                        Hide();
                    }
                    else if (role == "2")
                    {
                        UserAndManagerWindow userAndManager = new UserAndManagerWindow();
                        userAndManager.Show();
                        Hide();
                    }
                    else if (role == "3")
                    {
                        UserAndManagerWindow userAndManager = new UserAndManagerWindow();
                        userAndManager.Show();
                        Hide();
                    }
                    dt.Clear();
                }
                catch
                {
                    MessageBox.Show("Неверно введены данные");
                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void guest_Click(object sender, RoutedEventArgs e)
        {
            UserAndManagerWindow userAndManager = new UserAndManagerWindow();
            userAndManager.Show();
            this.Close();
        }
    }
}
