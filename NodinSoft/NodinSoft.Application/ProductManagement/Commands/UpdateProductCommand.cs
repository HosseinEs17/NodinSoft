using NodinSoft.Common.Model;
using MediatR;
using System.Text.Json.Serialization;

namespace NodinSoft.Application.ProductManagement.Commands
{
    public class UpdateProductCommand : IRequest<MethodExecutionInfo>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}