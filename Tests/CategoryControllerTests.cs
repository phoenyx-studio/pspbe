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
    public class CategoryControllerTests : IDisposable
    {
        private readonly BlogDbContext _context;
        private readonly CategoryController _controller;
        private readonly Slugger _slugger;

        public CategoryControllerTests()
        {
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new BlogDbContext(options);
            _slugger = new Slugger();
            _controller = new CategoryController(_context, _slugger);

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

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_ReturnsViewWithCategories()
        {
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Details_WithValidId_ReturnsViewWithCategory()
        {
            var result = await _controller.Details(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Category>(viewResult.Model);
            Assert.Equal("Test Category", model.Title);
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
        public async Task Show_WithValidSlug_ReturnsDetailsView()
        {
            var result = await _controller.Show("test-category");
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Details", viewResult.ViewName);
            var model = Assert.IsType<Category>(viewResult.Model);
            Assert.Equal("Test Category", model.Title);
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
            var newCategory = new Category
            {
                Title = "New Test Category"
            };

            var result = await _controller.Create(newCategory);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var createdCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Title == "New Test Category");
            Assert.NotNull(createdCategory);
            Assert.Equal("new-test-category", createdCategory.Slug);
            Assert.True(createdCategory.Created > DateTime.MinValue);
            Assert.True(createdCategory.Updated > DateTime.MinValue);
        }

        [Fact]
        public async Task Edit_Post_WithValidModel_UpdatesCategory()
        {
            var updatedCategory = new Category
            {
                Id = 1,
                Title = "Updated Test Category"
            };

            var result = await _controller.Edit(1, updatedCategory);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var editedCategory = await _context.Categories.FindAsync(1);
            Assert.Equal("Updated Test Category", editedCategory.Title);
            Assert.Equal("updated-test-category", editedCategory.Slug);
        }

        [Fact]
        public async Task Edit_WithMismatchedId_ReturnsNotFound()
        {
            var category = new Category { Id = 2 };
            var result = await _controller.Edit(1, category);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesCategory()
        {
            var result = await _controller.DeleteConfirmed(1);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            var deletedCategory = await _context.Categories.FindAsync(1);
            Assert.Null(deletedCategory);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}