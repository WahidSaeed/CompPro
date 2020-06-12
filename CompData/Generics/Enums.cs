using System.ComponentModel;

namespace CRMData.Generics
{
    public enum IPType
    {
        WhiteList = 1,
        BlackList = 2
    }

    public enum FormType
    {
        SearchForm = 1,
        EntryForm = 2
    }

    public enum QueryType
    {
        Text = 1,
        StoredProcedure = 2
    }

    public enum ClassificationType
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Non-Cash")]
        NonCash = 2,
        [Description("Tax Challan")]
        TaxChallan = 3,
        [Description("Management Review")]
        ManagementReview = 4
    }

    public enum ControlType
    {
        TextBox = 1,
        DropDown = 2,
        DateRange = 3,
        Label = 4,
        DatePicker = 5,
        TextArea = 6,
        HiddenField = 7,
        SingleCheckbox = 8,
        CallToAction = 9,
        Currency = 10
    }

    public enum CallCentreScreenType
    {
        Supervisor = 1,
        Agent = 2
    }

    public enum DataSourceType 
    { 
        SystemFunction = 1,
        StoredProcedure = 2,
        Endpoint = 3,
        RequestParam = 4
    }
}
