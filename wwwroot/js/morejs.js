function init2(arr){
  $('#datetimepicker1').datetimepicker({
    format: 'YYYY-MM-DD',
    //defaultDate: "2012-9-30",
    locale: "uk",
    viewMode: 'months',
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
      delay(function (e) {
        console.log("delayb4callback::val="+val);
        setmonth(arr[ui.value]);
      }, 500);
    }
  });
  
}
function setmonth(val){
  console.log("setmonth::val="+val);
  DotNet.invokeMethodAsync('corelsp', 'SetMonth', val)
  $('#datetimepicker1').data("DateTimePicker").change(val);
}

function delay(callback, ms) {
  var timer = 0;
  return function() {
    var context = this, args = arguments;
    clearTimeout(timer);
    timer = setTimeout(function () {
      callback.apply(context, args);
    }, ms || 0);
  };
}

function loading_gif(){
  $.LoadingOverlay("show");

  // Hide it after 3 seconds
  setTimeout(function(){
      $.LoadingOverlay("hide");
  }, 3000);
}