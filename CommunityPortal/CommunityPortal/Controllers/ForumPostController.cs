﻿using CommunityPortal.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CommunityPortal.Controllers
{
    public class ForumPostController : Controller
    {
        [HttpPost]
        public ActionResult GetPosts(string id)
        {
            int result;
            int.TryParse(id, out result);

            using (var _db = new ApplicationDbContext())
            {
                var forumThread = _db.ForumThreads.FirstOrDefault(f => f.Id == result);
                var _dbPosts = _db.ForumPosts.Include("User");
                List<ForumPostViewModel> posts = new List<ForumPostViewModel>();

                if (forumThread != null)
                {
                    foreach (var post in _dbPosts)
                    {
                        if (post.Thread != null)
                        {
                            if (post.Thread.Id == result)
                            {
                                // using viewmodels so that no secret stuff about user gets sent
                                var pvm = new ForumPostViewModel();
                                var user = new UserViewModel();
                                pvm.Body = post.Body;
                                pvm.Id = post.Id;
                                pvm.PostTime = post.PostTime;
                                pvm.ThreadId = post.Thread.Id;
                                user.Email = post.User.Email;
                                user.UserName = post.User.UserName;
                                pvm.User = user;

                                if (post.ReplyPostId != 0)
                                {
                                    pvm.ReplyPostId = post.ReplyPostId;
                                }

                                posts.Add(pvm);
                            }
                        }
                    }
                    if (posts.Count > 0)
                    {   // sort list by posts and replys
                        for (int i = 0; i < posts.Count; i++)
                        {
                            if (posts[i].ReplyPostId != 0)
                            {   // get parent post
                                var parentPost = posts.FirstOrDefault(p => p.Id == posts[i].ReplyPostId);
                                // if parentPost has been removed,
                                // remove reference and leave at current index
                                if (parentPost == null)
                                {
                                    posts[i].ReplyPostId = 0;
                                }
                                else
                                {
                                    // get index of parent post
                                    var index = posts.IndexOf(parentPost);
                                    // save reply
                                    var temp = posts[i];
                                    // remove reply from postsList
                                    posts.Remove(posts[i]);
                                    // add reply to list again
                                    if (index + 1 > posts.Count)
                                    {
                                        posts.Add(temp);
                                    }
                                    else
                                    {
                                        posts.Insert(index + 1, temp);
                                    }
                                }
                            }
                        }
                        return Json(JsonConvert.SerializeObject(posts));
                    }
                }
            }
            return Json("no posts or no thread");
        }

        [Authorize]
        public JsonResult AddPost(ForumPost forumPost, string threadId, string username)
        {
            int result;
            int.TryParse(threadId, out result);

            using (var _db = new ApplicationDbContext())
            {
                if (forumPost.Body != null && forumPost.PostTime != null)
                {
                    var forumThread = _db.ForumThreads.FirstOrDefault(f => f.Id == result);
                    var user = _db.Users.FirstOrDefault(u => u.UserName == username);

                    if (user != null && forumThread != null)
                    {
                        forumPost.User = user;
                        forumPost.Thread = forumThread;
                        _db.ForumPosts.Add(forumPost);
                        _db.SaveChanges();

                        return Json("Success");
                    }
                }
            }
            return Json("Something went wrong");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        public JsonResult DeletePost(string Id)
        {
            int result;
            int.TryParse(Id, out result);

            using (var _db = new ApplicationDbContext())
            {
                var post = _db.ForumPosts.FirstOrDefault(p => p.Id == result);

                if (post != null)
                {
                    post.ReplyPostId = 0;
                    post.Thread = null;

                    _db.ForumPosts.Remove(post);
                    _db.SaveChanges();

                    return Json("Success");
                }
                return Json("Something went wrong");
            }
        }

        //[Authorize]
        //[HttpPost]
        //public JsonResult EditPost(string Id)
        //{
        //    int result;
        //    int.TryParse(Id, out result);

        //    using(var _db = new ApplicationDbContext())
        //    {
        //        var post = _db.ForumPosts.FirstOrDefault(p => p.Id == result);

        //        if(post != null)
        //        {

        //        }
        //    }
        //}
    }
}