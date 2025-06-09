using NodinSoft.Common.Enums; // Corrected to plural

namespace NodinSoft.Common.Models // Corrected to plural
{
    public class MethodExecutionInfo
    {
        public object? Result { get; set; }
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public ApiStatus Status { get; set; }
    }
}