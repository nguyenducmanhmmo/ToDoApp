namespace TodoApp.Core.Entities.General
{
    public class User : Base<Guid>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
