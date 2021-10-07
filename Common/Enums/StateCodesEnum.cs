using System.ComponentModel;

namespace Common.Enums
{
    public enum StateCodesEnum
    {
        [Description("Blocked or remote user")]
        Blocked = 0,
        
        [Description("Active user")]
        Active = 1
    }
}