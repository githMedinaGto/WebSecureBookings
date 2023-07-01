// Se ejecuta cuando el contenido del documento HTML ha sido completamente cargado
document.addEventListener("DOMContentLoaded", function () {
    // Llama a la función LlenarComboSeleLiga()
    LlenarComboSeleLiga();

    document.getElementById('btnExample').addEventListener('click', function () {
        var modal = new bootstrap.Modal(document.getElementById('exampleModalCenter'));
        modal.show();
    });
});

// Función para llenar el combo de selección de liga
function LlenarComboSeleLiga() {
    // Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "Index.aspx/LlenarComboLiga", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            if (data.d[0] == 200) { // Si el código de estado es igual a 200 (OK)
                console.log(data.d[2]); // Imprime el contenido en la posición 2 de los datos recibidos
            } else {
                console.log(data.d[0] + data.d[1]); // Imprime el código de estado y el texto de estado concatenados
            }
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            console.log(status); // Imprime el estado del error
        }
    });
}