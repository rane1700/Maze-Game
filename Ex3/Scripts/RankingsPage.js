$(document).ready(function () {
    //get registered users details to the user ranking table
    apiUser = "../api/Users"
    $.getJSON(apiUser).done(function (data) {
        var table = document.getElementById("rankingT");
        data.sort(function (a, b) { return (a.Wins - a.Losses) < (b.Wins - b.Losses) })
        for (var i = 0; i < data.length; i++) {
        var row = table.insertRow(i+1);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);
        cell1.innerHTML = i+1;
        cell2.innerHTML = data[i].Name;
        cell3.innerHTML = data[i].Wins;
        cell4.innerHTML = data[i].Losses;
    }
}
)
})