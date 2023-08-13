<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VistaCitas.aspx.cs" Inherits="WebSecureBookings.Views.VistaCitas.VistaCitas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="VistaCitas.js"></script>
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>Lista de Citas</h1>
    <table id="tablaCitas" class="table">
        <!-- Encabezados de la tabla -->
        <thead>
            <tr>
                <th>Profesional</th>
                <th>Especialidad</th>
                <th>Teléfono</th>
                <th>Hora</th>
                <th>Fecha</th>
                <th>Cliente</th>
                <th>Email Cliente</th>
                <th>Número de Cita</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            <!-- Aquí se generará dinámicamente el contenido de la tabla -->
        </tbody>
    </table>

    <!-- Modales para Calificar y Eliminar -->
    <div id="modalCalificar" class="modal">
        <!-- Contenido del modal para calificar -->
        <h2>Calificar Cita</h2>
        <p>Aquí colocas el contenido del modal para calificar la cita.</p>
    </div>

    <div id="modalEliminar" class="modal">
        <!-- Contenido del modal para eliminar -->
        <h2>Eliminar Cita</h2>
        <p>Aquí colocas el contenido del modal para eliminar la cita.</p>
    </div>

    <!-- Botones para abrir los modales -->
    <button id="btnCalificar" style="display: none;">Calificar Cita</button>
    <button id="btnEliminar" style="display: none;">Eliminar Cita</button>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
    
</asp:Content>
