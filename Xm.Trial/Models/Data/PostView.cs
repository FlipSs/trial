using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xm.Trial.Models.Data
{
    public class PostView
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        public string Views { get; set; }

        [Required]
        public int Count { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}