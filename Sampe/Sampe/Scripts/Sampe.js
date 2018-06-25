$(document).ready(function () {
    $("#ocultar").click(function (event) {
        event.preventDefault();
        $("#capaefectos").hide("slow");
    });

    $("#mostrar").click(function (event) {
        event.preventDefault();
        $("#capaefectos").show("slow");
    });

    $("#ocultar2").click(function (event) {
        event.preventDefault();
        $("#capaefectos2").hide("slow");
    });

    $("#mostrar2").click(function (event) {
        event.preventDefault();
        $("#capaefectos2").show("slow");
    });

    $("#enable").click(function (event) {
        // habilita o campo 
        $("input").prop("disabled", false);

    });

    $("#disable").click(function (event) {
        // desabilita o campo 
        $("input").prop("disabled", true);

    });


});






 /*$(document).ready(function() {
    $('select').material_select();

    $('select').material_select('destroy');
  });*/


