window.started_set_month=false;
Pusher.logToConsole = true;

var pusher = new Pusher('e012c5a35d8c08d175b9', {
  cluster: 'eu',
  forceTLS: true
});

var channel = pusher.subscribe('my-channel');
channel.bind('new-space-transaction', function(data) {
  alert(JSON.stringify(data));
});

function init2(arr){
  $('#datetimepicker1').datetimepicker({
    format: 'YYYY-MM-DD',
    //defaultDate: "2018-05-31",
    locale: "uk",
    viewMode: 'months',
  });
  DotNet.invokeMethodAsync('corelsp', 'GetCMonth').then(resp=>{
    //console.log(resp);
    $('#datetimepicker1').data("DateTimePicker").defaultDate(resp);
  });
// $('#ex1Slider').bootstrapSlider({
    //     formatter: function(value) {
    //         return 'Current value: ' + value;
    //     }
    // });
  var handle = $( "#custom-handle" );
  $( "#slider" ).slider({
    max: arr.length-1,
    value: arr.length-1,
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

function saveSong(){
  //hchcs.curr_song().lyrics().replace(new RegExp('`', 'g'), "")
  //var pd=JSON.stringify({Id:hchcs.curr_song().id(),Lyrics:hchcs.curr_song().lyrics()});
  var pd=JSON.stringify({id:hchcs.curr_song().id(),Lyrics:hchcs.curr_song().lyrics()});
  console.log("saving song="+pd);
  $.post(usong_url, pd)
  .done(function(data) {
      console.log("Server Response: " + data);
      alert('Success!')
  });
}