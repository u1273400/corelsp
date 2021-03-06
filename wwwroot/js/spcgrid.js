var spcView;
var spc_grid;
var ctx_cell;
var ctx_data;
function display_spc_grid(data,cols){
  ctx_data = data;
  columns=processCols(cols);
  spcView = new Slick.Data.DataView({ inlineFilters: true });
  spc_grid = new Slick.Grid("#spcGrid", spcView, columns, options);
  spc_grid.setSelectionModel(new Slick.RowSelectionModel());
  var pager = new Slick.Controls.Pager(spcView, spc_grid, $("#spcPager"));
  // var columnpicker = new Slick.Controls.ColumnPicker(columns, spc_grid, options);
  // move the filter panel defined in a hidden div into spc_grid top panel
  $("#inlineFilterPanel")
      .appendTo(spc_grid.getTopPanel())
      .show();

  spc_grid.onCellChange.subscribe(function (e, args) {
    spcView.updateItem(args.item.id, args.item);
    spc_cell_changed(args.item);
  });
  spc_grid.onAddNewRow.subscribe(function (e, args) {
    var item = {"num": data.length, "id": "new_" + (Math.round(Math.random() * 10000)), "title": "New task", "duration": "1 day", "percentComplete": 0, "start": "01/01/2009", "finish": "01/01/2009", "effortDriven": false};
    $.extend(item, args.item);
    spcView.addItem(item);
  });
  spc_grid.onKeyDown.subscribe(function (e) {
    // select all rows on ctrl-a
    if (e.which != 65 || !e.ctrlKey) {
        return false;
    }
    var rows = [];
    for (var i = 0; i < spcView.getLength(); i++) {
        rows.push(i);
    }
    spc_grid.setSelectedRows(rows);
      e.preventDefault();
  });

  spc_grid.onBeforeCellEditorDestroy.subscribe(function (e) {
    $( "#spcGrid" ).removeClass("editing")
    $( "#spcGrid" ).removeClass("saved")
    $( "#spcGrid" ).removeClass("save-error")
    //console.dir('editor destroyed');
  });

  spc_grid.onBeforeEditCell.subscribe(function (e) {
    $( "#spcGrid" ).removeClass("saved")
    $( "#spcGrid" ).removeClass("save-error")
    $( "#spcGrid" ).addClass("editing")
  });

  spc_grid.onDblClick.subscribe(function (e) {
    var cell = spc_grid.getCellFromEvent(e);
    var id=data[cell.row].id;
    window.open(
      '../admin/space-transactions/resolve/'+id,
      '_blank' // <- This is what makes it open in a new window.
    );
  });

  // spc_grid.onClick.subscribe(function (e) {
  //   var cell = spc_grid.getCellFromEvent(e);
  //   // if (spc_grid.getColumns()[cell.cell].id == "priority") {
  //   //   if (!spc_grid.getEditorLock().commitCurrentEdit()) {
  //   //     return;
  //   //   }
  //   //   var states = { "Low": "Medium", "Medium": "High", "High": "Low" };
  //   //   data[cell.row].priority = states[data[cell.row].priority];
  //   //   spc_grid.updateRow(cell.row);
  //   //   e.stopPropagation();
  //   // }
  // });

  spc_grid.onContextMenu.subscribe(function (e) {
    e.preventDefault();
    ctx_cell = spc_grid.getCellFromEvent(e);
    window.ctx_row = ctx_cell.row;
    $( "#spcGrid" ).removeClass("saved")
    $( "#spcGrid" ).removeClass("save-error")
    $( "#spcGrid" ).addClass("editing")
    $("#usagesMenu")
        .data("row", window.ctx_row)
        .css("top", 0) //e.pageY)
        .css("left", e.pageX)
        .show();
    $("body").one("click", function () {
      $("#usagesMenu").hide();
    });
  });

  spcView.onRowCountChanged.subscribe(function (e, args) {
    spc_grid.updateRowCount();
    spc_grid.render();
  });

  spcView.onRowsChanged.subscribe(function (e, args) {
    spc_grid.invalidateRows(args.rows);
    spc_grid.render();
  });
  spcView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
    spc_grid.updatePagingStatusFromView( pagingInfo );
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
  //   spcView.setFilterArgs({
  //       percentCompleteThreshold: percentCompleteThreshold,
  //       searchString: searchString
  // });
  spcView.refresh();
  //}
  $("#btnSelectRows").click(function () {
    if (!Slick.GlobalEditorLock.commitCurrentEdit()) {
        return;
  }
  var rows = [];
  for (var i = 0; i < 10 && i < spcView.getLength(); i++) {
      rows.push(i);
  }
  spc_grid.setSelectedRows(rows);
  });
  // initialize the model after all the events have been hooked up
  spcView.beginUpdate();
  spcView.setItems(data);
  // spcView.setFilterArgs({
  // percentCompleteThreshold: percentCompleteThreshold,
  // searchString: searchString
  // });
  // spcView.setFilter(myFilter);
  spcView.endUpdate();
  // if you don't want the items that are not visible (due to being filtered out
  // or being on a different page) to stay selected, pass 'false' to the second arg
  spcView.syncGridSelection(spc_grid, true);
  $("#gridContainer").resizable();
  setTimeout(function () {
      started_set_month=false;
  }, 2500);
  $.LoadingOverlay("hide");
}
function spc_cell_changed(item){
  DotNet.invokeMethodAsync('corelsp', 'SetSpace', JSON.stringify(item)).then(function(saved){
    $( "#spcGrid" ).removeClass("editing")
    $( "#spcGrid" ).addClass(saved?"saved":"save-error")
    //console.dir(resp);
  });
}
function save_usage(val) {
    if (!spc_grid.getEditorLock().commitCurrentEdit()) {
      return;
    }
    switch(spc_grid.getColumns()[ctx_cell.cell].id) {
      case "usageName":
        ctx_data[ctx_cell.row].usageName = val;
        break;
      case "dept":
        ctx_data[ctx_cell.row].dept = val;
        break;
    default:
        return;
    }  
    spc_grid.updateRow(ctx_cell.row);
    spc_cell_changed(ctx_data[ctx_cell.row]);
}
