namespace ImpeccableService.Backend.Domain.UserManagement
{
    public class Company
    {
        public Company(string id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public string Id { get; }

        public string Name { get; }
    }
}