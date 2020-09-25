using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImpeccableService.Backend.Domain.Utility;
using Newtonsoft.Json;

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
        
        public string ThumbnailImageSerialized { get; set; }
        
        [NotMapped]
        public Image Thumbnail
        {
            get => JsonConvert.DeserializeObject<Image>(ThumbnailImageSerialized);
            set => ThumbnailImageSerialized = JsonConvert.SerializeObject(value);
        }
    }
}