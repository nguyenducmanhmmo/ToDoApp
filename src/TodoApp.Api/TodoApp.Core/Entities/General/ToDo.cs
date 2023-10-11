namespace TodoApp.Core.Entities.General
{
    public class ToDo : Base<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User User { get; set; }
    }
}
