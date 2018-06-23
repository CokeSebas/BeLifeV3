using Clases;
using MahApps.Metro.Controls;
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

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para addContrato.xaml
    /// </summary>
    public partial class addContrato : MetroWindow
    {
        public Conexion conec = new Conexion();
        public Cliente objCli = new Cliente();
        public Selecciones objSelec = new Selecciones();
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

        public addContrato()
        {
            InitializeComponent();
            listCombo();
            dtpFechaInicio.SelectedDate = DateTime.Today;
        }

        public void listCombo()
        {
            string[] listRut = conec.listRut();

            //string[] listPlan = conec.listPlan();
            /*cbbPlan.SelectedIndex = 0;
            cbbPlan.Items.Add("Seleccione");
            for (int x = 0; x < listPlan.Length; x++)
            {
                cbbPlan.Items.Add(listPlan[x]);
            }*/

            cbbSalud.SelectedIndex = 0;
            cbbSalud.Items.Add("Seleccione");
            cbbSalud.Items.Add("No");
            cbbSalud.Items.Add("Si");
        }
    }
}
