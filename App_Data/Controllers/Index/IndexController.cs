using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebSecureBookings.Controllers.IndexController
{
    public class IndexController
    {

        #region Obtener la tabla dejugadores
        [HttpPost]
        public List<string> TablaEquipos()
        {
            List<string> resultados = new List<string>();
            string sTabla = "";
            string sIdTabla = "";
            string[] aColumnas = new string[] { "NOMBRE", "Color", "Código", "Liga", "Deporte", "Director Técnico", "ACCIONES" };
            string query = "SELECT [idEquipo], te.sNombre, [sColor], [sCodigoEquipo], tl.sNombre, td.sNombre, sNomDirTec" +
                "\r\nFROM [tEquipo] as te" +
                "\r\nInner Join [tLiga] tl ON te.iIdLiga = tl.idLiga" +
                "\r\nInner Join [tDeporte] td ON tl.iIdDeporte = td.idDeporte";
            DataTable dtDatos = new DataTable();
            try
            {
                Connection cn = new Connection();
                dtDatos = cn.ExecuteQueryDataTable(query);
                sIdTabla = "tblEquipos";

                if (dtDatos != null && dtDatos.Rows.Count > 0)
                {
                    sTabla += "<table id='" + sIdTabla + "' class='table table-striped table-bordered table-hover dataTable' style='width: 100%'> " +
                        "<thead>" +
                        "<tr>";
                    foreach (string sColumna in aColumnas)
                    {
                        sTabla += "<th>" + sColumna + "</th>";
                    }
                    sTabla += "</tr></thead>";
                    sTabla += "<tbody>";
                    foreach (DataRow oRow in dtDatos.Rows)
                    {
                        sTabla += "<tr>";
                        sTabla += "<td>" + oRow[1].ToString() + "</td>";
                        sTabla += "<td>" + oRow[2].ToString() + "</td>";
                        sTabla += "<td>" + oRow[3].ToString() + "</td>";
                        sTabla += "<td>" + oRow[4].ToString() + "</td>";
                        sTabla += "<td>" + oRow[5].ToString() + "</td>";
                        sTabla += "<td>" + oRow[6].ToString() + "</td>";

                        sTabla += "<td>" +
                            "<span class='fa fa-pencil-square-o' style='color:#85c555;font-size: 35px;  cursor: pointer;' onclick='javascript:fn_EditEquipo(" + oRow[0].ToString() + ");'></span>" +
                            "&nbsp;&nbsp;" +
                            "<i class=\"fa fa-duotone fa-trash-can\" style='color:red;font-size: 35px;  cursor: pointer;' onclick='javascript:fn_EliminarEquipo(" + oRow[0].ToString() + ");'></i>" +
                            "</td>";
                        sTabla += "</tr>";
                    }
                    sTabla += "</tbody>";
                    sTabla += "<tfoot>" +
                        "<tr>";
                    sTabla += "</tr>" +
                        "</tfoot>" +
                        "</table>";

                    resultados.Add("200"); // Código de estado
                    resultados.Add("OK"); // Texto del estado
                    resultados.Add(sTabla); // Contenido de la tabla
                }
                else
                {
                    resultados.Add("404"); // Código de estado
                    resultados.Add("No se encontraron registros"); // Texto del estado
                    resultados.Add(string.Empty); // Contenido de la tabla vacío
                }
            }
            catch (Exception ex)
            {
                resultados.Add("500"); // Código de estado
                resultados.Add("Error interno del servidor"); // Texto del estado
                resultados.Add(string.Empty); // Contenido de la tabla vacío
            }

            return resultados;
        }
        #endregion
    }

}