<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebSecureBookings.Index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <%--Boostrap--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>

    <%--JQuery--%>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/jquery-3.6.3.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/blockUI.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/ohsnap.js") %>"></script>
    <script src="<%= ResolveUrl("~/Scripts/Jquery/ConsumoAlertas.js") %>"></script>
    <script src="Scripts/Jquery/AccionesModales.js"></script>
    <script src="Scripts/Jquery/ohsnap.js"></script>
    <script src="Scripts/Jquery/ValidacionCaracteres.js"></script>
    <link href="https://api.mapbox.com/mapbox-gl-js/v2.15.0/mapbox-gl.css" rel="stylesheet">
    <script src="https://api.mapbox.com/mapbox-gl-js/v2.15.0/mapbox-gl.js"></script>
    <!-- link icon -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-icons/1.10.5/font/bootstrap-icons.min.css" integrity="sha512-ZnR2wlLbSbr8/c9AgLg3jQPAattCUImNsae6NHYnS9KrIwRdcY9DxFotXhNAKIKbAXlRnujIqUWoXXwqyFOeIQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>WebSecureteBoking</title>

    <!-- link css -->
    <link href="Scripts/Css/Style.css" rel="stylesheet" />
    <link href="Scripts/Css/RegistroUsuarios.css" rel="stylesheet" />
</head>
<body>
    <!--INICIO HEADER-->
    <header class="header-sistema">
        <!--ENCABEZADO-->
        <nav class="navbar navbar-expand-lg fixed-top text-white">
            <div class="container-fluid">
                <a class="navbar-brand" href="Index.aspx">
                    <img src="../../Assests/Img/Logo%201.jpeg" width="50" height="50" alt="WSB" class="img-fluid" />
                </a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav">
                        <li class="nav-item active">
                            <a class="nav-link text-white" href="Index.aspx">WebSecureBoking</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
        <!--MENU-->
    </header>
    <!--FIN HEADER-->

    <!--INICIO CONTENIDO-->
    <div class="body-sistema container-fluid container">
        <div class="box">
            <div class="form sign_in">
                <h3 class="display-5">Iniciar sesión</h3>
                <span>o utilice su cuenta</span>

                <div class="body" action="#" id="form_inputSignIn">
                    <div class="row text-center justify-content-center">
                        <div class="col-md-8 form-group">
                            <input type="email" placeholder="Correo" class="form-control" id="email" onkeypress="validateEmail(this)"/>
                        </div>
                        <div class="col-md-8 form-group">
                            <input type="password" placeholder="Contraseña" class="form-control" id="password" oninput="validarTexto(this, 8, 50)"/>
                        </div>
                        <div class="col-md-12 form-group forgot">
                            <%--<span>¿Ha olvidado su contraseña?</span>--%>
                            <span>¿Ha olvidado su contraseña?</span><br />
                            <a class=" btn-link" href="/Views/RecuperarCuenta/RecuperarCuenta.aspx">Recupérela aquí</a>
                        </div>
                        <div class="col-md-12 form-group forgot">
                            <button type="button" class="btn bkg" onclick="iniciarSesion()">Acceder</button>
                        </div>

                    </div>
                </div>
            </div>
            <div class="form sign_up">
                <h3>Registrarse</h3>
                <div class="body" action="#" id="form_inputSignUp">
                    <div class="row text-center justify-content-center" >
                        <div class="col-md-8 form-group">
                            <input type="text" placeholder="Nombre(s)" class="form-control" id="txtNombre" onkeypress="return permite(event, 'car')" oninput="validarTexto(this, 3, 150)"/>
                        </div>
                        <div class="col-md-8 form-group">
                            <input type="text" placeholder="Apellido Paterno" class="form-control" id="txtApP" onkeypress="return permite(event, 'car')" oninput="validarTexto(this, 3, 150)"/>
                        </div>
                        <div class="col-md-8 form-group">
                            <input type="text" placeholder="Apellido Materno" class="form-control" id="txtApM" onkeypress="return permite(event, 'car')" oninput="validarTexto(this, 3, 150)"/>
                        </div>
                        <div class="col-md-8 form-group">
                            <select class="form-select form-select-sm select2" aria-label=".form-select-sm example" id="cboRol">
                                <option value="" disabled selected>Selecciona un rol</option>
                                <option value="1">Profesionista</option>
                                <option value="2">Cliente</option>
                            </select>
                        </div>
                        <div class="col-md-12 form-group forgot">
                            <button type="button" class="btn bkg" id="btnInscribir">Inscribirse</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="overlay">
            <div class="page page_signIn">
                <h3 class="display-5">Bienvenido de nuevo!</h3>
                <img src="../../Assests/Img/Logo%202.jpeg" width="200" height="200" alt="ProChat" class="img-fluid" />
                <br />
                <button class="btn btnSign-in">
                    Inscríbete 
                    <i class="bi bi-arrow-right"></i>
                </button>
            </div>
            <div class="page page_signUp">
                <h3 class="display-5">Hola Amigo!</h3>
                <img src="../../Assests/Img/Logo%201.jpeg" width="200" height="200" alt="ProChat" class="img-fluid" />
                <br />
                <button class="btn btnSign-up">
                    <i class="bi bi-arrow-left"></i> 
                    Inicia sesión
                </button>
            </div>
        </div>
    </div>

    <!--Inicio de Modales-->
    <!-- Modal Registro de Cliente-->
    <div class="modal fade" id="mRegistrClientes" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloRegistrarCliente">Registro Cliente</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#mRegistrClientes').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div></div>
                            <div class="col-md-12 form-group">
                                <label><b>Correo Electrónico</b></label>
                                <input type="email" placeholder="Correo Electrónico" class="form-control" id="txtCorreo" onkeypress="validateEmail(this)"/>
                            </div>
                            <div class="col-md-12 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Contraseña" class="form-control" id="txtPassword" oninput="validarTexto(this, 8, 50)"/>
                            </div>
                            <div class="col-md-12 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Repita su Contraseña" class="form-control" id="txtPassword2" oninput="validarTexto(this, 8, 50)"/>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn bkg" data-dismiss="modal" onclick="JavaScript:$('#mRegistrClientes').modal('hide');">Cancelar</button>
                    <button type="button" class="btn bkg" onclick="fn_AgregarCliente()">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Registro de Profesionista-->
    <div class="modal fade" id="mRegistroProfesionista" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalTituloRegistrarProfesionista">Registro Profesionista</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#mRegistroProfesionista').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                            <div></div>
                            <div class="col-md-4 form-group">
                                <label><b>Correo Electrónico</b></label>
                                <input type="email" placeholder="Correo Electrónico" class="form-control" id="txtCorreop" onkeypress="validateEmail(this)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Contraseña" class="form-control" id="txtPassword01" oninput="validarTexto(this, 8, 15)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Constraseña</b></label>
                                <input type="password" placeholder="Repita su Contraseña" class="form-control" id="txtPassword02" oninput="validarTexto(this, 8, 15)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Numero de telefono</b></label>
                                <input type="text" placeholder="Telefono" class="form-control" id="NumTelefono" maxlength="12" onkeypress="return permite(event, 'num')" oninput="validarTexto(this, 10, 12)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Profesión</b></label>
                                <input type="text" placeholder="Profesión" class="form-control" id="txtProfesion" onkeypress="return permite(event, 'car')" oninput="validarTexto(this, 3, 150)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Area</b></label>
                                <input type="text" placeholder="Area de especializacion" class="form-control" id="txtArea" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 3, 150)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Municipio</b></label>
                                <select class="form-select form-select-sm select2" aria-label=".form-select-sm example"  id="cboMunicipio" ></select>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Colonia</b></label>
                                <input type="text" placeholder="Colonia" class="form-control" id="txtColonia" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 3, 250)"/>
                            </div>
                            <div class="col-md-4 form-group">
                                <label><b>Calle</b></label>
                                <input type="text" placeholder="Calle" class="form-control" id="txtCalle" onkeypress="return permite(event, 'num_car')" oninput="validarTexto(this, 3, 250)"/>
                            </div>
                            <div class="col-md-12 form-group">
                                <label for="txtUbicacion"><b>Ubicacion</b></label>
                                <div id="map"></div>
                                <%--<div id="fn_Map" style="width: 100%; height: 200px;" width="600" height="450" ></div>--%>
                                <%--<div id="fn_Map" class="map-container" style="width: 100%; height: 100%;" height="450"></div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn bkg" data-dismiss="modal" onclick="JavaScript:$('#mRegistroProfesionista').modal('hide');">Cancelar</button>
                    <button type="button" class="btn bkg" onclick="fn_AgregarCliente()">Guardar</button>
                </div>
            </div>
        </div>
    </div>
    <!--FIn de Modales-->
    <!--FIN DEL CONTENIDO-->

    <!--INICIO FOOTER-->
    
    <!--FIN FOOTER-->
    <!-- link script -->
    <div id="ohsnap" style="z-index: 9999;"></div>
    <script src="Scripts/Jquery/RegistroUsuarios.js"></script>
</body>
</html>
