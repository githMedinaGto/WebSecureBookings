function fn_AbrirModal(idModal){
    var modal = new bootstrap.Modal(document.getElementById(idModal));
    modal.show();
}

function fn_CerrarModal(idModal) {
    var modalElement = document.getElementById(idModal);
    var modal = bootstrap.Modal.getInstance(modalElement);

    if (modal) {
        modal.hide();
    }
}