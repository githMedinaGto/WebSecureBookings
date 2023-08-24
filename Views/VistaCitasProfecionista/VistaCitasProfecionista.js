var iIdCita;
var iIdDia;
document.addEventListener("DOMContentLoaded", function () {
    //Llama a las funciones
    GetCitasProximas();
    GetCitas();

    $("#btnAgregarCitas").click(function () {
        fn_AbrirModal('modalAgregarFechas');
    });

    $("#btnGenerarCita").click(function () {
        fn_GenerarActCita();
    });

    let appointments = {};

    $('#btnGenerar').click(function () {
        const diaSemana = $('#diaSemana').val();
        const horaInicio = $('#horaInicio').val();
        const horaFin = $('#horaFin').val();

        if (!diaSemana || !horaInicio || !horaFin) {
            fn_Alert("Por favor complete todos los campos.");
            return;
        }

        const formattedHoraInicio = formatTime(horaInicio);
        const formattedHoraFin = formatTime(horaFin);

        if (formattedHoraInicio === formattedHoraFin) {
            fn_Alert("La hora de inicio y fin no pueden ser iguales.");
            return;
        } else if (formattedHoraInicio > formattedHoraFin) {
            fn_Alert("La hora de inicio debe ser antes que la hora de fin.");
            return;
        }

        if (appointments[diaSemana]) {
            for (let i = 0; i < appointments[diaSemana].length; i++) {
                const appointment = appointments[diaSemana][i];
                if (
                    (formattedHoraInicio >= appointment.horaInicio && formattedHoraInicio < appointment.horaFin) ||
                    (formattedHoraFin > appointment.horaInicio && formattedHoraFin <= appointment.horaFin)
                ) {
                    fn_Alert("Ya existe una cita en ese horario para este día.");
                    return;
                }
            }
        } else {
            appointments[diaSemana] = [];
        }

        appointments[diaSemana].push({
            horaInicio: formattedHoraInicio,
            horaFin: formattedHoraFin
        });

        updateAppointmentsDiv();
    });

    function formatTime(time) {
        const [hour, minute] = time.split(':');
        let formattedHour = parseInt(hour);
        const ampm = formattedHour >= 12 ? 'PM' : 'AM';
        if (formattedHour > 12) {
            formattedHour -= 12;
        } else if (formattedHour === 0) {
            formattedHour = 12;
        }
        return formattedHour + ':' + minute + ' ' + ampm;
    }

    function updateAppointmentsDiv() {
        const appointmentsDiv = $('#appointmentsDiv');
        appointmentsDiv.empty();

        for (const dia in appointments) {
            const dayDiv = $('<div class="appointment-day"></div>');
            dayDiv.append($('<h4>Día de la Semana: ' + dia + '</h4>'));

            for (let i = 0; i < appointments[dia].length; i++) {
                const appointment = appointments[dia][i];
                const appointmentInfo = $('<div class="appointment-info"></div>');
                appointmentInfo.append($('<p>Hora: ' + appointment.horaInicio + ' - ' + appointment.horaFin + '</p>'));
                dayDiv.append(appointmentInfo);
            }

            appointmentsDiv.append(dayDiv);
        }
    }


    $('#btnGuardarFechas').click(function () {
        const citasGuardadas = [];

        for (const dia in appointments) {
            const citasDia = appointments[dia];
            const citasDiaGuardadas = [];

            for (let i = 0; i < citasDia.length; i++) {
                const cita = citasDia[i];
                const citaString = "Inicio: " + cita.horaInicio + " Fin: " + cita.horaFin;
                citasDiaGuardadas.push(citaString);
            }

            if (citasDiaGuardadas.length > 0) {
                citasGuardadas.push(dia + ": " + citasDiaGuardadas.join("; "));
            }
        }

        const citasString = citasGuardadas.join("\n");
        console.log(citasString);

        // Llamar a la función para guardar las citas
        fn_GuardarCitas(citasString);
    });
});

function GetCitasProximas() {
    fn_block();
    $("#divCitasprox").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "VistaCitasProfecionista.aspx/GetCitasProximas", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#divCitasprox").html(obj.Resultado);

                // Espera a que el DOM esté listo
                $(document).ready(function () {

                    const table = new DataTable('#example', {
                        language: {
                            url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-MX.json'
                        },
                        dom: 'Bfrtip',
                        scrollX: true
                    });
                });
            } else {
                fn_Alert(obj.StatusCode + "\n" + obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Alert(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function GetCitas() {
    fn_block();
    $("#divCitas").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "VistaCitasProfecionista.aspx/GetCitas", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#divCitas").html(obj.Resultado);
            } else {
                fn_Alert(obj.StatusCode + "\n" + obj.Message);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Alert(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_cambiarfecha(idCita) {
    fn_block();
    iIdCita = idCita;
    fn_AbrirModal('modalCalendario');
    fn_unBlock();
    $.ajax({
        url: "VistaCitasProfecionista.aspx/GetProfesionistaCalendario", // URL de la solicitud
        data: "", // Datos a enviar 
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

function fn_GuardarCitas(citasString) {
    if (citasString != null) {
        $.ajax({
            url: "VistaCitasProfecionista.aspx/GenerarFechasHoras",
            data: JSON.stringify({ datos: citasString }),
            type: "POST",
            dataType: "json", // Cambiado a "json" en lugar de "JSON"
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var obj = data.d;

                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalCalendario');
                    fn_Success(obj.Message);
                } else {
                    fn_CerrarModal('modalCalendario');
                    fn_Error(obj.StatusCode + "\n" + obj.Message);
                }
            },
            error: function (xhr, status, error) {
                fn_Error(status);
                fn_unBlock();
            }
        });
    } else {
        fn_Alert('Se debe de agregar al menos un valor');
    }
}

function fn_GenerarActCita() {
    fn_block();
    var error = "";
    var valorSpanIdCalendario = document.getElementById("txtIdCalendario" + iIdDia).innerHTML;
    var valorSpanFecha = document.getElementById("txtFecha" + iIdDia).innerHTML;

    var sMotivo = $("#txtMotivo").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("txtMotivo");

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);

    if (validacion) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "VistaCitasProfecionista.aspx/PostAcAcata", // URL de la solicitud
            data: JSON.stringify({ sMotivo: sMotivo, idCalendario: iIdCaldendario, sFecha: valorSpanFecha, idActa: iIdCita }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalCalendario');
                    fn_CerrarModal('modalRegistroCita');
                    GetCitasProximas();
                    GetCitas();
                    fn_Succcess(obj.Message);

                } else {
                    fn_Error(obj.StatusCode + "\n" + obj.Message);
                }
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Error(status); // Imprime el estado del error
            }
        });
    } else {

    }

    fn_unBlock();
}

function fn_AbiriModalGenerarCita(idDia) {

    iIdDia = idDia;
    // Obtener el valor de un campo <span> con un id específico
    var valorSpanIdCalendario = document.getElementById("txtIdCalendario" + idDia).innerHTML;
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