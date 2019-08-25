function SpaceEditor(args) {
    var $from, $to;
    var scope = this;
    this.init = function () {
      $from = $("<INPUT type=text style='width:140px' />")
          .appendTo(args.container)
          .bind("keydown", scope.handleKeyDown);
      $(args.container).append("&nbsp; to &nbsp;");
    //   $to = $("<INPUT type=text style='width:40px' />")
    //       .appendTo(args.container)
    //       .bind("keydown", scope.handleKeyDown);
    //   scope.focus();
    };
    this.handleKeyDown = function (e) {
      if (e.keyCode == $.ui.keyCode.LEFT || e.keyCode == $.ui.keyCode.RIGHT || e.keyCode == $.ui.keyCode.TAB) {
        e.stopImmediatePropagation();
      }
    };
    this.destroy = function () {
      $(args.container).empty();
    };
    this.focus = function () {
      $from.focus();
    };
    this.serializeValue = function () {
      //return {from: parseInt($from.val(), 10), to: parseInt($to.val(), 10)};
      return parseInt($from.val(), 10);
    };
    this.applyValue = function (item, state) {
      item.from = state.from;
      //item.to = state.to;
    };
    this.loadValue = function (item) {
      $from.val(item.from);
      $to.val(item.to);
    };
    this.isValueChanged = function () {
      return args.item.from != parseInt($from.val(), 10) || args.item.to != parseInt($from.val(), 10);
    };
    this.validate = function () {
      if (isNaN(parseInt($from.val(), 10)) ) {
    //   if (isNaN(parseInt($from.val(), 10)) || isNaN(parseInt($to.val(), 10))) {
        return {valid: false, msg: "Please type in valid numbers."};
      }
    //   if (parseInt($from.val(), 10) > parseInt($to.val(), 10)) {
    //     return {valid: false, msg: "'from' cannot be greater than 'to'"};
    //   }
      return {valid: true, msg: null};
    };
    this.init();
}
function AutoCompleteEditor(args) {
    var $input;
    var defaultValue;
    var scope = this;
    var calendarOpen = false;
  
     this.keyCaptureList = [ Slick.keyCode.UP, Slick.keyCode.DOWN, Slick.keyCode.ENTER ];
  
    this.init = function () {
        $input = $("<INPUT id='tags' class='editor-text' />");
        $input.appendTo(args.container);
        $input.focus().select();
  
        $input.autocomplete({
            source: args.column.dataSource.map(item=>{return item.value;})
        });
    };
  
    this.destroy = function () {
        $input.autocomplete("destroy");
        $input.remove();
   };
  
    this.focus = function () {
        $input.focus();
    };
  
    this.loadValue = function (item) {
        defaultValue = item[args.column.field];
        $input.val(defaultValue);
        $input[0].defaultValue = defaultValue;
        $input.select();
    };
  
    this.serializeValue = function () {
        // return $input.val();
        return args.column.dataSource.find(x => x.value === $input.val()).key
    };
  
    this.applyValue = function (item, state) {
        item[args.column.field] = state;
    };
  
    this.isValueChanged = function () {
        return (!($input.val() == "" && defaultValue == null)) && ($input.val() != defaultValue);
    };
  
    this.validate = function () {
        return {
            valid: true,
            msg: null
        };
    };
  
    this.init();
  }
  