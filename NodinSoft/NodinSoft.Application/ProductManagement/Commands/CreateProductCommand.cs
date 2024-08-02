using MediatR;
using System.Text.Json.Serialization;

namespace NodinSoft.Application.ProductManagement.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        [JsonIgnore]
        public string? ManufacturePhone { get; set; }
        [JsonIgnore]
        public string? ManufactureEmail { get; set; }
        public bool? IsAvailable { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
    }
}