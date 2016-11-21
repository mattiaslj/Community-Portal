using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CommunityPortal.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [StringLength(2000)]
        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime PostTime { get; set; }
        
    }
}