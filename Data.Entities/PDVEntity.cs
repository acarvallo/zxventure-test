using Data.Entities.Geo;
using Data.Repository.Core;

namespace Data.Entities
{
    public class PDVEntity : BaseEntity
    {
        public string tradingName { get; set; }
        public string ownerName { get; set; }
        public string document { get; set; }

       
    
        public Multipolygon coverageArea { get; set; }
        public Point address { get; set; }
    }
}
