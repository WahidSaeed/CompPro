function Customer360() {
    var $Customer360 = this, Customer360 = new Object(), _$ = new Extention();
    var InsuredSearch = 1;
    $Customer360.searchInsured = function () {
        var cnic = $('#CNIC').val();
        var phoneNumber = $('#PhoneNumber').val();
        var healthCardNumber = $('#HealthCardNumber').val();
        if ((cnic == '' || cnic == undefined) && (phoneNumber == '' || phoneNumber == undefined) && (healthCardNumber == '' || healthCardNumber == undefined)) {
            toastr.warning('Enter CNIC Or PhoneNumber Or HealthCardNumber to search');
            return;
        }
        InsuredSearch = 1;
        var Parmeters = {
            MobileNo: phoneNumber,
            CNIC: cnic,
            HealthCardNumber: healthCardNumber,
            IsInsuredSearch: InsuredSearch,
        };
        $('#SearchCustomer360').html('');
        $('#ClientDetails').html('');
        $('#PolicyInsured360').html('');
        _$.ShowLoader();
        var divCustomer360 = $('#SearchCustomer360');
        var url = '/Customer360/customer/_customer360search';
        $.ajax({
            url: url,
            type: "GET",
            data: Parmeters,
            dataType: "html",
            async: true,
            success: function (result) {
                $(divCustomer360).html(result);
                _$.HideLoader();
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });

    };

    $Customer360.advancedSearch = function () {
        var PolicyNumber = $('#PolicyNumber').val();
        var NameOfContact = $('#NameOfContact').val();
        var NameOfCustomer = $('#NameOfCustomer').val();
        var BusinessType = $('#BusinessType').val();
        var PolicyType = $('input[name$="PolicyType"]:checked').val();
        InsuredSearch = 0;
        var Parmeters = {
            PolicyType: PolicyType,
            PolicyNumber: PolicyNumber,
            CustomerName: NameOfCustomer,
            ContactName: NameOfContact,
            PolicyType: PolicyType,
            BusinessType: BusinessType,
            IsInsuredSearch: InsuredSearch,
        };
        $('#SearchCustomer360').html('');
        $('#ClientDetails').html('');
        $('#PolicyInsured360').html('');
        _$.ShowLoader();
        var divCustomer360 = $('#SearchCustomer360');
        var url = '/Customer360/customer/_customer360search';
        $.ajax({
            url: url,
            type: "GET",
            data: Parmeters,
            dataType: "html",
            async: true,
            success: function (result) {
                $(divCustomer360).html(result);
                _$.HideLoader();
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    };

    $Customer360.InsuredSearchDetails = function (policyId, InsuredKey, applicationType) {
        $('#ClientDetails').html('');
        $('#PolicyInsured360').html('');
        $Customer360.Details360(policyId, applicationType);
        if (InsuredSearch === 1)
        {
            $Customer360.Insured360(InsuredKey, applicationType);
        }
    }

    $Customer360.Details360 = function (policyId, applicationType) {
        _$.ShowLoader();
        var dvInsured360 = $('#ClientDetails');
        var url = '/Customer360/customer/_GetClientDetails';
        $.ajax({
            url: url + '?ApplicationType=' + applicationType + '&policyId=' + policyId,
            type: "GET",
            dataType: "html",
            async: true,
            success: function (result) {
                $(dvInsured360).html(result);
                _$.HideLoader();
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    }

    $Customer360.Insured360 = function (InsuredKey, applicationType) {
        _$.ShowLoader();
        var dvInsured360 = $('#PolicyInsured360');
        var url = '/Customer360/customer/_GetPolicyInsured360';
        $.ajax({
            url: url + '?ApplicationProduct=' + applicationType + '&InsuredKey=' + InsuredKey,
            type: "GET",
            dataType: "html",
            async: true,
            success: function (result) {
                $(dvInsured360).html(result);
                _$.HideLoader();
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });

    }
}