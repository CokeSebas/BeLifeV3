using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Clases
{
    public class Conexion
    {
        private string cadena = "Server=localhost\\SQLEXPRESS;Database=BeLifeV3;Trusted_Connection=True;";
        public SqlConnection cn;
        private SqlCommandBuilder cmb;
        public DataSet ds = new DataSet();
        public SqlDataAdapter da;
        public SqlCommand comando;
        public SqlDataReader registros;

        private List<Cliente> clientes = new List<Cliente>();
        private List<Contrato> contratos = new List<Contrato>();

        //se conecta con sqlserver
        private void conectar() {
            cn = new SqlConnection(cadena);
        }

        //abre la conexion a la BD
        public void abrirConexion() {
            conectar();
            cn.Open();
        }

        //cierra la conexión a la BD
        public void cerraConexion() {
            cn.Close();
        }

        //consulta datos, no he probado
        public void consultar(string sql, string tabla) {
            //abrirConexion();
            ds.Tables.Clear();
            da = new SqlDataAdapter(sql, cn);
            cmb = new SqlCommandBuilder(da);
            da.Fill(ds, tabla);
        }

        //eliminar datos, Funciona
        public bool eliminar(string tabla, string condicion) {
            abrirConexion();
            string sql = "DELETE FROM " + tabla + " WHERE " + condicion;
            comando = new SqlCommand(sql, cn);
            int i = comando.ExecuteNonQuery();
            cerraConexion();
            if (i > 0) {
                return true;
            } else {
                return false;
            }
        }

        //actualizar datos, Funciona
        public bool actualizar(string tabla, string campos, string condicion) {
            abrirConexion();
            string sql = "UPDATE " + tabla + " SET " + campos + " WHERE " + condicion;
            
            comando = new SqlCommand(sql, cn);
            int i = comando.ExecuteNonQuery();
            cerraConexion();
            if (i > 0) {
                return true;
            } else {
                return false;
            }
        }

        //traer datos, no lo uso
        public DataTable consultar2(string tabla) {
            abrirConexion();
            string sql = "SELECT * FROM " + tabla;
            da = new SqlDataAdapter(sql, cn);
            DataSet dts = new DataSet();
            da.Fill(dts, tabla);
            DataTable dt = new DataTable();
            dt = dts.Tables[tabla];
            return dt;
        }

        //trae datos para llenar combobox,funciona correcto
        public string[] listSelec(string tabla) {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM " + tabla;
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read()) {
                string sql = "";
                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                if (tabla == "Region"){
                    sql = "SELECT NombreRegion FROM " + tabla;
                }else{
                    sql = "SELECT Descripcion FROM " + tabla;
                }
                
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    if (tabla == "Region")
                    {
                        listaSelec[x] = registros["NombreRegion"].ToString();
                    }
                    else
                    {
                        listaSelec[x] = registros["Descripcion"].ToString();
                    }
                    
                    x++;
                }
            }
            return listaSelec;
        }

        public string[] listRut()
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM Cliente;";
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read())
            {

                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT RutCliente FROM Cliente;";
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["RutCliente"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }

        public string[] listRutContrato()
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM Contrato;";
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read())
            {
                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT DISTINCT RutCliente FROM Contrato";
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["RutCliente"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }

        public string[] listContratos(string rut)
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM Contrato WHERE RutCliente = '" + rut + "';";
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read()){
                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT Numero FROM Contrato WHERE RutCliente = '" + rut + "';";
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read()){
                    listaSelec[x] = registros["Numero"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }


        public string[] listModelo(int idModelo)
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) AS count FROM ModeloVehiculo mv " +
                          "JOIN MarcaModeloVehiculo mdv ON (mv.IdModelo = mdv.IdModelo) " +
                          "WHERE mdv.IdMarca = " + idModelo;
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read())
            {

                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT mv.Descripcion FROM ModeloVehiculo mv " +
                             "JOIN MarcaModeloVehiculo mdv ON (mv.IdModelo = mdv.IdModelo) " +
                             "WHERE mdv.IdMarca = " + idModelo;
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["Descripcion"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }

        public string[] listPlan(int tipoCont) {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM [Plan] WHERE IdTipoContrato = "+tipoCont;
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read()){
                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT Nombre FROM [Plan] WHERE IdTipoContrato = " + tipoCont;
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["Nombre"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }


        public string[] listComuna(int idRegion)
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) AS count FROM Comuna c JOIN RegionComuna rg ON (c.idComuna = rg.idComuna) WHERE rg.idRegion ="+idRegion;
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read()){

                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT NombreComuna FROM Comuna c JOIN RegionComuna rg ON (c.idComuna = rg.idComuna) WHERE rg.idRegion =" + idRegion;
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read()){
                    listaSelec[x] = registros["NombreComuna"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }


        public string[] getlistPoliza(){
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM [Plan]";
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read())
            {

                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT PolizaActual FROM [Plan]";
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["PolizaActual"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }

        public string[] getListCont()
        {
            string[] listaSelec;
            int x = 0;
            listaSelec = new String[1];
            abrirConexion();

            string sql1 = "SELECT COUNT(*) as count FROM Contrato";
            comando = new SqlCommand(sql1, cn);
            registros = comando.ExecuteReader();

            if (registros.Read())
            {
                listaSelec = new string[int.Parse(registros["count"].ToString())];
                cerraConexion();
                string sql = "SELECT Numero FROM Contrato";
                abrirConexion();
                comando = new SqlCommand(sql, cn);
                registros = comando.ExecuteReader();

                while (registros.Read())
                {
                    listaSelec[x] = registros["Numero"].ToString();
                    x++;
                }
            }
            return listaSelec;
        }

        //validar existe, funciona correcto
        public bool validar(string tabla, string condicion) {
            abrirConexion();
            string sql = "SELECT COUNT(*) AS count FROM " + tabla + " WHERE " + condicion;
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            if (registros.Read()) {
                if (int.Parse(registros["count"].ToString()) == 0) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
            //retorna true si el dato no esta ingresado.
            //retorna false si el dato ya esta ingresado.
        }

        //insertar, funciona correcto
        public bool insertar(string sql) {
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            int i = comando.ExecuteNonQuery();
            cerraConexion();
            if (i > 0) {
                return true;
            } else {
                return false;
            }
        }

        public string[] getDatosCliente(string rut) {
            string[] datos = new string[5];

            string sql = "SELECT * FROM cliente WHERE rutCliente = '" + rut + "';";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            while (registros.Read()) {

                datos[0] = registros["Nombres"].ToString();
                datos[1] = registros["Apellidos"].ToString();
                datos[2] = registros["FechaNacimiento"].ToString();
                datos[3] = registros["IdSexo"].ToString();
                datos[4] = registros["IdEstadoCivil"].ToString();
            }
            return datos;
        }

        public string[] getDatosContrato(string numeroCon, string rutCliente)
        {
            string[] datos = new string[9];
            string sql = "SELECT * FROM Contrato WHERE Numero = '" + numeroCon + "' AND RutCliente = '" + rutCliente + "';";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            while (registros.Read())
            {
                datos[0] = registros["CodigoPlan"].ToString();
                datos[1] = registros["FechaInicioVigencia"].ToString();
                datos[2] = registros["FechaFinVigencia"].ToString();
                datos[3] = registros["Vigente"].ToString();
                datos[4] = registros["DeclaracionSalud"].ToString();
                datos[5] = registros["PrimaAnual"].ToString();
                datos[6] = registros["PrimaMensual"].ToString();
                datos[7] = registros["ObservacioneS"].ToString();
                datos[8] = registros["idTipoContrato"].ToString();
            }
            return datos;
        }

        public string[] getDatosContratoVeh(string numeroCon)
        {
            string[] datos = new string[4];
            string sql = "SELECT * FROM contratoVehiculo cv " +
                        "JOIN Vehiculo v ON(cv.Patente = v.Patente) " +
                        "JOIN ModeloVehiculo mv ON (v.IdModelo = mv.IdModelo)" +
                         "WHERE cv.numero = '" + numeroCon + "';";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            while (registros.Read())
            {
                datos[0] = registros["Patente"].ToString();
                datos[1] = registros["IdMarca"].ToString();
                datos[2] = registros["Descripcion"].ToString();
                datos[3] = registros["Anho"].ToString();
            }
            return datos;
        }

        public string[] getDatosContratoViv(string numeroCon)
        {
            string[] datos = new string[7];
            string sql = "SELECT * FROM Contrato_vivienda cvi " +
                            "JOIN Vivienda vi ON(cvi.codigoPostal = vi.CodigoPostal) " +
                            "JOIN Comuna c ON (vi.IdComuna = c.IdComuna)" +
                            "WHERE cvi.numero = '" + numeroCon + "'; ";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            while (registros.Read())
            {
                datos[0] = registros["CodigoPostal"].ToString();
                datos[1] = registros["Anho"].ToString();
                datos[2] = registros["Direccion"].ToString();
                datos[3] = registros["ValorInmueble"].ToString();
                datos[4] = registros["ValorContenido"].ToString();
                datos[5] = registros["IdRegion"].ToString();
                datos[6] = registros["NombreComuna"].ToString();
            }
            return datos;
        }

        public List<Cliente> ListarClientes() {
            string sql = "SELECT * FROM Cliente";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();

            while (registros.Read()) {
                Cliente objC = new Cliente();
                string sexo, estadoCivil;

                objC.Rut = registros["RutCliente"].ToString();
                objC.Nombre = registros["Nombres"].ToString();
                objC.Apellido = registros["Apellidos"].ToString();
                string fecha = registros[3].ToString();
                objC.FechaNacimiento = fecha.Substring(0, 10);
                sexo = registros[4].ToString();
                estadoCivil = registros[5].ToString();
                if (sexo == "1") {
                    objC.Sexo = "Masculino";
                } else if (sexo == "2") {
                    objC.Sexo = "Femenino";
                }

                if (estadoCivil == "1") {
                    objC.EstadoCivil = "Soltero";
                } else if (estadoCivil == "2") {
                    objC.EstadoCivil = "Casado";
                } else if (estadoCivil == "3") {
                    objC.EstadoCivil = "Divorciado";
                } else if (estadoCivil == "4") {
                    objC.EstadoCivil = "Viudo";
                }

                clientes.Add(objC);
            }
            return clientes;
        }

        public List<Contrato> ListarContratos() {
            string sql = "SELECT * FROM Contrato";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();

            while (registros.Read()) {
                Contrato objCont;
                Salud objSal = new Salud();
                string vigente, declaracionSalud;

                objSal.NumeroContrato = registros["Numero"].ToString();
                string fechaCr = registros["FechaCreacion"].ToString();
                objSal.FechaCreacion = fechaCr.Substring(0, 10);
                objSal.RutCliente = registros["RutCliente"].ToString();
                objSal.CodigoPlan = registros["CodigoPlan"].ToString();
                string fechaIV = registros["FechaInicioVigencia"].ToString();
                objSal.FechaInicioVigencia = fechaIV.Substring(0,10);
                string fechaFV = registros["FechaFinVigencia"].ToString();
                objSal.FechaFinVigencia = fechaFV.Substring(0, 10);

                vigente = registros[6].ToString();
                declaracionSalud = registros[7].ToString();
                if (vigente == "True") {
                    objSal.Vigente = "Vigente";
                } else if (vigente == "False") {
                    objSal.Vigente = "Vencido";
                }

                if (declaracionSalud == "True") {
                    objSal.DeclaracionSalud = "Presenta";
                } else if (declaracionSalud == "False") {
                    objSal.DeclaracionSalud = "No Presenta";
                }

                objSal.PrimaAnual = registros[8].ToString();
                objSal.PrimaMensual = registros[9].ToString();
                objSal.Observaciones = registros[10].ToString();

                contratos.Add(objSal);
            }
            return contratos;
        }

        public List<Cliente> filtroClientes(string filtro)
        {
            clientes.Clear();
            string sql = "SELECT * FROM Cliente WHERE "+filtro;
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Cliente objC = new Cliente();
                string sexo, estadoCivil;

                objC.Rut = registros["RutCliente"].ToString();
                objC.Nombre = registros["Nombres"].ToString();
                objC.Apellido = registros["Apellidos"].ToString();
                string fecha = registros[3].ToString();
                objC.FechaNacimiento = fecha.Substring(0, 10);
                sexo = registros[4].ToString();
                estadoCivil = registros[5].ToString();
                if (sexo == "1")
                {
                    objC.Sexo = "Masculino";
                }
                else if (sexo == "2")
                {
                    objC.Sexo = "Femenino";
                }

                if (estadoCivil == "1")
                {
                    objC.EstadoCivil = "Soltero";
                }
                else if (estadoCivil == "2")
                {
                    objC.EstadoCivil = "Casado";
                }
                else if (estadoCivil == "3")
                {
                    objC.EstadoCivil = "Divorciado";
                }
                else if (estadoCivil == "4")
                {
                    objC.EstadoCivil = "Viudo";
                }

                clientes.Add(objC);
            }
            return clientes;
        }

        public List<Contrato> filtroContratos(string filtro)
        {
            contratos.Clear();
            string sql = "SELECT * FROM Contrato WHERE "+filtro;
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();

            while (registros.Read())
            {
                Contrato objCont;
                Salud objSal = new Salud();
                string vigente, declaracionSalud;

                objSal.NumeroContrato = registros["Numero"].ToString();
                string fechaCr = registros["FechaCreacion"].ToString();
                objSal.FechaCreacion = fechaCr.Substring(0, 10);
                objSal.RutCliente = registros["RutCliente"].ToString();
                objSal.CodigoPlan = registros["CodigoPlan"].ToString();
                string fechaIV = registros["FechaInicioVigencia"].ToString();
                objSal.FechaInicioVigencia = fechaIV.Substring(0, 10);
                string fechaFV = registros["FechaFinVigencia"].ToString();
                objSal.FechaFinVigencia = fechaFV.Substring(0, 10);

                vigente = registros[6].ToString();
                declaracionSalud = registros[7].ToString();
                if (vigente == "True")
                {
                    objSal.Vigente = "Vigente";
                }
                else if (vigente == "False")
                {
                    objSal.Vigente = "Vencido";
                }

                if (declaracionSalud == "True")
                {
                    objSal.DeclaracionSalud = "Presenta";
                }
                else if (declaracionSalud == "False")
                {
                    objSal.DeclaracionSalud = "No Presenta";
                }

                objSal.PrimaAnual = registros[8].ToString();
                objSal.PrimaMensual = registros[9].ToString();
                objSal.Observaciones = registros[10].ToString();

                contratos.Add(objSal);
            }
            return contratos;
        }

        public string[] datosPoliza(string nombrePlan)
        {
            string[] datos = new string[2];
            string sql = "SELECT PolizaActual, PrimaBase FROM [Plan] WHERE Nombre = '" + nombrePlan + "';";
            abrirConexion();
            comando = new SqlCommand(sql, cn);
            registros = comando.ExecuteReader();
            if (registros.Read()){
                datos[0] = registros[0].ToString();
                datos[1] = registros[1].ToString();
            }
            return datos;

        }
        /*SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=BeLife;Trusted_Connection=True;");
        conexion.Open();
           conexion.Close();*/

    }
}
