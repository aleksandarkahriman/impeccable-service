using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("section")]
    internal class SectionEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string MenuId { get; set; }
        
        public MenuEntity Menu { get; set; }
        
        public string Name { get; set; }
    }
}