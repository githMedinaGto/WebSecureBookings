<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PerfilesProfesionistas.aspx.cs" Inherits="WebSecureBookings.Views.Perfilesprofecionista.PerfilesProfesionistas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://api.mapbox.com/mapbox-gl-js/v2.4.1/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v2.4.1/mapbox-gl.css' rel='stylesheet' />

    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="PerfilesProfesionistas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12 text-center">
            <h1 style="color: #3385d9">Profesionistas</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>

    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-4 text-center">
            <label><b>Profesión</b></label>
            <select class="form-select form-select-sm selectpicker" aria-label=".form-select-sm example" id="cboProfeciones">
            </select>
        </div>
        <div class="col-md-5 text-center">
            <label><b>Ciudad</b></label>
            <select class="form-select form-select-sm select2" aria-label=".form-select-sm example" id="cboCiudades">
            </select>
        </div>
        <div class="col-md-1 text-right">
            <br />
            <button id="btnBuscar" type="button" class="btn btn-used">Buscar</button>
        </div>
        <div class="col-md-2 text-right">
            <br />
            <button id="btnLimpiarFiltro" type="button" class="btn btn-secondary">Limpiar filtro</button>
        </div>
    </div>

    <div class="row" id="div-profesionales">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    <!-- ModalMustrta de datos del Usuario Profesionista-->
    <div class="modal fade" id="modalGenerarCita" tabindex="-1" role="dialog" aria-labelledby="modalGenerarCita" aria-hidden="true">
    </div>

    <!-- Modal de las fechas disponibles-->
    <div class="modal fade" id="modalCalendario" tabindex="-1" role="dialog" aria-labelledby="modalCalendario" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitulo">Calendario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalCalendario').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content" id="contect-calendario">
                        <%--<div id='calendar'></div>--%>
                        <nav>
                            <div class="nav custom-nav-tabs nav-justified" id="nav-tab" role="tablist">
                                <button class="nav-link active" id="nav-lunes-tab" data-bs-toggle="tab" data-bs-target="#nav-lunes" type="button" role="tab" aria-controls="nav-lunes" aria-selected="true">Lunes</button>
                                <button class="nav-link" id="nav-mares-tab" data-bs-toggle="tab" data-bs-target="#nav-martes" type="button" role="tab" aria-controls="nav-martes" aria-selected="false">Martes</button>
                                <button class="nav-link" id="nav-miercoles-tab" data-bs-toggle="tab" data-bs-target="#nav-miercoles" type="button" role="tab" aria-controls="nav-miercoles" aria-selected="false">Miercoles</button>
                                <button class="nav-link" id="nav-jueves-tab" data-bs-toggle="tab" data-bs-target="#nav-jueves" type="button" role="tab" aria-controls="nav-jueves" aria-selected="false">Jueves</button>
                                <button class="nav-link" id="nav-viernes-tab" data-bs-toggle="tab" data-bs-target="#nav-viernes" type="button" role="tab" aria-controls="nav-viernes" aria-selected="false">Viernes</button>
                            </div>
                        </nav>
                        <div class="tab-content" id="nav-tabContent">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalCalendario').modal('hide');">Cancelar</button>
                    <%--                    <button type="button" class="btn btn-used" id="btnGenerar">Generar Cita</button>--%>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Registro de Cita-->
    <div class="modal fade" id="modalRegistroCita" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloCita">Evento disponible</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalRegistroCita').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div></div>
                            <div class="col-md-12 form-group">
                                <label><b>Fecha y Hora de la Cita:</b></label>
                                <label id="txtFecha"></label>
                            </div>
                            <div class="col-md-12 form-group">
                                <label><b>Motivo de la cita</b></label>
                                <textarea class="form-control" id="txtMotivo" rows="3" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 4, 250)" required></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalRegistroCita').modal('hide');">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnGenerarCita">Generar Cita</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
