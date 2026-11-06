using EstateFlow.Server.Domain;

namespace EstateFlow.Server.Contracts.Meters
{
    public class MeterResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public string? ReadingPeriod { get; set; }
        public string? Note { get; set; }
        public Guid PropertyId { get; set; }
        public int UtilityTypeId { get; set; }
        public required string UtilityTypeName { get; set; }
        public int ProviderId { get; set; }
        public required string ProviderName { get; set; }
    }
}
