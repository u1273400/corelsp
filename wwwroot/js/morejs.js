window.started_set_month=false;

function init2(arr){
  $('#datetimepicker1').datetimepicker({
    format: 'YYYY-MM-DD',
    defaultDate: "2012-9-30",
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
      var self=this;
      self.v=arr[ui.value];
      if(started_set_month)return;
      setTimeout(function () {
        if(started_set_month)return;
        setmonth(self.v);
      }, 2500);
    }
  });
  
}
function setmonth(val){
  started_set_month=true;
  $.LoadingOverlay("show");
  DotNet.invokeMethodAsync('corelsp', 'SetMonth', val)
  $("#dti").text(val);
  console.log("setmonth:: ended set month "+ $("#dti").text());
  console.log("setmonth:: ended set month "+ (val));
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

function loading_gif(x){
  $.LoadingOverlay("show");

  // Hide it after 3 seconds
  setTimeout(function(){
      $.LoadingOverlay("hide");
  },x || 2000);
}