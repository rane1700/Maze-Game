(function ($) {
    $.fn.drawGame = function () {
        var mazeRows = $("#mazeRowsBox").val();
        var mazeCols = $("#mazeColsBox").val();
        var mazeName = $("#mazeNameBox").val();

        var productsUrl = "../api/generate" + "/" + mazeName + "/" + mazeCols + "/" + mazeRows;
        mazeJson = $.getJSON(productsUrl, function (data) {
            mazeJson = data;
            var player = document.getElementById("jon_snow");
            var endCell = document.getElementById("throne");
            var myCanvas = document.getElementById("mazeCanvas");
            var context = mazeCanvas.getContext("2d");
            var rows = data.Rows;
            var cols = data.Cols;
            var cellWidth = mazeCanvas.width / cols;
            var cellHeight = mazeCanvas.height / rows;
            playerPosition.x = data.Start.Row;
            playerPosition.y = data.Start.Col;
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (data.Maze[i * cols + j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i,
                            cellWidth, cellHeight);
                    } else if (i == data.Start.Row && j == data.Start.Col) {
                        context.drawImage(player, cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                        playerPosition.x = cellWidth * j;
                        playerPosition.y = cellHeight * i;
                    } else if (i == data.End.Row && j == data.End.Col) {
                        context.drawImage(endCell, cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
            console.log(data); // show on browser debugger
            return data;
        });
    };
    $.fn.playerMove = function () {

    };
})(jQuery);