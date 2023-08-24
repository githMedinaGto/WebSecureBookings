<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VistaCitas.aspx.cs" Inherits="WebSecureBookings.Views.VistaCitas.VistaCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.datatables.net/1.13.6/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.0.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="VistaCitas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12 text-center">
            <h1 style="color: #3385d9">Cita de las Citas</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12">
            <div class="table table-striped table-hover" id="divCitas"></div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    <!-- Modal para calificar-->
    <div class="modal fade" id="modalCalificar" tabindex="-1" role="dialog" aria-labelledby="modalCalificar" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloComentar">Calificación y Comentario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalCalificar').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <p><strong>¿Qué calificación le da al profesionista?</strong></p>
                    <select name="calificacion" id="valorCalificacion">
                        <option value="" disabled selected>Seleccione alguna calificación</option>
                        <option value="5">5 - Servicio recomendable</option>
                        <option value="4">4 - Bueno</option>
                        <option value="3">3 - Regular</option>
                        <option value="2">2 - Necesita mejora</option>
                        <option value="1">1 - No satisfactorio</option>
                    </select><br>
                    <br>

                    <p><strong>Deje un comentario:</strong></p>
                    <textarea id="comentario" name="comentario" rows="4" cols="50" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 4, 250)"></textarea><br>
                    <br>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalCalificar').modal('hide');">Cancelar</button>
                    <button id="btn_Calificar" type="button" class="btn btn-used" data-dismiss="modal" onclick="JavaScript:$('#modalCalificar').modal('hide');">Calificar</button>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal de Actualizar cita-->
    <div class="modal fade" id="modalActCita" tabindex="-1" role="dialog" aria-labelledby="modalActCita" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloActCita">Actualizar cita</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalActCita').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-md-12 form-group">
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
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalActCita').modal('hide');">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnEliminarCita">Eliminar Cita</button>
                </div>
            </div>
        </div>
    </div>


     <!-- Modal Registro de Cita-->
    <div class="modal fade" id="modalActualizarCita" tabindex="-1" role="dialog" aria-labelledby="modalActualizarCita" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloCita">Actualizar evento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalActualizarCita').modal('hide');">
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
                                <label><b>Motivo</b></label>
                                <textarea class="form-control" id="txtMotivo" rows="3" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 4, 250)" required></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalActualizarCita').modal('hide');"  id="btnCancelar">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnActualizarCita">Actualizar Cita</button>
                    <button type="button" class="btn btn-used" id="btnAEliminarCita">Eliminar Cita</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
