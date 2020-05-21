namespace ImpeccableService.Backend.Domain.Utility
{
    public class File
    {
        public File(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
