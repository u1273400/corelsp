function init2(arr){
  $('#datetimepicker1').datetimepicker({
    format: 'YYYY-MM-DD',
    //defaultDate: "2012-9-30",
    locale: "uk",
  });
// $('#ex1Slider').bootstrapSlider({
    //     formatter: function(value) {
    //         return 'Current value: ' + value;
    //     }
    // });
  var handle = $( "#custom-handle" );
  $( "#slider" ).slider({
    max: arr.length,
    create: function() {
      handle.text( arr[$( this ).slider( "value" )] );
    },
    slide: function( event, ui ) {
      handle.text( arr[ui.value] );
      DotNet.invokeMethodAsync('corelsp', 'SetMonth', arr[ui.value]);
    }
  });
  
}

$(function () {
});