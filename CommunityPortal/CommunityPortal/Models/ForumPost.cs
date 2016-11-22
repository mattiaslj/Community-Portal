using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class ForumPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(5000)]
        public string Body { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public ForumThread Thread { get; set; }

        [Required]
        public DateTime PostTime { get; set; }
    }
}