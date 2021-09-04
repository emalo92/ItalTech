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

function StartTimerRemoveModalBootsrap() {
    window.setTimeout(function () { RemoveModalBootsrap(); }, 100);
}

//Questo metodo può servire quando la form resta in ombra
function RemoveModalBootsrap() {
    var obj = document.getElementsByClassName("modal-backdrop");
    for (var i = 0; i < obj.length; ++i) {
        obj[i].remove();
    }
}

//Trasforma gli invii in tab per navigare tra gli elementi della form
//Ogni elemento di input deve avere un attributo tabindex con un valore intero univoco e consecutivo
function ReturnToTabBehaviour(idForm) {
    $("#" + idForm + " :input").keydown(function (event) {
        if (event.keyCode === 13 && this.getAttribute('type') !== 'submit') {
            event.stopPropagation();
            event.preventDefault();

            var tab = parseInt($(this).attr("tabindex"), 10);
            var newTab = tab + 1;
            var newElem = $("#" + idForm + " :input[tabindex='" + newTab + "']");
            if (newElem.attr('tabindex') === undefined) {
                tab = 1;
                newElem = $("#" + idForm + " :input[tabindex='" + tab + "']");
            }
            newElem.focus();
        }
    })
}

/*Per disabilitare il loading commentare questa parte */
//*******************************************************
$(document).ready(function () {
    $("form").submit(function () {
        var error = $(".field-validation-error");
        var formAction = $(this).attr("action");
        if (error.length === 0 && formAction.indexOf('ExportTable') === -1) {
            showLoading();
        }

    });
    $("a").click(function (e) {
        var href = $(this).attr('href');
        var str = e.currentTarget.innerText;

        if (href !== '#' && href.indexOf('#') !== 0 && href.indexOf('ExportTable') === -1 && href !== 'javascript:void(0)') {
            showLoading();
        }
    });
    $(document).ajaxSend(function () {
        showLoading();
    });
    $(document).ajaxComplete(function () {
        hideLoading();
    })
})
//**************************************************************

//$(window).on('hashchange', function (e) {
//    showLoading();
//});


function setKeyValueQueryString(target, key, value) {
    if (!target.includes("?")) {
        return target += "?" + key + "=" + value;
    }
    if (!target.includes(key)) {
        return target += "&" + key + "=" + value;
    }
    var root = target.split("?");
    var queryString = root[1];
    var elements = queryString.split("&");
    var newQueryString = "";
    var separator = ""
    for (var i = 0; i < elements.length; ++i) {
        var element = elements[i];
        if (element.split("=")[0] !== key) {
            newQueryString += separator + element;
            separator = "&";
        }
    }
    newQueryString += "&" + key + "=" + value;
    return root[0] + "?" + newQueryString;
}

function jsonFormatter(valJson) {
    var jsonObj = JSON.parse(valJson);
    return JSON.stringify(jsonObj, null, 4);
}

function saveChartCustomHeightjsAsPdf(canvas, pdfFileName, altezza) {
    saveTwoChartjsAsPdf(canvas, null, pdfFileName, altezza);
}

function saveChartjsAsPdf(canvas, pdfFileName) {
    saveTwoChartjsAsPdf(canvas, null, pdfFileName);
}

function saveTwoChartjsAsPdf(canvas1, canvas2, pdfFileName, altezza) {
    var canvasImg1 = canvas1.toDataURL("image/png", 1.0);
    var canvasImg2 = null;
    var width = 275;
    var heigth = 150;
    if (altezza != null) {
        heigth = altezza;
    }
    var numCanvas = 1;
    if (canvas2 !== null) {
        canvasImg2 = canvas2.toDataURL("image/png", 1.0);
        numCanvas = 2;
        heigth = 80;
    }
    var doc = new jsPDF('landscape');
    doc.setFontSize(20);

    doc.addImage(canvasImg1, 'png', 10, 10, width, heigth);
    if (numCanvas === 2) {
        doc.addImage(canvasImg2, 'png', 10, 100, width, heigth);
    }

    doc.save(pdfFileName);
}


//*********** EVENTS ***************
const openPopupCustomEvent = document.createEvent('Event');
const closedPopupCustomEvent = document.createEvent('Event');

//Metodo per notificare l'apertura del popup
function dispatchOpenPopupCustomEvent(dataSender) {
    openPopupCustomEvent.initEvent('openPopup_CustomEvent', true, true);
    openPopupCustomEvent.Sender = dataSender;
    document.dispatchEvent(openPopupCustomEvent);
}

//Metodo per notificare la chiusura del popup
function dispatchClosedPopupCustomEvent(dataSender) {
    closedPopupCustomEvent.initEvent('closedPopup_CustomEvent', true, true);
    closedPopupCustomEvent.Sender = dataSender;
    document.dispatchEvent(closedPopupCustomEvent);
}

function dispatchClosedPopupCustomEventWithCallback(dataSender, callbackMethod, data) {
    closedPopupCustomEvent.initEvent('closedPopup_CustomEvent', true, true);
    closedPopupCustomEvent.Sender = dataSender;
    callbackMethod(data);
    document.dispatchEvent(closedPopupCustomEvent);
}