﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vehiculo:Contrato
    {
        Conexion objConec = new Conexion();
        public string Patente { get; set; }
        public int Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }

        public override bool agregarContrato(){

            string sql = "INSERT INTO Contrato VALUES ('" + NumeroContrato + "', GETDATE(), " +
                "convert(date, '" + FechaFinVigencia + "'), '" + RutCliente +
                        "',(SELECT idPlan FROM [Plan] WHERE Nombre = '" + CodigoPlan + "'), " + TipoContrato
                        + ", convert(date, '" + FechaInicioVigencia + "'), convert(date, '" + FechaFinVigencia + "'), "
                        + Vigente + ", 0," + PrimaAnual.Replace(",", ".") + "," + PrimaMensual.Replace(",", ".") +
                        ",'" + Observaciones + "');";

            string sql1 = "INSERT INTO Vehiculo VALUES ('"+Patente+"',"+Marca
                            +", (SELECT IdModelo FROM ModeloVehiculo WHERE Descripcion = '"+Modelo+"'),"+Anio+");";
            string sql2 = "INSERT INTO contratoVehiculo VALUES ('"+NumeroContrato+"', '"+Patente+"');";

            bool guarda = objConec.insertar(sql);

            bool guarda1 = objConec.insertar(sql1);

            bool guarda2 = objConec.insertar(sql2);
            
            if (guarda == true){
                return guarda;
            }else{
                return guarda;
            }
        }

        public override bool editarContrato()
        {
            string campos = "CodigoPlan = (SELECT idPlan FROM [Plan] WHERE Nombre = '" + CodigoPlan + "') " + 
                ", PrimaAnual = " + PrimaAnual.Replace(",", ".") + ", " +
                "PrimaMensual = " + PrimaMensual.Replace(",", ".") + ", Observaciones = '" + Observaciones + "' ";
            string condicion = " Numero = '" + NumeroContrato + "';";
            bool edita = objConec.actualizar("Contrato", campos, condicion);

            string campos1 = "IdMarca =" + Marca
                            + ", IdModelo = (SELECT IdModelo FROM ModeloVehiculo WHERE Descripcion = '" + Modelo + "')," +
                            "Anho = " + Anio + "";
            string condicion1 = "Patente = '" + Patente + "';";

            bool edita1 = objConec.actualizar("Vehiculo",campos1,condicion1);

            
            if (edita == true)
            {
                return edita;
            }
            else
            {
                return edita;
            }
        }
    }
}
