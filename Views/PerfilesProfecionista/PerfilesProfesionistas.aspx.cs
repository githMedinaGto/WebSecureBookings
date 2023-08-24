using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSecureBookings.App_Data.Models;
using WebSecureBookings.Controllers.IndexController;

namespace WebSecureBookings.Views.Perfilesprofecionista
{
    public partial class PerfilesProfesionistas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ResponseModel<string>> GetProfesionistas()
        {
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            var data = profesionistasController.GetProfesionistas();

            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                {
                    new ResponseModel<string>
                    {
                        StatusCode = data.StatusCode,
                        Message = data.Message,
                        Resultado = ""
                    }
                };
            }

            string result = GenerateTableHtml(data.Data);

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = result
                }
            };
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetProfesiones()
        {
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            var data = profesionistasController.GetProfesiones();

            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                    {
                        new ResponseModel<string>
                        {
                            StatusCode = data.StatusCode,
                            Message = data.Message,
                            Resultado = ""
                        }
                    };
            }

            string selectOptions = @"<option disabled selected value=0>Seleccione una profesión</option>";

            foreach (string profesion in data.Data)
            {
                selectOptions += $"<option value=\"{profesion}\">{profesion}</option>";
            }

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = selectOptions
                }
            };
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetCiudades()
        {
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            var data = profesionistasController.GetCiudades();

            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                    {
                        new ResponseModel<string>
                        {
                            StatusCode = data.StatusCode,
                            Message = data.Message,
                            Resultado = ""
                        }
                    };
            }

            string selectOptions = @"<option disabled selected value=0>Seleccione una ciudad</option>";

            foreach (EstadoModel ciudad in data.Data)
            {
                selectOptions += $"<option value=\"{ciudad.sEstado}\">{ciudad.sEstado}</option>";
            }

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = selectOptions
                }
            };
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetProfesionistasFiltro(string sProfesion, string sEstado)
        {
            //int iEstado = int.Parse(sEstado);
            var data = new ResponseModel<List<UsuarioModel>>(); // Inicializar con un valor predeterminado
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            if (sProfesion != "0" && sEstado != "0")
            {
                data = profesionistasController.GetProfesionistasProfesionEstado(sProfesion, sEstado);
            }else if(sProfesion != "0")
            {
                data = profesionistasController.GetProfesionistasProfesion(sProfesion);

            }else if(sEstado != "0")
            {
                data = profesionistasController.GetProfesionistasEstado(sEstado);
            }
            

            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                {
                    new ResponseModel<string>
                    {
                        StatusCode = data.StatusCode,
                        Message = data.Message,
                        Resultado = ""
                    }
                };
            }

            string result = GenerateTableHtml(data.Data);

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = result
                }
            };
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetProfesionista(string idUsuario)
        {
            int iIdUsuario = int.Parse(idUsuario);
            
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            var data = profesionistasController.GetProfesionista(iIdUsuario);
            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                {
                    new ResponseModel<string>
                    {
                        StatusCode = data.StatusCode,
                        Message = data.Message,
                        Resultado = ""
                    }
                };
            }

            string result = GenerateModal(data.Data);
            string sUbicacion = data.Data.FirstOrDefault()?.sUbicacion;

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = sUbicacion,
                    Resultado = result,
                    
                }
            };
        }

        [WebMethod]
        public static ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario(string idUsuario)
        {
            int iIdUsuario = int.Parse(idUsuario);
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            return profesionistasController.GetProfesionistaCalendario(iIdUsuario);
        }

        [WebMethod]
        public static ResponseModel<string> PostAcata(string idUsuario, string sMotivo, string idCalendario, string sFecha)
        {
            int iIdUsuario = int.Parse(idUsuario);
            int iIdCalendario = int.Parse(idCalendario);
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            return profesionistasController.PostCrearACta(iIdUsuario, iIdCalendario, sMotivo, sFecha);
        }

        private static string GenerateTableHtml(List<UsuarioModel> data)
        {
            string result = "";
            foreach (UsuarioModel usuario in data)
            {
                result += $@"
                    <div class=""col-6 my-4"">       
                        <div class=""card"" >
                            <div class=""card-body text-center"">
                                <h5 class=""card-title"">{usuario.sNombre + " " + usuario.sApellidoP + " " + usuario.sApellidoM}</h5>
                                <p class=""card-text"">{usuario.sProfecion}</p>
                                <ul class=""list-group list-group-flush"">
                                    <li class=""list-group-item""><b>Ubicación: </b>{usuario.sCalle + " " + usuario.sColonia + " " + usuario.sMunicipio + " " + usuario.sEstado}</li>
                                    <li class=""list-group-item""><b>Telefono: </b>{usuario.stelefono}</li>
                                    <li class=""list-group-item""><b>Area: </b>{usuario.sAreaProfesion}</li>
                                    <li class=""list-group-item""><b>Correo: </b>{usuario.sCorreo}</li>
                                </ul>
                                <br/>
                                <button type=""button"" class=""btn btn-used"" data-toggle=""modal"" data-target=""#myModal"" onclick=""mostrarInfo({usuario.idUsuario})"">
                                    Mostrar Info
                                </button>
                            </div>
                        </div>
                    </div>
                    ";
            }
            return result;
        }

        private static string GenerateModal(List<UsuarioModel> data)
        {
            string result = "";
            foreach (UsuarioModel usuario in data)
            {
                result += $@"
                    <div class=""modal-dialog modal-dialog-centered"" role=""document"">
                        <div class=""modal-content"">
                            <div class=""modal-header"">
                                <h5 class=""modal-title"" id=""modalTituloProfesionista"">{usuario.sProfecion}</h5>
                                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"" onclick=""JavaScript:$('#modalGenerarCita').modal('hide');"">
                                    <span aria-hidden=""true"">&times;</span>
                                </button>
                            </div>
                            <div class=""modal-body"">
                                <div class=""tab-content"">
                                    <div class=""row"">
                                       <div class=""col-md-12 form-group"">
                                            <label><b>{usuario.sNombre + " " + usuario.sApellidoP + " " + usuario.sApellidoM}</b></label>
                                        </div>
                                        <div class=""col-md-12 form-group"">
                                            <label><b>Area de Profesión: </b></label>
                                            <label>{usuario.sAreaProfesion}</label>
                                        </div>
                                        <div class=""col-md-6 form-group"">
                                            <label><b>Telefono: </b></label>
                                            <label>{usuario.stelefono}</label>
                                        </div>
                                        <div class=""col-md-6 form-group"">
                                            <label><b>Correo: </b></label>
                                            <label>{usuario.sCorreo}</label>
                                        </div>
                                        <div class=""col-md-12 form-group"">
                                            <label><b>Ubicación: </b></label>
                                            <label>{usuario.sCalle + " " + usuario.sColonia + " " + usuario.sMunicipio + " " + usuario.sEstado}</label>
                                        </div>
                                        <div class=""col-md-12 form-group"">
                                            <div id=""map"" style=""width: 100%; height: 200px;""></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=""modal-footer"">
                                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"" onclick=""JavaScript:$('#modalGenerarCita').modal('hide');"">Cancelar</button>
                                <button type=""button"" class=""btn btn-used"" id=""btnVerCalendario"" onclick=""fn_GenerarCalendario()"">Mostrar Calendario</button>
                            </div>
                        </div>
                    </div>
                    ";
            }
            return result;
        }
    }
}