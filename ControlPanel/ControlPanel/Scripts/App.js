$(function () {

    addPageChange();

    $("#ImageFile").change(function () {
        readURL(this);
    });

    //To allow only specific format for image upload
    $("#ImageFile").change(function () {
        var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
        if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            alert("Only formats allowed are: " + fileExtension.join(', '));
            $('#ImageFile').val('');
        }
    });

    //To allow only pdf file upload
    $("#File").change(function () {
        var fileExtension = ['pdf'];
        if ($(this).val() != null || $(this).val() != "")
        {
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats allowed are: " + fileExtension.join(', '));
                $('#File').val('');
            }
        }
    });


    $('#AddPage').on("change", function () { addPageChange(); });

    //$('.datepicker').datepicker({
    //    format: 'dd-M-yyyy',
    //    defaultDate: 'now',
    //    autoclose: true
    //});

    
    //var generalErrors = $(".validation-summary-errors li");
    //if (generalErrors && generalErrors[0] && generalErrors[0].innerHTML) {
    //    generalErrors = generalErrors[0].innerHTML;
    //    if (generalErrors !== "") {
    //        notifyError(generalErrors);
    //    }
    //}
    //try {
    //    $(".field-validation-error").each(function () {
    //        var id = $(this).attr("data-valmsg-for");
    //        id = id.replace(".", "_");
    //        id = id.replace("[", "");
    //        id = id.replace("]", "");
    //        var v = "#" + id;
    //        var d = this.innerHTML;
    //        $(v).attr("title", d);
    //    });
    //    $(".input-validation-error").tooltip({
    //        trigger: 'focus',
    //        placement: 'top'
    //    });
    //    $(".combobox").tooltip({
    //        trigger: 'focus',
    //        placement: 'top'
    //    });
    //    $(".input-validation-error")[0].focus();
    //} catch (e) {
    //    return false;
    //}
});

function notify(message, type) {
    $(function () {
        var n = $.notify(message, type);
    });
}

function hideNotifications() {
    $(".notifyjs-corner").html("");
}

function notifyMessage(message) {
    notify(message, "success");
}

function notifyError(message) {
    notify(message, "error");
}


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ImageViewer').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}



function removeImage() {
    $('#Image').val("");
    $('#image-name').hide();
    $('#ImageViewer').attr('src', null);
    $('#ImageFile').val('');
}

function removeFile() {
    $('#FileName').val("");
    $('#file-name').hide();
}


function addPageChange() {

    if ($('#AddPage').is(":checked")) {
        $('#pdf-file').hide();
        $('#pdf-file-name').hide();
        $('#page-title').show();
        $('#image-file').show();
        $('#image-viewer').show();
    }
    else {
        $('#pdf-file').show();
        $('#pdf-file-name').show();
        $('#page-title').hide();
        $('#image-file').hide();
        $('#image-viewer').hide();
    }
}