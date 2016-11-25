using System;

namespace CommunityPortal.Models
{
    public class ForumPostViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime PostTime { get; set; }
        public int ThreadId { get; set; }
        public UserViewModel User { get; set; }

    }
}