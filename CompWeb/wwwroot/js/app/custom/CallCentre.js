
function CallCentre() {
    var $CallCentre = this, CallCentre = new Object(), _$ = new Extention();

    $CallCentre.IsSupervisorView = function () { return (_$.GetParameterByName('formType') == '1'); }
    $CallCentre.CampaignTypeId = function () { return _$.GetParameterByName('campaignTypeId') }

    $CallCentre.PopulateSearchFilter = function (campaignTypeId, dvFilter) {
        _$.ShowLoader();

        $.ajax({
            url: '/CallCentre/CallCentre/_GetSearchFilters?formType=' + _$.GetParameterByName('formType') + '&campaignTypeId=' + campaignTypeId,
            type: "GET",
            dataType: "html",
            async: true,
            success: function (result) {
                $(dvFilter).html(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //debugger;
                _$.Notification('Problem Encountered', 200);
                _$.HideLoader();
            }
        });
    }

    $CallCentre.SearchRecords = function (campaignTypeId, formId, epUrl) {
        //debugger;
        if ($(formId).valid()) {

            /* Make List For Search Filter */
            var filters = Array.from($(formId + ' [dbset-column]')
                .map(function () {
                    return {
                        value: $(this).val(),
                        text: $(this).attr('dbset-column'),
                        operator: $(this).attr('dbset-searchop'),
                        isLocal: $(this).attr('dbset-local')
                    }
                }));

            var IsSupervisor = $CallCentre.IsSupervisorView();
            _$.DynamicDataTable({
                tableDVId: '#dvDT',
                tableId: '#dvJDataTable',
                headerUrl: '/CallCentre/CallCentre/GetJQueryDataTableHeaders',
                dataUrl: '/CallCentre/CallCentre/GetSearchRecords',
                headerFilter: function (result) {
                    return (!IsSupervisor ? result.filter(x => x.isSupervisor === true) : result);
                },
                columnDefs: function (result) {
                    return Array.from(result
                        .map(function (data, i) {
                            if (data.isVisible) {
                                if (data.controlTypeId === ControlType.SingleCheckbox) {
                                    return {
                                        orderable: false,
                                        targets: i,
                                        render: function (data, type, full, meta) {
                                            if (IsSupervisor)
                                                return '<input type="checkbox" name="id[]" ' + (full.Case_Status == 'A' ? "disabled" : "") + ' value="' + full.InvoiceID + "~" + full.ApplicationType + "-" + full.Channel + '">';
                                            else {
                                                return '<a data-toggle="modal" href="#" class="callEntry"> Call </a>';
                                            }
                                        }
                                    }
                                }
                                else if (data.controlTypeId === ControlType.Currency) {
                                    return {
                                        orderable: true,
                                        targets: i,
                                        render: function (data, type, full, meta) {
                                            return _$.numberWithCommas(data);
                                            //return data + ' abc';
                                        }
                                    }
                                }
                                else {
                                    return undefined;
                                }
                            }
                            else {
                                return {
                                    targets: i,
                                    visible: false
                                }
                            }
                        }).filter(x => x !== undefined));
                },
                content: { formType: _$.GetParameterByName('formType'), campaignTypeId: campaignTypeId, searchFilters: filters, endPointUrl: epUrl },
                columns: function (result) {
                    return Array.from(result.map(function (data) { return { "data": data.columnName } }));
                },
                renderHeader: function (result) {
                    return '<table class="table table-striped table-hover table-bordered" id="dvJDataTable"> \
                                    <thead> \
                                    <tr>' + Array.from(result.map(function (data) { return (data.isVisible ? "<th>" + data.displayText + "</th>" : undefined) }).filter(x => x !== undefined)).join('') + '</tr> \
                                    </thead> \
                                    </table>';
                },
                callback: function (returnData, tableRef) {
                    $CallCentre.HighLightRows(returnData);
                    $('#dvJDataTable tbody').on('click', '.callEntry', function () {
                        var rwData = tableRef.row($(this).closest('tr').index()).data();
                        $CallCentre.PopulateCallEntryView(rwData.InvoiceID, rwData.Classification, rwData.CampaignCaseId, '#dv_EntryForm', '#dv_CallEntryModal');
                    });
                }
            });

        }
    }

    $CallCentre.HighLightRows = function (result) {
        console.log('result: ', result);
        var table = $('#dvJDataTable');
        result.forEach(function (data, i) {
            if (data.AlertID === "1") {
                var tr = table.find('tr:nth(' + (i + 1) + ')');
                tr.addClass('redrow');
            }
            else if (data.AlertID === "2") {
                var tr = table.find('tr:nth(' + (i + 1) + ')');
                console.log('Data: ', (data.AlertID === "2"), tr);
                tr.addClass('greenrow');
            }
        });
    }

    $CallCentre.ResetFilter = function () {
        $('#callCentreSearchFilters' + ' [dbset-column]').each(function () {
            $(this).val('');
        });
        $('#btnSearch').click();
    }

    $CallCentre.AssignCase = function (campaignTypeId, teamId, teamMemberId, jDataTable) {
        //debugger;
        if (teamMemberId != undefined && teamMemberId != "") {
            var table = $(jDataTable).DataTable(), selectedCases = [];
            if (table != undefined) {
                // Iterate over all checkboxes in the table
                table.$('input[type="checkbox"]:checked').each(function (index, value, element) {
                    selectedCases.push(this.value);
                });
            }

            if (selectedCases.length > 0) {
                _$.ShowLoader();

                _$.Post('/CallCentre/CallCentre/AssignCases', { campaignTypeId: campaignTypeId, teamId: teamId, teamMemberId: teamMemberId, campaignCases: selectedCases }, function (result) {
                    //debugger;

                    _$.Notification(result.message, result.status);

                    // Uncheck and Disable Selected Checkbox
                    table.$('input[type="checkbox"]:checked').attr("disabled", "disabled");
                    table.$('input[type="checkbox"]:checked').prop("checked", false);

                    _$.HideLoader();
                });
            }
            else
                _$.Notification('Please select atleast one Case', 200);
        }
        else
            _$.Notification('Please select any TeamMember', 200);
    }

    $CallCentre.PopulateCallEntryView = function (_transactionId, _classification, _campaignCaseId, dvEntryForm, dvModal) {
        //debugger;
        _$.ShowLoader();

        $.ajax({
            url: '/CallCentre/CallCentre/_GetCallEntryView?transactionId=' + _transactionId + '&classification=' + _classification + '&campaignCaseId=' + _campaignCaseId + '&campaignTypeId=' + $CallCentre.CampaignTypeId(),
            type: "GET",
            dataType: "html",
            async: true,
            success: function (result) {
                $(dvEntryForm).html("");
                $(dvEntryForm).html(result);

                $('.date-picker').datepicker();
                $(dvModal).modal('show');

                _$.HideLoader();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //debugger;
                _$.Notification('Problem Encountered', 200);
                _$.HideLoader();
            }
        });
    }

    $CallCentre.SaveCallEntry = function (formId, dvModal) {
        //debugger;
        if ($(formId).valid()) {

            var myform = $(formId), CollectionCampaignData = new Object(), propName = '';

            // Find disabled inputs, and remove the "disabled" attribute
            var disabled = myform.find(':input:disabled').removeAttr('disabled');

            // serialize the form
            var serialized = $(formId).serializeArray();

            // re-disabled the set of inputs that you previously enabled
            disabled.attr('disabled', 'disabled');

            $.each(serialized, function (i, field) {
                if (field.value != undefined && field.value != "") {
                    propName = myform.find('#' + field.name).attr("dbset-column");
                    CollectionCampaignData[propName] = field.value;
                }
            });

            _$.ShowLoader();

            _$.Post('/CallCentre/CallCentre/_SaveCallEntry', { collectionCampaignData: CollectionCampaignData }, function (result) {
                _$.Notification(result.message, result.status);
                _$.HideLoader();
                $('#btnSearch').click();
                //debugger;
                if (result.status == 100) $(dvModal).modal('hide');
            });
        }
    }
}