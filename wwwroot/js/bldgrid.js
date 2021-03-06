function isIEPreVer9() { var v = navigator.appVersion.match(/MSIE ([\d.]+)/i); return (v ? v[1] < 9 : false); }
var dataView;
var grid;
function processCols(cols){
  for(i in cols)
    for(j in cols[i]){
      if(j &&  cols[i][j]==="Slick.Editors.Text")
        cols[i][j]=Slick.Editors.Text;
      if(j &&  cols[i][j]==="Slick.Editors.Date")
        cols[i][j]=Slick.Editors.Date;
      if(j &&  cols[i][j]==="Slick.Formatters.PercentCompleteBar")
        cols[i][j]=Slick.Formatters.PercentCompleteBar ;
      if(j && cols[i][j]==="Slick.Formatters.Checkbox")
        cols[i][j]=Slick.Formatters.Checkbox;
      if(j &&  cols[i][j]==="Slick.Editors.Checkbox")
        cols[i][j]=Slick.Editors.Checkbox;
      if(j &&  cols[i][j]==="Slick.Editors.PercentComplete")
        cols[i][j]=Slick.Editors.PercentComplete;
      if(j &&  cols[i][j]==="AutoCompleteEditor")
        cols[i][j]=AutoCompleteEditor;
      if(j &&  cols[i][j]==="DataSource.Departments")
        cols[i][j]=DotNet.invokeMethodAsync('corelsp', 'Departments');
      if(j &&  cols[i][j]==="DataSource.Usages")
        cols[i][j]=DotNet.invokeMethodAsync('corelsp', 'Usages');
      if(j &&  cols[i][j]==="DataSource.FloorsList")
        cols[i][j]=DotNet.invokeMethodAsync('corelsp', 'FloorsList');
      if(j &&  cols[i][j]==="DataSource.Labels")
        cols[i][j]=DotNet.invokeMethodAsync('corelsp', 'Labels');
      if(j &&  cols[i][j]==="Slick.Formatters.Fixed2")
        cols[i][j]=function(r,c,v,cd,dc){return parseFloat(v).toFixed(2)};
      // console.log(j+":"+ cols[i][j]);
    }
    // console.dir(cols);
    return cols;
}

var columns = []
var options = {
  columnPicker: {
    columnTitle: "Columns",
    hideForceFitButton: false,
    hideSyncResizeButton: false, 
    forceFitTitle: "Force fit columns",
    syncResizeTitle: "Synchronous resize",
  },
  editable: true,
  enableAddRow: true,
  enableCellNavigation: true,
  asyncEditorLoading: true,
  forceFitColumns: false,
  topPanelHeight: 25
};
var sortcol = "title";
var sortdir = 1;
var percentCompleteThreshold = 0;
var searchString = "";
function requiredFieldValidator(value) {
  if (value == null || value == undefined || !value.length) {
    return {valid: false, msg: "This is a required field"};
  }
  else {
    return {valid: true, msg: null};
  }
}
// function myFilter(item, args) {
//   if (item["percentComplete"] < args.percentCompleteThreshold) {
//     return false;
//   }
//   if (args.searchString != "" && item["title"].indexOf(args.searchString) == -1) {
//     return false;
//   }
//   return true;
// }
// function percentCompleteSort(a, b) {
//   return a["percentComplete"] - b["percentComplete"];
// }
function comparer(a, b) {
  var x = a[sortcol], y = b[sortcol];
  return (x == y ? 0 : (x > y ? 1 : -1));
}
function toggleFilterRow() {
  grid.setTopPanelVisibility(!grid.getOptions().showTopPanel);
}
$(".grid-header .ui-icon")
        .addClass("ui-state-default ui-corner-all")
        .mouseover(function (e) {
          $(e.target).addClass("ui-state-hover")
        })
        .mouseout(function (e) {
          $(e.target).removeClass("ui-state-hover")
        });
function display_grid(data,cols){
    //console.dir(data);
    columns=processCols(cols);
    dataView = new Slick.Data.DataView({ inlineFilters: true });
    grid = new Slick.Grid("#myGrid", dataView, columns, options);
    grid.setSelectionModel(new Slick.RowSelectionModel());
    var pager = new Slick.Controls.Pager(dataView, grid, $("#pager"));
    var columnpicker = new Slick.Controls.ColumnPicker(columns, grid, options);
    // move the filter panel defined in a hidden div into grid top panel
    $("#inlineFilterPanel")
        .appendTo(grid.getTopPanel())
        .show();
    grid.onCellChange.subscribe(function (e, args) {
      dataView.updateItem(args.item.id, args.item);
    });
    grid.onAddNewRow.subscribe(function (e, args) {
      var item = {"num": data.length, "id": "new_" + (Math.round(Math.random() * 10000)), "title": "New task", "duration": "1 day", "percentComplete": 0, "start": "01/01/2009", "finish": "01/01/2009", "effortDriven": false};
      $.extend(item, args.item);
      dataView.addItem(item);
    });
    grid.onKeyDown.subscribe(function (e) {
      // select all rows on ctrl-a
      if (e.which != 65 || !e.ctrlKey) {
          return false;
      }
      var rows = [];
      for (var i = 0; i < dataView.getLength(); i++) {
          rows.push(i);
      }
      grid.setSelectedRows(rows);
        e.preventDefault();
    });
    grid.onClick.subscribe(function (e) {
      var cell = grid.getCellFromEvent(e);
      var id=data[cell.row].id;
      //console.log('opening bid='+id);
      DotNet.invokeMethodAsync('corelsp', 'SetBuilding', id);
      // if (grid.getColumns()[cell.cell].id == "priority") {
      //   if (!grid.getEditorLock().commitCurrentEdit()) {
      //     return;
      //   }
      //   var states = { "Low": "Medium", "Medium": "High", "High": "Low" };
      //   data[cell.row].priority = states[data[cell.row].priority];
      //   grid.updateRow(cell.row);
      //   e.stopPropagation();
      // }
    });

    grid.onDblClick.subscribe(function (e) {
      var cell = grid.getCellFromEvent(e);
      var id=data[cell.row].id;
      window.open(
        '../admin/building-transactions/resolve/'+id,
        '_blank' // <- This is what makes it open in a new window.
      );
    });

    dataView.onRowCountChanged.subscribe(function (e, args) {
      grid.updateRowCount();
      grid.render();
    });

    dataView.onRowsChanged.subscribe(function (e, args) {
      grid.invalidateRows(args.rows);
      grid.render();
    });
    dataView.onPagingInfoChanged.subscribe(function (e, pagingInfo) {
      grid.updatePagingStatusFromView( pagingInfo );
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
    //   dataView.setFilterArgs({
    //       percentCompleteThreshold: percentCompleteThreshold,
    //       searchString: searchString
    // });
    dataView.refresh();
    //}
    $("#btnSelectRows").click(function () {
      if (!Slick.GlobalEditorLock.commitCurrentEdit()) {
          return;
    }
    var rows = [];
    for (var i = 0; i < 10 && i < dataView.getLength(); i++) {
        rows.push(i);
    }
    grid.setSelectedRows(rows);
    });
    // initialize the model after all the events have been hooked up
    dataView.beginUpdate();
    dataView.setItems(data);
    // dataView.setFilterArgs({
    // percentCompleteThreshold: percentCompleteThreshold,
    // searchString: searchString
    // });
    // dataView.setFilter(myFilter);
    dataView.endUpdate();
    // if you don't want the items that are not visible (due to being filtered out
    // or being on a different page) to stay selected, pass 'false' to the second arg
    dataView.syncGridSelection(grid, true);
    $("#gridContainer").resizable();
}