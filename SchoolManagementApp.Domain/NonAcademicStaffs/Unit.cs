using System.ComponentModel;

namespace SchoolManagementApp.Domain.NonAcademicStaffs
{
    public enum Unit
    {
        [Description("Administrative")]
        Administrative, 
        [Description("Technical")]
        Technical,
        [Description("Services")]
        Service
    }
}
