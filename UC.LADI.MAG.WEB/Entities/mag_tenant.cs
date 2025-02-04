using UC.Core.Models;

namespace UC.LADI.MAG.WEB.Entities
{
    public class mag_tenant : CoreEntity<Guid>
    {
        public string code { get; set; }
        public string name { get; set; }
    }
}
