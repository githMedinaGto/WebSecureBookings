// Función para abrir el modal de calificar
    function abrirModalCalificar(numeroCita) {
        // Aquí puedes implementar la lógica para cargar los datos de la cita según su número
        // y mostrarlos en el modal de calificación.

        // Luego, muestra el modal
        $("#modalCalificar").fadeIn();
    }

    // Función para abrir el modal de eliminar
    function abrirModalEliminar(numeroCita) {
        // Aquí puedes implementar la lógica para cargar los datos de la cita según su número
        // y mostrarlos en el modal de eliminación.

        // Luego, muestra el modal
        $("#modalEliminar").fadeIn();
    }

    // Event listener para los botones de calificar y eliminar
    $(document).on('click', '.btnCalificar', function() {
        var numeroCita = $(this).closest('tr').find('td:eq(7)').text();
        abrirModalCalificar(numeroCita);
    });

    $(document).on('click', '.btnEliminar', function() {
        var numeroCita = $(this).closest('tr').find('td:eq(7)').text();
        abrirModalEliminar(numeroCita);
    });