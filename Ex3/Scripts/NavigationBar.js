//Loading the navigation bar at the top of every page
$("#nav-placeholder").load("NavigationBar.html", function () {
    //checking if login was committed and change nav-bar accordingly
    if (sessionStorage.getItem("userName")) {
        document.getElementById("register").textContent = "Hello "
            + sessionStorage.getItem("userName");
        document.getElementById("register").href = "#";
        document.getElementById("login").textContent = "Logout";
        document.getElementById("login").onclick = logout;
        document.getElementById("login").href = "HomePage.html";
        document.getElementById("multi").href = "MultiGame.html";
    } else {
        document.getElementById("multi").onclick = alertLogin;
    }
});
//loging out event of nav-bar
function logout() {
    sessionStorage.removeItem("userName");
}
//checking if user logged in, in case he wants to enter multi-player.
function alertLogin() {
    alert("Please login first!");
}