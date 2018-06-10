BootstrapDialog.show({
    title: 'Say-hello dialog',
    message: 'Hello world!',
    buttons: [{
        id: 'btn-ok',
        icon: 'glyphicon glyphicon-check',
        label: 'OK',
        cssClass: 'btn-primary',
        autospin: false,
        action: function (dialogRef) {
            dialogRef.close();
        }
    }]
});