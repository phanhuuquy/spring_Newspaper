using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    public class mag_event : CoreEntity<Guid>
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTimeOffset start_date { get; set; }
        public DateTimeOffset end_date { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public int year { get; set; }
        public string tenant { get; set; }
    }
}
