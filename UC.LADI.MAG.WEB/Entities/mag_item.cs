using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    public class mag_item : CoreEntity<Guid>
    {
        public string title { get; set; }
        public string description { get; set; }
        public int order_index { get; set; }
        public string avatar { get; set; }
        public string path { get; set; }
        public string tenant { get; set; }
    }
}
