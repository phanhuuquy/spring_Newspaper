namespace UC.LADI.MAG.WEB.Dtos
{
    public class UpdateItemRequest
    {
        public Entities.mag_item Item { get; set; }
        public List<Guid> CollectionIds { get; set; }
    }
}
