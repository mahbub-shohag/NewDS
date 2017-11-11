$(document).ready(function () {
    $('ul li a').has('span').attr('data-toggle', 'dropdown').addClass('dropdown-toggle');


    $('#Controller').change(function () {
        //var url = $(location).attr('host');
        var parent = $('#Controller option:selected').text();
        //alert(parent);
        $.ajax({
            type: "POST",
            url: "/Menu/Actions/"+parent,
            data: '{Controller : "' + parent + '"}',
            contentType: "applicatin/json; charset = utf-8",
            dataType: "json",
            success: function (data) {
                //var arr = $.parseJSON(data);
                //alert(data);
                var html = '';
                $.each(data, function (key, item) {
                    //alert(key);
                    html += '<option>'+item+'</option>'; 
                });
                $('#Action').children('option').remove();
                $('#Action').append(html);
            },
            failure: function (response) {
                alert("Failuer");
            },
            error: function (response) {
                alert("Error");
            }
        });
    });
});