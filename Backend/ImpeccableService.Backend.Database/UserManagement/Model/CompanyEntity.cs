using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImpeccableService.Backend.Database.UserManagement.Model
{
    [Table("company")]
    public class CompanyEntity
    {
        [Key]
        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}