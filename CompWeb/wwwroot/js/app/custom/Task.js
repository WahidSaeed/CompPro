function Task() {

    var $Task = this, Task = new Object(), _$ = new Extention();

    $Task.GetScreenName = function (_ModuleName) {

        _$.Post('/Task/Task/_GetScreenName', { ModuleName: _ModuleName }, function (result) {
            $("#ddScreenName").html("");
            $("#ddScreenName").append($("<option></option>").val("Please Select").html("Please Select"));
            if (result != null) {
                if (result.data.screenName) {
                    $.each(result.data.screenName, function (text, value) {
                        $("#ddScreenName").append($("<option></option>").val(value.value).html(value.text));
                    });
                }
            }
        });
    }

    $Task.GetScreenEvents = function (_ScreenName) {

        _$.Post('/Task/Task/_GetScreenEvents', {ScreenName:_ScreenName}, function (result) {
            $("#ddScreenEvents").html("");
            $("#ddScreenEvents").append($("<option></option>").val("Please Select").html("Please Select"));
            if (result != null) {
                if (result.data) {
                    $("#lblScreenURL").val(result.data.url);
                    $("#lblCaseDetailURL").val(result.data.caseDetailURL);
                    if (result.data.screenEvents) {
                        $.each(result.data.screenEvents, function (text, value) {
                            $("#ddScreenEvents").append($("<option></option>").val(value.value).html(value.text));
                        });
                    }
                }
            }
        });
    }

    $Task.GetEventTransaction = function (_ScreenEventName) {

        var ScreenName = $('#ddScreenName :selected').text();

        _$.Post('/Task/Task/_GetEventTransaction', { ScreenName: ScreenName, EventName: _ScreenEventName }, function (result) {
            $("#ddinpColumns").html("");
            $("#ddinpUpdateWhereExpColumns").html("");
            $("#ddinpUpdateSetExpColumns").html("");
            $("#ddinpColumns").append($("<option></option>").val("Please Select").html("Please Select"));
            $("#ddinpUpdateWhereExpColumns").append($("<option></option>").val("Please Select").html("Please Select"));
            $("#ddinpUpdateSetExpColumns").append($("<option></option>").val("Please Select").html("Please Select"));
            if (result != null) {
                if (result.data) {
                    $("#lblTransactionTable").val(result.data.transactionTable + " PK " + result.data.transactionPKColumn);
                    $("#lblUpdateTableName").val(result.data.updateTableName + " PK " + result.data.updateTablePKColumn);
                    if (result.data.columns) {
                        $.each(result.data.columns, function (text, value) {
                            $("#ddinpColumns").append($("<option></option>").val(value.value).html(value.text));
                            $("#ddinpUpdateWhereExpColumns").append($("<option></option>").val(value.value).html(value.text));
                            $("#ddinpUpdateSetExpColumns").append($("<option></option>").val(value.value).html(value.text));
                        });
                    }
                }
            }
        });
    }

    $Task.SetinpRuleconditionOfTransaction = function () {

        var vinpColumns = $("#ddinpColumns").val() == "Please Select" ? "" : $("#ddinpColumns").val();
        var vinpOperators = $("#ddinpOperators").val();
        var vtxtinpvalue = $("#txtinpvalue").val();
        var vinpColumnValues = $("#ddinpColumnValues").val();

        var Expression = vinpColumns + vinpOperators + (vtxtinpvalue != "" ? vtxtinpvalue : vinpColumnValues);
        $("#txtinpRuleCondition").val(Expression);

    }

    $Task.SetinpUpdateRuleconditionOfTransaction = function () {

        var vinpColumns = $("#ddinpUpdateColumnValues").val();
        var vinpOperators = $("#ddinpUpdateSetExpOperators").val();
        var vtxtinpvalue = $("#txtinpUpdateSetExp").val();
        var vinpColumnValues = $("#ddinpUpdateColumnValues").val();

        var Expression = vinpColumns + vinpOperators + (vtxtinpvalue != "" ? vtxtinpvalue : vinpColumnValues);
        $("#txtinpUpdateRuleCondition").val(Expression);

    }

    $Task.SetinpUpdateWhereRuleconditionOfTransaction = function () {

        var vinpColumns = $("#ddinpUpdateWhereExpColumns").val() == "Please Select" ? "" : $("#ddinpUpdateWhereExpColumns").val();
        var vinpOperators = $("#ddinpUpdateWhereExpOperators").val();
        var vtxtinpvalue = $("#txtinpUpdateWhereExp").val();
        var vinpColumnValues = $("#ddinpUpdateWhereColumnValues").val();

        var Expression = vinpColumns + vinpOperators + (vtxtinpvalue != "" ? vtxtinpvalue : vinpColumnValues);
        $("#txtinpUpdateWhereRuleCondition").val(Expression);

    }

    $Task.SaveTaskConfig = function () {

        var vtranstiontable = $("#lblTransactionTable").val().split("PK");
        var vupdatetranstiontable = $("#lblUpdateTableName").val().split("PK");

        var obj = {
            Type_Name: $("#txtinpTypeName").val(),
            Transaction_Table_Name: vtranstiontable[0],
            Transaction_PK_Column_Name: vtranstiontable[1],
            Update_Table_Name: vupdatetranstiontable[0],
            UpdateTable_PK_Column: vupdatetranstiontable[1],
            Url: $("#lblScreenURL").val(),
            CaseDetail_Url: $("#lblCaseDetailURL").val(),
            Module_Name: $("#ddinpModuleName").val(),
            Rule_Condition: $("#txtinpRuleCondition").val(),
            Update_SetExpression: $("#txtinpUpdateRuleCondition").val(),
            Update_WhereExpression: $("#txtinpUpdateWhereRuleCondition").val(),
            IsUpdate_Transaction: $('#chkIsupdateTransaction').prop('checked'),
            Task_Event: $("#ddinpTaskEvent").val(),
            Priority: $("#ddinpTaskPriority").val(),
            Subject: $("#txtinpTaskSubject").val(),
            Description: $("#txtinpTaskDescription").val(),
            OfferToCurrentUser: $("#chkinpOfferToCurrentUser").prop('checked'),
            OfferToInitiator: $("#chkinpOfferToInitiator").prop('checked'),
            OfferToBackUser: $("#chkinpOfferToBackUser").prop('checked'),
            GetUsers_SPName: $("#ddinpGetUsers_SPName").val(),
            Task_Level: 0,
            Team_Ids: GetTeamIds(),
            User_Ids: GetUserIds(),
            Accept_Right: $("#chkAcceptRights").prop('checked'),
            Reject_Right: $("#chkRejectRights").prop('checked'),
            Done_Right: $("#chkDoneRights").prop('checked'),
            Cancel_Right: $("#chkCancelRights").prop('checked'),
            Forward_Right: $("#chkForwardRights").prop('checked'),
            Task_Right: $("#chkTaskRight").prop('checked'),
            UpdatePercentage_Right: $("#chkUpdate").prop('checked'),
            Perform_Right: $("#chkPerform").prop('checked'),
            ViewTask_Right: $("#chkViewTask").prop('checked'),
            Email_Right: $("#chkEmail").prop('checked'),
            Call_Right: $("#chkPhoneCall").prop('checked'),
            Fax_Right: $("#chkFax").prop('checked'),
            Visit_Right: $("#chkVisit").prop('checked'),
            SMS_Right: $("#chkSMS").prop('checked'),
            Letter_Right: $("#chkLetter").prop('checked'),
            Owner_EmailConfiguration_Id: $("#ddEmailtoOwner").val() == "" ? 0 : $("#ddEmailtoOwner").val(),
            Offeree_EmailConfiguration_Id: $("#ddEmailtoCurrentAssignee").val() == "" ? 0 : $("#ddEmailtoCurrentAssignee").val(),
            CurrentAssignee_EmailConfiguration_Id: $("#ddEmailtoCurrentAssignee").val() == "" ? 0 : $("#ddEmailtoCurrentAssignee").val(),
            AllAssignee_EmailConfiguration_Id: $("#ddEmailtoAllAssignee").val() == "" ? 0 : $("#ddEmailtoAllAssignee").val()
        };

        _$.PostAsync('/Task/Task/_SaveTaskConfig', obj, true, function (result) {
            if (result) {
                alert(result.message);
            }
        });
    }

    function GetUserIds() {
        var Ids = "";
        $('#tbAssignUsers tr:not(:has(th))').each(function () {
            var this_row = $(this);
            var vcheckbox = $(this).find("input[type='checkbox']");
            if (vcheckbox.prop("checked")) {
                Ids += vcheckbox.attr('id') + ",";
            }
        });
        return Ids.replace(/,\s*$/, "");
    }

    function GetTeamIds() {
        var Ids = "";
        $('#tbAssignTeam tr:not(:has(th))').each(function () {
            var this_row = $(this);
            var vcheckbox = $(this).find("input[type='checkbox']");
            if (vcheckbox.prop("checked")) {
                Ids += vcheckbox.attr('id') + ",";
            }
        });
        return Ids.replace(/,\s*$/, "");
    }

}


