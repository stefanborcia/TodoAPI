using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Contracts
{
    public class UpdateTodoRequest
    {
        [StringLength(100)]
        public string? Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        [Range(1, 5)]
        public int? Priority { get; set; }

        public UpdateTodoRequest()
        {
            IsCompleted = false;
        }
    }
}
