namespace EstateFlow.Server.Domain
{
    public class Meter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string SerialNumber { get; set; }
        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }
        public int UtilityTypeId { get; set; }
        public UtilityType? UtilityType { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
