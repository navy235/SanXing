(function ($) {
  $.extend($.fn, {
    treeCombo:
    function (setting) {
      if (!setting) {
        setting = {};
      }
      var ps = $.extend({
        method: 'get',
        multiple: false,
        width: 186,
        url: '',
        prefix: '',
        value: '',
        callback: null
      }, setting);
      var that = this;
      var id = that.attr('id');
      var comboxId = id + '_ComboTree';
      var currentValue = ps.value;
      if (ps.multiple) {
        currentValue = currentValue.split(',');
      }
      function binding() {
        createComboTree();
      }

      function createComboTree() {
        var control = $('<select />').attr('id', comboxId)
        if (ps.multiple) {
          control.prop('multiple', true);
        }
        that.after(control);
        var opts = $.extend(ps, {
          onChange: onChange,
          onHidePanel: onHidePanel
        })
        opts.value = null;

        if (currentValue.length > 0) {
          if (ps.multiple) {
            opts.setValues = currentValue;
          }
        }
        control.combotree(opts);
        if (currentValue.length > 0 && !ps.multiple) {
          control.combotree('setValue', currentValue);
        }
      }

      function onChange(newvalue, oldvalue) {
        //var value = newvalue.concat(newvalue);
        //value = _.uniq(value);
        //过滤父节点
        var valArr = [];
        var paraArr = [];
        if (!ps.multiple) {
          paraArr.push(newvalue);
        } else {
          paraArr = newvalue;
        }
        $.each(paraArr, function (index, item) {
          if (ps.prefix != '') {
            if (item.indexOf(ps.prefix) == -1) {
              valArr.push(item);
            }
          } else {
            valArr.push(item);
          }
        })
        var textVal = valArr.join(",");
        that.val(textVal);
        setTimeout(function () {
          that.parents('form:first').validate().element('#' + id);
          if (ps.callback) {
            ps.callback(textVal);
          }
        }, 0)
      }

      function onHidePanel(e) {
        if (that.val() == '') {
          $('#' + comboxId).combotree('textbox').val('');
        }
      }

      binding();
    }
  })
})(jQuery)