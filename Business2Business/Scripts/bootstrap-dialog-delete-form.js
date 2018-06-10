$(document).ready(function () {
    var BootstrapDialogType = BootstrapDialog.TYPE_DANGER;
    var on_submit_function = function (evt) {
        
        var crntForm = $(this);
        if (crntForm.attr('id') != "Edit") {
        evt.preventDefault(); //The form wouln't be submitted Yet.

        BootstrapDialog.show({
            title: 'Delete',
            draggable: true,
            message: "are you sure you want to Delete " + crntForm.attr('id') + ".",
            type: BootstrapDialogType,
            buttons: [{
                label: 'Yes',
                action: function (dialog) {
                    BootstrapDialog.show({
                        title: 'Success',
                        message: "Delete Success.",
                        type: BootstrapDialogType
                    });
                    crntForm.off('submit', on_submit_function); //It will remove this handle and will submit the form again if it's all ok.
                    crntForm.submit();
                }
            }, {
                label: 'No',
                action: function (dialog) {
                    dialog.close();
                }
            }]
        });
    }
    }
    $('form').on('submit', on_submit_function); //Registering on submit.
});