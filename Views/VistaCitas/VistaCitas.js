document.addEventListener("DOMContentLoaded", function () {
    GetTablaCitas();

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
            } else {
                console.log(obj.StatusCode);
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
        }
    });
    fn_unBlock();
}

function fn_Calificar(id) {
    fn_AbrirModal('modalCalificar');
}