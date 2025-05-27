using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using pspbe.Controllers;
using pspbe.Data;
using pspbe.Models;
using pspbe.Service;

namespace pspbe.Tests
{
    public class PostControllerTests : IDisposable
    {
        private readonly BlogDbContext _context;
        private readonly PostController _controller;
        private readonly Slugger _slugger;

        public PostControllerTests()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BlogDbContext(options);
            _slugger = new Slugger();
            _controller = new PostController(_context, _slugger);

            SeedTestData();
        }

        private void SeedTestData()
        {
            var category = new Category
            {
                Id = 1,
                Title = "Test Category",
                Slug = "test-category",
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

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

            _context.Categories.Add(category);
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewWithPosts()
        {
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_WithValidId_ReturnsViewWithPost()
        {
            var result = await _controller.Details(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Post>(viewResult.Model);
            Assert.Equal("Test Post", model.Title);
        }

        [Fact]
        public async Task Details_WithInvalidId_ReturnsNotFound()
        {
            var result = await _controller.Details(999);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_WithNullId_ReturnsNotFound()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Show_WithValidSlug_ReturnsPublicDetailsView()
        {
            var result = await _controller.Show("test-post");
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("PublicDetails", viewResult.ViewName);
            var model = Assert.IsType<Post>(viewResult.Model);
            Assert.Equal("Test Post", model.Title);
        }

        [Fact]
        public async Task Show_WithInvalidSlug_ReturnsNotFound()
        {
            var result = await _controller.Show("invalid-slug");
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Post_WithValidModel_RedirectsToIndex()
        {
            var newPost = new Post
            {
                Title = "New Test Post",
                Content = "New test content",
                CategoryId = 1
            };

            var result = await _controller.Create(newPost);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var createdPost = await _context.Posts.FirstOrDefaultAsync(p => p.Title == "New Test Post");
            Assert.NotNull(createdPost);
            Assert.Equal("new-test-post", createdPost.Slug);
            Assert.True(createdPost.Created > DateTime.MinValue);
            Assert.True(createdPost.Updated > DateTime.MinValue);
        }

        [Fact]
        public async Task Edit_Post_WithValidModel_UpdatesPost()
        {
            var updatedPost = new Post
            {
                Id = 1,
                Title = "Updated Test Post",
                Content = "Updated content",
                CategoryId = 1
            };

            var result = await _controller.Edit(1, updatedPost);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var editedPost = await _context.Posts.FindAsync(1);
            Assert.Equal("Updated Test Post", editedPost.Title);
            Assert.Equal("updated-test-post", editedPost.Slug);
            Assert.Equal("Updated content", editedPost.Content);
        }

        [Fact]
        public async Task Edit_WithMismatchedId_ReturnsNotFound()
        {
            var post = new Post { Id = 2 };
            var result = await _controller.Edit(1, post);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesPost()
        {
            var result = await _controller.DeleteConfirmed(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var deletedPost = await _context.Posts.FindAsync(1);
            Assert.Null(deletedPost);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}