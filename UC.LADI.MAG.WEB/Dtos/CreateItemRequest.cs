namespace UC.LADI.MAG.WEB.Dtos
{
    public class CreateItemRequest
    {
        public Entities.mag_item Item { get; set; }
        public List<Guid> CollectionIds { get; set; }
    }
}
