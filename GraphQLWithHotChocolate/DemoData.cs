using System.Collections.Generic;
using System.Linq;
using GraphQLWithHotChocolate.Context;
using GraphQLWithHotChocolate.Models;

namespace GraphQLWithHotChocolate.DemoData {
    public class DemoDataCreator {
        public List<Blog> CreateDemoData () {
            List<Blog> demoBlogs = new List<Blog> () {
                new Blog () { Url = "http://www.youtube.com" },
                new Blog () { Url = "http://www.google.com" },
                new Blog () { Url = "http://www.microsoft.com" },
                new Blog () { Url = "http://www.azure.com" }
            };
            using (var db = new DemoContext ()) {
                foreach (var blog in demoBlogs) {
                    blog.Posts.AddRange (new List<Post> () {
                        new Post () { Title = "Post1", Content = "Lorem Ipsum" },
                        new Post () { Title = "Post2", Content = "Lorem Ipsum" },
                        new Post () { Title = "Post3", Content = "Lorem Ipsum" },
                        new Post () { Title = "Post4", Content = "Lorem Ipsum" }
                    });
                    db.Add (blog);
                    db.SaveChanges ();
                    db.ChangeTracker.Clear ();
                }
                return db.Blogs.ToList ();
            }
        }
    }
}