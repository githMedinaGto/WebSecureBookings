<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VistaCitas.aspx.cs" Inherits="WebSecureBookings.Views.VistaCitas.VistaCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="VistaCitas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row" style="width: 100%;">
        <div class="col-md-8 text-center">
            <h1 style="color: #3385d9">Vista de Citas</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>
    <br />
    <div class="table table-striped table-hover" id="divCitas"></div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    <!-- Modal de las fechas disponibles-->
    <div class="modal fade" id="modalCalificar" tabindex="-1" role="dialog" aria-labelledby="modalCalificar" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitulo">Calificación</h5>
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
                    <textarea id="comentario" name="comentario" rows="4" cols="50"></textarea><br>
                    <br>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#modalCalificar').modal('hide');">Cancelar</button>
                    <button id="btn_Calificar" type="button" class="btn btn-success" data-dismiss="modal" onclick="JavaScript:$('#modalCalificar').modal('hide');">Calificar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
