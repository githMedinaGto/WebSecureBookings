document.addEventListener("DOMContentLoaded", function () {
    var inputCorr = document.getElementById("txtCorreo");
    inputCorr.value = "";

    $("#btnBuscarCuenta").click(function () {
        fn_BuscarCuenta();
    });
    $("#btnActuaizar").click(function () {
        fn_AbrirModal('modalActualizarContraseña');
    });
    $("#btnActualizarDatos").click(function () {
        fn_Actualizardatos();
    });
    $("#btnCancelar").click(function () {
        var inputCorrP = document.getElementById("txtCorreoAc");
        var inputPassP = document.getElementById("txtPasswordAc1");
        var inputVerPassP = document.getElementById("txtPasswordAc2");
        var inputToken = document.getElementById("token");
        inputCorrP.value = "";
        inputPassP.value = "";
        inputVerPassP.value = "";
        inputToken.value = "";
    });
});

function fn_BuscarCuenta() {
    fn_block();

    var inputCorr = document.getElementById("txtCorreo");
    var valEmail = validateEmail(inputCorr);

    if (valEmail) {
        //Realiza una solicitud AJAX utilizando jQuery
        $.ajax({
            url: "RecuperarCuenta.aspx/BuscarCuenta", // URL de la solicitud
            data: JSON.stringify({ sCorreo: inputCorr.value }), // Datos a enviar 
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;

                if (obj.StatusCode == 200) {
                    fn_Success(obj.Message);
                } else {
                    fn_Alert(obj.StatusCode + "\n" + obj.Message);
                }
                fn_unBlock();
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                fn_Alert(status); // Imprime el estado del error
                fn_unBlock();
            }
        });
    } else {
        fn_Alert("Favor de checar el formato del correo");
        fn_unBlock();
    }
}

function fn_Actualizardatos() {
    fn_block();
    var inputCorrP = document.getElementById("txtCorreoAc");
    var inputPassP = document.getElementById("txtPasswordAc1");
    var inputVerPassP = document.getElementById("txtPasswordAc2");
    var inputToken = document.getElementById("token");
    var valEmail = validateEmail(inputCorrP);
    var valPassP = validarTexto(inputPassP, 8, 15);
    var valVerPassP = validarTexto(inputVerPassP, 8, 15);
    var valToken = validarTexto(inputToken, 5, 5);

    if (valEmail & valPassP & valVerPassP & valToken) {
        if (valPassP == valVerPassP) {
            fn_CerrarModal('modalActualizarContraseña');
            $.ajax({
                url: "RecuperarCuenta.aspx/ActualizarContrasenia", // URL de la solicitud
                data: JSON.stringify({ sCorreo: inputCorrP.value, sContrasenia: inputPassP.value, sToken: inputToken.value }), // Datos a enviar 
                type: "POST", // Método de la solicitud (POST en este caso)
                dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
                contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                    //Acceder a las propiedades del objeto dentro del array
                    var obj = data.d;

                    if (obj.StatusCode == 200) {
                        fn_Success(obj.Message);
                    } else {
                        fn_Alert(obj.StatusCode + "\n" + obj.Message);
                    }
                    fn_unBlock();
                    // Limpiar los valores de los campos
                    inputCorrP.value = "";
                    inputPassP.value = "";
                    inputVerPassP.value = "";
                    inputToken.value = "";
                },
                error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                    fn_Alert(status); // Imprime el estado del error
                    fn_unBlock();
                }
            });
        } else {
            fn_Alert("Las contraseñas no conciden");
            fn_unBlock();
        }
    } else {
        fn_Alert("Favor de constestar el formulario");
        fn_unBlock();
    }
}