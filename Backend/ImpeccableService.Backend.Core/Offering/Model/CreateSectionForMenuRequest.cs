namespace ImpeccableService.Backend.Core.Offering.Model
{
    public class CreateSectionForMenuRequest
    {
        public CreateSectionForMenuRequest(string menuId, string name)
        {
            MenuId = menuId;
            Name = name;
        }

        public string MenuId { get; }
        
        public string Name { get; }
    }
}