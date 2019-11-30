$(document).ready(function () {

    $(document.body).on('click', '.isCompleted', function () { updateStatusToInProgress(event, this) });
    $(document.body).on('click', '.isInProgress', function () { updateStatusToCompleted(event, this) });   
});


function updateStatusToInProgress(event, element) {
    console.log("in is completed tag ");
    $(element)
        .replaceWith("<img src='/images/notes/in-progress.png' class='status-icon isInProgress' alt='In progress'>"); 
}

function updateStatusToCompleted(event, element) {
    console.log("in is progress tag ");
    $(element)
        .replaceWith("<img src='/images/notes/completed.svg' class='status-icon isCompleted' alt='Completed'>");
}