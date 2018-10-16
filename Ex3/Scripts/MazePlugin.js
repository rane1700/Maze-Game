(function ($) {
    $.fn.mazeBoard = function (option) {
        var mazeInfo = {
            // the matrix containing the maze cells
            mazeData: option.mazeData, 
            rows: option.rows,
            cols: option.cols,
            startRow: option.startRow,
            // initial position of the player
            startCol: option.startCol, 
            exitRow: option.exitRow,
            // the exit position
            exitCol: option.exitCol,
            // player's icon (of type Image)
            playerImage: option.playerImage,
            // exit's icon (of type Image)
            exitImage: option.exitImage,
            // is the board enabled (i.e., player can move)
            isEnabled: true,
            //representing player current and former position in the game.
            playerPosition: { x: "", y: "", prevX: "", prevY: "" },
            //function drawing the maze.
            DrowMaze: function (data) {
                var theCanvas = document.getElementById(data.canvas);
                var context = data.canvas.getContext("2d");
                context.clearRect(0, 0, data.canvas.width, data.canvas.height);
                //width and height of images and wall.
                var cellWidth = data.canvas.width / mazeInfo.cols;
                var cellHeight = data.canvas.height / mazeInfo.rows;
                for (var i = 0; i < mazeInfo.rows; i++) {
                    for (var j = 0; j < mazeInfo.cols; j++) {
                        //check if wall is encountered.
                        if (mazeInfo.mazeData[i * mazeInfo.cols + j] == 1) {
                            //draw the wall.
                            context.fillRect(cellWidth * j, cellHeight * i,
                                cellWidth, cellHeight);
                            //check for starting point to draw player image.
                        } else if (i == mazeInfo.startRow && j == mazeInfo.startCol) {
                            context.drawImage(mazeInfo.playerImage, cellWidth * j,
                                cellHeight * i, cellWidth, cellHeight);
                            mazeInfo.playerPosition.x = cellWidth * j;
                            mazeInfo.playerPosition.y = cellHeight * i;
                            //check for end point to draw end image.
                        } else if (i == mazeInfo.exitRow && j == mazeInfo.exitCol) {
                            context.drawImage(mazeInfo.exitImage, cellWidth * j,
                                cellHeight * i, cellWidth, cellHeight);
                        }
                    }
                }
            },
            //Moving the player according to his arrow key pressed.
            Move: function (data) {
                if (typeof mazeInfo !== 'undefined') {
                    var myCanvas = document.getElementById(data.canvas);
                    //distance to cover in every movement.
                    var cellWidth = data.canvas.width / mazeInfo.cols;
                    var cellHeight = data.canvas.height / mazeInfo.rows;
                    var currentRow = Math.round(mazeInfo.playerPosition.y / cellHeight);
                    var currentCol = Math.round(mazeInfo.playerPosition.x / cellWidth);
                    //checking that ending point was not reached.
                    if (currentRow != mazeInfo.exitRow || currentCol != mazeInfo.exitCol) {
                        mazeInfo.playerPosition.prevX = mazeInfo.playerPosition.x;
                        mazeInfo.playerPosition.prevY = mazeInfo.playerPosition.y;
                        //Moving up
                        if (data.keyPres == 38 && currentRow > 0) {
                            currentRow = currentRow - 1;
                            if (mazeInfo.mazeData[currentRow * mazeInfo.cols +
                                currentCol] != 1) {
                                mazeInfo.playerPosition.y -= cellHeight;
                            }
                        }
                        //Moving down
                        if (data.keyPres == 40 && currentRow + 1 < mazeInfo.rows) {
                            currentRow = currentRow + 1;
                            if (mazeInfo.mazeData[currentRow * mazeInfo.cols +
                                currentCol] != 1) {
                                mazeInfo.playerPosition.y += cellHeight;
                            }
                        }
                        //Moving left
                        if (data.keyPres == 37 && currentCol > 0) {
                            if (mazeInfo.mazeData[currentRow * mazeInfo.cols +
                                currentCol - 1] != 1) {
                                mazeInfo.playerPosition.x -= cellWidth;
                            }
                        }
                        //Moving right
                        if (data.keyPres == 39 && currentCol + 1 < mazeInfo.cols) {
                            if (mazeInfo.mazeData[currentRow * mazeInfo.cols +
                                currentCol + 1] != 1) {
                                mazeInfo.playerPosition.x += cellWidth;
                            }
                        }

                        var context = data.canvas.getContext("2d");
                        var imgWidth = data.canvas.width / mazeInfo.cols;
                        var imgHeight = data.canvas.height / mazeInfo.rows;
                        currentRow = Math.round(mazeInfo.playerPosition.y / cellHeight);
                        currentCol = Math.round(mazeInfo.playerPosition.x / cellWidth);
                        //Check if exit point was reached.
                        if (currentRow == mazeInfo.exitRow && currentCol == mazeInfo.exitCol) {
                            context.clearRect(mazeInfo.playerPosition.prevX,
                                mazeInfo.playerPosition.prevY, imgWidth, imgHeight);
                            //draw winning image
                            context.drawImage(document.getElementById("jonWinner"),
                                0, 0, data.canvas.width, data.canvas.height);
                            return 1;

                        } else {
                            //clear previous place
                            context.clearRect(mazeInfo.playerPosition.prevX,
                                mazeInfo.playerPosition.prevY, imgWidth, imgHeight);
                            //draw player in current place
                            context.drawImage(mazeInfo.playerImage,
                                mazeInfo.playerPosition.x, mazeInfo.playerPosition.y,
                                imgWidth, imgHeight);
                        }
                    }
                    return 0;
                }
                //Method to solve the maze.
            }, Solve: function (data) {
                var mazeCanvas = document.getElementById("mazeCanvas");
                var elem = document.getElementById("jon_snow");
                var rows = mazeInfo.rows;
                var cols = mazeInfo.cols;
                var cellWidth = mazeCanvas.width / cols;
                var cellHeight = mazeCanvas.height / rows;
                var context = mazeCanvas.getContext("2d");
                var imgWidth = mazeCanvas.width / mazeInfo.cols;
                var imgHeight = mazeCanvas.height / mazeInfo.rows;
                playerPosition.prevX = mazeInfo.playerPosition.x;
                playerPosition.prevY = mazeInfo.playerPosition.y;
                playerPosition.x = cellWidth * mazeInfo.startCol;
                playerPosition.y = cellHeight * mazeInfo.startRow;
                mazeInfo.Render(mazeCanvas, playerPosition, elem);
                var i = 1;
                id = window.setInterval(frame, 300);
                function frame() {
                    //check if solution was ended and we reached end point.
                    if (i == data.Solution.length || i == data.Solution.length - 1) {
                        context.clearRect(mazeInfo.playerPosition.prevX,
                            mazeInfo.playerPosition.prevY, imgWidth, imgHeight);
                        //draw winning image
                        context.drawImage(document.getElementById("jonWinner"),
                            0, 0, mazeCanvas.width, mazeCanvas.height);
                        clearInterval(id);
                    } else {
                        playerPosition.prevX = playerPosition.x;
                        playerPosition.prevY = playerPosition.y;
                        //Move left
                        if (data.Solution[i] == 0) { 
                            playerPosition.x -= cellWidth;
                        //Move right
                        } else if (data.Solution[i] == 1) {
                            playerPosition.x += cellWidth
                        //Move up
                        } else if (data.Solution[i] == 2) {
                            playerPosition.y -= cellHeight;
                        } else {
                            //Move down
                            playerPosition.y += cellHeight;
                        }
                        mazeInfo.Render(mazeCanvas, playerPosition, elem);
                        i += 2;
                    }
                    mazeInfo.playerPosition.x = playerPosition.x;
                    mazeInfo.playerPosition.y = playerPosition.y;
                }

                //method to redraw player position
            }, Render: function (myCanvas, playerPosition, player) {
                var context = myCanvas.getContext("2d");
                var imgWidth = myCanvas.width / mazeInfo.cols;
                var imgHeight = myCanvas.height / mazeInfo.rows;
                //clear player previous position.
                context.clearRect(playerPosition.prevX,
                    playerPosition.prevY, imgWidth, imgHeight);
                //draw the player in his current position.
                context.drawImage(player, playerPosition.x,
                    playerPosition.y, imgWidth, imgHeight);
            }
        }
        return mazeInfo;
    }
})(jQuery);