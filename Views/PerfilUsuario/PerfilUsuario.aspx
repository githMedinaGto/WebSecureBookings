<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="WebSecureBookings.Views.PerfilUsuario.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://api.mapbox.com/mapbox-gl-js/v2.4.1/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v2.4.1/mapbox-gl.css' rel='stylesheet' />

    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="https://kjur.github.io/jsrsasign/jsrsasign-latest-all-min.js"></script>
        <script src="../../Scripts/Jquery/ohsnap.js"></script>

    <script src="../../Scripts/Jquery/ConsumoAlertas.js"></script>
    <script src="PerfilesUsuario.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12 text-center">
            <h1 style="color: #3385d9">Usuario</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>

    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <form>
                    <div class="mb-3">
                        <label for="nombre" class="form-label">Nombre</label>
                        <input type="text" class="form-control" id="nombre" placeholder="Nombre" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="apellidoP" class="form-label">Apellido Paterno</label>
                        <input type="text" class="form-control" id="apellidoP" placeholder="Apellido Paterno" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="apellidoM" class="form-label">Apellido Materno</label>
                        <input type="text" class="form-control" id="apellidoM" placeholder="Apellido Materno" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="profesion" class="form-label">Profesión</label>
                        <input type="text" class="form-control" id="profesion" placeholder="Profesión" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="areaProfesion" class="form-label">Área de Profesión</label>
                        <input type="text" class="form-control" id="areaProfesion" placeholder="Área de Profesión" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="telefono" class="form-label">Teléfono</label>
                        <input type="tel" class="form-control" id="telefono" placeholder="Teléfono" onkeypress="return permite(event, 'num')">
                    </div>
                    <div class="mb-3">
                        <label for="correo" class="form-label">Correo</label>
                        <input type="email" class="form-control" id="correo" placeholder="Correo" readonly >
                    </div>
                    <div class="mb-3">
                        <label for="municipio" class="form-label">Municipio</label>
                        <input type="text" class="form-control" id="municipio" placeholder="Municipio" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="colonia" class="form-label">Colonia</label>
                        <input type="text" class="form-control" id="colonia" placeholder="Colonia" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="calle" class="form-label">Calle</label>
                        <input type="text" class="form-control" id="calle" placeholder="Calle" onkeypress="return permite(event, 'car')">
                    </div>
                    <div class="mb-3">
                        <label for="estado" class="form-label">Estado</label>
                        <input type="text" class="form-control" id="estado" placeholder="Estado" onkeypress="return permite(event, 'car')">
                    </div>
                    <!-- ... Otros campos del formulario ... -->
                </form>
            </div>
            <%--<div class="col-md-6">
                <div class="mb-3">
                    <label for="foto" class="form-label">Foto de Perfil</label>
                    <input type="file" class="form-control" id="foto">
                </div>
                <div class="mb-3">
                    <div class="text-center">
                        <label for="imagenPerfil" class="form-label">Vista previa</label>
                    </div>
                    <div class="border rounded p-3">
                        <img src='../../Assests/Fotos/633262.png' alt="Foto de perfil" id="imagenPerfil" class="img-fluid">
                    </div>
                </div>--%>
                <div class="mb-3 form-check">
                    <input type="checkbox" class="form-check-input" id="desactivarPerfil">
                    <label class="form-check-label" for="desactivarPerfil">Desactivar perfil</label>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <button type="button" class="btn btn-primary" onclick="mostrarAlerta()">Guardar</button>
            </div>
        </div>
        <br>
        </br>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    <!-- ModalMustrta de datos del Usuario Profesionista-->
   
    <!-- Modal de las fechas disponibles-->
    <div class="modal fade" id="validarCampos" tabindex="-1" role="dialog" aria-labelledby="validarCampos" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTitulo">Calendario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#validarCampos').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="conte">
                        <h4>Deseas Actualizar tu Datos?</h4>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="JavaScript:$('#validarCampos').modal('hide');">Cancelar</button>
                     <button type="button" class="btn btn-used" id="btnAceptar" onclick="mostrar()" >Aceptar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>