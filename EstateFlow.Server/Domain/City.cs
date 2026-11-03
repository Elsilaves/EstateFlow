using System.Text.Json.Serialization; // For temporary JasonIgnore attribute.

namespace EstateFlow.Server.Domain
{
    public class City
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Temporary solution to avoid cyclic reference issues during serialization, before implementing DTOs as final solution.
        [JsonIgnore]
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
