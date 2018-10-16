var mazHub = $.connection.mazeHub;
var myMazeBoard;
var opponentMazeBoard;

mazHub.client.drowoncanvas = function (mazejason) {
    $("#loading").hide();
    myMazeBoard = $.fn.mazeBoard({
        mazeData: mazejason.Maze, rows: mazejason.Rows, cols: mazejason.Cols, // the matrix containing the maze cells
        startRow: mazejason.Start.Row, startCol: mazejason.Start.Col, // initial position of the player
        exitRow: mazejason.End.Row, exitCol: mazejason.End.Col, // the exit position
        playerImage: document.getElementById("jon_snow"), // player's icon (of type Image)
        exitImage: document.getElementById("throne") // exit's icon (of type Image)
    });
    opponentMazeBoard = $.fn.mazeBoard({
        mazeData: mazejason.Maze, rows: mazejason.Rows, cols: mazejason.Cols, // the matrix containing the maze cells
        startRow: mazejason.Start.Row, startCol: mazejason.Start.Col, // initial position of the player
        exitRow: mazejason.End.Row, exitCol: mazejason.End.Col, // the exit position
        playerImage: document.getElementById("jon_snow"), // player's icon (of type Image)
        exitImage: document.getElementById("throne") // exit's icon (of type Image)
    });
    myMazeBoard.DrowMaze({ canvas: myCanvas});
    opponentMazeBoard.DrowMaze({ canvas: opponentCanvas });
}

mazHub.client.getListGame = function (arr) {
    var i = 0;
    var x = document.getElementById("gameSelect");
    var flag = false;
    for (i = 0; i < arr.length; i++) {
        for (var j = 0; j < x.options.length; j++) {
            if (x.options[j].text == arr[i]) {
                flag = true;
            }
        }
        if (!flag) {
            var option = document.createElement("option");
            option.text = arr[i];
            x.add(option, x[i]);
        }
        flag = false;
    }
}
//move player according to his movement request.
mazHub.client.opponentMove = function (move) {
    var resolt = opponentMazeBoard.Move({ keyPres: move, canvas: opponentCanvas });
    if (resolt == 1) {
        alert("You have lost!");
    }
}
//move player according to his movement request.
mazHub.client.myMove = function (move) {
    var resolt = myMazeBoard.Move({ keyPres: move, canvas: myCanvas });
    if (resolt == 1) {
        mazHub.server.notifyWinner();
        apiUser = "../api/Users"
        var userInfo = {
            Name: sessionStorage.getItem("userName")
        };
        $.getJSON(apiUser + "/" + userInfo.Name).done(function (data) {
            //data.Wins++;
            $.post(apiUser + "/" + "PostUsers" + "/" + data.Name + "/" + 1)
                .done(function () {
                });
        })
    }
}
//move player according to his movement request.
mazHub.client.opponentLoss = function () {
        apiUser = "../api/Users"
        var userInfo = {
            Name: sessionStorage.getItem("userName")
        };
        $.getJSON(apiUser + "/" + userInfo.Name).done(function (data) {
            //data.Losses++;
            $.post(apiUser + "/" + "PostUsers" + "/" + data.Name + "/" + 0)
                .done(function () {
                    window.location.href = "MultiGame.html";
                });
        })
}
//connect player to hub.
$.connection.hub.start().done(function () {
    //get local storage valuse if exists.
    if (!(localStorage.rows === null)) {
        document.getElementById("mazeRowsBox").value = localStorage.rows;
    }
    if (!(localStorage.cols === null)) {
        document.getElementById("mazeColsBox").value = localStorage.cols;
    }
    //Generate maze button click event
    $("#startbtn1").click(function () {
       
        var mazeName = $("#mazeNameBox").val();
        var mazeRows = $("#mazeRowsBox").val();
        var mazeCols = $("#mazeColsBox").val();
        $("#loading").show();
        document.title = mazeName;
        mazHub.server.startGame(mazeName, mazeRows, mazeCols);
    });
    //Join to existing game button click event.
    $("#joinBtn").click(function () {
        var e = document.getElementById("gameSelect");
        var mazeName = e.options[e.selectedIndex].value;
        document.title = mazeName;
        mazHub.server.joinGame(mazeName);
    });

    $("#gameSelect").click(function () {
        mazHub.server.gameToJoin();
    });
    //handling the case of arrow key pressed.
    $(document).keydown(function () {
        if (event.keyCode in { '37': 37, "38": 38, "39": 39, "40": 40 }) {
            if (myMazeBoard != null) {
                mazHub.server.playMove(event.keyCode);
            }
        }
    });
})