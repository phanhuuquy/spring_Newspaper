namespace UC.LADI.MAG.WEB.Dtos
{
    public class ItemDto
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string avatar { get; set; }
        public string path { get; set; }
        public int order_index { get; set; }

        public string tenant { get; set; }

    }
}
