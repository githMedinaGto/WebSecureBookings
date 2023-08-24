function fn_Success(message) {
    ohSnap(message, { color: 'green'});
}

function fn_Error(message) {
    ohSnap(message, { color: 'red' });
}

function fn_Alert(message) {
    ohSnap(message, { color: 'orange' });
}

function fn_Info(message) {
    ohSnap(message, { color: 'blue' });
}

function CerrarSesion() {
    fn_block();
    $.ajax({
        url: "/Index.aspx/CerrarSesion", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            fn_Info(data.d[0]);
            // Redireccionar a la página principal
            window.location.href = data.d[1];
            // Recargar la página
            location.reload();
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}