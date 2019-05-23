"use strict";
debugger
var clientConnection = new signalR.HubConnectionBuilder().withUrl("/MyHub").build(); 
///服务端调用客户端的方案
clientConnection.on("ClientGetOnlineUserNum", function (num) {
    //$("#userNumTx").text(num);
    //document.getElementById("userNumTx").nodeValue = num;
});
clientConnection.on("UserOutLine", function (msg) {
    //$("#outLineMsg").html($("#outLineMsg").html() + "<br/>掉线了：" + msg)
    //document.getElementById("outLineMsg").nodeValue = msg;
});
clientConnection.start().then(function () {
    //调用服务端方法
    //chat.server.getOnlineUserNum();
    //console.log("Connected,transport=" + $.connection.hub.transport.name);
    //clientConnection.invoke("getOnlineUserNum")
});

        