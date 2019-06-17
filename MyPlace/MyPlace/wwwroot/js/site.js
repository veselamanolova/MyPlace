
$(document).ready(function () {

    $(".hover").mouseleave(
        function () {
            $(this).removeClass("hover");
        }
    );

    var text_max = 400;
    $('#count_message').html(text_max + ' remaining');

    $('#comment-area').keyup(function () {
        var text_val = $('#comment-area').val();
        var text_length = $('#comment-area').val().length;
        if (text_length > text_max) {
            $('#comment-area').val((text_val).substring(0, text_length - 1));
            return false;
        }

        var text_remaining = text_max - text_length;

        $('#count_message').html(text_remaining + ' remaining');
    });
});

$(document).ready(function () {

    $('.edit-button').click(function () {

        var url = $('#NickNameModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#NickNameModal").html(data);
            $("#NickNameModal").modal('show');
        });
    });

});


