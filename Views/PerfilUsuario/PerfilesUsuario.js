var iIdUsuario;
var iIdCaldendario;
var iIdDia;
// Define una variable global para almacenar la ubicación
var sUbicacionGlb = { latitud: 0, longitud: 0 };

document.addEventListener("DOMContentLoaded", function () {
    GetCiudades();
    // Obtén una referencia a los botones
    const btnActualizarCita = document.getElementById("btnActaulizarMapa");
    const btnAEliminarCita = document.getElementById("btnGuardarMapa");
    btnActualizarCita.style.display = "block";
    btnAEliminarCita.style.display = "none";
    mostrarInfoDeUsuario();

    $("#btnActaulizarMapa").click(function () {
        fn_block();
        fn_ActualizarMapa();
        const btnActualizarCita = document.getElementById("btnActaulizarMapa");
        const btnAEliminarCita = document.getElementById("btnGuardarMapa");
        btnActualizarCita.style.display = "none";
        btnAEliminarCita.style.display = "block";
        fn_unBlock();
    });
    $("#btnCancelar").click(function () {
        fn_block();
        const btnActualizarCita = document.getElementById("btnActaulizarMapa");
        const btnAEliminarCita = document.getElementById("btnGuardarMapa");
        btnActualizarCita.style.display = "block";
        btnAEliminarCita.style.display = "none";
        fn_unBlock();
    });

    $("#btnGuardarMapa").click(function () {
        fn_block();
        if (sUbicacionGlb.latitud === 0 && sUbicacionGlb.longitud === 0) {
            fn_Error("La ubicación tiene valores de latitud y longitud igual a cero.");
            setTimeout(function () {
                fn_Info("Favor de mover el puntero para obtener la ubicación");
            }, 5200); // 2000 milisegundos = 2 segundos
        } else {
            $.ajax({
                url: "PerfilUsuario.aspx/PutUbicacion", // URL de la solicitud
                data: JSON.stringify({ sUbicacion: JSON.stringify(sUbicacionGlb).replace(/[{}]/g, '') }), // Datos a enviar en formato JSON
                type: "POST", // Método de la solicitud (POST en este caso)
                dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                    //Acceder a las propiedades del objeto dentro del array
                    var obj = data.d;
                    console.log(obj);

                    if (obj.StatusCode == 200) {
                        fn_CerrarModal('modalMapa');
                        fn_Success(obj.Message);

                        const btnActualizarCita = document.getElementById("btnActaulizarMapa");
                        const btnAEliminarCita = document.getElementById("btnGuardarMapa");
                        btnActualizarCita.style.display = "block";
                        btnAEliminarCita.style.display = "none";
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
    });
});


//Función para llenar el combo de selección de Profesión

function mostrarInfoDeUsuario() {
    fn_block();
    $.ajax({
        url: "PerfilUsuario.aspx/GetUsuarioProfesionista", // URL de la solicitud
        data: "", // Datos a enviar 
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            var obj = data.d;
            if (obj.StatusCode == 200) {
                $("#nombre").val(obj.Data[0].sNombre);
                $("#apellidoP").val(obj.Data[0].sApellidoP);
                $("#apellidoM").val(obj.Data[0].sApellidoM);
                $("#profesion").val(obj.Data[0].sProfecion);
                $("#areaProfesion").val(obj.Data[0].sAreaProfesion);
                $("#telefono").val(obj.Data[0].stelefono);
                $("#correo").val(obj.Data[0].sCorreo);
                $("#colonia").val(obj.Data[0].sColonia);
                $("#calle").val(obj.Data[0].sCalle);
                $("#cboMunicipio").val(obj.Data[0].idMunicipio);
                $("#estado").val(obj.Data[0].sEstado);
                var checkbox = document.getElementById("desactivarPerfil");
                checkbox.checked = obj.Data[0].bEstatus;
                fn_ConvertirCoordenadas(obj.Data[0].sUbicacion);
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

function mostrarAlerta() {
    fn_AbrirModal('modalMapa');
}

function fn_ConvertirCoordenadas(Ubicacion) {
    fn_block();
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
    fn_unBlock();
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

function obtenerUbicacion() {
    return new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(
            (position) => resolve(position),
            (error) => reject(error)
        );
    });
}

async function fn_ActualizarMapa() {
    fn_block();
    mapboxgl.accessToken = 'pk.eyJ1Ijoiam9uYXRoYW5tZWRpbmEiLCJhIjoiY2t3am5ndGs3MHFxeDJ1cXZ5Z2Z0enZwdCJ9.-IeXyNNpFbRta6WDxASIrA';

    try {
        var position = await obtenerUbicacion();
        var latitud = position.coords.latitude;
        var longitud = position.coords.longitude;

        var map = new mapboxgl.Map({
            container: 'map',
            style: 'mapbox://styles/mapbox/streets-v11',
            center: [longitud, latitud],
            zoom: 16
        });

        var marker = new mapboxgl.Marker({ draggable: true })
            .setLngLat([longitud, latitud])
            .addTo(map);

        marker.on('dragend', function () {
            var coordinates = marker.getLngLat();
            sUbicacionGlb.latitud = coordinates.lat;
            sUbicacionGlb.longitud = coordinates.lng;
        });
    } catch (error) {
        fn_Error('Error al obtener la ubicación:', error);
    }
    fn_unBlock();
}


function UpdateUsario() {
    fn_block();
    // Datos Profesinista
    var inputProf = document.getElementById("profesion");
    var inputAreProf = document.getElementById("areaProfesion");
    var inputTel = document.getElementById("telefono");
    var inputCorr = document.getElementById("correo");
    var inputMun = document.getElementById("cboMunicipio");
    var inputCol = document.getElementById("colonia");
    var inputCall = document.getElementById("calle");
    var inputEst = document.getElementById("desactivarPerfil");

    var valProf = validarTexto(inputProf, 3, 150);
    var valAreProf = validarTexto(inputAreProf, 3, 150);
    var valTel = validarTexto(inputTel, 10, 12);
    var valEmail = validateEmail(inputCorr);
    var valCol = validarTexto(inputCol, 3, 150);
    var valCall = validarTexto(inputCall, 3, 150);

    if (valProf & valAreProf & valTel & valEmail & inputMun.value != "" & inputMun.value != "0" & inputMun.value != null
        & valCol & valCall) {

        $.ajax({
            url: "PerfilUsuario.aspx/PutUsuario", // URL de la solicitud
            data: JSON.stringify({
                sProfecion: inputProf.value, sAreaProfesion: inputAreProf.value, sTelefono: inputTel.value, sCorreo: inputCorr.value,
                idMunicipio: inputMun.value, sColonia: inputCol.value, sCalle: inputCall.value, sBEstatus: inputEst.checked
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
                    mostrarInfoDeUsuario();
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
        fn_Alert("Favor de contestar el formulario");
        fn_unBlock();
    }

    fn_unBlock();
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