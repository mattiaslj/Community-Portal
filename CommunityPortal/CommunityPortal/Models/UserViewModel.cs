using System.Collections.Generic;

namespace CommunityPortal.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public List<ForumPost> Posts { get; set; }
        public List<ForumThread> Threads { get; set; }
    }
}