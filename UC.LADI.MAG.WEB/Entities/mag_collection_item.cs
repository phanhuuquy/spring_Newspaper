using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    public class mag_collection_item : CoreEntity<Guid>
    {
        public Guid collection_id { get; set; }
        public Guid item_id { get; set; }
        public string tenant { get; set; }
    }
}
