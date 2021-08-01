namespace ControlPanel.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum Category
    {
        Services=1,
        Products=2,
        Careers=3
    }
}