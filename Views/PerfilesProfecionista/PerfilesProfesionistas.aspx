<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="PerfilesProfesionistas.aspx.cs" Inherits="WebSecureBookings.Views.Perfilesprofecionista.PerfilesProfesionistas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="PerfilesProfesionistas.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-10 text-center">
            <h1 style="color: #3385d9">Profesionistas</h1>
        </div>
        <div class="col-md-2 text-right">
            <br />
            <button id="btnRegistrar" type="button" class="btn btn-used">Nuevo Rol</button>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>
    <br />
    <div class="row" id="div-profesionales">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="modalDialog" runat="server">
</asp:Content>
