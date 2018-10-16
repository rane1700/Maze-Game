//check if user name was filled
function checkUserName() {
    apiUser = "../api/Users"
    var userInfo = {
        Name: $("#userName").val()
    };
    if (userInfo.Name == "") {
        alert("User name needed!");
        return;
    }
    $.getJSON(apiUser + "/" + userInfo.Name).done(function (data) {
        alert("User name already exist");
        $("#userName").val("");
    })
}
//check if the reapted password matches the initial password
function checkUserRepeatPass() {
    if ($("#userPassword").val() != $("#repeatPassword").val()) {
        alert("password not matching");
        $("#userPassword").val("");
        $("#repeatPassword").val("");
    }
}
//Register click event
$("#registerbtn").click(function () {
    apiUser = "../api/Users"
    var userInfo = {
        Name: $("#userName").val(),
        Email: $("#userEmail").val(),
        Password: $("#userPassword").val()
    };
    var rpass = $("#repeatPassword").val();
    if (userInfo.Name == "") {
        alert("User name needed!");
        return;
    }
    if (userInfo.Email == "") {
        alert("User email needed!");
        return;
    }
    if (userInfo.Password == "") {
        alert("User password needed!");
        return;
    }
    if (rpass == "") {
        alert("User repeat password needed!");
        return;
    }
    if ($("#userPassword").val() == $("#repeatPassword").val()) {
        $("#loading").show();
        $.post(apiUser, userInfo)
            .done(function () {
                alert("User Register successfully");
                sessionStorage.setItem("userName", userInfo.Name);
                window.location.href = "HomePage.html";
            });
    } else {
        alert("password not matching");
        $("#userPassword").val("");
        $("#repeatPassword").val("");
    }
    $("#loading").hide();
});
