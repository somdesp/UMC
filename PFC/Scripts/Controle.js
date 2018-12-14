//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
 
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Java/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Nome + '</td>';
                html += '<td>' + item.RGM + '</td>';
                html += '<td>' + item.Email + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Cadastrar() {
    var res = validate();
    if (res === false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        Nome: $('#Nome').val(),
        Login: $('#Login').val(),
        Email: $('#Email').val(),
        Senha: $('#Senha').val(),
        RGM: $('#RGM').val()
    };
    $.ajax({
        url: "/Java/Create",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#Nome').val().trim() === "") {
        $('#Nome').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Nome').css('border-color', 'lightgrey');
    }
    if ($('#Login').val().trim() === "") {
        $('#Login').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Login').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() === "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Senha').css('border-color', 'lightgrey');
    }
    if ($('#Senha').val().trim() === "") {
        $('#Senha').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Senha').css('border-color', 'lightgrey');
    }
    if ($('#RGM').val().trim() === "") {
        $('#RGM').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RGM').css('border-color', 'lightgrey');
    }
    return isValid;
}  


