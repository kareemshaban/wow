const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", function (fromUser, message) {
    var msg = fromUser + " : " + message;
    var li = document.createElement("li");
    li.textContent = msg;
    $("#msgs").append(li);
})
connection.start().then(function () { connection.invoke('GetConnectionId').then(function (connectionId) {alert(connectionId);}); });
debugger;

$("#send").click(function () {
    var user = $("#yourName").val();
    var msg = $("#msg").val();
    connection.invoke("SendMsg", user, msg);
})