var idAcata;
var iIdUsuario;
var iIdCaldendario;
var iIdDia;

document.addEventListener("DOMContentLoaded", function () {
    const btnActualizarCita = document.getElementById("btnActualizarCita");
    const btnAEliminarCita = document.getElementById("btnAEliminarCita");
    btnActualizarCita.style.display = "block";
    btnAEliminarCita.style.display = "none";
    GetTablaCitas();

    $("#btn_Calificar").click(function () {
        fn_Comentario();
    });
    $("#btnActualizarCita").click(function () {
        fn_ActualizarCita();
    });
    $("#btnAEliminarCita").click(function () {
        // Obtén una referencia a los botones
        const btnActualizarCita = document.getElementById("btnActualizarCita");
        const btnAEliminarCita = document.getElementById("btnAEliminarCita");
        btnActualizarCita.style.display = "none";
        btnAEliminarCita.style.display = "block";
        fn_EliminarCita();
        
    });
    $("#btnEliminarCita").click(function () {
        // Obtén una referencia a los botones
        const btnActualizarCita = document.getElementById("btnActualizarCita");
        const btnAEliminarCita = document.getElementById("btnAEliminarCita");
        btnActualizarCita.style.display = "none";
        btnAEliminarCita.style.display = "block";
        fn_AbrirModal('modalActualizarCita');
    });
    $("#btnCancelar").click(function () {
        const btnActualizarCita = document.getElementById("btnActualizarCita");
        const btnAEliminarCita = document.getElementById("btnAEliminarCita");
        btnActualizarCita.style.display = "block";
        btnAEliminarCita.style.display = "none";
    });

});

//Función para llenar el combo de selección de liga

function GetTablaCitas() {
    fn_block();
    $("#divCitas").html("");
    //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "VistaCitas.aspx/GetCitas", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#divCitas").html(obj.Resultado);

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
                fn_Error(obj.StatusCode);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_Calificar(id) {
    idAcata = id;
    fn_AbrirModal('modalCalificar');
}

function fn_Comentario() {
    var slCsl = $("#valorCalificacion").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("comentario");
    var sDescripcion = $("#comentario").val();

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);

    if (validacion & slCsl != "" & slCsl != null) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "VistaCitas.aspx/GenComentario", // URL de la solicitud
            data: JSON.stringify({ selectca: slCsl, txtArea: sDescripcion, idActa: idAcata }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalCalificar');
                    fn_Success(obj.Message);
                    GetTablaCitas();
                } else {
                    fn_Error(obj.StatusCode + "\n" + obj.Message);
                }
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Error(status); // Imprime el estado del error
            }
        });
    } else {
        fn_Error("Favor de contestar el formulario");
    }
}


function fn_Eliminar(idActa, idUsProf) {
    idAcata = idActa;
    fn_AbrirModal('modalActCita');
    fn_unBlock();
    $.ajax({
        url: "VistaCitas.aspx/GetProfesionistaCalendario", // URL de la solicitud
        data: JSON.stringify({ idUsuario: idUsProf }), // Datos a enviar 
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

function fn_AbiriModalGenerarCita(idDia) {
    fn_block();
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
    fn_AbrirModal('modalActualizarCita');
    fn_unBlock();
}

function fn_ActualizarCita() {
    fn_block();
    var valorSpanFecha = document.getElementById("txtFecha" + iIdDia).innerHTML;

    var sMotivo = $("#txtMotivo").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("txtMotivo");

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);

    if (validacion) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "VistaCitas.aspx/PutAcata", // URL de la solicitud
            data: JSON.stringify({ sMotivo: sMotivo, sFecha: valorSpanFecha, idActa: idAcata, idCalendario: iIdCaldendario }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                console.log(obj);

                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalActualizarCita');
                    fn_CerrarModal('modalActCita');
                    GetTablaCitas();
                    fn_Success(obj.Message);

                    const btnActualizarCita = document.getElementById("btnActualizarCita");
                    const btnAEliminarCita = document.getElementById("btnAEliminarCita");
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
    } else {
    }
    fn_unBlock();
}

function fn_EliminarCita() {
    fn_block();
    var sMotivo = $("#txtMotivo").val();

    // Obtener referencias a los campos que deseas validar
    var inputMotivo = document.getElementById("txtMotivo");

    // Realizar la validación de los campos
    var validacion = validarTexto(inputMotivo, 4, 250);

    if (validacion) {
        // Verificar si no hay errores de validación antes de ejecutar el ajax
        $.ajax({
            url: "VistaCitas.aspx/DeleteACta", // URL de la solicitud
            data: JSON.stringify({sMotivo: sMotivo, idActa: idAcata }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;

                if (obj.StatusCode == 200) {
                    fn_CerrarModal('modalActualizarCita');
                    fn_CerrarModal('modalActCita');
                    GetTablaCitas();
                    fn_Success(obj.Message);

                    const btnActualizarCita = document.getElementById("btnActualizarCita");
                    const btnAEliminarCita = document.getElementById("btnAEliminarCita");
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
    } else {
        // Formato no reconocido
        fn_Error('Favor de contestar el formulario');
    }
    fn_unBlock();
}