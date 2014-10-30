(function () {

  var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
      timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
      timezoneClip = /[^-+\dA-Z]/g,
      pad = function (val, len) {
        val = String(val);
        len = len || 2;
        while (val.length < len) val = "0" + val;
        return val;
      };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
      var dF = dateFormat;

      // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
      if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
        mask = date;
        date = undefined;
      }

      // Passing date through Date applies Date.parse, if necessary
      date = date ? new Date(date) : new Date;
      if (isNaN(date)) throw SyntaxError("invalid date");

      mask = String(dF.masks[mask] || mask || dF.masks["default"]);

      // Allow setting the utc argument via the mask
      if (mask.slice(0, 4) == "UTC:") {
        mask = mask.slice(4);
        utc = true;
      }

      var _ = utc ? "getUTC" : "get",
        d = date[_ + "Date"](),
        D = date[_ + "Day"](),
        m = date[_ + "Month"](),
        y = date[_ + "FullYear"](),
        H = date[_ + "Hours"](),
        M = date[_ + "Minutes"](),
        s = date[_ + "Seconds"](),
        L = date[_ + "Milliseconds"](),
        o = utc ? 0 : date.getTimezoneOffset(),
        flags = {
          d: d,
          dd: pad(d),
          ddd: dF.i18n.dayNames[D],
          dddd: dF.i18n.dayNames[D + 7],
          m: m + 1,
          mm: pad(m + 1),
          mmm: dF.i18n.monthNames[m],
          mmmm: dF.i18n.monthNames[m + 12],
          yy: String(y).slice(2),
          yyyy: y,
          h: H % 12 || 12,
          hh: pad(H % 12 || 12),
          H: H,
          HH: pad(H),
          M: M,
          MM: pad(M),
          s: s,
          ss: pad(s),
          l: pad(L, 3),
          L: pad(L > 99 ? Math.round(L / 10) : L),
          t: H < 12 ? "a" : "p",
          tt: H < 12 ? "am" : "pm",
          T: H < 12 ? "A" : "P",
          TT: H < 12 ? "AM" : "PM",
          Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
          o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
          S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
        };

      return mask.replace(token, function ($0) {
        return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
      });
    };
  }();

  // Some common format strings
  dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
  };

  // Internationalization strings
  dateFormat.i18n = {
    dayNames: [
      "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
      "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
      "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
      "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
  };

  // For convenience...
  Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
  };


  Date.prototype.addDays = function (num) {
    var value = this.valueOf();
    value += 86400000 * num;
    return new Date(value);
  }

  Date.prototype.addSeconds = function (num) {
    var value = this.valueOf();
    value += 1000 * num;
    return new Date(value);
  }

  Date.prototype.addMinutes = function (num) {
    var value = this.valueOf();
    value += 60000 * num;
    return new Date(value);
  }

  Date.prototype.addHours = function (num) {
    var value = this.valueOf();
    value += 3600000 * num;
    return new Date(value);
  }

  Date.prototype.addMonths = function (num) {
    var value = new Date(this.valueOf());

    var mo = this.getMonth();
    var yr = this.getYear();

    mo = (mo + num) % 12;
    if (0 > mo) {
      yr += (this.getMonth() + num - mo - 12) / 12;
      mo += 12;
    }
    else
      yr += ((this.getMonth() + num - mo) / 12);

    value.setMonth(mo);
    value.setYear(yr);
    return value;
  }


  var JHelper = {
    //设置页面内容高度
    initLeftMenu: function (e) {
      var self = this;
      $('.easyui-accordion li a').click(function (e) {
        e.preventDefault();
        var target = $(e.currentTarget);
        var tabTitle = target.text();
        var url = target.attr("href");
        var css = target.find('span').attr('class').replace('link-icon', '');
        $('.easyui-accordion li a').removeClass('selected');
        target.addClass('selected');
        self.addTab(tabTitle, url, css);
      })
    },
    addTab: function (subtitle, url, css) {
      var self = this;
      if (!top.$('#tabs').tabs('exists', subtitle)) {
        top.$('#tabs').tabs('add', {
          title: subtitle,
          content: self.createFrame(url),
          //href: url,
          closable: true,
          iconCls: css
          //width: $('#mainPanle').width() - 10,
          //height: $('#mainPanle').height() - 26
        });
        var tab = top.$('#tabs').tabs('getTab', subtitle);
        tab.css("overflow", "hidden");
      } else {
        top.$('#tabs').tabs('select', subtitle);
      }
      self.tabClose();
    },
    createFrame: function (url) {
      var s = '<iframe frameborder="0" scrolling="no" name="mainFrame" src="' + url + '" style="width:100%;height:100%; margin:0;padding:0;border:0"></iframe>';
      return s;
    },


    tabClose: function () {
      top.$(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children("span").text();
        top.$('#tabs').tabs('close', subtitle);
      })

      top.$(".tabs-inner").bind('contextmenu', function (e) {
        e.preventDefault();
        top.$('#mm').menu('show', {
          left: e.pageX,
          top: e.pageY,
        });

        var subtitle = $(this).children("span").text();
        top.$('#mm').data("currtab", subtitle);

        return false;
      });
    },
    tabCloseEven: function () {
      //关闭当前
      $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
      })

      $('#mm-tabrefresh').click(function () {
        var currtab_title = $('#mm').data("currtab");
        var tab = $('#tabs').tabs('getTab', currtab_title);
        tab.panel('refresh');
      })
      //全部关闭
      $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
          var t = $(n).text();
          $('#tabs').tabs('close', t);
        });
      });
      //关闭除当前之外的TAB
      $('#mm-tabcloseother').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('.tabs-inner span').each(function (i, n) {
          var t = $(n).text();
          if (t != currtab_title)
            $('#tabs').tabs('close', t);
        });
      });
      //关闭当前右侧的TAB
      $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
          //msgShow('系统提示','后边没有啦~~','error');
          alert('后边没有啦~~');
          return false;
        }
        nextall.each(function (i, n) {
          var t = $('a:eq(0) span', $(n)).text();
          $('#tabs').tabs('close', t);
        });
        return false;
      });
      //关闭当前左侧的TAB
      $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
          alert('到头了，前边没有啦~~');
          return false;
        }
        prevall.each(function (i, n) {
          var t = $('a:eq(0) span', $(n)).text();
          $('#tabs').tabs('close', t);
        });
        return false;
      });

      //退出
      $("#mm-exit").click(function () {
        $('#mm').menu('hide');
      })
    },



    /******************************
    ***************常用方法*****************
    ********************************/


    showMessage: function (res) {
      if (res.Success) {
        $.messager.show({
          title: '操作提示',
          msg: res.Message,
          timeout: 1000,
          showType: 'slide'
        });
      } else {
        $.messager.alert('操作失败', res.Message, 'error');
      }
    },
    showError: function (title, message) {
      $.messager.alert(title, message, 'error');
    },


    /******************************
  ***************formatter*****************
  ********************************/


    //EasyUI用DataGrid用日期格式化
    TimeFormatter: function (value, rec, index) {
      if (value == undefined) {
        return "";
      }
      /*json格式时间转js时间格式*/
      value = value.substr(1, value.length - 2);
      var obj = eval('(' + "{Date: new " + value + "}" + ')');
      var dateValue = obj["Date"];
      if (dateValue.getFullYear() < 1900) {
        return "";
      }
      var val = dateValue.format("yyyy-mm-dd HH:MM");
      return val.substr(11, 5);
    },
    DateTimeFormatter: function (value, rec, index) {
      if (value == undefined) {
        return "";
      }
      /*json格式时间转js时间格式*/
      value = value.substr(1, value.length - 2);
      var obj = eval('(' + "{Date: new " + value + "}" + ')');
      var dateValue = obj["Date"];
      if (dateValue.getFullYear() < 1900) {
        return "";
      }

      return dateValue.format("yyyy-mm-dd HH:MM");
    },

    //EasyUI用DataGrid用日期格式化
    DateFormatter: function (value, rec, index) {
      if (value == undefined) {
        return "";
      }
      /*json格式时间转js时间格式*/
      value = value.substr(1, value.length - 2);
      var obj = eval('(' + "{Date: new " + value + "}" + ')');
      var dateValue = obj["Date"];
      if (dateValue.getFullYear() < 1900) {
        return "";
      }

      return dateValue.format("yyyy-mm-dd");
    },


    //EasyUI用Datebox用日期格式化
    DateboxFormatter: function (value) {
      if (value == undefined) {
        return "";
      }
      /*json格式时间转js时间格式*/

      var dateValue = new Date(value);
      if (dateValue.getFullYear() < 1900) {
        return "";
      }

      return dateValue.format("yyyy-mm-dd");
    },
    BoolFormatter: function (value, rec, index) {
      var obj = {
        "true": "是",
        "false": "否"
      };
      return obj[value.toString()];
    },
    IsDeleteFormatter: function (value, rec, index) {
      var obj = {
        "true": "否",
        "false": "是"
      };
      return obj[value.toString()];
    },
    BoolFormatterSex: function (value, rec, index) {
      var obj = {
        "true": "女",
        "false": "男"
      };
      return obj[value.toString()];
    },
    BoolFormatterBarthday: function (value, rec, index) {
      var obj = {
        "true": "阴历",
        "false": "阳历"
      };
      return obj[value.toString()];
    },
    DownLoadFormatter: function (value, rec, index) {
      if (value == undefined || value == '') {
        return "";
      }
      else {
        return '<a href="' + value + '" target="_blank">查看附件</a>';
      }
    },
    LinkCompanyFormatter: function (value, rec, index) {
      if (value == undefined || value == '') {
        return "";
      }
      else {
        return "<a href='javascript:void(0);' onclick=\"Mt.JHelper.addTab('" + value + "', '" + rec.CompanyLinkUrl + "', 'icon-view');\" >" + value + "</a>";
      }
    }
  }

  window.Mt = window.Mt || {};
  window.Mt.JHelper = JHelper;
  $(function () {
    Mt.JHelper.initLeftMenu();
    Mt.JHelper.tabCloseEven();
  })
})()


