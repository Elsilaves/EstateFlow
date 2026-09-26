namespace EstateFlow.Server.Domain
{
    public class PropertyImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; }

        public Guid propertyId { get; set; }
        public Property? Property { get; set; }
    }
}
