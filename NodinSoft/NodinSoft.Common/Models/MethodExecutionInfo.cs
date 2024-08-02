using NodinSoft.Common.Enum;

namespace NodinSoft.Common.Model
{
    public class MethodExecutionInfo
    {
        public object? Result { get; set; }
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public ApiStatus Status { get; set; }
    }
}