$(document).ready(function () {

    $(document.body).on('click', '.isCompleted', function () { updateStatusToInProgress(event, this) });
    $(document.body).on('click', '.isInProgress', function () { updateStatusToCompleted(event, this) });
});


function updateStatusToInProgress(event, element) {
    let id = $(element).attr('id');

    let noteNewStatus = {
        id: id,
        IsCompleted: false
    };

    changeStatusIconToInProgress(element);
}

function updateStatusToCompleted(event, element) {

    let id = $(element).attr('id');

    let noteNewStatus = {
        id: id,
        IsCompleted: true
    };

    console.log(noteNewStatus);
    $.ajax({
        type: "POST",
        url: 'https://localhost:5001/Notes/ChangeNoteStatus',
        data: noteNewStatus,
        error: function (xhr, status, error) { },
        success: function (response) {
            changeStatusIconToCompleted(element);
        }
    });
}


function changeStatusIconToInProgress(element) {
    $(element).parent().find('.isInProgress')
        .attr("class", 'status-icon isInProgress d-inline-flex');
    $(element).attr("class", 'status-icon isCompleted d-none');
}


function changeStatusIconToCompleted(element) {
    $(element).parent().find('.isCompleted')
        .attr("class", 'status-icon isCompleted d-inline-flex');
    $(element).attr("class", 'status-icon isInProgress d-none');
}