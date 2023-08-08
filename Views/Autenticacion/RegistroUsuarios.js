var clipboard = new ClipboardJS('.copy-code');

clipboard.on('success', function (event) {
    event.clearSelection();
    alertify.message('Código copiado al portapapeles.');
});

const container = document.querySelector('.container')
const btnSignIn = document.querySelector('.btnSign-in')
const btnSignUp = document.querySelector('.btnSign-up')

btnSignIn.addEventListener('click', () => {
    container.classList.add('active')
})

btnSignUp.addEventListener('click', () => {
    container.classList.remove('active')
})

$("#btnInscribir").click(function () {
    if ($("#txtRol").val() == 1004) {
        fn_AbrirModal('mRegistroProfesionista');
    } else if ($("#txtRol").val() == 1005) {
        fn_AbrirModal('mRegistrClientes');
    }
});

// Funcion para crear al usuario Cliente
function fn_AgregarCliente() {
    var error = "";
    var sNombre = $("#txtName").val();
    var sApellidoP = $("#txtApP").val();
    var sApellidoM = $("#txtApM").val();
    var sCorreo = $("#txtCorreo").val();
    var sCorreop = $("#txtCorreop").val();
    var sPassword = $("#txtPassword").val();
    var sPassword2 = $("#txtPassword2").val();
    var sPassword01 = $("#txtPassword01").val();
    var sPassword02 = $("#txtPassword02").val();
    var sTelefono = $("#NumTelefono").val();
    var sArea = $("#txtArea").val();
    var sProfesion = $("#txtProfesion").val();
    var sHora = $("#txtHora").val();
    var sRol = $("#txtRol").val();

    var sMunicipio = $("#txtMunicipio").val();
    var sColonia = $("#txtColonia").val();
    var sCalle = $("#txtCalle").val();
    var sUbicacion = $("#txtUbicacion").val();

    if (sRol != "" && sRol == 1004) {

        // Obtener referencias a los campos que deseas validar
        var input1 = document.getElementById("txtName");
        var input2 = document.getElementById("txtApP");
        var input3 = document.getElementById("txtApM");

        var input4 = document.getElementById("txtCorreop");
        var input5 = document.getElementById("txtPassword01");
        var input6 = document.getElementById("txtPassword02");
        var input7 = document.getElementById("NumTelefono");
        var input8 = document.getElementById("txtArea");
        var input9 = document.getElementById("txtProfesion");

        var input10 = document.getElementById("txtMunicipio");
        var input11 = document.getElementById("txtColonia");
        var input12 = document.getElementById("txtCalle");
        var input13 = document.getElementById("txtUbicacion");

        var input14 = document.getElementById("txtHora");

        // Realizar la validación de los campos
        var validacion1 = validarTexto(input1, 4, 50);
        var validacion2 = validarTexto(input2, 4, 300);
        var validacion3 = validarTexto(input3, 4, 300);
        var validacion4 = validarTexto(input4, 4, 300);
        var validacion5 = validarTexto(input5, 4, 300);
        var validacion6 = validarTexto(input6, 4, 300);
        var validacion7 = validarTexto(input7, 4, 300);
        var validacion8 = validarTexto(input8, 4, 300);
        var validacion9 = validarTexto(input9, 4, 300);
        var validacion10 = validarTexto(input10, 0, 300);
        var validacion11 = validarTexto(input11, 4, 300);
        var validacion12 = validarTexto(input12, 4, 300);
        var validacion13 = validarTexto(input13, 4, 300);
        var validacion14 = validarTexto(input14, 4, 300);

        // Verificar si no hay errores de validación antes de ejecutar la función fn_AgregarRol
        if (validacion1 && validacion2 && validacion3 && validacion4 && validacion5 &&
            validacion6 && validacion7 && validacion8 && validacion9 && validacion10 && validacion11 && validacion12 && validacion13 && validacion14) {
            $.ajax({
                url: "RegistroUsuarios.aspx/PostCrearProfesionista", // URL de la solicitud
                data: JSON.stringify({ sRol: sRol, sNombre: sNombre, sApellidoP: sApellidoP, sApellidoM: sApellidoM, sCorreop: sCorreop, sPassword01: sPassword01, sTelefono: sTelefono, sArea: sArea, sProfesion: sProfesion, sMunicipio: sMunicipio, sColonia: sColonia, sCalle: sCalle, sUbicacion: sUbicacion }), // Datos a enviar en formato JSON
                type: "POST", // Método de la solicitud (POST en este caso)
                dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                    //Acceder a las propiedades del objeto dentro del array
                    var obj = data.d;

                    if (obj.StatusCode == 200) {
                        fn_CerrarModal('mRegistroProfesionista');
                        console.log(obj.Message);

                    } else {
                        console.log(obj.StatusCode);
                        console.log(obj.Message);
                    }
                    console.log(response);
                },
                error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                    console.log(status); // Imprime el estado del error
                }
            });

        } else if (sRol == "" || sNombre == "" || sApellidoP == "" || sApellidoM == "") {
            error = '<span style="color: red;">Favor de contestar el formulario</span>';
            $('#txtDescripcion').after($(error).fadeOut(2000));
        }

    } else if (sRol != "" && sRol == 1005) {

        // Obtener referencias a los campos que deseas validar
        var input1 = document.getElementById("txtName");
        var input2 = document.getElementById("txtApP");
        var input3 = document.getElementById("txtApM");
        var input4 = document.getElementById("txtCorreo");
        var input5 = document.getElementById("txtPassword");

        // Realizar la validación de los campos
        var validacion1 = validarTexto(input1, 4, 50);
        var validacion2 = validarTexto(input2, 4, 300);
        var validacion3 = validarTexto(input3, 4, 300);
        var validacion4 = validarTexto(input4, 4, 300);
        var validacion5 = validarTexto(input5, 4, 300);


        // Verificar si no hay errores de validación antes de ejecutar la función fn_AgregarRol
        if (validacion1 && validacion2 && validacion3 && validacion4 && validacion5) {
            $.ajax({
                url: "RegistroUsuarios.aspx/PostCrearCliente", // URL de la solicitud
                data: JSON.stringify({ sRol: sRol, sNombre: sNombre, sApellidoP: sApellidoP, sApellidoM: sApellidoM, sCorreo: sCorreo, sPassword: sPassword }), // Datos a enviar en formato JSON
                type: "POST", // Método de la solicitud (POST en este caso)
                dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                    //Acceder a las propiedades del objeto dentro del array
                    var obj = data.d;

                    if (obj.StatusCode == 200) {
                        fn_CerrarModal('mRegistrClientes');
                        console.log(obj.Message);

                    } else {
                        console.log(obj.StatusCode);
                        console.log(obj.Message);
                    }
                    console.log(response);
                },
                error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                    console.log(status); // Imprime el estado del error
                }
            });

        } else if (sRol == "" || sNombre == "" || sApellidoP == "" || sApellidoM == "") {
            error = '<span style="color: red;">Favor de contestar el formulario</span>';
            $('#txtDescripcion').after($(error).fadeOut(2000));
        }

    }

}



function iniciarSesion() {
    var usuario = document.getElementById("email").value;
    var contrasena = document.getElementById("password").value;

    //if (usuario == "usuario@gmail.com" && contrasena == "123") {
    //    window.location.href = "https://localhost:44372/Views/index/index.aspx?";
    //} else {
    //    console.log("Error");
    //}
    if (usuario == "usuario@gmail.com") {
        $.ajax({
            url: "RegistroUsuarios.aspx/IniciarSesion", // URL de la solicitud
            data: JSON.stringify({ usuario: usuario, contrasena: contrasena }), // Datos a enviar en formato JSON
            type: "POST", // Método de la solicitud (POST en este caso)
            dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
            contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
            success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                //Acceder a las propiedades del objeto dentro del array
                var obj = data.d;
                window.location.href = "https://localhost:44372/Views/index/index.aspx?";
                console.log(data.d);
            },
            error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                console.log(status); // Imprime el estado del error
            }
        }); 
    }

    
}