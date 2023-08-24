<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarCuenta.aspx.cs" Inherits="WebSecureBookings.Views.RecuperarCuenta.RecuperarCuenta" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WebSecureteBooking</title>

    <%--Boostrap--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>

    <%--JQuery--%>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/jquery-3.6.3.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/blockUI.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/ohsnap.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/ConsumoAlertas.js") %>"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="RecuperarCuenta.js"></script>

    <%--Toma estilo--%>
    <link href="/Scripts/Css/Style.css" rel="stylesheet" />

    <%--Toma de iconos --%>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />

</head>
<body>
    <!--INICIO HEADER-->
    <header class="header-sistema">
        <nav class="navbar navbar-expand-lg fixed-top">
            <div class="container-fluid">
                <!-- Encabezado del navbar -->
                <a class="navbar-brand text-white" href="#" onclick="location.reload()">
                    <img class="img-fluid manImg" src="<%= ResolveUrl("~/Assests/Img/Logo 2.jpeg") %>" alt="Image" height="20" width="20" />
                    WebSecureBookings
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-collapse" aria-controls="navbar-collapse" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <!-- Contenido del navbar -->
                <div class="collapse navbar-collapse text-white" id="navbar-collapse">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0" id="menu" runat="server">
                        <li class="nav-item">
                            <a class="nav-link active" aria-current="page" href="/Index.aspx">Inicio</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <!--MENU-->
    </header>
    <!--FIN HEADER-->

    <!--INICIO CUERPO-->
    <div class="content-fluid pd-10">
        <div class="body-sistema container-fluid">
            <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-12 text-center">
            <h1 style="color: #3385d9">Recuperar Cuenta</h1>
        </div>
        <div class="col-md-12 center">
            <hr style="width: auto; height: 3px; background: #caebf2">
        </div>
    </div>
    <br />
    <div class="row" style="width: 100%;">
        <div class="col-md-6 text-center">
            <label><b>Correo Electrónico</b></label>
            <input type="email" placeholder="Correo Electrónico" class="form-control" id="txtCorreo" onkeypress="validateEmail(this)"/>
        </div>
        <div class="col-md-6 text-center"></div>
        <div class="col-md-3 text-center">
            <br />
            <button type="button" class="btn btn-used" id="btnBuscarCuenta">Buscar</button>
            <br />
        </div>
        <div class="col-md-3 text-center">
            <br />
            <button type="button" class="btn btn-used" id="btnActuaizar">Actualizar Contraseña</button>
            <br />
        </div>
        <div class="col-md-6 text-center">
            <br />
            <br />
            <br />
        </div>
    </div>
        </div>
        <div id="ohsnap"></div>
    </div>
    <!--FIN CUERPO-->

    <!--INICIO FOOTER-->
    <footer class="footer-sistema container-fluid">
        <div class="row">
            <div class="col-12">
                <br />
                <%--<img id="Img" src="porDefinir" runat="server" alt="logo" />--%>
            </div>
            <div class="col-lg-3 col-md-3 aling-middle">
                <h3>Web Securete Booking</h3>
                <p>
                    Av.Univesidad
                    <br />
                    Dolores Hidalgo, GTO.<br />
                    <strong>Cuentas:</strong>
                    <br />
                    <a href="https://github.com/AlexAlonRo" target="_blank">
                        <i class="fa fa-github"></i>Alexander Alonso
                    </a>
                    <br />
                    <a href="https://github.com/AndresArrEsc" target="_blank">
                        <i class="fa fa-github"></i>Andrés Arredondo
                    </a>
                    <br />
                    <a href="https://github.com/githMedinaGto" target="_blank">
                        <i class="fa fa-github"></i>Jonathan Medina
                    </a>
                    <br />
                    <a href="https://github.com/spalomino13" target="_blank">
                        <i class="fa fa-github"></i>Samuel Palominon
                    </a>
                </p>
            </div>
            <div class="col-lg-3 col-md-3 aling-middle text-center">
                <br />
                <br />
                <h5><b>Organización</b></h5>
                <br />
                <p>Universidad Tecnologica Norte de Guanajuato</p>
            </div>
            <div class="col-lg-6 col-md-6 aling-middle text-center">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7441.618527043131!2d-100.93666822228995!3d21.159987600000004!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x842b3fdfaaed04ad%3A0x2ce997e250d0e900!2sUtng!5e0!3m2!1ses-419!2smx!4v1688433682462!5m2!1ses-419!2smx" width="400" height="200" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
            </div>

            <div class="col-12 text-center footer-derechos text-white">
                <p>WebSecureBookings 2023. ©Todos los derechos reservados.</p>
            </div>
        </div>
    </footer>
    <!--FIN FOOTER-->


    <!-- Modal Registro de Cita-->
    <div class="modal fade" id="modalActualizarContraseña" tabindex="-1" role="dialog" aria-labelledby="modalActualizarContraseña" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloCita">Evento disponible</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#modalActualizarContraseña').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div class="col-md-4 form-group">
                                <label><b>Correo Electrónico</b></label>
                                <input type="email" placeholder="Correo Electrónico" class="form-control" id="txtCorreoAc" onkeypress="validateEmail(this)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Contraseña" class="form-control" id="txtPasswordAc1" oninput="validarTexto(this, 8, 15)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Repita su Contraseña" class="form-control" id="txtPasswordAc2" oninput="validarTexto(this, 8, 15)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Token</b></label>
                                <input type="text" placeholder="Token" class="form-control" id="token" maxlength="5" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 5, 5)"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" id="btnCancelar" data-dismiss="modal" onclick="JavaScript:$('#modalActualizarContraseña').modal('hide');">Cancelar</button>
                    <button type="button" class="btn btn-used" id="btnActualizarDatos">Actualizar</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
