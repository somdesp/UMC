$(document).ready(function () {
    LoadCMBGenero();
    LoadCMBTurmas();
})

// Listar categorias

function LoadCMBGenero() {
    $.ajax({
        type: "POST",
        url: "/Java/GetGenero",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {

            $("#cmbGenero").empty();
            $("#cmbGenero").append('<option value>Selecione...</option>');
            $.each(msg, function (index, element) {
                $("#cmbGenero").append('<option value="' + element.id + '">' + element.genero + '</option>');
            });
        },
        error: function() {
            alert("Falha ao carregar os Generos");
        }
    });
}

function LoadCMBCursos() {
    $.ajax({
        type: "POST",
        url: "/Java/GetCurso",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $("#cmbCurso").empty();
            $("#cmbCurso").append('<option value>Selecione...</option>');
            $.each(msg, function (index, element) {
                $("#cmbCurso").append('<option value="' + element.id + '">' + element.curso + '</option>');
            });
        },
        error: function () {
            alert("Falha ao carregar os Cursos");
        }


    });
}


function LoadCMBTurmas() {

    // Get a list of categories and a list of products of the first category.
    $.getJSON('/Java/GetCurso', null, function (data) {
        $.each(data, function () {
            $('#cmbCurso').append('<option value=' +
                this.id + '>' + this.curso + '</option>');
        });
        $.getJSON('/Java/GetTurma', {Id: $(this).val() }, function (data) {

            $.each(data, function () {
                $('#cmbCurso').append('<option value=' +
                    this.id + '>' + this.turma + '</option>');
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Erro ao carregar Turmas!');
        });
    }).fail(function (jqXHR, textStatus, errorThrown) {
        alert('Erro ao carregar Cursos!');
    });

    // Dropdown list change event.
    $('#cmbCurso').change(function () {
        $('#cmbTurma option').remove();
        $.getJSON('/Java/GetTurma', { Id: $(this.id).val() }, function (data) {
            $.each(data, function () {
                $('#cmbTurma').append('<option value=' +
                    this.id + '>' + this.turma + '</option>');
            });
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert('Erro ao carregar Turmas!');
        });
    });
}