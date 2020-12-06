using System;
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
            List<Author> demoAuthors = new List<Author> () {
                new Author () { Name = "Bob" },
                new Author () { Name = "Peter" }
            };
            using (var db = new DemoContext ()) {
                foreach (var blog in demoBlogs) {
                    blog.Posts.AddRange (new List<Post> () {
                        new Post () { Title = "Post1", Content = "Lorem Ipsum", Author = demoAuthors[0] },
                            new Post () { Title = "Post2", Content = "Lorem Ipsum", Author = demoAuthors[1] },
                            new Post () { Title = "Post3", Content = "Lorem Ipsum", Author = demoAuthors[0] },
                            new Post () { Title = "Post4", Content = "Lorem Ipsum", Author = demoAuthors[1] }
                    });
                    db.Add (blog);
                    db.SaveChanges ();
                }
                return db.Blogs.ToList ();
            }
        }
    }
}