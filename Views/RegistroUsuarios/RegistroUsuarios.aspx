<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuarios.aspx.cs" Inherits="WebSecureBookings.Views.RegistroUsuarios.RegistroUsuarios" %>

<!DOCTYPE html>
<html>
<head>
    <title>WebSecureBooking</title>
    <!-- Incluimos los archivos CSS de Bootstrap -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <script src="https://kit.fontawesome.com/53601ddadf.js" crossorigin="anonymous"></script>
    <!-- CSS de Prism.js -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/themes/prism.min.css" />

    <link href="../../Scripts/Css/RegistroUsuarios.css" rel="stylesheet" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-lg navbar-light text-white">
            <a class="navbar-brand" href="index.html">
                <img src="../../Assests/Img/Logo%201.jpeg" width="50" height="50" alt="ProChat" class="img-fluid" />
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item active">
                        <a class="nav-link text-white" href="index.html">WebSecureBoking</a>
                    </li>
                </ul>
            </div>
        </nav>
    </header>
    <div class="container mt-4 mb-4">
        <div class="box">
            <div class="form sign_in">
                <h3>Iniciar sesión</h3>
                <span>o utilice su cuenta</span>

                <form id="form_input_login">
                    <div class="type">
                        <input type="email" class="form-control" placeholder="Correo" name="" id="email">
                    </div>
                    <div class="type">
                        <input type="password" class="form-control" placeholder="Contraseña" name="" id="password">
                    </div>

                    <div class="forgot">
                        <span>¿Ha olvidado su contraseña?</span>
                    </div>

                    <button class="btn bkg">Acceder</button>
                </form>
            </div>

            <div class="form sign_up">
                <h3>Regístrarse</h3>
                <%--<span>o utilice su correo electrónico para registrarse</span>--%>

                <form id="form_input_registro">
                    <div class="type">

                        <input type="text" class="form-control" placeholder="Nombre(s)" id="txtName">
                    </div>
                    <div class="type">

                        <input type="text" class="form-control" placeholder="Apellido Paterno" id="txtApP">
                    </div>
                    <div class="type">

                        <input type="text" class="form-control" placeholder="Apellido Materno" id="txtApM">
                    </div>
                    <div class="type">
                        <select class="form-control" id="txtRol">
                            <option value="1004">Profesionista</option>
                            <option value="1005">Cliente</option>
                        </select>
                    </div>

                    <button type="button" class="btn bkg" id="btnInscribir">Inscribirse</button>
                </form>
            </div>
        </div>


        <!-- Modal para registro de clientes -->
        <div class="modal fade" id="mRegistrClientes" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="nRol">Nuevo Rol</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="JavaScript:$('#exampleModalCenter').modal('hide');">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="tab-content">
                        <div class="row">
                             <form id="form_input_cliente">
                        <div class="type">
                            
                            <input type="email" class="form-control" placeholder="Correo Electronico" id="txtCorreo">
                        </div> 
                        <div class="type">
    
                            <input type="password" class="form-control" placeholder="Contraseña" id="txtPassword">
                        </div>
                        <div class="type">
                            
                            <input type="password" class="form-control" placeholder="Repita su Contraseña" id="txtPassword2">
                        </div> 
                    </form>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn bkg" data-dismiss="modal" onclick="JavaScript:$('#exampleModalCenter').modal('hide');">Cancelar</button>--%>
                    <button type="button" class="btn bkg" onclick="fn_AgregarCliente()">Guardar</button>
                </div>
            </div>
        </div>
    </div>

        <!-- Modal para registro de profesionistas -->
        <div class="modal fade" id="mRegistroProfesionista" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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

                            <div style="width: 50%; float:left">
                                <form id="form_input_profesionista">
                                     <div class="type">
                            
                            <input type="email" class="form-control" placeholder="Correo Electronico" id="txtCorreop">
                        </div> 
                        <div class="type">
    
                            <input type="password" class="form-control" placeholder="Contraseña" id="txtPassword01">
                        </div>
                        <div class="type">
                            
                            <input type="password" class="form-control" placeholder="Repita su Contraseña" id="txtPassword02">
                        </div> 
                         <div class="type">
                            
                            <input type="number" class="form-control" placeholder="Telefono" id="NumTelefono">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Area de especializacion" id="txtArea">
                        </div> 
                                </form>
                            </div> 
                            <div style="width: 50%; float:right">
                                <form id="from_input">
                                     <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Profesion" id="txtProfesion">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Municipio" id="txtMunicipio">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Colonia" id="txtColonia">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Calle" id="txtCalle">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Ubicacion" id="txtUbicacion">
                        </div> 
                        <div class="type">
                            
                            <input type="text" class="form-control" placeholder="Horario de atencion" id="txtHora">
                        </div> 
                                </form>
                            </div>


                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn bkg" data-dismiss="modal" onclick="JavaScript:$('#exampleModalCenter').modal('hide');">Cancelar</button>
                    <button type="button" class="btn bkg" onclick="fn_AgregarCliente()">Guardar</button>
                </div>
            </div>
        </div>
    </div>



        <div class="overlay">
            <div class="page page_signIn">
                <h3>Bienvenido de nuevo!</h3>
                <img src="../../Assests/Img/Logo%202.jpeg" width="200" height="200" alt="ProChat" class="img-fluid" />

                <button class="btn btnSign-in">Inscríbete <i class="bi bi-arrow-right"></i></button>
            </div>

            <div class="page page_signUp">
                <h3>Hola Amigo!</h3>
                <img src="../../Assests/Img/Logo%201.jpeg" width="200" height="200" alt="ProChat" class="img-fluid" />

                <button class="btn btnSign-up">
                    <i class="bi bi-arrow-left"></i>Inicia sesión</button>
            </div>
        </div>
    </div>




    <footer class="footer-sistema">
        <div class="container-">
            <div class="row">
                <div class="col-md-6 mx-auto text-center text-white">
                    <h3>Web Secure Booking</h3>
                    <p>Si tiene alguna pregunta o comentario,no dudes en ponerte en con nosotros.</p>
                    <p><strong>Correo electronico: </strong>lapaz_dsw@gmail.com</p>
                    <p><strong>Cuentas: </strong></p>

                </div>
                <div class="col-md-6 mx-auto text-center">
                    <%--<h3>Web Secure Booking</h3>
                        <p>Av. Universidad, Dolores Hidalgo GTO.</p>--%>
                    <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d7441.618527043131!2d-100.93666822228995!3d21.159987600000004!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x842b3fdfaaed04ad%3A0x2ce997e250d0e900!2sUtng!5e0!3m2!1ses-419!2smx!4v1688433682462!5m2!1ses-419!2smx" width="400" height="200" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </div>

            </div>
        </div>
        <div class="container-fluid text-white">
            <div class="row">
                <div class="col-md-12 text-center">
                    <p>Derechos de autor © 2023 WebSecureBooking . Todos los derechos reservados.</p>
                </div>
            </div>
        </div>
    </footer>

    <!-- Incluimos los archivos JavaScript de Bootstrap y nuestro archivo app.js -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <!-- JavaScript de Prism.js -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/prism.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/components/prism-clike.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/prism/1.24.1/components/prism-javascript.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/clipboard.js/2.0.8/clipboard.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/clipboard.js/2.0.8/clipboard.min.js"></script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>


    <!-- JavaScript -->
    <script src="../../Scripts/Jquery/ValidacionCaracteres.js"></script>
    <script src="../../Scripts/Jquery/jquery-3.6.3.js"></script>
    <script src="../../Scripts/Jquery/blockUI.js"></script>
    <script src="../../Scripts/Jquery/AccionesModales.js"></script>
    <script src="RegistroUsuarios.js"></script>
</body>
</html>
