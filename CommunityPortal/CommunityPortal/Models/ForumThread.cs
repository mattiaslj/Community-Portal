using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class ForumThread
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Title { get; set; }

        
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime PostTime { get; set; }

    }
}