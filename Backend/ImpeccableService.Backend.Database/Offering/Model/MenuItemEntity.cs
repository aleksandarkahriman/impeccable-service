using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImpeccableService.Backend.Domain.Offering;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("menu_item")]
    internal class MenuItemEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string MenuSectionId { get; set; }
        
        public MenuSectionEntity Section { get; set; }
        
        public string Name { get; set; }
    }
}