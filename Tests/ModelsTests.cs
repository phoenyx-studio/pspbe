using System;
using System.ComponentModel.DataAnnotations;
using Xunit;
using pspbe.Models;

namespace pspbe.Tests
{
    public class ModelsTests
    {
        [Fact]
        public void Category_Properties_SetCorrectly()
        {
            var category = new Category
            {
                Id = 1,
                Title = "Test Category",
                Slug = "test-category",
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            Assert.Equal(1, category.Id);
            Assert.Equal("Test Category", category.Title);
            Assert.Equal("test-category", category.Slug);
            Assert.True(category.Created > DateTime.MinValue);
            Assert.True(category.Updated > DateTime.MinValue);
        }

        [Fact]
        public void Post_Properties_SetCorrectly()
        {
            var post = new Post
            {
                Id = 1,
                Title = "Test Post",
                Slug = "test-post",
                Content = "Test content",
                CategoryId = 1,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            Assert.Equal(1, post.Id);
            Assert.Equal("Test Post", post.Title);
            Assert.Equal("test-post", post.Slug);
            Assert.Equal("Test content", post.Content);
            Assert.Equal(1, post.CategoryId);
            Assert.True(post.Created > DateTime.MinValue);
            Assert.True(post.Updated > DateTime.MinValue);
        }

        [Fact]
        public void Post_Category_Navigation_Property_Works()
        {
            var category = new Category
            {
                Id = 1,
                Title = "Test Category",
                Slug = "test-category"
            };

            var post = new Post
            {
                Id = 1,
                Title = "Test Post",
                CategoryId = 1,
                Category = category
            };

            Assert.NotNull(post.Category);
            Assert.Equal(category.Id, post.Category.Id);
            Assert.Equal(category.Title, post.Category.Title);
        }

        [Fact]
        public void Category_Posts_Navigation_Property_Works()
        {
            var category = new Category
            {
                Id = 1,
                Title = "Test Category",
                Posts = new[]
                {
                    new Post { Id = 1, Title = "Post 1", CategoryId = 1 },
                    new Post { Id = 2, Title = "Post 2", CategoryId = 1 }
                }
            };

            Assert.NotNull(category.Posts);
            Assert.Equal(2, category.Posts.Count);
        }
    }
}