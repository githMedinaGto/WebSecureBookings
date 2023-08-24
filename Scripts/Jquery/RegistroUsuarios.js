//Se ejecuta cuando el contenido del documento HTML ha sido completamente cargado
document.addEventListener("DOMContentLoaded", function () {
    fn_limpiarCampos();
    fn_Map();
});

const container = document.querySelector('.container')
const btnSignIn = document.querySelector('.btnSign-in')
const btnSignUp = document.querySelector('.btnSign-up')

btnSignIn.addEventListener('click', () => {
    container.classList.add('active')
})

btnSignUp.addEventListener('click', () => {
    container.classList.remove('active')
})

$("#btnInscribir").click(function () {
    fn_block();
    var inputN = document.getElementById("txtNombre");
    var inputP = document.getElementById("txtApP");
    var inputM = document.getElementById("txtApM");

    // Realizar la validación de los campos
    var vNombre = validarTexto(inputN, 3, 150);
    var vApP = validarTexto(inputP, 3, 150);
    var cboRol = document.getElementById("cboRol");
    // Obtener el valor seleccionado
    var sRol = cboRol.value;
    if (vNombre & vApP & sRol != "0" & sRol != "") {
        if (sRol == "1") {
            fn_AbrirModal('mRegistroProfesionista');
            GetCiudades();
            fn_Map();
            fn_unBlock();
        }
        else {
            fn_AbrirModal('mRegistrClientes');
            fn_unBlock();
        }
        
    } else {
        fn_Alert("Favor de contestar el formulario");
        fn_unBlock();
    }
    
});

function fn_AgregarCliente() {
    fn_block();
    var inputN = document.getElementById("txtNombre").value;
    var inputP = document.getElementById("txtApP").value;
    var inputM = document.getElementById("txtApM").value;
    var cboRol = document.getElementById("cboRol").value;
    //Datos Profesinista
    var inputCorrP = document.getElementById("txtCorreop");
    var inputPassP = document.getElementById("txtPassword01");
    var inputVerPassP = document.getElementById("txtPassword02");
    var inputTelefono = document.getElementById("NumTelefono");
    var inputProfesion = document.getElementById("txtProfesion");
    var inputArea = document.getElementById("txtArea");
    var cboMunicipio = document.getElementById("cboMunicipio").value;
    var inputColonia = document.getElementById("txtColonia");
    var inputCalle = document.getElementById("txtCalle");

    var valEmail = validateEmail(inputCorrP);
    var valPassP = validarTexto(inputPassP, 8, 50);
    var valVerPassP = validarTexto(inputVerPassP, 8, 50);
    var valTel = validarTexto(inputTelefono, 10, 12);
    var valPro = validarTexto(inputProfesion, 3, 150);
    var valArea = validarTexto(inputArea, 3, 150);
    var valCol = validarTexto(inputColonia, 3, 250);
    var valCall = validarTexto(inputCalle, 3, 250);
    

    //Datos de cliente
    var inputCorreoCl = document.getElementById("txtCorreo");
    var inputPassC = document.getElementById("txtPassword");
    var inputVerPassC = document.getElementById("txtPassword2");

    var valCorreoCl = validateEmail(inputCorreoCl);
    var valPassC = validarTexto(inputPassC, 8, 50);
    var valVerPassC = validarTexto(inputVerPassC, 8, 50);
    // Realizar la validación de los campos
    if (cboRol == "1") {
        
        if (valEmail & valPassP & valVerPassP & valTel & valPro & valArea & valCol & valCall & cboMunicipio != "" || cboMunicipio != "0" || cboMunicipio != null) {
            if (inputPassP.value == inputVerPassP.value) {
                fn_Map(function (ubicacion) {
                    if (ubicacion) {
                        var sUbicacionString = JSON.stringify(ubicacion).replace(/[{}]/g, '');
                        $.ajax({
                            url: "/Index.aspx/PostCrearProfesionista", // URL de la solicitud
                            data: JSON.stringify({
                                sRol: cboRol, sNombre: inputN, sApellidoP: inputP, sApellidoM: inputM,
                                sCorreop: inputCorrP.value, sPassword01: inputPassP.value, sTelefono: inputTelefono.value,
                                sArea: inputArea.value, sProfesion: inputProfesion.value, sMunicipio: cboMunicipio,
                                sColonia: inputColonia.value, sCalle: inputCalle.value, sUbicacion: sUbicacionString
                            }), // Datos a enviar en formato JSON
                            type: "POST", // Método de la solicitud (POST en este caso)
                            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                                //Acceder a las propiedades del objeto dentro del array
                                var obj = data.d;
                                if (obj.StatusCode == 200) {
                                    location.reload();
                                    fn_Success(obj.Message);
                                    fn_limpiarCampos();
                                } else {
                                    fn_Error(obj.Message);
                                }
                                sUbicacion = {};
                                fn_unBlock();
                            },
                            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                                fn_Alert(status); // Imprime el estado del error
                                fn_unBlock();
                            }
                        });
                    } else {
                        // Maneja el caso en el que no se pueda obtener la ubicación
                        fn_Info("No se pudo obtener la ubicación");
                    }
                });
            } else {
                fn_Alert("Error de contraseñas diferentes");
                fn_unBlock();
            }
        } else {
            fn_Alert("Favor de contestar el formulario");
            fn_unBlock();
        }
    } else {
        if (valCorreoCl & valPassC & valVerPassC) {
            if (valPassC == valVerPassC) {
                $.ajax({
                    url: "/Index.aspx/PostCrearCliente", // URL de la solicitud
                    data: JSON.stringify({ sRol: cboRol, sNombre: inputN, sApellidoP: inputP, sApellidoM: inputM, sCorreo: inputCorreoCl.value, sPassword: inputPassC.value }), // Datos a enviar en formato JSON
                    type: "POST", // Método de la solicitud (POST en este caso)
                    dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                    contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                    success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                        //Acceder a las propiedades del objeto dentro del array
                        var obj = data.d;
                        if (obj.StatusCode == 200) {
                            location.reload();
                            fn_Success(obj.Message);
                            fn_limpiarCampos();
                        } else {
                            fn_Error(obj.Message);
                        }
                        fn_unBlock();
                    },
                    error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                        fn_Alert(status);
                        fn_unBlock();
                    }
                });
            } else {
                fn_Alert("Error de contraseñas diferentes");
                fn_unBlock();
            }
        } else {
            fn_Alert("Favor de contestar el formulario");
            fn_unBlock();
        }
    }
    
}

function iniciarSesion() {
    fn_block();
    var usuario = document.getElementById("email");
    var contrasena = document.getElementById("password");

    // Realizar la validación de los campos
    var validacionEmail = validateEmail(usuario);
    var validacionPassword = validarTexto(contrasena, 8, 50);

    if (validacionEmail && validacionPassword) {
        $.ajax({
            url: "/Index.aspx/IniciarSesion", // URL de la solicitud
            data: JSON.stringify({ usuario: usuario.value, contrasena: contrasena.value }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                if (obj.StatusCode == 200) {
                    fn_unBlock();
                    fn_Success(obj.Message);
                    window.location.href = obj.Data;
                    fn_limpiarCampos();
                } else {
                    fn_Error(obj.Message);
                }
                fn_unBlock();
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Alert(status + "\n en las credenciales");; // Imprime el estado del error
                fn_unBlock();
            }
        });
    } else {
        fn_Alert("Error en sus credenciales");
        fn_unBlock();
    }
}

function fn_Map(callback) {
    fn_block();
    $("#map").html("");
    // Configura tu token de acceso a Mapbox
    mapboxgl.accessToken = 'pk.eyJ1Ijoiam9uYXRoYW5tZWRpbmEiLCJhIjoiY2t3am5ndGs3MHFxeDJ1cXZ5Z2Z0enZwdCJ9.-IeXyNNpFbRta6WDxASIrA';

    // Obtén la ubicación del usuario en tiempo real
    navigator.geolocation.getCurrentPosition(function (position) {
        var latitud = position.coords.latitude;
        var longitud = position.coords.longitude;

        // Crea una instancia del mapa
        var map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [longitud, latitud], // Coordenadas del centro del mapa (longitud, latitud)
            zoom: 16 // Nivel de zoom inicial
        });

        // Crea un marcador en la ubicación del usuario
        var marker = new mapboxgl.Marker({ draggable: true })
            .setLngLat([longitud, latitud]) // Coordenadas del marcador (longitud, latitud)
            .addTo(map);

        // Maneja el evento de arrastre del marcador para obtener las coordenadas actualizadas
        marker.on('dragend', function () {
            var coordinates = marker.getLngLat();
            sUbicacionGlb.latitud = coordinates.lat;
            sUbicacionGlb.longitud = coordinates.lng;
        });

        // Llama al callback con la ubicación actualizada
        callback({ latitud: latitud, longitud: longitud });

    }, function (error) {
        // Maneja el error en caso de que no se pueda obtener la ubicación
        fn_Error(error);
        callback(null);
    });

    fn_unBlock();
}


function fn_limpiarCampos() {
    // Obtener todos los campos de entrada y selección
    var campos = document.querySelectorAll('input, select');

    // Recorrer y limpiar los campos
    campos.forEach(function (campo) {
        // Reiniciar el valor del campo
        campo.value = '';

        // Si es un campo select, restablecer la selección al primer valor
        if (campo.tagName === 'SELECT') {
            campo.selectedIndex = 0;
        }
    });
}

function GetCiudades() {
    fn_block();
    $("#cboMunicipio").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "/Index.aspx/GetCiudades", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#cboMunicipio").html(obj.Resultado);
                fn_unBlock();
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
                fn_unBlock();
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
            fn_unBlock();
        }
    });
}