﻿$("#UploadImg").change(function () {
    var data = new FormData();
    var files = $("#UploadImg").get(0).files;
    if (files.length > 0) {
        data.append("MyImages", files[0]);
    }

    $.ajax({
        url: "/Usuario/UploadImage",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            //code after success
            $("#txtImg").val(response);
            $("#imgPreview").attr('src', '/Upload/' + response);
        },
        error: function (er) {
            alert(er);
        }

    });
});

