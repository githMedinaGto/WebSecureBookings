<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VistaCitasProfecionista.aspx.cs" Inherits="WebSecureBookings.Views.VistaCitasProfecionista.VistaCitasProfecionista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.0.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>


    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="../../Scripts/Jquery/ConsumoAlertas.js"></script>
    <script src="VistaCitasProfecionista.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12 text-center">
            <h1 style="color: #3385d9">Citas</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>

    <div class="row" style="width: 100%;">
        <div class="col-md-8">
        </div>
        <div class="col-md-4 text-right">
            <br />
            <button id="btnAgregarCitas" type="button" class="btn btn-used">Agregar fechas</button>
            <br />

        </div>
        <div class="col-md-12">
            <h4><b>Citas Agendadas Proximas a Atender</b></h4>
        </div>
        <div class="col-md-12">
            <div class="table table-striped table-hover" id="divCitasprox"></div>
        </div>
    </div>

    <div class="row" style="width: 100%;">
        <div class="col-md-8">
            <h4><b>Citas Agendadas</b></h4>
        </div>
        <div class="col-md-12">
            <div class="table table-striped table-hover" id="divCitas"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">

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

    <!-- Modal agregar fechas disponibles-->
    <div class="modal fade" id="modalAgregarFechas" tabindex="-1" role="dialog" aria-labelledby="modalAgregarFechas" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloFe">Seleccione un Día de la Semana</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalAgregarFechas').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-md-4 text-center"></div>
                            <div class="col-md-4 text-center">
                                <label for="diaSemana">Día de la Semana:</label>
                                <select id="diaSemana" class="form-select form-select-sm selectpicker" aria-label=".form-select-sm example">
                                    <option value="1">Lunes</option>
                                    <option value="2">Martes</option>
                                    <option value="3">Miércoles</option>
                                    <option value="4">Jueves</option>
                                    <option value="5">Viernes</option>
                                </select>
                            </div>

                            <div class="col-md-12 text-center">
                                <div class="container mt-5">
                                    <h4>Campo de Entrada de Hora</h4>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="horaInicio" class="form-label">Hora de Inicio:</label>
                                            <input type="time" class="form-control" id="horaInicio" name="horaInicio">
                                        </div>
                                        <div class="col-md-4">
                                            <label for="horaFin" class="form-label">Hora de Fin:</label>
                                            <input type="time" class="form-control" id="horaFin" name="horaFin">
                                        </div>
                                        <div class="col-md-4">
                                            <br />
                                            <button type="button" class="btn btn-used" id="btnGenerar">Agregar nueva fecha y hora</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 text-center">
                                <%--<textarea class="form-control" id="txtMotivo" rows="3" required></textarea>--%>
                                <div id="appointmentsDiv"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalAgregarFechas').modal('hide');">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnGuardarFechas">Guardar Fechas</button>
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
                                <label><b>Motivo del cambio</b></label>
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
