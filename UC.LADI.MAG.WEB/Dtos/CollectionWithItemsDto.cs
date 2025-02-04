namespace UC.LADI.MAG.WEB.Dtos
{
    public class CollectionWithItemsDto
    {
        public Guid CollectionId { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
