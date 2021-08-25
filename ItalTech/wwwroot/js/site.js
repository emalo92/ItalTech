function showLoading() {
    document.getElementById("divPageLoading").style.display = "block";
}
function hideLoading() {
    document.getElementById("divPageLoading").style.display = "none";
}


function openSidebar() {
    document.getElementById("mySidenav").style.width = "300px";
    document.getElementById("main").style.marginLeft = "300px";
}

function closeSidebar() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
}