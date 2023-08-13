<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebSecureBookings.Views.Index.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="https://kjur.github.io/jsrsasign/jsrsasign-latest-all-min.js"></script>
    <script src="Index.js"></script>
    <%--<script src="Token.js"></script>--%>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-8 text-center">
            <h1 style="color: #3385d9">Registro de Roles</h1>
        </div>
        <div class="col-md-4 text-right">
            <br />
            <button id="btnRegistrar" type="button" class="btn btn-used">Nuevo Rol</button>
            <button id="btnLogin" type="button" class="btn btn-used">Token</button>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>
    <br />
    <div class="table table-striped table-hover" id="divRoles"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    <!-- Modal Registro de Rol-->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitulo">Nuevo Rol</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#exampleModalCenter').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-md-12 form-group">
                                <label><b>Nombre del Rol</b></label>
                                <input type="text" id="txtRol" class="form-control" onkeypress="return permite(event, 'car')" oninput="validarTexto(this, 4, 50)" required>
                            </div>
                            <div class="col-md-12 form-group">
                                <label><b>Descripción</b></label>
                                <textarea class="form-control" id="txtDescripcion" rows="3" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 4, 300)" required></textarea>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#exampleModalCenter').modal('hide');">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnGuardarRol">Guardar</button>
                    <button type="button" class="btn btn-used" id="btnActualizarRol">Actualizar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
