// Variable para controlar el estado
var modoEditar = false;
var sIdRol;

//Se ejecuta cuando el contenido del documento HTML ha sido completamente cargado
document.addEventListener("DOMContentLoaded", function () {
     //Llama a la función LlenarComboSeleLiga()
    GetTablaRoles();
    $("#btnRegistrar").click(function () {
        $("#txtRol").val("");
        $("#txtDescripcion").val("");
        var modal = document.getElementById("modalTitulo");
        modal.innerText = "Agregar Rol";
        var btnGuardar = document.getElementById("btnGuardarRol");
        // Mostrar el botón cambiando su estilo
        btnGuardar.style.display = "block";
        // Obtener el elemento del botón mediante su ID
        var botonActualizar = document.getElementById("btnActualizarRol");
        // Ocultar el botón cambiando su estilo
        botonActualizar.style.display = "none";
        fn_AbrirModal('exampleModalCenter');
    });

    $("#btnGuardarRol").click(function () {
        fn_AgregarRol();
    });

    $("#btnActualizarRol").click(function () {
        fn_EditarRol();
    });
});

 //Función para llenar el combo de selección de liga
function GetTablaRoles() {
    fn_block();
    $("#divRoles").html("");
     //Realiza una solicitud AJAX utilizando jQuery
    $.ajax({
        url: "Index.aspx/GetTablaRoles", // URL de la solicitud
        data: "", // Datos a enviar (vacío en este caso)
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "JSON", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
             //Acceder a las propiedades del objeto dentro del array
            var obj = data.d[0];

            if (obj.StatusCode == 200) {
                $("#divRoles").html(obj.Resultado);
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

function fn_AgregarRol() {
    fn_block();
    var error = "";
    var sRol = $("#txtRol").val();
    var sDescripcion = $("#txtDescripcion").val();

    if (sRol != "") {
        if (sDescripcion != "") {
            // Obtener referencias a los campos que deseas validar
            var input1 = document.getElementById("txtRol");
            var input2 = document.getElementById("txtDescripcion");

            // Realizar la validación de los campos
            var validacion1 = validarTexto(input1,4,50);
            var validacion2 = validarTexto(input2,4,300);

            // Verificar si no hay errores de validación antes de ejecutar la función fn_AgregarRol
            if (validacion1 && validacion2) {
                $.ajax({
                    url: "Index.aspx/PostCrearRol", // URL de la solicitud
                    data: JSON.stringify({ sRol: sRol, sDescripcion: sDescripcion }), // Datos a enviar en formato JSON
                    type: "POST", // Método de la solicitud (POST en este caso)
                    dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                    contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                    success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                        //Acceder a las propiedades del objeto dentro del array
                        var obj = data.d;

                        if (obj.StatusCode == 200) {
                            fn_CerrarModal('exampleModalCenter');
                            GetTablaRoles();
                            console.log(obj.Message);

                        } else {
                            console.log(obj.StatusCode);
                            console.log(obj.Message);
                        }
                    },
                    error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                        console.log(status); // Imprime el estado del error
                    }
                });


            } else if (sRol == "" && sDescripcion == "") {
                error = '<span style="color: red;">Favor de contestar el formulario</span>';
                $('#txtDescripcion').after($(error).fadeOut(2000));
            }

        } else
        {
            error = '<span style="color: red;">Favor de agregar una descripción</span>';
            $('#txtDescripcion').after($(error).fadeOut(2000));
        }
            
    }else {
        error = '<span style="color: red;">Favor de agregar un rol</span>';
        $('#txtRol').after($(error).fadeOut(2000));
    }
    fn_unBlock();
}

function fn_SeleccionarRol(idRol) {
    fn_block();
    $.ajax({
        url: "Index.aspx/GetRol", // URL de la solicitud
        data: JSON.stringify({ idRol: idRol}), // Datos a enviar en formato JSON
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d;
            if (obj.StatusCode == 200) {
                var modal = document.getElementById("modalTitulo");
                modal.innerText = "Actualizar Rol";

                var btnGuardar = document.getElementById("btnGuardarRol");
                // Mostrar el botón cambiando su estilo
                btnGuardar.style.display = "none";
                // Obtener el elemento del botón mediante su ID
                var botonActualizar = document.getElementById("btnActualizarRol");
                // Ocultar el botón cambiando su estilo
                botonActualizar.style.display = "block";

                sIdRol = obj.Data[0].idRol;
                $("#txtRol").val(obj.Data[0].sNomRol);
                $("#txtDescripcion").val(obj.Data[0].sDescripcion);

                fn_AbrirModal('exampleModalCenter');

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

function fn_EditarRol() {
    fn_block();
    var error = "";
    var sRol = $("#txtRol").val();
    var sDescripcion = $("#txtDescripcion").val();

    if (sRol != "") {//validacion de los campos no esten vacios
        if (sDescripcion != "") {
            // Obtener referencias a los campos que deseas validar
            var input1 = document.getElementById("txtRol");
            var input2 = document.getElementById("txtDescripcion");

            // Realizar la validación de los campos
            var validacion1 = validarTexto(input1, 4, 50);
            var validacion2 = validarTexto(input2, 4, 300);

            // Verificar si no hay errores de validación antes de ejecutar la función fn_AgregarRol
            if (validacion1 && validacion2) {
                $.ajax({
                    url: "Index.aspx/PutRol", // URL de la solicitud
                    data: JSON.stringify({ idRol: sIdRol, sRol: sRol, sDescripcion: sDescripcion }), // Datos a enviar en formato JSON
                    type: "POST", // Método de la solicitud (POST en este caso)
                    dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
                    contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
                    success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
                        //Acceder a las propiedades del objeto dentro del array
                        var obj = data.d;

                        if (obj.StatusCode == 200) {
                            fn_CerrarModal('exampleModalCenter');
                            $("#txtRol").val("");
                            $("#txtDescripcion").val("");
                            GetTablaRoles();
                            //console.log(obj.Message);

                        } else {
                            console.log(obj.StatusCode);
                            console.log(obj.Message);
                        }
                    },
                    error: function (xhr, status, error) { // Función que se ejecuta cuando hay un error en la solicitud
                        console.log(status); // Imprime el estado del error
                    }
                });


            } else if (sRol == "" && sDescripcion == "") {
                error = '<span style="color: red;">Favor de contestar el formulario</span>';
                $('#txtDescripcion').after($(error).fadeOut(2000));
            }

        } else {
            error = '<span style="color: red;">Favor de agregar una descripción</span>';
            $('#txtDescripcion').after($(error).fadeOut(2000));
        }

    } else {
        error = '<span style="color: red;">Favor de agregar un rol</span>';
        $('#txtRol').after($(error).fadeOut(2000));
    }
    fn_unBlock();
}

function fn_EliminarEquipo(idRol) {
    fn_block();
    $.ajax({
        url: "Index.aspx/DeleteRol", // URL de la solicitud
        data: JSON.stringify({ idRol: idRol }), // Datos a enviar en formato JSON
        type: "POST", // Método de la solicitud (POST en este caso)
        dataType: "json", // Tipo de datos esperado en la respuesta (JSON en este caso)
        contentType: "application/json; charset=utf-8", // Tipo de contenido de la solicitud
        success: function (data) { // Función que se ejecuta cuando la solicitud es exitosa
            //Acceder a las propiedades del objeto dentro del array
            var obj = data.d;
            if (obj.StatusCode == 200) {
                GetTablaRoles();
                alert(obj.Message);

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