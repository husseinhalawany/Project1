jQuery(function () {
        $(".confirmActionDialog").on("click", function (e) {
           
             e.preventDefault(); 
           
            var url = $(this).attr('href');
           
            BootstrapDialog.show({
                title: $(this).attr('title'),
                draggable: true,
                message: $(this).attr('message'),
                autoOpen: false,
                resizable: false,
                height: 170,
                width: 350,
                show: { effect: 'drop', direction: "up" },
                modal: true,
                draggable: true,
                buttons: [{
                    label: 'Yes',
                    action: function (dialog) {
                        window.location = url;
                        BootstrapDialog.show({
                            title: 'Success',
                            message: " Success.",
                            autoOpen: false,
                            resizable: false,
                            height: 170,
                            width: 350,
                            show: { effect: 'drop', direction: "up" },
                            modal: true,
                            draggable: true,
                        });
                    }
                }, {
                    label: 'No',
                    action: function (dialog) {
                        dialog.close();
                    }
                }]
            });
        });
    });
