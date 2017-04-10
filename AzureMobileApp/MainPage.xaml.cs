using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using System.Diagnostics;

namespace AzureMobileApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IMobileServiceTable<UsersUPT> userTableObj = App.MobileService.GetTable<UsersUPT>();
        public MainPage()
        {
            this.InitializeComponent();
        }
       
        private void btnSave_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                UsersUPT obj = new UsersUPT();
                obj.PID = txtPID.Text;
                obj.nombre = txtNombre.Text;
                obj.edad = txtEdad.Text;
                obj.descripcion = txtDescripcion.Text;
                userTableObj.InsertAsync(obj);
                MessageDialog msgDialog = new MessageDialog("Data Inserted!!!");
                msgDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Error : " + ex.ToString());
                msgDialogError.ShowAsync();
            }
        }
        public SqlConnection conn;
        public void ConnectToSql()
        {
            conn=new SqlConnection ("Data Source=uptjpd.database.windows.net;Initial Catalog=FaceIdRecognition;Integrated Security=False;User ID=jpd21122012;Password=000pipo.182;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                conn.Open();
                Debug.WriteLine("Successfull");
                // Insert code to process data.
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }
        }
        public static string identificator;
        public void ConectTest()
        {
            SqlConnection cnn = new SqlConnection("Data Source=uptjpd.database.windows.net;Initial Catalog=FaceIdRecognition;Integrated Security=False;User ID=jpd21122012;Password=000pipo.182;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                cnn.Open();
                Debug.WriteLine("Entraste a open");
                MessageDialog msgDialog = new MessageDialog("Succesfull!!!");
                msgDialog.ShowAsync();
            }
            catch (Exception ex )
            {
                MessageDialog msgDialog = new MessageDialog("Failed to connect to data source "+ ex);
                msgDialog.ShowAsync();
                Debug.WriteLine("Failed to connect to data source");
            }
            finally
            {
                cnn.Close();
            }
        }
        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<UsersUPT> lista = new List<UsersUPT>();
                UsersUPT u = new UsersUPT();
                string idBuscar = "e058cd1a-e303-4fe6-9a87-b677c8bf00d0";
                lista = await userTableObj.Where(userTableObj=>userTableObj.PID==idBuscar).ToListAsync();
                li_nom.ItemsSource = lista;
                li_nom.DisplayMemberPath = "nombre";
                lista = await userTableObj.Where(userTableObj => userTableObj.PID == idBuscar).ToListAsync();
                li_age.ItemsSource = lista;
                li_age.DisplayMemberPath = "edad";
                lista = await userTableObj.Where(userTableObj => userTableObj.PID == idBuscar).ToListAsync();
                li_descrip.ItemsSource = lista;
                li_descrip.DisplayMemberPath = "descripcion";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: {0}",ex);
            }
        }

        private void btnSave_Click()
        {

        }
    }
}
