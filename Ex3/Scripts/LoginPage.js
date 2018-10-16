
$(document).ready(function () {
    //Login button click event
$("#loginbtn").click(function () {
        //show loader icon
        $("#loading").show();
    apiUser = "../api/Users/GetUsers"
    var id = $("#uname").val();
    var password = $("#passcheck").val();
    $.getJSON(apiUser + "/" + id + "/" + password).done(function (data) {
        $("#loading").hide();
        alert("User Login successfully");
        sessionStorage.setItem("userName", id);
        window.location.href = "HomePage.html";
    })
        .fail(function (jqXHR, textStatus, err) {
                alert("One of the values is incorrect");
        });
});
});
