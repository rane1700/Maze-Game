(function ($) {
    $(document).ready(function () {
        var myMazeBoard;
        //get default rows from local storage
        if (localStorage.rows !== null) {
            document.getElementById("mazeRowsBox").value = localStorage.rows;
        }
        //get default columns from local storage
        if (localStorage.cols !== null) {
            document.getElementById("mazeColsBox").value = localStorage.cols;
        }
        //get default algorithm selection from local storage
        if (localStorage.algorithm !== null) {
            if (localStorage.algorithm == "DFS") {
                document.getElementById("algoSelect").selectedIndex = 0;
            } else {
                document.getElementById("algoSelect").selectedIndex = 1;
            }
        }
        //Generate maze button click event
        $("#startbtn").click(function () {
            //show loader icon
            $("#loading").show();
                var mazeRows = $("#mazeRowsBox").val();
                var mazeCols = $("#mazeColsBox").val();
                var mazeName = $("#mazeNameBox").val();
                var productsUrl = "../api/Maze" + "/" + mazeName + "/" + mazeCols + "/" + mazeRows;
            //data returned from controller
            $.getJSON(productsUrl, function (data) {
                mazeJson = data;
                //variable to hold variuse attributes of the maze
                myMazeBoard = $("#mazeCanvas").mazeBoard({
                    // the matrix containing the maze cells
                    mazeData: data.Maze, rows: data.Rows, cols: data.Cols,
                    // initial position of the player
                    startRow: data.Start.Row, startCol: data.Start.Col,
                    // the exit position
                    exitRow: data.End.Row, exitCol: data.End.Col,
                    // player's icon (of type Image)
                    playerImage: document.getElementById("jon_snow"),
                    // exit's icon (of type Image)
                    exitImage: document.getElementById("throne")
                }
                );
                myMazeBoard.DrowMaze({ canvas: mazeCanvas });
                //hide loader icon
                $("#loading").hide();
                //change tab title
                document.title = data.Name;
                return data;
            }).error(function(){alert("Error occurred");})
        });
        //Solve button click event
        $("#solvebtn").click(function () {
            var mazeName = $("#mazeNameBox").val();
            var algorithm = $("#algoSelect").val();
            var solveUrl = "../api/Maze" + "/" + mazeName + "/" + algorithm;
            //solve json from controller
            $.getJSON(solveUrl, function (data) {
                myMazeBoard.Solve(data);
                //hide loader icon
                $("#loading").hide();

            }).error(function () { alert("Error occurred"); })
        });
        //Take care of arrow key movement request
        $(document).keydown(function () {
            if (event.keyCode in { '37': 37, "38": 38, "39": 39, "40": 40 }) {
                if (myMazeBoard != null) {
                    myMazeBoard.Move({ keyPres: event.keyCode, canvas: mazeCanvas })
                }
            }
        });
    });
})(jQuery);
var playerPosition = { x: "", y: "", prevX: "", prevY: "" };