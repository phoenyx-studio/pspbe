using System;
using Xunit;
using pspbe.Service;

namespace pspbe.Tests
{
    public class SluggerTests
    {
        private readonly Slugger _slugger;

        public SluggerTests()
        {
            _slugger = new Slugger();
        }

        [Fact]
        public void Sluggify_WithEnglishText_ReturnsCorrectSlug()
        {
            var title = "Hello World Test";
            var result = _slugger.Sluggify(title);
            Assert.Equal("hello-world-test", result);
        }

        [Fact]
        public void Sluggify_WithCyrillicText_ReturnsTransliteratedSlug()
        {
            var title = "Привет мир";
            var result = _slugger.Sluggify(title);
            Assert.Equal("privet-mir", result);
        }

        [Fact]
        public void Sluggify_WithSpecialCharacters_RemovesSpecialChars()
        {
            var title = "Hello, World! (Test)";
            var result = _slugger.Sluggify(title);
            Assert.Equal("hello-world-test", result);
        }

        [Fact]
        public void Sluggify_WithMultipleSpaces_ConvertsToSingleDash()
        {
            var title = "Hello    World";
            var result = _slugger.Sluggify(title);
            Assert.Equal("hello-world", result);
        }

        [Fact]
        public void Sluggify_WithMultipleDashes_ConvertsToSingleDash()
        {
            var title = "Hello---World";
            var result = _slugger.Sluggify(title);
            Assert.Equal("hello-world", result);
        }

        [Fact]
        public void Sluggify_WithMixedContent_HandlesCorrectly()
        {
            var title = "Тест Test: (Проверка)";
            var result = _slugger.Sluggify(title);
            Assert.Equal("test-test-proverka", result);
        }

        [Fact]
        public void Sluggify_WithEmptyString_ReturnsEmptyString()
        {
            var title = "";
            var result = _slugger.Sluggify(title);
            Assert.Equal("", result);
        }

        [Fact]
        public void Sluggify_WithOnlySpecialCharacters_ReturnsEmptyOrDashes()
        {
            var title = "!!!...()";
            var result = _slugger.Sluggify(title);
            Assert.Equal("", result);
        }
    }
}