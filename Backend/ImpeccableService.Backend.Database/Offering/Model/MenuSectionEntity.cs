using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpeccableService.Backend.Database.Offering.Model
{
    [Table("menu_section")]
    internal class MenuSectionEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string MenuId { get; set; }
        
        public MenuEntity Menu { get; set; }
        
        public string Name { get; set; }
        
        public List<MenuItemEntity> Items { get; set; }
    }
}