var iIdUsuario; 
var iIdCaldendario;
var iIdDia;

document.addEventListener("DOMContentLoaded", function () {
    // Obtener el elemento del botón por su ID
    var btnLimpiar = document.getElementById("btnLimpiarFiltro");
    // Verificar si el elemento existe antes de intentar ocultarlo
    if (btnLimpiar) {
        // Ocultar el botón estableciendo la propiedad "display" a "none"
        btnLimpiar.style.display = "none";
    }

    //Llama a la función GetProfesionistas()
    GetProfesionistas();
    GetProfesion();
    GetCiudades();

    $("#btnBuscar").click(function () {
        fn_getBusqueda();
    });

    $("#btnLimpiarFiltro").click(function () {
        GetProfesionistas();
    });

    $("#btnGenerarCita").click(function () {
        fn_GenerarCita();
    });
});

//Función para llenar el combo de selección de liga
function GetProfesionistas() {
    fn_block();
    $("#div-profesionales").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetProfesionistas", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            var cboProfesion = document.getElementById("cboProfeciones");
            // Obtener el valor seleccionado
            cboProfesion.selectedIndex = 0;

            var cboCiudad = document.getElementById("cboCiudades");
            // Obtener el valor seleccionado
            cboCiudad.selectedIndex = 0;


            if (obj.StatusCode == 200) {
                $("#div-profesionales").html(obj.Resultado);
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
            }

            var btnLimpiar = document.getElementById("btnLimpiarFiltro");
            if (btnLimpiar) {
                btnLimpiar.style.display = "none";
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

//Función para llenar el combo de selección de Profesión
function GetProfesion() {
    fn_block();
    $("#cboProfeciones").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetProfesiones", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#cboProfeciones").html(obj.Resultado);
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function GetCiudades() {
    fn_block();
    $("#cboCiudades").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetCiudades", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#cboCiudades").html(obj.Resultado);
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_getBusqueda() {
    fn_block();
    // Obtener el elemento select por su ID
    var cboProfesion = document.getElementById("cboProfeciones");
    // Obtener el valor seleccionado
    var sProfesion = cboProfesion.value;

    var cboCiudad = document.getElementById("cboCiudades");
    // Obtener el valor seleccionado
    var sCiudad = cboCiudad.value;
    // Mostrar el valor seleccionado en la consola
    if (sCiudad != 0 || sProfesion != 0) {
        $("#div-profesionales").html("");
        //Realiza una solicitud AJAX utilizando jQuery
        $.ajax({
            url: "PerfilesProfesionistas.aspx/GetProfesionistasFiltro", // URL de la solicitud
            data: JSON.stringify({ sProfesion: sProfesion, sEstado: sCiudad }), // Datos a enviar (vacío en este caso)
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d[0];

                if (obj.StatusCode == 200) {

                    $("#div-profesionales").html(obj.Resultado);
                    var btnLimpiar = document.getElementById("btnLimpiarFiltro");
                    // Verificar si el elemento existe antes de intentar ocultarlo
                    if (btnLimpiar) {
                        // Mostrar el botón estableciendo la propiedad "display" a "block"
                        btnLimpiar.style.display = "block";
                    }
                } else {
                    fn_Error(obj.StatusCode + "\n" + obj.Message);
                }
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Error(status); // Imprime el estado del error
            }
        });
    } else {
        error = '<br /><span style="color: red;">Favor de seleccionar almenos una profesión o ciudad</span>';
        $('#btnLimpiarFiltro').after($(error).fadeOut(2500));
    }
    fn_unBlock();
}

function mostrarInfo(idUsuario) {
    fn_block();
    $("#modalGenerarCita").html("");
    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetProfesionista", // URL de la solicitud
        data: JSON.stringify({ idUsuario: idUsuario }), // Datos a enviar 
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            var obj = data.d[0];
            if (obj.StatusCode == 200) {
                $("#modalGenerarCita").html(obj.Resultado);
                // Llamar a la función para generar el mapa con las coordenadas proporcionadas
                fn_ConvertirCoordenadas(obj.Message);
                fn_AbrirModal('modalGenerarCita');
                iIdUsuario = idUsuario;
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_ConvertirCoordenadas(Ubicacion) {
    var latitud = 0;
    var longitud = 0;

    // Verificar el formato de las coordenadas
    if (Ubicacion.includes('"latitud":') && Ubicacion.includes('"longitud":')) {
        // Formato: "latitud":21.1952063,"longitud":-100.9567593
        var coordenadas = Ubicacion.match(/"latitud":(.*?),"longitud":(.*?)$/);
        latitud = parseFloat(coordenadas[1]);
        longitud = parseFloat(coordenadas[2]);
    } else if (Ubicacion.includes('° N') && Ubicacion.includes('° W')) {
        // Formato: 20.6597° N, 103.3496° W
        var partes = Ubicacion.split(',');

        for (var i = 0; i < partes.length; i++) {
            var parte = partes[i].trim();

            if (parte.includes('° N') || parte.includes('° S')) {
                // Extraer la latitud y eliminar los caracteres no deseados
                latitud = parseFloat(parte.replace('° N', '').replace('° S', '').trim());

                // Verificar si la latitud está en el hemisferio sur
                if (parte.includes('° S')) {
                    latitud = -latitud;
                }
            } else if (parte.includes('° E') || parte.includes('° W')) {
                // Extraer la longitud y eliminar los caracteres no deseados
                longitud = parseFloat(parte.replace('° E', '').replace('° W', '').trim());

                // Ajustar el signo de la longitud según la dirección
                if (parte.includes('° W')) {
                    longitud = -longitud;
                }
            }
        }
    } else {
        // Formato no reconocido
        fn_Error('Formato de coordenadas no válido');
        return;
    }

    fn_GenerarMap(latitud, longitud);
}

function fn_GenerarMap(latitud, longitud) {
    // Configura tu token de acceso a Mapbox
    mapboxgl.accessToken = 'pk.eyJ1Ijoiam9uYXRoYW5tZWRpbmEiLCJhIjoiY2t3am5ndGs3MHFxeDJ1cXZ5Z2Z0enZwdCJ9.-IeXyNNpFbRta6WDxASIrA';

    // Crea una instancia del mapa
    var map = new mapboxgl.Map({
        container: 'map',
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [longitud, latitud], // Coordenadas del centro del mapa (longitud, latitud)
        zoom: 16 // Nivel de zoom inicial
    });

    // Agrega un marcador en la ubicación deseada
    var marker = new mapboxgl.Marker()
        .setLngLat([longitud, latitud]) // Coordenadas del marcador (longitud, latitud)
        .addTo(map);

}

function fn_GenerarCalendario() {
    fn_block();
    fn_AbrirModal('modalCalendario');
    fn_unBlock();
    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetProfesionistaCalendario", // URL de la solicitud
        data: JSON.stringify({ idUsuario: iIdUsuario }), // Datos a enviar 
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            var obj = data.d;

            if (obj.StatusCode == 200) {
                $("#nav-tabContent").html(obj.Resultado);
                // Seleccionar el elemento por defecto
                $("#nav-tabContent .tab-pane:first").addClass("active show");
            } else {
                fn_Error(obj.StatusCode + "\n" + obj.Message);
            }

            

        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
            fn_unBlock();
        }
    });
}

function fn_GenerarCita() {
    fn_block();
    var error = "";

    var valorSpanFecha = document.getElementById("txtFecha" + idDia).innerHTML;

    var sMotivo = $("#txtMotivo").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("txtMotivo");

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);

    if (validacion) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "PerfilesProfesionistas.aspx/PostAcata", // URL de la solicitud
            data: JSON.stringify({ idUsuario: iIdUsuario, sMotivo: sMotivo, idCalendario: iIdCaldendario, secha: valorSpanFecha }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;;

                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalCalendario');
                    fn_CerrarModal('modalRegistroCita');
                    fn_GenerarCalendario();
                    fn_success(obj.Message);

                } else {
                    fn_Error(obj.StatusCode + "\n" + obj.Message);
                }
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Error(status); // Imprime el estado del error
            }
        });
    }

    fn_unBlock();
}

function fn_AbiriModalGenerarCita(idDia) {
    

    // Obtener el valor de un campo <span> con un id específico
    var valorSpanIdCalendario = document.getElementById("txtIdCalendario"+idDia).innerHTML;
    var valorSpanIdProfesionista = document.getElementById("txtIdUsuProfesionista" + idDia).innerHTML;
    var valorSpanFecha = document.getElementById("txtFecha" + idDia).innerHTML;
    var valorSpanHoraIni = document.getElementById("txtHoaraInicio" + idDia).innerHTML;
    var valorSpanHoraFin = document.getElementById("txtHoraFechaFin" + idDia).innerHTML;

    iIdUsuario = valorSpanIdProfesionista;
    iIdCaldendario = valorSpanIdCalendario;
    iIdDia = idDia;

    $("#txtFecha").html(valorSpanFecha + " " + valorSpanHoraIni + " - " + valorSpanHoraFin);
    fn_AbrirModal('modalRegistroCita');
}