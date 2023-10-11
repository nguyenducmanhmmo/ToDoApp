using System.ComponentModel.DataAnnotations;

namespace TodoApp.Core.Entities.General
{
    //Base class for entities common properties
    public class Base<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
