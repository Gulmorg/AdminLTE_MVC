$(document).ready(function () {
    $('#dialog').dialog({
        autoOpen: true
    })
    $('#btn').click(function () {
        $("#dialog").dialog({
            modal: true,
            height: 600,
            width: 500,

            buttons: {
                Accept: function () {
                    $(this).dialog("close");
                }
            }
        });
        $('#dialog').dialog('open');
    });
});