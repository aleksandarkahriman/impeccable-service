namespace ImpeccableService.Backend.Core.UserManagement.Model
{
    public class CreateCompanyRequest
    {
        public CreateCompanyRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}