function Extention() {

    var _$ = this;
    var ToggleInitializerCheck = false;
    skipAjax = false;




    _$._messages = {
        Success: "",
        Eror: "",
        Add: "RECORD HAS BEEN ADDED",
        Update: "RECORD HAS BEEN UPDATED",
        Delete: "RECORD HAS BEEN DELETED",
        Required: "PLEASE FILL DESIRED FIELDS TO CONTINUE.",
        InProcess: "YOUR DESIRED REQUEST IS IN PROCESS, WE WILL NOTIFY YOU ONCE THE PROCESS IS COMPLETE.",
        NotFound: "NO RECORDS FOUND IN SHEET."
    };

    _$.tableCreateToExcel = function (fileName, tbleid) {
        $("#" + tbleid).table2excel({
            filename: fileName + ".xls"
        });
    }
    _$.tableToExcelCreate = function (filename, tableId) {
        _$.ShowLoader();
        $("#" + tableId).table2excel({
            filename: filename + ".xls"
        });
        _$.HideLoader();
    }

    _$.getId = function () {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    }


    _$.ValidateEmail = function (Email) {
        var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        if (Email.match(mailformat)) {
            return true;
        }
        else {
            return false;
        }
    }

    _$.PostAsync = function (_url, _data, async, _onsuccess) {
        $.ajax({
            url: _url,
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify(_data),
            async: async,
            success: _onsuccess,
            complete: function () {
                $(".CustomDisableClass2").each(function (index, element) {
                    $(element).removeClass("disabled");
                    $(element).removeClass("CustomDisableClass2");
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //.Notification(jqXHR, result.Status)
                console.log(textStatus, errorThrown);
            }
        });
    }

    _$.Post = function (_url, _data, _onsuccess) {
        $.ajax({
            url: _url,
            type: "POST",
            dataType: "json",
            data: _data,
            //contentType: "application/json",
            //data: JSON.stringify(_data),
            success: _onsuccess,
            complete: function () {
                $(".CustomDisableClass2").each(function (index, element) {
                    $(element).removeClass("disabled");
                    $(element).removeClass("CustomDisableClass2");
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //.Notification(jqXHR, result.Status)
                console.log(textStatus, errorThrown);
                _$.Notification('Server is not responding...', 200);
                _$.HideLoader();
            }
        });
    }

    _$.PostFormData = function (_url, _data, _onsuccess) {
        $.ajax({
            url: _url,
            type: "POST",
            dataType: "json",
            contentType: false,
            processData: false,
            data: _data,
            success: _onsuccess,
            complete: function () {
                $(".CustomDisableClass2").each(function (index, element) {
                    $(element).removeClass("disabled");
                    $(element).removeClass("CustomDisableClass2");
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }

    _$.GETHTML = function (_url, _data, async, _onsuccess) {
        $.ajax({
            url: _url,
            type: "POST",
            dataType: "html",
            contentType: "application/json",
            data: JSON.stringify(_data),
            async: async,
            success: _onsuccess,
            error: function (jqXHR, textStatus, errorThrown) {
                _$.HideLoader();
                console.log(textStatus, errorThrown);
            }
        });
    }

    _$.GETRequest = function (_url, async, _onsuccess) {
        $.ajax({
            url: _url,
            type: "POST",
            dataType: "json",
            contentType: false,
            async: async,
            success: _onsuccess,
            error: function (jqXHR, textStatus, errorThrown) {
                _$.HideLoader();
                console.log(textStatus, errorThrown);
            }
        });
    }

    _$.GetTextCombobox = function (id, Key) {
        return $(id).find('option[value=' + Key + ']').text();
    }

    _$.SetTextCombobox = function (id, key) {
        return $(id).val(key).change();
    }

    _$.RemoveObjectFromScreen = function (id) {
        return $(id).remove();
    }


    _$.ModalShow = function (id) {
        $("#" + id).modal('show');
    }

    _$.ModalHide = function (id) {
        $("#" + id).modal('hide');
    }

    _$.parseJsonDate = function (jsonDate) {
        if (jsonDate != null) {
            var dateString = jsonDate.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            day = ((day) > 9 ? (day) : "0" + (day));
            month = ((month) > 9 ? (month) : "0" + (month));

            var date = day + "/" + month + "/" + year;
            return date;
        }
        return jsonDate;
    };
    _$.parseJsonTime = function (jsonDate) {
        ;
        var date = eval('new' + jsonDate.replace(/\//g, ' '));
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }
    _$.parseJsonDateTime = function (jsonDate) {
        if (jsonDate != null) {
            var dateString = jsonDate.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();

            day = ((day) > 9 ? (day) : "0" + (day));
            month = ((month) > 9 ? (month) : "0" + (month));

            var date = day + "/" + month + "/" + year + " " + currentTime.toLocaleTimeString();
            return date;
        }
        return jsonDate;
    };

    _$.parseJsonDateTimeReturningDate = function (jsonDate) {
        if (jsonDate != null) {
            var dateString = jsonDate.substr(6);
            var currentTime = new Date(parseInt(dateString));
            return currentTime;
        }
        return jsonDate;
    };
    _$.Notification = function (_message, _type) {
        _type = parseInt(_type);

        switch (_type) {
            case 100:
                _title = "Success";
                toastr.success(_message, _title);
                break;
            case 200:
                _title = "Error"
                toastr.error(_message, _title);
                break;
            case 300:
                _title = "Warning"
                toastr.warning(_message, _title);
                break;
            case 400:
                _title = "NotFound"
                toastr.warning(_message, _title);
                break;
            case 500:
                _title = "Delete"
                toastr.success(_message, _title);
                break;
            case 600:
                _title = "Update"
                toastr.success(_message, _title);
                break;
            case 700:
                _title = "IN PROCESS"
                toastr.warning(_message, _title);
                break;
        }
    }

    _$.ParseString = function (data) {
        var Value = data.split("-")[0];
        return Value.replace(/_/g, '');
    }

    _$.ExcelToJSON = function (file) {
        this.parseExcel = function (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var data = e.target.result();
                var workbook = XLSX.read(data, { type: 'binary' });

                workbook.SheetNames.forEach(function (sheetName) {
                    // Here is your object
                    var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                    var json_object = JSON.stringify(XL_row_object);
                    console.log(json_object);
                })
            };

            reader.onerror = function (ex) {
                console.log(ex);
            };

            reader.readAsBinaryString(file);
        };
    };
    _$.GetFileSize = function (file) {
        return ((file.size / 1024) / 1024).toFixed(4);
    }
    _$.serializeForm = function (id) {
        var result = {};
        $.each($(id).serializeArray(), function (i, field) {
            result[field.name] = field.value.trim() || null;
        });
        return result;
    }

    _$.UpdateURLParameter = function (url, param, paramVal) {
        var newAdditionalURL = "";
        var tempArray = url.split("?");
        var baseURL = tempArray[0];
        var additionalURL = tempArray[1];
        var temp = "";
        if (additionalURL) {
            tempArray = additionalURL.split("&");
            for (i = 0; i < tempArray.length; i++) {
                if (tempArray[i].split('=')[0] != param) {
                    newAdditionalURL += temp + tempArray[i];
                    temp = "&";
                }
            }
        }

        var rows_txt = temp + "" + param + "=" + paramVal;
        return baseURL + "?" + newAdditionalURL + rows_txt;
    }

    _$.ResetClearContent = function (id) {
        $.each($(id + " input:not(:hidden),input[type=radio],input[type=checkbox],input[type=text],select,input[type=textarea]"), function (i, field) {
            if ($(field).is('input:checkbox')) {
                $(field).prop("checked", false);
            }
            else {
                $(field).val('');
            }
        });
    }

    _$.ResetAllContent = function (id) {
        $.each($(id + " input:hidden,input[type=radio],input[type=checkbox],input[type=text],select,input[type=textarea]"), function (i, field) {
            if ($(field).is('input:checkbox')) {
                $(field).prop("checked", false);
            }
            else {
                $(field).val('');
            }
        });
    }

    _$.GetPartialView = function (id, url) {
        $(id).load(url);
    }

    _$.CheckLineBox = function (prop) {
        switch (prop) {
            case "add":
                $(".check-line div").addClass("checked");
                break;
            case "remove":
                $(".check-line div").removeClass("checked");
                break;
        }
    }

    _$.findWithAttr = function (array, attr, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][attr] === value) {
                return i;
            }
        }
        return -1;
    }

    _$.SetCheckBox = function (id, status) {
        return $(id).prop("checked", status);
    }

    _$.GetCheckBox = function (id) {
        return $(id).prop("checked") === true ? 0 : 1;
    }

    _$.DataTable = function (Selector, groupcolumn, colspan) {
        var flag = false;
        var rows = [];
        if ($(Selector).length > 0) {
            $(Selector).addClass('compact');
            $(Selector).each(function () {
                $(Selector).find('tbody > tr').each(function () {
                    if ($(this).find('td').length == 1) {
                        this.innerHTML = '';
                        flag = true;
                    }
                    else {
                        rows.push(this);
                    }
                })
                if (!$(this).hasClass("dataTable-custom")) {
                    var opt = {
                        "pagingType": "full_numbers",
                        "destroy": true,
                        "oLanguage": {
                            //"search": "<span>Search:</span> ",
                            "info": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
                            //"lengthMenu": "_MENU_ <span>entries per page</span>"
                            "sLengthMenu": "_MENU_",
                            "sSearch": ""
                        },
                        "autoWidth": false,
                        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                        'dom': "lfrtip",
                        "drawCallback": function (settings) {
                            if ($(this).hasClass("dataTable-grouping")) {
                                var _table = this;
                                var api = this.api();
                                var rows = api.rows({ page: 'current' }).nodes();
                                var last = null;

                                api.column(groupcolumn, { page: 'current' }).data().each(function (group, i) {
                                    if (last !== group) {
                                        if ($(_table).hasClass("dataTable-checkbox")) {
                                            $(rows).eq(i).before(
                                                '<tr class="group"><td colspan="' + colspan + '"><button onclick="$Report.GetMonthlyInvoicePDF(this.value)" class="btn vertical_align_middle display_inline m-r-5" rel="tooltip" data-original-title="Print" type = "button" value=' + group + '><i class="fa fa-print"></i></button><span class="vertical_align_middle display_inline" style="line-height: normal;">' + group + '</span></td> </tr>'
                                            );
                                        }
                                        else {
                                            $(rows).eq(i).before(
                                                '<tr class="group"><td colspan="' + colspan + '">' + group + '</td></tr>'
                                            );
                                        }

                                        last = group;
                                    }
                                });
                            }
                        }
                    };

                    if ($(this).hasClass("dataTable-noheader")) {
                        opt.searching = false;
                        opt.lengthChange = false;
                    }
                    if ($(this).hasClass("dataTable-nofooter")) {
                        opt.info = false;
                        opt.paging = false;
                    }
                    if ($(this).hasClass("dataTable-nosort")) {

                        var column = $(this).attr('data-nosort');
                        column = column.split(',');
                        for (var i = 0; i < column.length; i++) {
                            column[i] = parseInt(column[i]);
                        };
                        opt.columnDefs = [{
                            'orderable': false,
                            'targets': column
                        }];
                    }
                    if ($(this).hasClass("dataTable-no-Initial-sort")) {
                        opt.order = []
                    }

                    if ($(this).hasClass("dataTable-sort")) {
                        var column = $(this).attr('data-sort');
                        opt.order = [[column, "desc"]]
                    }

                    if ($(this).hasClass("dataTable-scroll-x")) {
                        opt.scrollX = "100%";
                        opt.scrollCollapse = true;
                        $(window).resize(function () {
                        });
                    }
                    if ($(this).hasClass("dataTable-scroll-y")) {
                        opt.scrollY = "300px";
                        opt.paging = false;
                        opt.scrollCollapse = true;
                        $(window).resize(function () {
                            //   oTable.columns.adjust().draw();
                        });
                    }
                    if ($(this).hasClass("dataTable-reorder")) {
                        opt.dom = "R" + opt.dom;
                    }
                    if ($(this).hasClass("dataTable-colvis")) {
                        opt.dom = "C" + opt.dom;
                        opt.oColVis = {
                            "buttonText": "Change columns <i class='icon-angle-down'></i>"
                        };
                    }
                    if ($(this).hasClass('dataTable-tools')) {
                        //opt.sDom = "T" + opt.sDom;
                        //opt.oTableTools = {
                        //    "sSwfPath": "js/plugins/datatable/swf/copy_csv_xls_pdf.swf"
                        //};

                        opt.dom = "T" + opt.dom;
                        opt.oTableTools = {
                            "aButtons": [
                                "xls"
                            ]
                        };
                    }
                    if ($(this).hasClass("dataTable-scroller")) {
                        opt.sScrollY = "300px";
                        opt.bDeferRender = true;
                        if ($(this).hasClass("dataTable-tools")) {
                            opt.dom = 'TfrtiS';
                        } else {
                            opt.dom = 'frtiS';
                        }
                        opt.sAjaxSource = "js/plugins/datatable/demo.txt";
                    }
                    //if ($(this).hasClass("dataTable-grouping") && $(this).attr("data-grouping") == "expandable") {
                    //    opt.lengthChange = false;
                    //    opt.paging = false;
                    //}

                    var oTable = $(Selector).DataTable(opt);
                    oTable.columns.adjust().draw();
                    $(this).css("width", '100%');
                    $('.dataTables_filter input').attr("placeholder", "Search here...");
                    //$(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
                    //    disable_search_threshold: 9999999
                    //});
                    $("#check_all").click(function (e) {
                        $('input', oTable.rows().nodes()).prop('checked', this.checked);
                    });
                    if ($(this).hasClass("dataTable-fixedcolumn")) {
                        new FixedColumns(oTable);
                    }
                    if ($(this).hasClass("dataTable-columnfilter")) {
                        oTable.columnFilter({
                            "sPlaceHolder": "head:after"
                        });
                    }
                    //if ($(this).hasClass("dataTable-grouping")) {
                    //    var rowOpt = {};

                    //    if ($(this).attr("data-grouping") == 'expandable') {
                    //        rowOpt.bExpandableGrouping = true;
                    //    }
                    //    oTable.rowGrouping(rowOpt);
                    //}

                    if (flag == true)
                        $(rows).each(function () {
                            oTable.rows.add($(this)).draw();
                        })
                    oTable.columns.adjust().draw();
                    _$.Tooltip();
                }
            });
        }
    }

    _$.SplitCamelCase = function (word) {
        if (word != null) {
            return word.replace(/([A-Z]+)/g, "$1").replace(/([A-Z][a-z])/g, " $1");
        }
        else {
            return "";
        }
    }

    _$.Tooltip = function () {
        var mobile = false, tooltipOnlyForDesktop = true, notifyActivatedSelector = 'button-active';

        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
            mobile = true;
        }

        if (tooltipOnlyForDesktop) {
            if (!mobile) {
                $('[rel=tooltip]').tooltip();
            }
        }
    }

    _$.DatePick = function () {
        $('.datepick').datepicker({
            format: "dd/mm/yyyy",
            autoclose: true,
        }).on('change', function (ev) {
            var m = $(this).val().match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})$/);
            var a = (m) ? new Date(m[3], m[2] - 1, m[1]) : null;
            if (a == null) {
                $(this).val("");
            }
        });
    }

    _$.formWizard = function () {
        $(".form-wizard").formwizard({
            formPluginEnabled: true,
            validationEnabled: true,
            focusFirstInput: false,
            disableUIStyles: true,
            validationOptions: {
                errorElement: 'span',
                errorClass: 'help-block error',
                errorPlacement: function (error, element) {
                    element.parents('.controls').append(error);
                },
                highlight: function (label) {
                    $(label).closest('.control-group').removeClass('error success').addClass('error');
                },
                success: function (label) {
                    label.addClass('valid').closest('.control-group').removeClass('error success').addClass('success');
                }
            },
            formOptions: {
                success: function (data) {
                    alert("Response: \n\n" + data.say);
                },
                dataType: 'json',
                resetForm: true
            }
        });
    }

    _$.SetComboboxByValue = function (Id, theText) {
        $(Id + " option").each(function () {
            if ($(this).text() == theText) {
                return $(Id).val($(this).val()).change();
            }
        });
    }

    _$.ResetClearContentbenfits = function (id) {
        $.each($(id + "input:not(:hidden),input[type=radio],input[type=checkbox],input[type=text]:not([name='Name']),select,input[type=textarea]"), function (i, field) {
            if (field.name != "ParentId") {
                $(field).val('');
            }
        });
    }


    _$.AddValidation = function (Id) {

        $(Id).data("rule-required", true);
    }

    _$.RemoveValidation = function (Id) {

        $(Id).data("rule-required", false);
        $(Id).find('.error span').remove();
        $(Id).find('.error').removeClass('error')
        $($(Id).closest(".error")).removeClass('error')
    }

    _$.ClearMultiSelect = function () {
        $("#Disease").select2("val", "");
    }

    _$.isRealValue = function (obj) {
        return obj && obj !== 'null' && obj !== 'undefined';
    }

    _$.Disabled = function (Id) {
        $('#' + Id).addClass('disabledfield');
        $('#' + Id).attr('tabIndex', -1);
    }

    _$.removeDisabled = function (Id) {
        $('#' + Id).removeClass('disabledfield');
        $('#' + Id).removeAttr('tabIndex');
    }

    _$.RemoveDuplicates = function (arr, prop) {
        var new_arr = [];
        var lookup = {};
        for (var i in arr) {
            lookup[arr[i][prop]] = arr[i];
        }
        for (i in lookup) {
            new_arr.push(lookup[i]);
        }

        return new_arr;
    }

    _$.tableToExcel = (function () {
        var uri = 'data:application/vnd.ms-excel;base64,'
            , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
            , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
            , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
        return function (table, name) {
            if (!table.nodeType) table = document.getElementById(table)
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            window.location.href = uri + base64(format(template, ctx))
        }
    })();

    _$.numberWithCommas = function (x) {
        var data = parseInt(x).toFixed(0);
        return data.toString().replace(/,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    _$.isInArray = function (value, array) {
        return array.indexOf(value) > -1;
    }

    _$.GetCurrentDate = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        return ('0' + dd).slice(-2) + "/" + ('0' + mm).slice(-2) + "/" + yyyy;
    }

    _$.ParseStringToDate = function (date) {
        var _Date = date.split('/');

        var Day = _Date[0];
        var Month = _Date[1];
        var Year = _Date[2];

        return new Date(Year, (parseInt(Month) - 1), Day);
    }

    _$.isEmpty = function (val) {
        return (val === undefined || val == null || val.length <= 0) ? true : false;
    }

    _$.ScrollTop = function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }

    _$.trimObj = function (obj) {
        if (!Array.isArray(obj) && typeof obj != 'object') return obj;
        return Object.keys(obj).reduce(function (acc, key) {
            acc[key.trim()] = typeof obj[key] == 'string' ? obj[key].trim() : trimObj(obj[key]);
            return acc;
        }, Array.isArray(obj) ? [] : {});
    }

    _$.toDate = function (dateStr) {
        var parts = dateStr.split("/");
        return new Date(parts[2], parts[1] - 1, parts[0]);
    }

    _$.GetParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    _$.hasNull = function (target) {
        var status = false;
        for (var member in target) {
            if (target[member] != null) {
                status = true;
                break;
            }
        }
        return status;
    }

    //_$.makeDataTable = function (tbl, url, columns, params, div, fnCreatedRow) {
    //    try {
    //        $Extention.ShowLoader();

    //        var opt = {
    //            "bServerSide": true,
    //            "bProcessing": false,
    //            "bDestroy": true,
    //            "bAutoWidth": false,
    //            "oLanguage": {
    //                "sSearch": "<span>Search:</span> ",
    //                "sInfo": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
    //                "sLengthMenu": "_MENU_ <span>entries per page</span>"
    //            },
    //            "sAjaxSource": url,
    //            "sServerMethod": "POST",
    //            "sPaginationType": "full_numbers",
    //            "aoColumns": columns,
    //            "fnServerParams": params,
    //            "fnDrawCallback": function () {
    //                _$.Tooltip();
    //            },
    //            //"fnCreatedRow": fnCreatedRow,
    //            "sDom": "lfrtip"
    //        };
    //        var oTable = $(tbl).dataTable(opt);
    //        if ($(tbl).hasClass("dataTable-scroll-x")) {
    //            opt.sScrollX = "100%";
    //            opt.bScrollCollapse = true;
    //            $(window).resize(function () {
    //                oTable.fnAdjustColumnSizing(false);
    //            });
    //        }
    //        if ($(tbl).hasClass("dataTable-scroll-y")) {
    //            opt.sScrollY = "300px";
    //            opt.bPaginate = false;
    //            opt.bScrollCollapse = true;
    //            $(window).resize(function () {
    //                oTable.fnAdjustColumnSizing(false);
    //            });
    //        }

    //        $(tbl).css("width", '100%');
    //        $('.dataTables_filter input').attr("placeholder", "Search here...");
    //        $(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
    //            disable_search_threshold: 9999999
    //        });

    //        if ($(tbl).hasClass("dataTable-grouping")) {
    //            var rowOpt = {};

    //            if ($(tbl).attr("data-grouping") == 'expandable') {
    //                rowOpt.bExpandableGrouping = true;
    //            }
    //            $(tbl).rowGrouping(rowOpt);
    //        }
    //    }
    //    catch (e) {
    //        $Extention.HideLoader();
    //    }

    //    $(tbl).on('processing.dt', function (e, settings, processing) {
    //        $('#loading').css('display', processing ? 'block' : 'none');

    //        //  $Extention.HideLoader();

    //    });

    //    $(tbl).on('xhr.dt', function (e, settings, json, xhr) {
    //        $(div).show();

    //        $Extention.HideLoader();

    //        if ((typeof json.Status !== "undefined") && (json.Status != 100)) {
    //            _$.Notification(json.Message, json.Status);
    //            $(div).hide();
    //        }

    //    });
    //}

    _$.makeDataTable = function (tbl, url, columns, params, div, fnCreatedRow, sortColumnIndex, groupColumn, orderby, Searching) {
        if (orderby == null) {
            orderby = 'asc';
        }

        if (Searching == null) {
            Searching = true;
        }
        skipAjax = false;        //skipAjax = false;

        tbl.addClass('compact')
        //str = str || 2;
        $(div).show();
        aoData = []

        groupColumn = groupColumn | 0;

        $.each(columns, function (i, field) {
            field.data = field.mData;
            field.searchable = field.bSearchable;
            if (tbl.hasClass("dataTable-grouping")) {
                field.orderable = false;
                field.bSortable = false;
            }
            else { field.orderable = field.bSortable; }
            field.visible = field.bVisible;
            field.render = field.fnRender;
            field.name = field.mData;
        })

        params();
        var parameters = [];
        $.each(aoData, function (i, field) {
            parameters[field.name] = field.value;
        });
        // console.log(params())
        try {
            _$.ShowLoader();

            var opt = {
                "serverSide": true,
                //"columnDefs": [
                //      { "width": "20%", "targets": 0 }
                //],
                "columnDefs": [
                    { "orderable": false }
                ],
                "searching": Searching,
                "processing": false,
                "ordering": true,
                "lengthMenu": [[10, 25, 50, 100, 500, -1], [10, 25, 50, 100, 500]],
                "order": [[sortColumnIndex, orderby]],
                "destroy": true,

                "autoWidth": false,
                "pageLength": 10,
                "searchDelay": "500",
                "oLanguage": {
                    //"search": "<span>Search:</span> ",
                    "info": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
                    //"lengthMenu": "_MENU_ <span>entries per page</span>"

                    "sLengthMenu": "_MENU_",
                    "sSearch": ""
                },
                "ajax": {
                    "url": url,
                    //"async":true,
                    "data": parameters,
                    "dataSrc": function (json) {
                        if (json.data == null) {
                            if (json.aaData != null) {
                                json.data = json.aaData;
                            }
                            else if ((typeof json.Status !== "undefined") && (json.Status != 100)) {
                                _$.Notification(json.Message, json.Status);
                                _$.HideLoader();
                                $(div).hide();
                                return [];
                            }
                            else {
                                _$.Notification("SERVER PROCESSING ERROR", 200);
                                _$.HideLoader();
                                $(div).hide();
                                return [];
                            }
                        }
                        return json.data;
                    },
                    'beforeSend': function (jqXHR, settings) {
                        //if (tbl.hasClass("dataTable-grouping")) {
                        //}
                        //else {
                        //    $Extention.ShowLoader();
                        //}
                        if (tbl.hasClass("dataTable-grouping")) {
                            if (skipAjax) {
                                skipAjax = false; //reset the flag

                                return false; //cancel current AJAX request
                            }
                            else {
                                _$.ShowLoader();
                            }
                        } else {
                            _$.ShowLoader();
                        }
                    },

                    "type": "POST",
                },
                //"preDrawCallback": function (row) {
                //    return row
                //},
                "pagingType": "full_numbers",
                "columns": columns,

                //"initComplete": function (data) {
                //    //oTable.fnAdjustColumnSizing(false);
                //},
                "drawCallback": function (setting) {
                    _$.HideLoader();
                    if (tbl.hasClass("dataTable-grouping")) {
                        var api = this.api();
                        var rows = api.rows({
                            page: 'current'
                        }).nodes();
                        var last = null;
                        api.column(groupColumn, {
                            page: 'current'
                        }).data().each(function (group, i) {
                            if (last !== group) {
                                //<i class="icon-chevron-right" aria-hidden="true"></i><i class="icon-chevron-down" aria-hidden="true"></i>
                                $(rows).eq(i).before(
                                    '<tr class="group group-style"><td colspan="' + setting.aoColumns.length + '">' + group + '</td></tr>'
                                );
                                last = group;
                            }
                        });
                        //oTable.fnAdjustColumnSizing(false)
                    }

                    _$.Tooltip();
                },
                "createdRow": fnCreatedRow,
                "dom": "lfrtip"
            };
            //opt.rowGroup().data.each(function (group, i) {
            //
            //    var avg = rows
            //        .data()
            //        .pluck(5)
            //        .reduce(function (a, b) {
            //            return a + b.replace(/[^\d]/g, '') * 1;
            //        }, 0) / rows.count();

            //    return 'Average salary in ' + group + ': ' +
            //        $.fn.dataTable.render.number(',', '.', 0, '$').display(avg)

            //});
            //$(window).resize(function () {
            //
            //    skipAjax = true;
            //});
            if ($(tbl).hasClass("dataTable-scroll-x")) {
                opt.scrollX = "100%";
                opt.scrollCollapse = true;
                //oTable.fnAdjustColumnSizing()
            }
            if ($(tbl).hasClass("dataTable-scroll-y")) {
                opt.scrollY = "300px";
                opt.paginate = false;
                opt.scrollCollapse = true;
            }
            oTable = $(tbl).DataTable(opt);
            //  oTable.columns.adjust().draw();
            //oTable.fnAdjustColumnSizing()
            $(tbl).css("width", '100%');
            $('.dataTables_filter input').attr("placeholder", "Search here...");
            //$(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
            //    disable_search_threshold: 9999999
            //});

            //if ($(tbl).hasClass("dataTable-grouping")) {
            //    var rowOpt = {};

            //    if ($(tbl).attr("data-grouping") == 'expandable') {
            //        rowOpt.bExpandableGrouping = true;
            //    }
            //    $(tbl).rowGrouping(rowOpt);
            //}
        }
        catch (e) {
            _$.HideLoader();
        }
        //$(div).show();

        //if (ToggleInitializerCheck == false) {
        //    _$.InitializeGroupToggle(tbl);
        //    ToggleInitializerCheck = true;
        //}
        //$(tbl).on('processing.dt', function (e, settings, processing) {
        //    $('#loading').css('display', processing ? 'block' : 'none');

        //    //  $Extention.HideLoader();

        //}
        //    );

        //$(tbl).on('xhr.dt', function (e, settings, json, xhr) {
        //    $Extention.HideLoader();
        //    //oTable.fnAdjustColumnSizing()
        //    //if ((typeof json.Status !== "undefined") && (json.Status != 100)) {
        //    //    _$.Notification(json.Message, json.Status);
        //    //    $(div).hide();

        //    //}

        //});

        //$(tbl).on('column-sizing.dt', function (e, settings) {
        //
        //    settings.bAjaxDataGet = false;
        //});
    }

    //$Extention.HideLoader();

    _$.InitializeGroupToggle = function (tbl) {
        tbl.find('tbody').on('click', 'tr.group', function () {
            $(this).nextUntil('tr.group').fadeToggle(250);
            $(this).toggleClass("active");
        });
    }

    _$.tableExport = function (tbl, ignorecolumns, type, fileName) {
        $(tbl).show();
        $(tbl).tableExport({ type: type, escape: 'false', ignoreColumn: ignorecolumns, htmlContent: 'false', fileName: fileName });
        $(tbl).hide();
    }

    _$.tableCreateToExcel = function (fileName, tbleid) {
        $("#" + tbleid).table2excel({
            filename: fileName + ".xls"
        });
    }



    _$.ReplaceAllCharacters = function (target, search, replacement) {
        return target.replace(new RegExp(search, 'g'), replacement);
    };

    _$.ValidateDateRange = function (startdate, enddate) {
        var _startdate = startdate.split("/");
        var _enddate = enddate.split("/");

        startdate = new Date(_startdate[1] + '/' + _startdate[0] + '/' + _startdate[2]);
        enddate = new Date(_enddate[1] + '/' + _enddate[0] + '/' + _enddate[2]);

        if (startdate > enddate) {
            return false;
        }

        return true;
    }

    _$.GetColumnHeaders = function (tableName) {
        var columns = [];
        var obj = '#' + tableName + ' thead tr th';
        $(obj).each(function () {

            if ($(this).attr('donotinclude') != 'true') {
                var text = $(this).attr('aria-label');
                if (text == null) {
                    var text = $(this).children().text();
                    columns.push(text);
                } else {
                    if (text.indexOf(':') != -1) {
                        if (typeof text !== "undefined" && text.substr(0, text.indexOf(':')) != 'Action' && text.substr(0, text.indexOf(':')) != '') {
                            columns.push(text.substr(0, text.indexOf(':')));
                        }
                    }
                    else {
                        if (typeof text !== "undefined" && text != 'Action' && text != '')
                            columns.push(text);
                    }
                }
            }

        });

        return columns.join(", ");
        //return columns;
    };

    _$.GetTableHeaders = function (tableName) {
        var columns = [];
        var obj = '#' + tableName + ' thead tr th';
        $(obj).each(function () {
            var text = $(this).html();
            if (typeof text !== "undefined" || text == "") {
                columns.push(text);
            }
        });

        return columns.join(", ");
        //return columns;
    };
    _$.FindDateInRange = function (startDate, endDate, checkDate) {
        var dateFrom = startDate;
        var dateTo = endDate;
        var dateCheck = checkDate;

        var d1 = dateFrom.split("/");
        var d2 = dateTo.split("/");
        var c = dateCheck.split("/");

        var from = new Date(d1[2], parseInt(d1[1]) - 1, d1[0]);  // -1 because months are from 0 to 11
        var to = new Date(d2[2], parseInt(d2[1]) - 1, d2[0]);
        var check = new Date(c[2], parseInt(c[1]) - 1, c[0]);
        if (check >= from && check <= to) {
            return true;
        }
        else {
            return false;
        }
    }
    _$.GetAgeFromDateOfBirth = function (DateOfBirth) {
        _$.ShowLoader();
        var Age = '';
        $.ajax({
            url: '/Sales/Policy/GetAgeFromDateOfBirth/?DateOfBirth=' + DateOfBirth,
            dataType: 'json',
            async: false,
            success: function (result) {

                if (result.Status == 100)
                    Age = result.Data;
                else
                    _$.Notification(result.Message, result.Status);

                _$.HideLoader();
            }
        });
        return Age;

    }

    _$.TableToJSON = function (Tableid) {
        var table = $('#' + Tableid);
        var headers = [];
        var data = [];
        //Get Header
        for (var i = 0; i < table.find('thead tr th').length; i++) {
            headers[i] = table.find('thead tr th')[i].innerHTML;
        }
        //Get Data
        for (var i = 0; i < table.find('tbody tr').length; i++) {
            var rowData = {};
            for (var j = 0; j < document.getElementById(Tableid).rows[i].cells.length; j++) {
                rowData[headers[j]] = table.find('tbody tr td')[j].innerHTML;
            }
            data.push(rowData);
        }
        return data
    }

    _$.isObjectEmpty = function (object) {
        var isEmpty = true;
        for (keys in object) {
            isEmpty = false;
            break; // exiting since we found that the object is not empty
        }
        return isEmpty;
    }

    _$.ShowLoader = function () {
        //locked = true;
        //$('.loading-overlay').show();
        
        //$(".btn-success").click(function () { 
        $("btn.btn-success").each(function (index, element) {

            if (!($(element).hasClass("disabled"))) {
                $(element).addClass("disabled");
                $(element).addClass("CustomDisableClass");
            }
        });

        //});

        //$('.loading-overlay').css({ 'visibility': 'visible', 'opacity': '1' });
        $('.loader_overlay').css({ 'display': 'block' });
        
    }

    _$.HideLoader = function () {
        //$('.loading-overlay').hide();
        //$('.loading-overlay').css({ 'visibility': 'hidden', 'opacity': '0' });
        $('.loader_overlay').css({ 'display': 'none' });

        $(".CustomDisableClass2").each(function (index, element) {
            $(element).removeClass("disabled");
            $(element).removeClass("CustomDisableClass2");
        });

        $("btn.btn-success").click(function () {
            if (!$(this).hasClass("CustomDisableClass")) {
                if (!($(this).hasClass("disabled"))) {
                    $(this).addClass("disabled");
                    $(this).addClass("CustomDisableClass2");
                }
            }
        });

        $(".CustomDisableClass").each(function (index, element) {
            $(element).removeClass("disabled");
            $(element).removeClass("CustomDisableClass");
        });

        //locked = false;
    }

    _$.addDaysWithStartDate = function (startDate, days) {
        var datearray = startDate.split("/");
        var newdate = datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        var date = new Date(newdate);
        //  var newdate = new Date(date);
        date.setDate(date.getDate() + days);
        var dd = date.getDate();
        var mm = date.getMonth() + 1;
        var y = date.getFullYear();
        var FormattedDate = dd + '/' + mm + '/' + y;

        return FormattedDate;
    }

    _$.ToUpperCase = function (string) {
        return string.toUpperCase();
    }
    _$.ToLowerCase = function (string) {
        return string.toLowerCase();
    }


    _$.javascript_abort = function () {
        throw new Error('This is not an error. This is just to abort javascript');
    }

    //DataTable with Grouping
    _$.DataTable_custom_grouping = function (Selector, colspan) {

        var flag = false;
        var rows = [];
        if ($(Selector).length > 0) {

            $(Selector).addClass('compact');
            $(Selector).each(function () {
                if (!$(this).hasClass("dataTable-custom")) {
                    var opt = {
                        "sPaginationType": "full_numbers",
                        "bDestroy": "true",
                        "oLanguage": {
                            //<span>Search:</span>
                            "sSearch": "",
                            "sInfo": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
                            "sLengthMenu": "_MENU_"
                        },
                        'sDom': "lfrtip"
                    };
                    if ($(this).hasClass("dataTable-noheader")) {
                        opt.bFilter = false;
                        opt.bLengthChange = false;
                    }
                    if ($(this).hasClass("dataTable-nofooter")) {
                        opt.bInfo = false;
                        opt.bPaginate = false;
                    }
                    if ($(this).hasClass("dataTable-nosort")) {
                        var column = $(this).attr('data-nosort');
                        column = column.split(',');
                        for (var i = 0; i < column.length; i++) {
                            column[i] = parseInt(column[i]);
                        };
                        opt.aoColumnDefs = [{
                            'bSortable': false,
                            'aTargets': column
                        }];
                    }
                    if ($(this).hasClass("dataTable-scroll-x")) {
                        opt.sScrollX = "100%";
                        opt.bScrollCollapse = true;
                        $(window).resize(function () {
                            oTable.fnAdjustColumnSizing();
                        });
                    }
                    if ($(this).hasClass("dataTable-scroll-y")) {
                        opt.sScrollY = "300px";
                        opt.bPaginate = false;
                        opt.bScrollCollapse = true;
                        $(window).resize(function () {
                            oTable.fnAdjustColumnSizing();
                        });
                    }
                    if ($(this).hasClass("dataTable-reorder")) {
                        opt.sDom = "R" + opt.sDom;
                    }
                    if ($(this).hasClass("dataTable-colvis")) {
                        opt.sDom = "C" + opt.sDom;
                        opt.oColVis = {
                            "buttonText": "Change columns <i class='icon-angle-down'></i>"
                        };
                    }
                    if ($(this).hasClass('dataTable-tools')) {
                        opt.sDom = "T" + opt.sDom;
                        opt.oTableTools = {
                            "sSwfPath": "js/plugins/datatable/swf/copy_csv_xls_pdf.swf"
                        };
                    }
                    if ($(this).hasClass("dataTable-scroller")) {
                        opt.sScrollY = "300px";
                        opt.bDeferRender = true;
                        if ($(this).hasClass("dataTable-tools")) {
                            opt.sDom = 'TfrtiS';
                        } else {
                            opt.sDom = 'frtiS';
                        }
                        opt.sAjaxSource = "js/plugins/datatable/demo.txt";
                    }
                    if ($(this).hasClass("dataTable-grouping") && $(this).attr("data-grouping") == "expandable") {
                        //opt.bLengthChange = false;
                        //opt.bPaginate = false;
                    }

                    var oTable = $(this).dataTable(opt);
                    $(this).css("width", '100%');
                    $('.dataTables_filter input').attr("placeholder", "Search here...");

                    //$(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
                    //    disable_search_threshold: 9999999
                    //});
                    $("#check_all").click(function (e) {
                        $('input', oTable.rows().nodes()).prop('checked', this.checked);
                    });
                    if ($(this).hasClass("dataTable-fixedcolumn")) {
                        new FixedColumns(oTable);
                    }
                    if ($(this).hasClass("dataTable-columnfilter")) {
                        oTable.columnFilter({
                            "sPlaceHolder": "head:after"
                        });
                    }
                    if ($(this).hasClass("dataTable-grouping")) {
                        var rowOpt = {};

                        if ($(this).attr("data-grouping") == 'expandable') {
                            rowOpt.bExpandableGrouping = true;
                        }
                        oTable.rowGrouping(rowOpt);
                    }

                    oTable.fnDraw();
                    oTable.fnAdjustColumnSizing();
                }
            });
        }
    }

    _$.DynamicDataTable = function (setting) {

        //{
        //    campaignTypeId: setting.content.campaignTypeId,
        //        endPointUrl: setting.content.endPointUrl,
        //            formType: setting.content.formType,
        //                searchFilters: setting.content.searchFilters
        //}

        _$.ShowLoader();
        _$.Post(setting.headerUrl, setting.content, function (result) {

            if (setting.headerFilter) {
                result = setting.headerFilter(result);
            }

            const tableHTML = setting.renderHeader(result);
            $(setting.tableDVId).html(tableHTML);

            console.log('dataUrl: ', setting.dataUrl);
            console.log('content: ', setting.content);

            var returnData = [];
            var tableRef = $(setting.tableId).DataTable({
                "bJQueryUI": false,
                "dom": "lfrtip",
                "pagingType": "full_numbers",
                "serverSide": true,
                "columnDefs": setting.columnDefs(result),
                "processing": true,
                "ordering": true,
                "paging": true,
                "destroy": true,
                "autoWidth": false,
                "searchDelay": "800",
                "oLanguage": {
                    "search": "<span>Search:</span> ",
                    "info": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
                    "lengthMenu": "_MENU_ <span>entries per page</span>"
                },
                "processing": true,
                "serverSide": true,
                "ajax": {
                    "type": "POST",
                    "url": setting.dataUrl,
                    "data": setting.content,
                    "dataSrc": function (json) {
                        returnData = JSON.parse(json.data);
                        if (json.recordsTotal == 0) _$.Notification('No record found !!!', 200);
                        return returnData;
                    }
                },
                "preDrawCallback": function (settings) {
                    _$.ShowLoader();
                },
                "fnDrawCallback": function (oSettings) {
                    $('#dvDataTable').show();
                    _$.HideLoader();
                    if (setting.callback) {
                        setting.callback(returnData, tableRef);
                    }
                },
                "columns": setting.columns(result)
            });

        });
    }

    _$.DataTableCheckBox = function (Selector, groupcolumn, colspan) {
        var flag = false;
        var rows = [];
        if ($(Selector).length > 0) {
            $(Selector).addClass('compact');
            $(Selector).each(function () {
                $(Selector).find('tbody > tr').each(function () {
                    if ($(this).find('td').length == 1) {
                        this.innerHTML = '';
                        flag = true;
                    }
                    else {
                        rows.push(this);
                    }
                })
                if (!$(this).hasClass("dataTable-custom")) {
                    var opt = {
                        "pagingType": "full_numbers",
                        "destroy": true,
                        "oLanguage": {
                            //<span>Search:</span>
                            "sSearch": "",
                            "info": "Showing <span>_START_</span> to <span>_END_</span> of <span>_TOTAL_</span> entries",
                            //"lengthMenu": "_MENU_ <span>entries per page</span>"
                        },
                        "autoWidth": false,
                        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                        'dom': "lfrtip",
                        "drawCallback": function (settings) {
                            if ($(this).hasClass("dataTable-grouping")) {
                                var _table = this;
                                var api = this.api();
                                var rows = api.rows({ page: 'current' }).nodes();
                                var last = null;

                                api.column(groupcolumn, { page: 'current' }).data().each(function (group, i) {
                                    if (last !== group) {
                                        if ($(_table).hasClass("dataTable-checkbox")) {
                                            $(rows).eq(i).before(
                                                '<tr class="group"><td colspan="' + colspan + '"><button onclick="$Report.GetMonthlyInvoicePDF(this.value)" class="btn vertical_align_middle display_inline m-r-5" rel="tooltip" data-original-title="Print" type = "button" value=' + group + '><i class="fa fa-print"></i></button><span class="vertical_align_middle display_inline" style="line-height: normal;">' + group + '</span></td> </tr>'
                                            );
                                        }
                                        else {
                                            $(rows).eq(i).before(
                                                '<tr class="group"><td colspan="' + colspan + '">' + group + '</td></tr>'
                                            );
                                        }

                                        last = group;
                                    }
                                });
                            }
                        }
                    };

                    if ($(this).hasClass("dataTable-noheader")) {
                        opt.searching = false;
                        opt.lengthChange = false;
                    }
                    if ($(this).hasClass("dataTable-nofooter")) {
                        opt.info = false;
                        opt.paging = false;
                    }
                    if ($(this).hasClass("dataTable-nosort")) {
                        var column = $(this).attr('data-nosort');
                        column = column.split(',');
                        for (var i = 0; i < column.length; i++) {
                            column[i] = parseInt(column[i]);
                        };
                        opt.columnDefs = [{
                            'orderable': false,
                            'targets': column
                        }];
                    }
                    if ($(this).hasClass("dataTable-no-Initial-sort")) {
                        opt.order = []
                    }

                    if ($(this).hasClass("dataTable-sort")) {
                        var column = $(this).attr('data-sort');
                        opt.order = [[column, "desc"]]
                    }

                    if ($(this).hasClass("dataTable-scroll-x")) {
                        opt.scrollX = "100%";
                        opt.scrollCollapse = true;
                        $(window).resize(function () {
                        });
                    }
                    if ($(this).hasClass("dataTable-scroll-y")) {
                        opt.scrollY = "300px";
                        opt.paging = false;
                        opt.scrollCollapse = true;
                        $(window).resize(function () {
                            //   oTable.columns.adjust().draw();
                        });
                    }
                    if ($(this).hasClass("dataTable-reorder")) {
                        opt.dom = "R" + opt.dom;
                    }
                    if ($(this).hasClass("dataTable-colvis")) {
                        opt.dom = "C" + opt.dom;
                        opt.oColVis = {
                            "buttonText": "Change columns <i class='icon-angle-down'></i>"
                        };
                    }
                    if ($(this).hasClass('dataTable-tools')) {
                        //opt.sDom = "T" + opt.sDom;
                        //opt.oTableTools = {
                        //    "sSwfPath": "js/plugins/datatable/swf/copy_csv_xls_pdf.swf"
                        //};

                        opt.dom = "T" + opt.dom;
                        opt.oTableTools = {
                            "aButtons": [
                                "xls"
                            ]
                        };
                    }
                    if ($(this).hasClass("dataTable-scroller")) {
                        opt.sScrollY = "300px";
                        opt.bDeferRender = true;
                        if ($(this).hasClass("dataTable-tools")) {
                            opt.dom = 'TfrtiS';
                        } else {
                            opt.dom = 'frtiS';
                        }
                        opt.sAjaxSource = "js/plugins/datatable/demo.txt";
                    }
                    //if ($(this).hasClass("dataTable-grouping") && $(this).attr("data-grouping") == "expandable") {
                    //    opt.lengthChange = false;
                    //    opt.paging = false;
                    //}

                    var oTable = $(Selector).DataTable(opt);
                    oTable.columns.adjust().draw();
                    $(this).css("width", '100%');
                    $('.dataTables_filter input').attr("placeholder", "Search here...");
                    //$(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
                    //    disable_search_threshold: 9999999
                    //});
                    $("#check_alll").click(function (e) {
                        $('input', oTable.rows().nodes()).prop('checked', this.checked);
                    });
                    if ($(this).hasClass("dataTable-fixedcolumn")) {
                        new FixedColumns(oTable);
                    }
                    if ($(this).hasClass("dataTable-columnfilter")) {
                        oTable.columnFilter({
                            "sPlaceHolder": "head:after"
                        });
                    }
                    //if ($(this).hasClass("dataTable-grouping")) {
                    //    var rowOpt = {};

                    //    if ($(this).attr("data-grouping") == 'expandable') {
                    //        rowOpt.bExpandableGrouping = true;
                    //    }
                    //    oTable.rowGrouping(rowOpt);
                    //}

                    if (flag == true)
                        $(rows).each(function () {
                            oTable.rows.add($(this)).draw();
                        })
                    oTable.columns.adjust().draw();
                    _$.Tooltip();
                }
            });
        }
    }

    _$.DataTableColumnHasValue = function (Selector, Value, ColumnNo) {
        var table = $('#' + Selector).DataTable();
        if (table == null) {
            return false;
        }

        if (table.columns(ColumnNo)
            .data()
            .toArray()
            .toString()
            .toLowerCase()
            .split(',').indexOf(Value.toLowerCase()) > -1) {
            return true;
        }
        else {
            return false;
        }
    }

    _$.FormHasAllNullParameters = function (Id) {
        var Flag = true;
        $.each($('#' + Id).serializeArray(), function (i, field) {
            if (field.value != "" && field.value != null && field.value != 'undefined') {
                Flag = false;
            }
        });
        return Flag;
    }
};