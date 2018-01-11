function PageDialog(title, msg) {
    $("#dialog-modal").attr("title", title);
    $("#dialog-modal p")[0].innerHTML = msg;
    $("#dialog-modal").dialog({
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function PageConfirmDialog(title, msg) {
    $("#dialog-modal").attr("title", title);
    $("#dialog-modal p")[0].innerHTML = msg;
    $("#dialog-modal").dialog({
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                callback(true);
            },
            Cancel: function () {
                $(this).dialog("close");
                callback(false);
            }
        }
    });
}


$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        var _name = this.name.substr(this.name.lastIndexOf("$") + 1);
        if ($("[name*=" + _name + "]").attr("viewModel")) {
            if (o[$("[name*=" + _name + "]").attr("viewModel")] !== undefined) {
                if (!o[$("[name*=" + _name + "]").attr("viewModel")].push) {
                    o[$("[name*=" + _name + "]").attr("viewModel")] = [o[$("[name*=" + _name + "]").attr("viewModel")]];
                }

                var val = this.value || '';
                //Calendar
                if ($("[name*=" + _name + "]")[0].className.indexOf("calendarui") >= 0 && val != "") {
                    var arrayDate = val.split("/");
                    arrayDate[2] = parseInt(arrayDate[2]);
                    //Format mm/dd/yyyy
                    //val = arrayDate[1] + "/" + arrayDate[0] + "/" + arrayDate[2];

                    //Format dd/mm/yyyy
                    //val = arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2];

                    //Format yyyy/mm/dd
                    val = arrayDate[2] + "/" + arrayDate[1] + "/" + arrayDate[0];
                }

                o[$("[name*=" + _name + "]").attr("viewModel")].push(this.value || '');
            }
            else {
                
                var val = this.value || '';
                //Calendar
                if ($("[name*=" + _name + "]")[0].className.indexOf("calendarui") >= 0 && val != "") {
                    var arrayDate = val.split("/");
                    arrayDate[2] = parseInt(arrayDate[2]);
                    //Format mm/dd/yyyy
                    //val = arrayDate[1] + "/" + arrayDate[0] + "/" + arrayDate[2];

                    //Format dd/mm/yyyy
                    //val = arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2];

                    //Format yyyy/mm/dd
                    val = arrayDate[2] + "/" + arrayDate[1] + "/" + arrayDate[0];
                } 

                o[$("[name*=" + _name + "]").attr("viewModel")] = val;
            }
        } else {

            
        }
    });
    return o;
};
//========================================================================================================

$.fn.clearForm = function () {
    // iterate each matching form
    return this.each(function () {
        // iterate the elements within the form
        $(':input', this).each(function () {
            var type = this.type, tag = this.tagName.toLowerCase();
            if (type == 'text' || type == 'password' || tag == 'textarea')
                this.value = '';
            else if (type == 'checkbox' || type == 'radio')
                this.checked = false;
            else if (tag == 'select') {
                this.selectedIndex = -1;
            }
        });
    });
};

$.fn.enable = function () {
    return this.show().removeAttr("disabled");
}

$.fn.disable = function () {
    return this.hide().attr("disabled", "disabled");
}

$.fn.panelHide = function () {
    this.hide();
}

$.fn.panelShow = function () {
    this.hide();
    this.show("slow");
}

$.fn.serializeJSON = function () {
    var json = {}
    var form = $(this);
    form.find('input, select').each(function () {
        var val
        if (!this.name) return;

        if ('radio' === this.type) {
            if (json[this.name]) { return; }

            json[this.name] = this.checked ? this.value : '';
        } else if ('checkbox' === this.type) {
            val = json[this.name];

            if (!this.checked) {
                if (!val) { json[this.name] = ''; }
            } else {
                json[this.name] =
            typeof val === 'string' ? [val, this.value] :
            $.isArray(val) ? $.merge(val, [this.value]) :
            this.value;
            }
        } else {
            json[this.name] = this.value;
        }
    })
    return json;
}

jQuery(document).ajaxStart(function () {
    openProgress();
});

jQuery(document).ajaxStop(function () {
    closeProgress();
});

jQuery(document).ajaxError(function (e, jqxhr, settings, exception) {
    //--- code=444 no permission
    if (jqxhr.status == 444) {
        var data = jQuery.parseJSON(jqxhr.responseText);
        $(location).attr('href', data.url);
    } else {
        //alert(jqxhr.status + ":" + jqxhr.responseText);
        alert(jqxhr.status + exception);
    }
    closeProgress();
});

openProgress = function () {
    var divProgress = $("#divProgress");
    var divMsg = $(".display-summary");

    if (divMsg != null) {
        divMsg.html("");
    }
    if (divProgress != null) {
        divProgress.dialog({
            height: 150,
            draggable: false,
            modal: true
        });
    }
}

closeProgress = function () {
    var divProgress = $("#divProgress");
    if (divProgress != null) {
        divProgress.dialog("close");
    }
}

parseJsonDate = function (jsonDate) {
    if (jsonDate == null) return null;
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

    if (parts[2] == undefined)
        parts[2] = 0;

    if (parts[3] == undefined)
        parts[3] = 0;

    return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
};

jsonDateToDisplayDate = function (jsonDate) {
    if (jsonDate == null) return "";
    var d = parseJsonDate(jsonDate);
    return $.datepicker.formatDate("dd/mm/yy", d);
};

jsonOnlyDateToDisplayDate = function (jsonDate) {
    if (jsonDate == null) return "";
    var d = parseJsonDate(jsonDate);
    d.setDate(d.getDate() + 1);
    return $.datepicker.formatDate("dd/mm/yy", d);
};

jsonOnlyDateToDisplayDateByPeriod = function (jsonDate, options, rowValue) {
    var defaultFormat = "dd/mm/yy";
    var periodId = "";

    if (options.colModel.formatoptions.periodId != null)
        periodId = options.colModel.formatoptions.periodId;
    else if (options.periodId != null)
        periodId = options.periodId;

    if (periodId != "") {
        switch (periodId.toString()) {
            case "2":
                defaultFormat = "mm/yy";
                break;
            case "3":
                defaultFormat = "yy";
                break;
        }
    }
    if (jsonDate == null) return "";
    var d = parseJsonDate(jsonDate);
    d.setDate(d.getDate() + 1);
    return $.datepicker.formatDate(defaultFormat, d);
};

jsonOnlyDateToDisplayDateByPeriodId = function (jsonDate, periodId) {
    var defaultFormat = "dd/mm/yy";
    if (periodId != "") {
        switch (periodId.toString()) {
            case "2":
                defaultFormat = "mm/yy";
                break;
            case "3":
                defaultFormat = "yy";
                break;
        }
    }
    if (jsonDate == null) return "";
    var d = parseJsonDate(jsonDate);
    d.setDate(d.getDate() + 1);
    return $.datepicker.formatDate(defaultFormat, d);
};

unformatJsonDateByPeriodId = function (jsonDate, options, rowValue) {
    var periodId = "";

    if (options.colModel.formatoptions.periodId != null)
        periodId = options.colModel.formatoptions.periodId;
    else if (options.periodId != null)
        periodId = options.periodId;

    if (periodId != "") {
        switch (periodId.toString()) {
            case "1":
                return jsonDate;
                break;
            case "2":
                var d = $.datepicker.parseDate("dd/mm/yy", "01/" + jsonDate);
                var d2 = new Date(d.getFullYear(), d.getMonth() + 1, 0);
                return $.datepicker.formatDate("dd/mm/yy", d2);
            case "3":
                return "31/12/" + jsonDate;
                break;
        }
    } else {
        return jsonDate;
    }
};

jsonDateToDisplayDateTime = function (jsonDate) {
    if (jsonDate == null) return "";
    var timeZone = new Date().getTimezoneOffset() / 60;
    var d = parseJsonDate(jsonDate);
    var milli = jsonDate.replace(/\/Date\((-?\d+)\)\//, '$1');
    var dt = new Date(parseInt(milli));
    var hour = dt.getHours();
    if (timeZone > 0) {
        hour = hour - timeZone - 1;
    } else {
        hour = (timeZone + 7) + hour;
    }
    var time = (dt.getHours().toString().length == 1 ? "0" + hour : hour) + ":" + (dt.getMinutes().toString().length == 1 ? "0" + dt.getMinutes() : dt.getMinutes()) + ":" + (dt.getSeconds().toString().length == 1 ? "0" + dt.getSeconds() : dt.getSeconds());
    return $.datepicker.formatDate("dd/mm/yy", d) + " " + time;
}

// ======== Calendar EN ===========================================
$.fn.calendar = function (options) {
    options = $.extend({
        dateFormat: "dd/mm/yy",
        changeMonth: true,
        changeYear: true,
        //showButtonPanel: true,
        showAnim: "",
        yearRange: "-100:+5"
    }, options);

    function hideDaysFromCalendar() {
        var thisCalendar = $(this);

    }

    $(this).datepicker(options).focus(hideDaysFromCalendar);
}

//======== Calendar TH ===========================================
//$.fn.calendar = function (options) {
//    options = $.extend({
//        dateFormat: "dd/mm/yy",
//        changeMonth: true,
//        changeYear: true,
//        //showButtonPanel: true,
//        showAnim: "",
//        regional: $.datepicker.regional["th"],
//        beforeShow: function () {
//            if ($(this).val() != "") {
//                var arrayDate = $(this).val().split("/");
//                arrayDate[2] = parseInt(arrayDate[2]) - 543;
//                $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
//            }
//            setTimeout(function () {
//                $.each($(".ui-datepicker-year option"), function (j, k) {
//                    var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
//                    $(".ui-datepicker-year option").eq(j).text(textYear);
//                });
//            }, 50);
//        },
//        onChangeMonthYear: function () {
//            setTimeout(function () {
//                $.each($(".ui-datepicker-year option"), function (j, k) {
//                    var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
//                    $(".ui-datepicker-year option").eq(j).text(textYear);
//                });
//            }, 50);
//        },
//        onClose: function () {
//            //if ($(this).val() != "" && $(this).val() == dateBefore) {
//            if ($(this).val() != "" ) {
//                var arrayDate = $(this).val().split("/");
//                arrayDate[2] = parseInt(arrayDate[2]) + 543;
//                $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
//            }
//        },
//        onSelect: function (dateText, inst) {
//            dateBefore = $(this).val();
//            var arrayDate = dateText.split("/");
//            arrayDate[2] = parseInt(arrayDate[2]) + 543;
//            $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
//        }
//    }, options);


//    function hideDaysFromCalendar() {
//        var thisCalendar = $(this);
//    }

//    $(this).datepicker(options).focus(hideDaysFromCalendar);
//}

$.fn.monthYearPicker = function (options) {
    options = $.extend({
        dateFormat: "mm/yy",
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        showAnim: ""
    }, options);
    function hideDaysFromCalendar() {
        var thisCalendar = $(this);
        $('.ui-datepicker-calendar').detach();
        $(".ui-datepicker-current").hide();
        // Also fix the click event on the Done button.
        $('.ui-datepicker-close').unbind("click").click(function () {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            thisCalendar.datepicker('setDate', new Date(year, month, 1));
            //thisCalendar.datepicker("hide");
        });

        $(".ui-datepicker-prev, .ui-datepicker-next").remove();
    }
    $(this).datepicker(options).focus(hideDaysFromCalendar);
}

$.fn.yearPicker = function (options) {
    options = $.extend({
        dateFormat: "yy",
        changeMonth: false,
        changeYear: true,
        showButtonPanel: true,
    }, options);
    function hideDaysFromCalendar() {
        var thisCalendar = $(this);

        $('.ui-datepicker-calendar').detach();
        $(".ui-datepicker-month").hide();
        $(".ui-datepicker-current").hide();

        $('.ui-datepicker-close').unbind("click").click(function () {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            thisCalendar.datepicker('setDate', new Date(year, 1, 1));
            //thisCalendar.datepicker("hide");
        });

        //$(".ui-datepicker-year").unbind("change").change(function () {
        //    thisCalendar.datepicker('setDate', new Date($(this).val(), 1, 1));
        //    thisCalendar.datepicker("hide");
        //});

        $(".ui-datepicker-prev, .ui-datepicker-next").remove();
    }
    $(this).datepicker(options).focus(hideDaysFromCalendar);
}


//$.numberParentheses = function (number, decimal) {
//    if (number == null) return "";
//    if (decimal == null) decimal = 0;
//    var numberFmt = $.number(Math.abs(number), decimal);
//    if (number < 0) numberFmt = "(" + numberFmt + ")";
//    return numberFmt;
//}

//$.fn.fmatter.numberParentheses = function (cellval, opts) {
//    if (cellval == null) {
//        return '';
//    }

//    var op = $.extend({}, opts.currency);
//    var numberFmt = $.fmatter.util.NumberFormat(cellval, op);
//    if (cellval < 0) numberFmt = "(" + numberFmt.replace("-", "") + ")";
//    return numberFmt;
//};



//function headerAlign(header, align) {
//    var title = "";
//    if (jQuery.type(header) == "object") {
//        title = header.title;
//        if (align == null) {
//            if (header.align == null || header.align == "") {
//                align = "center";
//            } else {
//                align = header.align;
//            }
//        }
//    } else {
//        title = header;
//        if (align == null) align = "center";
//    }
//    return "<div style='text-align:" + align + "'>" + title + "</div>";
//}

//function groupHeader(header) {
//    if (jQuery.type(header) == "object") {
//        return "<div>" + header.title + "</div>";
//    } else {
//        return "<div>" + header + "</div>";
//    }
//}


//jqGridNegativeFormat = function (rowId, value, rowObject, colModel, arrData) {
//    var val = parseFloat(arrData[colModel.name]);
//    if (val > 0)
//        return "style=color:green;";
//    else if (val == 0)
//        return '';
//    else if (val < 0)
//        return 'style=color:red;';
//}

//jQuery.extend(jQuery.jgrid.defaults, {
//    onSelectRow: function (rowid, e) {
//        try {
//            $('#' + rowid).parents('table').resetSelection();
//        } catch (e) {

//        }
//    }
//});

//$.fn.fmatter.currencyK = function (cellval, opts) {
//    if (cellval == null) {
//        return '';
//    }

//    var op = $.extend({}, opts.currency);
//    return $.fmatter.util.NumberFormat(cellval / 1000.00, op);
//};

//$.fn.fmatter.percentAll = function (cellval, opts) {
//    if (cellval == null) {
//        return '';
//    }

//    var op = $.extend({}, opts.currency);
//    return $.fmatter.util.NumberFormat(cellval, op);
//};

//$.fn.fmatter.percentChange = function (cellval, opts) {
//    if (cellval == null) {
//        return '';
//    }

//    var op = $.extend({}, opts.currency);
//    return $.fmatter.util.NumberFormat(cellval, op);
//};
