export function showModal(modalId = 'confirmDialogModal') {
    $(`#${modalId}`).modal("show");
}

export function hideModal(modalId = 'confirmDialogModal') {
    $(`#${modalId}`).modal("hide");
}

export function showModalWithId(modalId) {
    $(`#${modalId}`).modal("show")
}

export function hideModalWithId(modalId) {
    $(`#${modalId}`).modal("hide")
}