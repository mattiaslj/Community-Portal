using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Message { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string ReceiverUsername { get; set; }

        public DateTime Time { get; set; }
    }
}