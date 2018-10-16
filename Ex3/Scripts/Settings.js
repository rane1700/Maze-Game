(function ($) {
    $(document).ready(function () {
        //get rows from local storage if exists
        if (localStorage.getItem("rows") !== null) {
            document.getElementById("inputRows").value = localStorage.rows;
        }
        //get cols from local storage if exists
        if (localStorage.getItem("cols") !== null) {
            document.getElementById("inputCols").value = localStorage.cols;
        }
        //get algorithm from local storage
        if (localStorage.getItem("algorithm") !== null) {
            if (localStorage.algorithm == "DFS") {
                document.getElementById("algorithm").selectedIndex = 0;
            } else {
                document.getElementById("algorithm").selectedIndex = 1;
            }
        }
        //Settings 'ok' button click event
        $("#okBtn").click(function () {
            localStorage.rows = $("#inputRows").val();
            localStorage.cols = $("#inputCols").val();
            localStorage.algorithm = $("#algorithm").val();
        })
    });
})(jQuery);