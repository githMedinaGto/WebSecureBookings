var iIdUsuario; 
var iIdCaldendario;

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
                console.log(obj.StatusCode);
                console.log(obj.Message);
            }

            var btnLimpiar = document.getElementById("btnLimpiarFiltro");
            if (btnLimpiar) {
                btnLimpiar.style.display = "none";
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
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
                console.log(obj.StatusCode);
                console.log(obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
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
                console.log(obj.StatusCode);
                console.log(obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
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
                    console.log(obj.StatusCode);
                    console.log(obj.Message);
                }
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                console.log(status); // Imprime el estado del error
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
                console.log(obj.StatusCode);
                console.log(obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_ConvertirCoordenadas(Ubicacion) {
    // Dividir la cadena de coordenadas en latitud y longitud
    var partes = Ubicacion.split(',');

    // Extraer la latitud y eliminar los caracteres no deseados
    var latitud = parseFloat(partes[0].replace('° N', '').trim());

    // Extraer la longitud y eliminar los caracteres no deseados
    var longitud = parseFloat(partes[1].replace('° W', '').trim());

    // Ajustar el signo de la longitud según la dirección
    longitud = -longitud;

    // Verificar si la latitud está en el hemisferio sur
    if (partes[0].includes('° S')) {
        latitud = -latitud;
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

    $.ajax({
        url: "PerfilesProfesionistas.aspx/GetProfesionistaCalendario", // URL de la solicitud
        data: JSON.stringify({ idUsuario: iIdUsuario }), // Datos a enviar 
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            var obj = data.d;
            var arrObj = obj.Data;

            var nuevosEventos = arrObj.map(function (evento) {
                var resourceId = evento.idCalendario.toString();
                var id = evento.idUsuarioP.toString();
                var start = generarFechaCompleta(evento.iDia, evento.sHorarioInicio);
                var end = generarFechaCompleta(evento.iDia, evento.sHorarioFin);
                var title = 'Disponible';
                var description = 'Fecha y hora disponible';

                return {
                    id: id,
                    resourceId: resourceId,
                    start: start,
                    end: end,
                    title: title,
                    description: description
                };
            });

            var resources = arrObj.map(function (evento) {
                var id = evento.idCalendario;
                var sDia = obtenerNumeroDiaSemana(evento.iDia);
                var title = 'Disponible del ' + sDia;

                return {
                    id: id.toString(),
                    title: title
                }
            });

            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                locale: 'es',
                initialView: 'resourceTimelineWeek',
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'resourceTimelineWeek,resourceTimelineDay'
                },
                views: {
                    resourceTimelineWeek: {
                        type: 'resourceTimeline',
                        duration: { weeks: 1 },
                        buttonText: 'Semana'
                    },
                    resourceTimelineDay: {
                        type: 'resourceTimeline',
                        duration: { days: 1 },
                        buttonText: 'Día'
                    }
                },
                resources: resources,
                events: nuevosEventos,
                slotDuration: '01:0:00',
                slotLabelInterval: '01:00',
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    omitZeroMinute: false,
                    hour12: false
                },
                nowIndicator: true,
                editable: false,
                eventTextColor: 'white',
                eventRender: function (info) {
                    // Personalizar la apariencia de los eventos en el calendario
                    // Puedes cambiar el formato o agregar más detalles según tus necesidades
                    var eventTitle = info.event.title;
                    var eventDescription = info.event.extendedProps.description;
                    info.el.querySelector('.fc-title').innerHTML = eventTitle + '<br/>' + eventDescription;
                },
                eventClick: function (info) {
                    iIdCaldendario = info.event._def.resourceIds[0].toString();
                    iIdUsuario = info.event.id;
                    var eventStart = info.event.start;

                    var sFecha = moment(eventStart).locale('es').format('MMMM D, YYYY h:mm A');
                    $("#txtFecha").html(sFecha);
                    fn_AbrirModal('modalRegistroCita');
                    //alert('Evento seleccionado: ' + eventTitle + '\nInicio: ' + eventStart + '\nFin: ' + eventEnd);
                }
            });

            calendar.render();

            fn_AbrirModal('modalCalendario');
            fn_unBlock();

        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
            fn_unBlock();
        }
    });
}

function fn_GenerarCita() {
    fn_block();
    var error = "";
    var sMotivo = $("#txtMotivo").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("txtMotivo");

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);
    if (iIdCaldendario == null) {
        iIdCaldendario == iIdCaldendario.toString();
    }

    if (validacion) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "PerfilesProfesionistas.aspx/PostAcata", // URL de la solicitud
            data: JSON.stringify({ idUsuario: iIdUsuario, sMotivo: sMotivo }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                console.log(obj);

                //if (obj.StatusCode == 200) {
                //    fn_CerrarModal('modalCalendario');
                //    fn_GenerarCalendario();
                //    console.log(obj.Message);

                //} else {
                //    console.log(obj.StatusCode);
                //    console.log(obj.Message);
                //}
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                console.log(status); // Imprime el estado del error
            }
        });
    }

    fn_unBlock();
}


function generarFechaCompleta(numeroDia, hora12) {
    // Generar la fecha a partir del número de día
    var fecha = generarFecha(numeroDia);

    // Convertir la hora en formato de 12 horas a formato de 24 horas
    var hora24 = convertirHora12a24(hora12);

    // Combinar la fecha y la hora para obtener la fecha completa
    var fechaCompleta = fecha + 'T' + hora24 + ':00';

    return fechaCompleta;
}

function generarFecha(numeroDia) {
    var fecha = new Date();
    fecha.setDate(fecha.getDate() + numeroDia - fecha.getDay());
    var anio = fecha.getFullYear();
    var mes = String(fecha.getMonth() + 1).padStart(2, '0');
    var dia = String(fecha.getDate()).padStart(2, '0');
    return anio + '-' + mes + '-' + dia;
}

function convertirHora12a24(hora12) {
    var [hora, minutos, periodo] = hora12.split(/:|\s/);
    hora = parseInt(hora);
    minutos = parseInt(minutos);

    if (periodo.toUpperCase() === 'PM' && hora !== 12) {
        hora += 12;
    } else if (periodo.toUpperCase() === 'AM' && hora === 12) {
        hora = 0;
    }

    var hora24 = String(hora).padStart(2, '0') + ':' + String(minutos).padStart(2, '0');
    return hora24;
}

function obtenerNumeroDiaSemana(diaSemana) {
    switch (diaSemana) {
        case 1:
            return "Lunes";
        case 2:
            return "Martes";
        case 3:
            return "Miércoles";
        case 4:
            return "Jueves";
        case 5:
            return "Viernes";
        default:
            return "Día no válido";
    }
}