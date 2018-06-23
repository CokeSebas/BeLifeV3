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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Actions;
using Clases;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para addCliente.xaml
    /// </summary>
    public partial class addCliente : MetroWindow
    {

        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();


        public addCliente()
        {
            InitializeComponent();
            limpiar();
            LlenarCombo();
            dtpFechaNacCli.SelectedDate = DateTime.Today;
        }
        public void limpiar()
        {
            txtApellidoCli.Clear();
            txtNombreCli.Clear();
            txtRutCli.Clear();
            dtpFechaNacCli.DisplayDate = DateTime.Today;
            cbbSexo.SelectedIndex = 0;
            cbbEstadoCivil.SelectedIndex = 0;
        }

        public void LlenarCombo(){

            string[] listSex = objSelec.listSexo();
            cbbSexo.SelectedIndex = 0;
            cbbSexo.Items.Add("Seleccione");
            for (int i = 0; i < listSex.Length; i++){
                cbbSexo.Items.Add(listSex[i]);
            }

            string[] listEC = objSelec.listEstadoCivil();
            cbbEstadoCivil.SelectedIndex = 0;
            cbbEstadoCivil.Items.Add("Seleccione");
            for (int x = 0; x < listEC.Length; x++){
                cbbEstadoCivil.Items.Add(listEC[x]);
            }
        }

        public async Task guardarClienteAsync(){
            try{
                bool guarda = false;
                string nombre = txtNombreCli.Text;
                string apellido = txtApellidoCli.Text;
                string rut = txtRutCli.Text;
                DateTime fechaC = dtpFechaNacCli.SelectedDate.Value;
                string fecNac = fechaC.Year.ToString() + "-" + fechaC.Month.ToString() + "-" + fechaC.Day.ToString();
                string sexo = cbbSexo.SelectedIndex.ToString();
                string estadoCi = cbbEstadoCivil.SelectedIndex.ToString();
                objCli.Rut = rut;
                objCli.Nombre = nombre;
                objCli.Apellido = apellido;
                objCli.FechaNacimiento = fecNac;
                objCli.EstadoCivil = estadoCi;
                objCli.Sexo = sexo;

                guarda = objCli.guardarCliente();
                if (guarda == true){
                    await this.ShowMessageAsync("Confirmacion!", "Cliente Guardado");
                    limpiar();
                }else{
                    await this.ShowMessageAsync("Advertencia!","Rut ya esta ingresado en la Base de Datos");
                    txtRutCli.Clear();
                }
            }catch (Exception error){
                await this.ShowMessageAsync("Error!", error.Message);
            }
        }

        private void btnLimpiarCli_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
