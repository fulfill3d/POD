using System.ComponentModel;

namespace POD.Common.Core.Enum
{
    public enum OrderPriority
    {
        [Description("Normal")]
        Normal = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("High")]
        High = 3,
    }
}