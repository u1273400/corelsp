function isIEPreVer9() { var v = navigator.appVersion.match(/MSIE ([\d.]+)/i); return (v ? v[1] < 9 : false); }
var flrView;
var floor_grid;

function display_flr_grid(data,cols){
  columns=processCols(cols);
  flrView = new Slick.Data.DataView({ inlineFilters: true });
  floor_grid = new Slick.Grid("#flrGrid", flrView, columns, options);
  floor_grid.setSelectionModel(new Slick.RowSelectionModel());
  // var pager = new Slick.Controls.Pager(flrView, floor_grid, $("#flrPager"));
  // var columnpicker = new Slick.Controls.ColumnPicker(columns, floor_grid, options);
  // move the filter panel defined in a hidden div into floor_grid top panel
  $("#inlineFilterPanel")
    .appendTo(floor_grid.getTopPanel())
    .show();
  floor_grid.onCellChange.subscribe(function (e, args) {
    flrView.updateItem(args.item.id, args.item);
    flr_cell_changed(args.item);
  });
  floor_grid.onAddNewRow.subscribe(function (e, args) {
    var item = {"num": data.length, "id": "new_" + (Math.round(Math.random() * 10000)), "title": "New task", "duration": "1 day", "percentComplete": 0, "start": "01/01/2009", "finish": "01/01/2009", "effortDriven": false};
    $.extend(item, args.item);
    flrView.addItem(item);
  });
  floor_grid.onKeyDown.subscribe(function (e) {
    // select all rows on ctrl-a
    if (e.which != 65 || !e.ctrlKey) {
        return false;
    }
    var rows = [];
    for (var i = 0; i < flrView.getLength(); i++) {
        rows.push(i);
    }
    floor_grid.setSelectedRows(rows);
      e.preventDefault();
  });

  floor_grid.onBeforeCellEditorDestroy.subscribe(function (e) {
    $( "#flrGrid" ).removeClass("editing")
    $( "#flrGrid" ).removeClass("saved")
    $( "#flrGrid" ).removeClass("save-error")
    //console.dir('editor destroyed');
  });

  floor_grid.onBeforeEditCell.subscribe(function (e) {
    $( "#flrGrid" ).removeClass("saved")
    $( "#flrGrid" ).removeClass("save-error")
    $( "#flrGrid" ).addClass("editing")
  });

  floor_grid.onDblClick.subscribe(function (e) {
    var cell = floor_grid.getCellFromEvent(e);
    var id=data[cell.row].id;
    window.open(
      '../admin/floor-transactions/resolve/'+id,
      '_blank' // <- This is what makes it open in a new window.
    );
  });

  flrView.onRowCountChanged.subscribe(function (e, args) {
    floor_grid.updateRowCount();
    floor_grid.render();
  });

  flrView.onRowsChanged.subscribe(function (e, args) {
    floor_grid.invalidateRows(args.rows);
    floor_grid.render();
  });
  flrView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
    floor_grid.updatePagingStatusFromView( pagingInfo );
  });
  var h_runfilters = null;
  // wire up the slider to apply the filter to the model
  // $("#pcSlider,#pcSlider2").slider({
  //   "range": "min",
  //   "slide": function (event, ui) {
  //       Slick.GlobalEditorLock.cancelCurrentEdit();
  //       if (percentCompleteThreshold != ui.value) {
  //       window.clearTimeout(h_runfilters);
  //       h_runfilters = window.setTimeout(updateFilter, 10);
  //       percentCompleteThreshold = ui.value;
  //       }
  //   }
  // });

  // wire up the search textbox to apply the filter to the model
  // $("#txtSearch,#txtSearch2").keyup(function (e) {
  //   Slick.GlobalEditorLock.cancelCurrentEdit();
  //   // clear on Esc
  //   if (e.which == 27) {
  //       this.value = "";
  //   }
  //   searchString = this.value;
  //   //updateFilter();
  // });
  // function updateFilter() {
  //   flrView.setFilterArgs({
  //       percentCompleteThreshold: percentCompleteThreshold,
  //       searchString: searchString
  // });
  flrView.refresh();
  //}
  $("#btnSelectRows").click(function () {
    if (!Slick.GlobalEditorLock.commitCurrentEdit()) {
        return;
  }
  var rows = [];
  for (var i = 0; i < 10 && i < flrView.getLength(); i++) {
      rows.push(i);
  }
  floor_grid.setSelectedRows(rows);
  });
  // initialize the model after all the events have been hooked up
  flrView.beginUpdate();
  flrView.setItems(data);
  // flrView.setFilterArgs({
  // percentCompleteThreshold: percentCompleteThreshold,
  // searchString: searchString
  // });
  // flrView.setFilter(myFilter);
  flrView.endUpdate();
  // if you don't want the items that are not visible (due to being filtered out
  // or being on a different page) to stay selected, pass 'false' to the second arg
  flrView.syncGridSelection(floor_grid, true);
  $("#gridContainer").resizable();
}
function flr_cell_changed(item){
  DotNet.invokeMethodAsync('corelsp', 'SaveFloorGia', JSON.stringify(item)).then(function(saved){
    $( "#flrGrid" ).removeClass("editing")
    $( "#flrGrid" ).addClass(saved?"saved":"save-error")
    //console.dir(resp);
  });
}
