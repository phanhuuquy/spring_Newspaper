using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    public class mag_collection : CoreEntity<Guid>
    {
        public string name { get; set; }
        public int order_index { get; set; }
        public string tenant {  get; set; }
    }
}
