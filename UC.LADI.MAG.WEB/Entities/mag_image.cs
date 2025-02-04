using UC.Core.Attributes;
using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    [KeyId("id", "AUTO_INCREMENT_GUID")]
    [Schema("mag")]
    [Table("mag_image")]
    public class mag_image : CoreEntity<Guid>
    {
        public string name { get; set; }
        public string path { get; set; }
        public int order_index { get; set; }
        public Guid event_id { get; set; }
        public bool is_hot { get; set; }
        public string tenant { get; set; }
    }
}
