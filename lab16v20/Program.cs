using System;
using System.Collections.Generic;
using System.Linq;

class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public List<Tag> Tags { get; set; } = new();
}

class Blog
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Post> Posts { get; set; } = new();
}

static class PostExtensions
{
    public static bool HasTag(this Post post, string tagName)
    {
        return post.Tags.Any(t =>
            t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));
    }
}

class Program
{
    static void Main()
    {
        List<Blog> blogs = new()
        {
            new Blog
            {
                Id = 1,
                Name = "IT Blog",
                Posts = new List<Post>
                {
                    new Post { Id = 1, Title = "C# Basics", Tags = new() {
                        new Tag { Id = 1, Name = "C#" },
                        new Tag { Id = 2, Name = "Programming" }
                    }},
                    new Post { Id = 2, Title = "LINQ Tutorial", Tags = new() {
                        new Tag { Id = 3, Name = "LINQ" },
                        new Tag { Id = 2, Name = "Programming" }
                    }},
                    new Post { Id = 3, Title = "OOP Principles", Tags = new() {
                        new Tag { Id = 4, Name = "OOP" }
                    }},
                    new Post { Id = 4, Title = "Python Basics", Tags = new() {
                        new Tag { Id = 5, Name = "Python" }
                    }},
                    new Post { Id = 5, Title = "Web Development", Tags = new() {
                        new Tag { Id = 6, Name = "Web" }
                    }},
                    new Post { Id = 6, Title = "Databases", Tags = new() {
                        new Tag { Id = 7, Name = "SQL" }
                    }}
                }
            },

            new Blog
            {
                Id = 2,
                Name = "Game Development",
                Posts = new List<Post>
                {
                    new Post { Id = 7, Title = "Unity Basics", Tags = new() {
                        new Tag { Id = 8, Name = "Unity" }
                    }},
                    new Post { Id = 8, Title = "Game Design", Tags = new() {
                        new Tag { Id = 9, Name = "Games" }
                    }},
                    new Post { Id = 9, Title = "C# in Unity", Tags = new() {
                        new Tag { Id = 1, Name = "C#" }
                    }},
                    new Post { Id = 10, Title = "2D Games", Tags = new() {
                        new Tag { Id = 9, Name = "Games" }
                    }}
                }
            }
        };

        // 1. SelectMany - отримання унікальних тегів

        Console.WriteLine("=== SELECTMANY: UNIQUE TAGS ===");

        var uniqueTags = blogs
            .SelectMany(blog => blog.Posts)
            .SelectMany(post => post.Tags)
            .Select(tag => tag.Name)
            .Distinct()
            .OrderBy(name => name);

        foreach (var tag in uniqueTags)
        {
            Console.WriteLine(tag);
        }

        // 2. Pagination - пагінація по 5 постів

        Console.WriteLine("\n=== PAGINATION ===");

        var allPosts = blogs
            .SelectMany(blog => blog.Posts)
            .ToList();

        int pageSize = 5;
        int totalPages = (int)Math.Ceiling(
            allPosts.Count / (double)pageSize);

        for (int page = 1; page <= totalPages; page++)
        {
            Console.WriteLine($"\nPage {page}:");

            var pagePosts = allPosts
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            foreach (var post in pagePosts)
            {
                Console.WriteLine($"{post.Id}. {post.Title}");
            }
        }

        // 3. Custom Extension Method

        Console.WriteLine("\n=== CUSTOM EXTENSION: HasTag ===");

        foreach (var blog in blogs)
        {
            foreach (var post in blog.Posts)
            {
                if (post.HasTag("C#"))
                {
                    Console.WriteLine(
                        $"{post.Title} contains tag C#");
                }
            }
        }

        // Перевірка іншого тегу

        Console.WriteLine("\n=== TAG SEARCH ===");

        var selectedPost = allPosts.First();

        Console.WriteLine(
            $"Post: {selectedPost.Title}");

        Console.WriteLine(
            $"Has C# tag: {selectedPost.HasTag("C#")}");

        Console.WriteLine(
            $"Has Python tag: {selectedPost.HasTag("Python")}");
    }
}