
var form;


var result;


var editLocation = window.location.origin + "/cms/menu/edit";
//console.log("location", editLocation);
toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

function RedirectToEdit() {
    if (confirm("Düzenle sayfasına dönmek ister misin?"))
        window.location.href = editLocation;
    else
        return false;
}



window.onload = function Onload() {


    form = document.getElementById("editForm");

    result= document.getElementById("result").value;


   // console.log("sonuc", result);

    if (result == "success") {
        toastr.success("Başarıyla Kaydedildi!");
        
        setTimeout(() => {
            RedirectToEdit();
        },1200);

    }

    else if (result =="fail") {
        toastr.error("Yanlış veya eksik veri girişi gerçekleşti!");
    }
 
}




