function init2(){
    $('#datetimepicker1').datetimepicker();
    // $('#ex1Slider').bootstrapSlider({
    //     formatter: function(value) {
    //         return 'Current value: ' + value;
    //     }
    // });
    var handle = $( "#custom-handle" );
    $( "#slider" ).slider({
      create: function() {
        handle.text( $( this ).slider( "value" ) );
      },
      slide: function( event, ui ) {
        handle.text( ui.value );
      }
    });
  
}

$(function () {
});