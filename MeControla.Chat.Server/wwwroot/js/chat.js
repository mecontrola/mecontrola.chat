(function (window, undefined) {
    "use strict";

    var app = function (isDebug, undefined) {
        var protocol = location.protocol === "https:" ? "wss:" : "ws:",
            wsUri = protocol + "//" + window.location.host,
            socket = new WebSocket(wsUri);

        isDebug = isDebug === undefined
            ? false
            : isDebug;

        socket.onopen = e => {
            if (isDebug) console.info('[Open] Connection established', e);
        };

        socket.onclose = e => {
            if (isDebug) {
                if (event.wasClean) {
                    console.info(`[Close] Connection closed cleanly, code=${e.code} reason=${e.reason}`, e);
                } else {
                    console.info('[Close] Connection died', e);
                }
            }
        };

        socket.onerror = e => {
            if (isDebug) console.info(`[Error] ${e.data}`, e);
        };

        var waitForSocketConnection = function waitForSocketConnection(socket, callback) {
            setTimeout(function () {
                if (socket.readyState === 1) {
                    if (callback !== undefined) {
                        callback();
                    }
                    return;
                } else {
                    waitForSocketConnection(socket, callback);
                }
            }, 5);
        };

        var sendMessage = function (msg, fnc) {
            waitForSocketConnection(socket, function () {
                console.log(msg);
                socket.send(msg);
                socket.onmessage = e => {
                    if (isDebug) console.log(e);
                    if (fnc !== undefined) fnc(e.data);
                }
            });
        };

        var _connect = function (username, room, fnc) {
            var msg = "/connect " + username + " " + room;
            sendMessage(msg, fnc);
        };

        var _createRoom = function (room, fnc) {
            var msg = "/createroom " + room;
            sendMessage(msg, fnc);
        };

        var _changeRoom = function (room, fnc) {
            var msg = "/changeroom " + room;
            sendMessage(msg, fnc);
        };

        var _everone = function (message, fnc) {
            var msg = "/msgall " + message;
            sendMessage(msg, fnc);
        };

        var _public = function (user, message, fnc) {
            var msg = "/public " + user + " " + message;
            sendMessage(msg, fnc);
        };

        var _private = function (user, message, fnc) {
            var msg = "/private " + user + " " + message;
            sendMessage(msg, fnc);
        };

        var _listRooms = function (fnc) {
            sendMessage("/list rooms", fnc);
        };

        var _listUsers = function (fnc) {
            sendMessage("/list users", fnc);
        };

        var _ping = function (fnc) {
            sendMessage("/ping", fnc);
        };

        var _exit = function (fnc) {
            sendMessage("/exit", fnc);
        };

        return {
            connect: _connect,
            createRoom: _createRoom,
            changeRoom: _changeRoom,
            messageAll: _everone,
            messagePrivate: _private,
            messagePublic: _public,
            listRooms: _listRooms,
            listUsers: _listUsers,
            ping: _ping,
            exit: _exit
        };
    };

    if (typeof window.chat === "undefined")
        window.chat = app;
})(window);