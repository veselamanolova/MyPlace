
window.onload = function () {

    var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();

    document.getElementById("sendButton").disabled = true;

    connection.on("AddComment", function (message, id) {
        let url = window.location.pathname
        let urlElements = url.split('/')
        let establishmentId = +urlElements.pop()
        if (id === establishmentId) {
            let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            let li = document.createElement("h6");
            li.textContent = msg;
            document.getElementById("messagesList").appendChild(li);
        }
    });

    connection.on("OffensiveComment", function (message) {
        let msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        let li = document.createElement("h6");
        li.style.color = 'red'
        li.textContent = msg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click", function (event) {
        let establishmentId = document.getElementById("establishment-id").value;
        let commentElement = document.getElementById("comment-area");
        let user = document.getElementById("userInput").value;
        let comment = commentElement.value
        connection.invoke("SendMessage", establishmentId, user, comment).catch(function (err) {
            return console.error(err.toString());
        });

        commentElement.value = ""
        event.preventDefault();
    });
}


