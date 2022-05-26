using System.ComponentModel;

namespace SchoolManagementApp.Domain.Results
{
    public enum Term
    {
        [Description("First")]
        First,
        [Description("Second")]
        Second, 
        [Description("Third")]
        Third
    }

    public enum Grade
    {
        [Description("A")]
        A, 
        [Description("B")]
        B, 
        [Description("C")]
        C, 
        [Description("D")]
        D,
        [Description("E")]
        E, 
        [Description("F")]
        F
    }

    public enum Remark
    {
        [Description("Excellent")]
        Excellent,
        [Description("Good")]
        Good,
        [Description("Pass")]
        Pass,
        [Description("Fair")]
        Fair,
        [Description("Fail")]
        Fail
    }
}
