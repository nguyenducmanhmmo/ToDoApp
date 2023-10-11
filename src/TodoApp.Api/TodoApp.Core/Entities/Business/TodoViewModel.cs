using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.Entities.General;

namespace TodoApp.Core.Entities.Business
{
    public class TodoViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Required, StringLength(maximumLength: 200)]
        public string Title { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    }
}
