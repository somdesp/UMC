$(document).ready(function () {
    //Dropdownlist Selectedchange event  
    $("#Curso").change(function () {
        $("#turma").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.Action("GetCity")',
            dataType: 'json',
            data: { id: $("#State").val() },
            success: function (citys) {
                // states contains the JSON formatted list  
                // of states passed from the controller  
                $.each(citys, function (i, city) {
                    $("#city").append('<option value="'
                        + city.Value + '">'
                        + city.Text + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
        return false;
    })
});  