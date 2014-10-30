(function ($) {

  $.extend($.fn, {
    serializeFormJSON:
      function () {
        var o = {},
            a = this.serializeArray(),
            re = /\w+\-(.*)/;

        $.each(a, function () {
          // convert input attributes to lower camel case so the API
          // can interpret them correctly
          if (re.test(this.name)) {
            var name = this.name.split('_');
            name[1] = name[1].charAt(0).toUpperCase() + name[1].substr(1);
            this.name = name[0] + name[1];
          }

          if (o[this.name]) {
            o[this.name] += "," + this.value;
          } else {
            o[this.name] = this.value || '';
          }
        });
        return o;
      }
  });
})(jQuery);