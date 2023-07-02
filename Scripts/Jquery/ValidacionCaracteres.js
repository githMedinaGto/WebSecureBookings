function permite(elEvento, permitidos) {
    // Variables que definen los caracteres permitidos
    var numeros = "0123456789";
    var caracteres = " abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
    var numeros_caracteres = numeros + caracteres;
    var teclas_especiales = [8, 37, 39, 46];
    // 8 = BackSpace, 46 = Supr, 37 = flecha izquierda, 39 = flecha derecha


    // Seleccionar los caracteres a partir del parámetro de la función
    switch (permitidos) {
        case 'num':
            permitidos = numeros;
            break;
        case 'car':
            permitidos = caracteres;
            break;
        case 'num_car':
            permitidos = numeros_caracteres;
            break;
    }

    // Obtener la tecla pulsada
    var evento = elEvento || window.event;
    var codigoCaracter = evento.charCode || evento.keyCode;
    var caracter = String.fromCharCode(codigoCaracter);

    // Comprobar si la tecla pulsada es alguna de las teclas especiales
    // (teclas de borrado y flechas horizontales)
    var tecla_especial = false;
    for (var i in teclas_especiales) {
        if (codigoCaracter == teclas_especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    // Comprobar si la tecla pulsada se encuentra en los caracteres permitidos
    // o si es una tecla especial
    return permitidos.indexOf(caracter) != -1 || tecla_especial;
}

var errorTimer; // Temporizador para mostrar/ocultar el mensaje de error

function validarTexto(input, minLength, maxLength) {
    var value = input.value.trim();

    // Verificar la longitud del valor ingresado
    if (value.length < minLength) {
        mostrarError(input, 'Se requiere un mínimo de ' + minLength + ' caracteres');
    } else if (value.length > maxLength) {
        mostrarError(input, 'Se permite un máximo de ' + maxLength + ' caracteres');
        input.value = value.substring(0, maxLength); // Elimina los caracteres adicionales
    } else {
        ocultarError(input);
    }
}

function mostrarError(input, mensaje) {
    clearTimeout(errorTimer); // Reiniciar el temporizador si ya está en funcionamiento
    var errorElement = document.getElementById(input.id + '-error');

    // Crear el elemento de error si no existe
    if (!errorElement) {
        errorElement = document.createElement('div');
        errorElement.id = input.id + '-error';
        errorElement.style.color = 'red';
        input.parentNode.appendChild(errorElement);
    }

    // Establecer el mensaje de error en el elemento
    errorElement.textContent = mensaje;

    // Configurar el temporizador para ocultar el mensaje después de 2 segundos
    errorTimer = setTimeout(function () {
        ocultarError(input);
    }, 2000);
}

function ocultarError(input) {
    clearTimeout(errorTimer);
    var errorElement = document.getElementById(input.id + '-error');

    // Eliminar el elemento de error si existe
    if (errorElement) {
        errorElement.parentNode.removeChild(errorElement);
    }
}
