function myfunction() {
    var copyText = document.getElementById("myinput")

    copyText.select();
    copyText.setselectionRange(0, 99999);
    navigator.clipboard.writeText(copyText.value);
    alert("Copied link :" + copyText.value);
}