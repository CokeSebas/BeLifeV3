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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_agregar_cliente_Click(object sender, RoutedEventArgs e)
        {
            addCliente addCliente = new addCliente();
            addCliente.Owner = this;
            addCliente.ShowDialog();
        }

        private void btn_agregar_contrato_Click(object sender, RoutedEventArgs e)
        {
            addContrato addContrato = new addContrato();
            addContrato.Owner = this;
            addContrato.ShowDialog();
        }
    }
}
