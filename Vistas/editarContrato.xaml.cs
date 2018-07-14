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
using Clases;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Threading;


namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para editarContrato.xaml
    /// </summary>
    public partial class editarContrato : MetroWindow
    {
        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();
        public Contrato objCont;
        public Vehiculo objVehi;
        public Vivienda objViv;
        public Salud objSalud;
        private int _edad;
        private int _estadoC;
        private int _sexo;
        private double _primaBase;

        public int Edad
        {
            get
            {
                return _edad;
            }

            set
            {
                _edad = value;
            }
        }

        public int EstadoC
        {
            get
            {
                return _estadoC;
            }

            set
            {
                _estadoC = value;
            }
        }

        public int Sexo
        {
            get
            {
                return _sexo;
            }

            set
            {
                _sexo = value;
            }
        }

        public double PrimaBase
        {
            get
            {
                return _primaBase;
            }

            set
            {
                _primaBase = value;
            }
        }

        public editarContrato()
        {
            InitializeComponent();

            listCombo();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public editarContrato(string numero, string rut, string plan, string salud, string primaAn, string primaMen, string obs)
        {
            InitializeComponent();
            listCombo();

            cbbRutCli.SelectedValue = rut;
            cbbListaContrato.SelectedValue = numero;
            dataCliente();
            buscarContrato();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*ctr = 1;
            PlaySlideShow(ctr);*/
        }

        private void PlaySlideShow(int ctr)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            string filename = ((ctr < 10) ? "images/contratos/contr0" + ctr + ".jpg" : "images/contratos/contr" + ctr + ".jpg");
            image.UriSource = new Uri(filename, UriKind.Relative);
            image.EndInit();
            //myImage.Source = image;
            //myImage.Stretch = Stretch.Uniform;

        }

        public void listCombo()
        {
            string[] listRutC = conec.listRutContrato();
            cbbRutCli.SelectedIndex = 0;
            cbbRutCli.Items.Add("Seleccione");
            for (int z = 0; z < listRutC.Length; z++)
            {
                cbbRutCli.Items.Add(listRutC[z]);
            }

            string[] listTipCont = objSelec.listTipoCont();
            cbbTipoCont.SelectedIndex = 0;
            cbbTipoCont.Items.Add("Seleccione");
            for (int x = 0; x < listTipCont.Length; x++)
            {
                cbbTipoCont.Items.Add(listTipCont[x]);
            }

            string[] listMarca = objSelec.listMarca();
            cbbMarca.SelectedIndex = 0;
            cbbMarca.Items.Add("Seleccione");
            for (int x = 0; x < listMarca.Length; x++)
            {
                cbbMarca.Items.Add(listMarca[x]);
            }

            string[] listRegion = objSelec.listRegion();
            cbbRegion.SelectedIndex = 0;
            cbbRegion.Items.Add("Seleccione");
            for (int x = 0; x < listRegion.Length; x++)
            {
                cbbRegion.Items.Add(listRegion[x]);
            }

            cbbSalud.SelectedIndex = 0;
            cbbSalud.Items.Add("Seleccione");
            cbbSalud.Items.Add("No");
            cbbSalud.Items.Add("Si");
        }

        public void buscarContrato()
        {
            string rut = cbbRutCli.SelectedItem.ToString();
            bool itemsPlan = cbbListaContrato.HasItems;

            if (itemsPlan == true)
            {
                string numero = cbbListaContrato.SelectedItem.ToString();
                string[] datos, datosVeh, datosViv;

                datos = conec.getDatosContrato(numero, rut);

                if (int.Parse(datos[8]) == 10)
                {
                    if (datos[0] == "VID01")
                    {
                        cbbPlan.SelectedIndex = 1;
                    }
                    else if (datos[0] == "VID02")
                    {
                        cbbPlan.SelectedIndex = 2;
                    }
                    else if (datos[0] == "VID03")
                    {
                        cbbPlan.SelectedIndex = 3;
                    }
                }
                else if (int.Parse(datos[8]) == 20)
                {
                    if (datos[0] == "VEH01")
                    {
                        cbbPlan.SelectedIndex = 1;
                    }
                    else if (datos[0] == "VEH02")
                    {
                        cbbPlan.SelectedIndex = 2;
                    }
                    else if (datos[0] == "VEH03")
                    {
                        cbbPlan.SelectedIndex = 3;
                    }
                }
                else if (int.Parse(datos[8]) == 30)
                {
                    if (datos[0] == "HOG01")
                    {
                        cbbPlan.SelectedIndex = 1;
                    }
                    else if (datos[0] == "HOG02")
                    {
                        cbbPlan.SelectedIndex = 2;
                    }
                    else if (datos[0] == "HOG03")
                    {
                        cbbPlan.SelectedIndex = 3;
                    }
                }

                if (datos[4] == "true")
                {
                    cbbSalud.SelectedIndex = 1;
                }
                else
                {
                    cbbSalud.SelectedIndex = 2;
                }

                txtPrimaAnu.Text = datos[5];
                txtPrimaMen.Text = datos[6];
                txtObsv.Text = datos[7];
                cbbTipoCont.SelectedIndex = int.Parse(datos[8]) / 10;
                if (int.Parse(datos[8]) == 20)
                {
                    datosVeh = conec.getDatosContratoVeh(numero);
                    txtPatente.Text = datosVeh[0];
                    cbbMarca.SelectedIndex = int.Parse(datosVeh[1]);
                    cbbModelo.SelectedItem = datosVeh[2];
                    //cbbModelo.SelectedIndex = int.Parse(datosVeh[2]);
                    txtAnioVe.Text = datosVeh[3];

                }
                else if (int.Parse(datos[8]) == 30)
                {
                    datosViv = conec.getDatosContratoViv(numero);
                    txtCodigoPost.Text = datosViv[0];
                    txtAnioVi.Text = datosViv[1];
                    txtDirec.Text = datosViv[2];
                    txtValorIn.Text = datosViv[3];
                    txtValorCont.Text = datosViv[4];
                    cbbRegion.SelectedIndex = int.Parse(datosViv[5]);
                    cbbComuna.SelectedItem = datosViv[6];
                }
                activarOpciones();
            }   
        }

        public void activarOpciones()
        {
            btnEditarCont.IsEnabled = true;
            //btnTerminarContrato.IsEnabled = true;
            cbbPlan.IsEnabled = true;
            cbbSalud.IsEnabled = true;
            txtObsv.IsEnabled = true;
        }

        public void desactivarOpciones()
        {
            btnEditarCont.IsEnabled = false;
            //btnTerminarContrato.IsEnabled = false;
            cbbListaContrato.IsEnabled = false;
            cbbPlan.IsEnabled = false;
            //dtpFechaInicio.IsEnabled = false;
            cbbSalud.IsEnabled = false;
            txtObsv.IsEnabled = false;
        }

        public void tipoContrato()
        {

            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10){
                idTipoCont = 30;
            }else if (idTipoCont ==30){
                idTipoCont = 10;
            }*/
            string[] listPlan = conec.listPlan(idTipoCont);
            cbbPlan.Items.Clear();
            //cbbPlan.Items.Add("Seleccione");
            //cbbPlan.SelectedIndex = 0;

            for (int x = 0; x < listPlan.Length; x++)
            {
                cbbPlan.Items.Add(listPlan[x]);
            }

            if (cbbTipoCont.SelectedIndex == 2)
            {
                cnvAuto.Visibility = Visibility.Visible;
                cnvHogar.Visibility = Visibility.Hidden;
                cnvDatos.Margin = new Thickness(10, 352, 0, 0);
                lblSalud.Visibility = Visibility.Hidden;
                cbbSalud.Visibility = Visibility.Hidden;
            }
            else if (cbbTipoCont.SelectedIndex == 3)
            {
                cnvAuto.Visibility = Visibility.Hidden;
                cnvHogar.Visibility = Visibility.Visible;
                cnvHogar.Margin = new Thickness(10, 156, 0, 0);
                cnvDatos.Margin = new Thickness(10, 392, 0, 0);
                lblSalud.Visibility = Visibility.Hidden;
                cbbSalud.Visibility = Visibility.Hidden;
            }
            else if (cbbTipoCont.SelectedIndex == 1)
            {
                cnvAuto.Visibility = Visibility.Hidden;
                cnvHogar.Visibility = Visibility.Hidden;
                cnvDatos.Margin = new Thickness(10, 156, 0, 0);
                lblSalud.Visibility = Visibility.Visible;
                cbbSalud.Visibility = Visibility.Visible;
                MessageBox.Show("Recordar que Declaración de salud es Obligatoria", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void limpiar()
        {
            txtNombreCliCon.Clear();
            //txtRutCont.Clear();
            cbbPlan.SelectedIndex = 0;
            txtPoliza.Clear();
            dtpFechaInicio.DisplayDate = DateTime.Today;
            dtpFechaInicio.SelectedDate = DateTime.Today;
            cbbSalud.SelectedIndex = 0;
            txtPrimaMen.Clear();
            txtPrimaAnu.Clear();
            txtObsv.Clear();
        }

        //funcion se agrega en el onchange del cbbPlan
        public void calcularPlan()
        {
            bool itemsPlan = cbbPlan.HasItems;

            if (itemsPlan == true)
            {
                string plan = cbbPlan.SelectedItem.ToString();
                string[] datosPoliza = conec.datosPoliza(plan);
                txtPoliza.Text = datosPoliza[0];
                PrimaBase = Convert.ToDouble(datosPoliza[1]);
                int idTipoCont = cbbTipoCont.SelectedIndex;

                double total, recargoEdad = 0, recargoSexo = 0, recargoEstadoC = 0, recargoBase = 0,
                    recargoAnioVeh = 0, recargoAnioHog = 0, recargoReg = 0;

                if (idTipoCont == 1)
                {
                    if (Edad < 18 && Edad > 25)
                    {
                        recargoEdad = 3.6;
                    }
                    else if (Edad < 26 && Edad > 45)
                    {
                        recargoEdad = 2.4;
                    }
                    else if (Edad > 45)
                    {
                        recargoEdad = 6.0;
                    }

                    if (Sexo == 1)
                    {
                        recargoSexo = 2.4;
                    }
                    else if (Sexo == 2)
                    {
                        recargoSexo = 1.2;
                    }

                    if (EstadoC == 1)
                    {
                        recargoEstadoC = 4.8;
                    }
                    else if (EstadoC == 2)
                    {
                        recargoEstadoC = 2.4;
                    }
                    else if (EstadoC == 3 || EstadoC == 4)
                    {
                        recargoEstadoC = 3.6;
                    }
                }
                else if (idTipoCont == 2)
                {
                    if (Edad < 18 && Edad > 25)
                    {
                        recargoEdad = 3.6;
                    }
                    else if (Edad < 26 && Edad > 45)
                    {
                        recargoEdad = 2.4;
                    }
                    else if (Edad > 45)
                    {
                        recargoEdad = 6.0;
                    }

                    if (Sexo == 1)
                    {
                        recargoSexo = 2.4;
                    }
                    else if (Sexo == 2)
                    {
                        recargoSexo = 1.2;
                    }

                    int anioVehi = int.Parse(txtAnioVe.Text);
                    if (anioVehi == DateTime.Today.Year)
                    {
                        recargoAnioVeh = 1.2;
                    }
                    else if (anioVehi < (DateTime.Today.Year - 5))
                    {
                        recargoAnioVeh = 0.8;
                    }
                    else if (anioVehi > (DateTime.Today.Year - 5))
                    {
                        recargoAnioVeh = 0.4;
                    }
                }
                else if (idTipoCont == 3)
                {
                    if (Edad < 18 && Edad > 25)
                    {
                        recargoEdad = 3.6;
                    }
                    else if (Edad < 26 && Edad > 45)
                    {
                        recargoEdad = 2.4;
                    }
                    else if (Edad > 45)
                    {
                        recargoEdad = 6.0;
                    }

                    if (Sexo == 1)
                    {
                        recargoSexo = 2.4;
                    }
                    else if (Sexo == 2)
                    {
                        recargoSexo = 1.2;
                    }

                    int anioViv = int.Parse(txtAnioVi.Text);
                    if (anioViv < 5 && anioViv > 0)
                    {
                        recargoAnioHog = 1.0;
                    }
                    else if (anioViv < 10 && anioViv > 5)
                    {
                        recargoAnioHog = 0.8;
                    }
                    else if (anioViv < 30 && anioViv > 10)
                    {
                        recargoAnioHog = 0.4;
                    }
                    else if (anioViv > 30)
                    {
                        recargoAnioHog = 0.2;
                    }

                    int idRegion = cbbRegion.SelectedIndex;
                    if (idRegion == 13)
                    {
                        recargoReg = 3.2;
                    }
                    else
                    {
                        recargoReg = 2.8;
                    }
                }

                recargoBase = PrimaBase;
                total = recargoBase + recargoEdad + recargoSexo + recargoEstadoC + recargoAnioVeh + recargoAnioHog + recargoReg;
                txtPrimaAnu.Text = total.ToString();
                txtPrimaMen.Text = Math.Round((total / 12), 2).ToString();
            }
        }
        
        public async Task editarContAsync()
        {
            bool edita = false;

            string plan = cbbPlan.SelectedValue.ToString();
            string salud = cbbSalud.SelectedValue.ToString();
            string numero = cbbListaContrato.SelectedValue.ToString();
            if (salud == "Si")
            {
                salud = "1";
            }
            else
            {
                salud = "0";
            }

            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;

            /*objCont.CodigoPlan = plan;
            objCont.DeclaracionSalud = salud;
            objCont.PrimaAnual = primaAnu;
            objCont.PrimaMensual = primaMen;
            objCont.Observaciones = observacion;
            objCont.NumeroContrato = numero;
            edita = objCont.editarContrato();
            if (edita == true)
            {
                await this.ShowMessageAsync("Confirmacion!", "Contrato Modificado");
                limpiar();
            }
            else
            {
                await this.ShowMessageAsync("Error!", "Contrato no se pudo modificafr");
            }*/
        }

        public async Task terminarContAsync()
        {
            bool edita = false;
            /*objCont.Vigente = "0";
            objCont.NumeroContrato = cbbListaContrato.SelectedValue.ToString();
            edita = objCont.terminarContrato();
            if (edita == true)
            {
                await this.ShowMessageAsync("Confirmación!", "Contrato Terminado");
                limpiar();
                desactivarOpciones();
                txtNombreCliCon.Clear();
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Contrato no se puede Terminar");
            }*/
        }

        public void dataCliente()
        {
            cbbListaContrato.Items.Clear();
            string rut = cbbRutCli.SelectedValue.ToString();
            string[] listCont = conec.listContratos(rut);
            cbbListaContrato.SelectedIndex = 0;
            //cbbRutCli.Items.Add("Seleccione");
            for (int i = 0; i < listCont.Length; i++)
            {
                cbbListaContrato.Items.Add(listCont[i]);
            }
            string[] datos = conec.getDatosCliente(rut);
            txtNombreCliCon.Text = datos[0] + " " + datos[1];
        }

        //funciona
        public async void editarVida()
        {
            //try{
            bool guarda = false;
            string rutCliente = cbbRutCli.SelectedItem.ToString();

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = cbbListaContrato.SelectedItem.ToString();

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10)
            {
                idTipoCont = 30;
            }
            else if (idTipoCont == 30)
            {
                idTipoCont = 10;
            }*/

            objSalud = new Salud();
            objSalud.RutCliente = rutCliente;
            objSalud.NumeroContrato = numero;
            objSalud.CodigoPlan = plan;
            objSalud.FechaInicioVigencia = fechaVigencia;
            objSalud.FechaFinVigencia = fechaFinVigencia;
            objSalud.DeclaracionSalud = salud;
            objSalud.PrimaAnual = primaAnu;
            objSalud.PrimaMensual = primaMen;
            objSalud.Vigente = "1";
            objSalud.Observaciones = observacion;
            objSalud.TipoContrato = idTipoCont;


            //DateTime fecha_iniv = Convert.ToDateTime(value);
            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            //if (result >= 0){
            if (mesV < 1)
            {
                guarda = objSalud.editarContrato();
                if (guarda == true)
                {

                    MessageBox.Show("Contrato de Salud Modificado", "Confirmacion!", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Contrato Ya ha Ingresado", "Advertencia!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        public async void editarVehiculo()
        {
            //try{
            bool guarda = false;
            string rutCliente = cbbRutCli.SelectedItem.ToString();

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = cbbListaContrato.SelectedItem.ToString();

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            /*if (idTipoCont == 10)
            {
                idTipoCont = 30;
            }
            else if (idTipoCont == 30)
            {
                idTipoCont = 10;
            }*/

            objVehi = new Vehiculo();
            objVehi.RutCliente = rutCliente;
            objVehi.NumeroContrato = numero;
            objVehi.CodigoPlan = plan;
            objVehi.FechaInicioVigencia = fechaVigencia;
            objVehi.FechaFinVigencia = fechaFinVigencia;
            //objVehi.DeclaracionSalud = salud;
            objVehi.PrimaAnual = primaAnu;
            objVehi.PrimaMensual = primaMen;
            objVehi.Vigente = "1";
            objVehi.Observaciones = observacion;
            objVehi.TipoContrato = idTipoCont;

            string patente = txtPatente.Text;
            string idMarca = cbbMarca.SelectedIndex.ToString();
            string idModelo = cbbModelo.SelectedItem.ToString();
            string anioVeh = txtAnioVe.Text;
            objVehi.Patente = patente;
            objVehi.Marca = int.Parse(idMarca);
            objVehi.Modelo = idModelo;
            objVehi.Anio = int.Parse(anioVeh);

            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            if (mesV < 1)
            {
                guarda = objVehi.editarContrato();
                if (guarda == true)
                {
                    await this.ShowMessageAsync("Confirmación!", "Contrato de Vehículo Editado");
                    limpiar();
                }
                else
                {
                    await this.ShowMessageAsync("Advertencia!", "Contrato ya se ingresó");
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        public async void editarHogar()
        {
            //try{
            bool guarda = false;
            string rutCliente = cbbRutCli.SelectedItem.ToString();

            DateTime localDate = DateTime.Now;
            string mes = "", dia = "", minu = "", seg = "";
            string anio = localDate.Year.ToString();
            mes = localDate.Month.ToString();
            dia = localDate.Day.ToString();
            string hora = localDate.Hour.ToString();
            minu = localDate.Minute.ToString();
            seg = localDate.Second.ToString();

            if (mes.Length == 1)
            {
                mes = "0" + mes;
            }
            if (dia.Length == 1)
            {
                dia = "0" + dia;
            }
            if (minu.Length == 1)
            {
                minu = "0" + minu;
            }
            if (seg.Length == 1)
            {
                seg = "0" + seg;
            }
            string numero = cbbListaContrato.SelectedItem.ToString();

            string plan = cbbPlan.SelectedValue.ToString();
            DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
            DateTime fechaFinVig = fechaIniVig.AddYears(1);
            string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
            string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
            string salud = cbbSalud.SelectedValue.ToString();

            if (salud == "Si")
            {
                salud = "1";
            }
            else if (salud == "No")
            {
                salud = "0";
            }
            else
            {
                salud = "";
            }
            string primaAnu = txtPrimaAnu.Text;
            string primaMen = txtPrimaMen.Text;
            string observacion = txtObsv.Text;
            int idTipoCont = cbbTipoCont.SelectedIndex * 10;
            objViv = new Vivienda();
            objViv.RutCliente = rutCliente;
            objViv.NumeroContrato = numero;
            objViv.CodigoPlan = plan;
            objViv.FechaInicioVigencia = fechaVigencia;
            objViv.FechaFinVigencia = fechaFinVigencia;
            //objViv.DeclaracionSalud = salud;
            objViv.PrimaAnual = primaAnu;
            objViv.PrimaMensual = primaMen;
            objViv.Vigente = "1";
            objViv.Observaciones = observacion;
            objViv.TipoContrato = idTipoCont;

            string codigoPost = txtCodigoPost.Text;
            string anioVi = txtAnioVi.Text;
            string direc = txtDirec.Text;
            string valorInm = txtValorIn.Text;
            string valorCont = txtValorCont.Text;
            int idRegion = cbbRegion.SelectedIndex;
            string idComuna = cbbComuna.SelectedItem.ToString();
            objViv.CodigoPostal = codigoPost;
            objViv.Anio = int.Parse(anioVi);
            objViv.Direccion = direc;
            objViv.ValorInmu = int.Parse(valorInm);
            objViv.ValorConte = int.Parse(valorCont);
            objViv.Region = idRegion;
            objViv.Comuna = idComuna;

            int result = DateTime.Compare(fechaIniVig, DateTime.Today);
            int mesV = fechaIniVig.Month - DateTime.Today.Month;
            if (mesV < 1)
            {
                guarda = objViv.editarContrato();
                if (guarda == true)
                {
                    await this.ShowMessageAsync("Confirmación!", "Contrato de Vivienda Editado");
                    limpiar();
                }
                else
                {
                    await this.ShowMessageAsync("Advertencia!", "Contrato ya se ha ingresado");
                }
            }
            else
            {
                await this.ShowMessageAsync("Advertencia!", "Mes de inicio de vigencia no puede ser superior a un mes");
            }
            /*}
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }

        private void cbbPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calcularPlan();
        }

        private void cbbListaContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbbSalud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbbRutCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataCliente();
        }

        private void txtNombreCliCon_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPoliza_TextChanged(object sender, TextChangedEventArgs e)
        {

        }  

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnBuscarCto_Click(object sender, RoutedEventArgs e)
        {
            buscarContrato();
        }

        private void btnEditarCont_Click(object sender, RoutedEventArgs e)
        {
            editarContAsync();
        }

        private void btnTerminarContrato_Click(object sender, RoutedEventArgs e)
        {
            terminarContAsync();
        }


        private void txtCodigoPost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtValorIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtValorCont_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtAnioVe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtAnioVi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void cbbListaContrato_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            buscarContrato();
        }

        private void cbbTipoCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipoContrato();
        }

        private void cbbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbModelo.Items.Clear();
            int idMarca = cbbMarca.SelectedIndex;
            if (idMarca != 0)
            {
                string[] listMarca = conec.listModelo(idMarca);
                cbbModelo.SelectedIndex = 0;
                cbbModelo.Items.Add("Seleccione");
                for (int x = 0; x < listMarca.Length; x++)
                {
                    cbbModelo.Items.Add(listMarca[x]);
                }
            }
        }

        private void cbbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbbComuna.Items.Clear();
            string[] listComuna = conec.listComuna(cbbRegion.SelectedIndex);
            cbbComuna.SelectedIndex = 0;
            cbbComuna.Items.Add("Seleccione");
            for (int x = 0; x < listComuna.Length; x++)
            {
                cbbComuna.Items.Add(listComuna[x]);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (cbbTipoCont.SelectedIndex == 2)
            {
                editarVehiculo();
            }
            else if (cbbTipoCont.SelectedIndex == 3)
            {
                editarHogar();
            }
            else if (cbbTipoCont.SelectedIndex == 1)
            {
                editarVida();
            }
        }
    }
}
