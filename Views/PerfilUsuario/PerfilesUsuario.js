var iIdUsuario; 
var iIdCaldendario;
var iIdDia;

document.addEventListener("DOMContentLoaded", function () {
    mostrarInfoDeUsuario();

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
            console.log(obj.Data);
            $("#nombre").val(obj.Data[0].sNombre);
            $("#apellidoP").val(obj.Data[0].sApellidoP);
            $("#apellidoM").val(obj.Data[0].sApellidoM);
            $("#profesion").val(obj.Data[0].sProfecion);
            $("#areaProfesion").val(obj.Data[0].sAreaProfesion);
            $("#telefono").val(obj.Data[0].stelefono);
            $("#correo").val(obj.Data[0].sCorreo);
            $("#municipio").val(obj.Data[0].Municipio);

            //if (obj.StatusCode == 200) {
            //    $("#modalGenerarCita").html(obj.Resultado);
            //    // Llamar a la función para generar el mapa con las coordenadas proporcionadas
            //    fn_ConvertirCoordenadas(obj.Message);
            //    fn_AbrirModal('modalGenerarCita');
            //    iIdUsuario = idUsuario;
            //} else {
            //    fn_Error(obj.StatusCode + "\n" + obj.Message);
            //}
        },
        error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
            fn_Error(status); // Imprime el estado del error
        }
    });
    fn_unBlock();


}
function mostrarAlerta() {
    fn_AbrirModal('validarCampos');
}

function mostrar() {

    var nombre = document.getElementById("nombre");
    var apellidoP = document.getElementById("apellidoP");
    var apellidoM = document.getElementById("apellidoM");
    var profesion = document.getElementById("profesion");
    var areaProfesion = document.getElementById("areaProfesion");
    var telefono = document.getElementById("telefono");
    var correo = document.getElementById("correo");
    var municipio = document.getElementById("municipio");
    var colonia = document.getElementById("colonia");
    var calle = document.getElementById("calle");
    var estado = document.getElementById("estado");



    //// Realizar la validación de los campos
    var vnombre = validarTexto(nombre, 3, 50);
    var vapellidoP = validarTexto(apellidoP, 3, 250);
    var vapellidoM = validarTexto(apellidoM, 3, 250);
    var vprofesion = validarTexto(profesion, 10, 250);
    var vareaProfesion = validarTexto(areaProfesion, 3, 250);
    var vtelefono = validarTexto(telefono, 10, 12);
    var vmunicipio = validarTexto(nombre, 3, 250);
    var vcolonia = validarTexto(colonia, 4, 250);
    var vcalle = validarTexto(calle, 3, 250);
    var vestado = validarTexto(estado, 3, 250);

    if (vnombre && vapellidoM && vapellidoP && vprofesion && vareaProfesion && vtelefono && vmunicipio && vcolonia && vcalle && vestado) {



    } else {
        fn_CerrarModal('validarCampos');
        fn_Alert('Favor de validar los datos correctamente');
    }

}