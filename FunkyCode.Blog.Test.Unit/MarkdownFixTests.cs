using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using FunkyCode.Blog.App.Internals.Tools;
using NUnit.Framework;

namespace FunkyCode.Blog.Test.Unit
{
    public class MarkdownFix_Should_
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Convert_Paragraph_To_H1()
        {
            const string input = "<p># Title </p>\r\n";

            var fixer = new MarkdownPreprocessor();
            var output = fixer.Fix(input);

            const string expected = "<h1>Title </h1>\r\n";

            output.Should().Be(expected);
        }

        [Test]
        public void Convert_Header_Content_To_MarkdownParagraph()
        {
            var testHeader = "Test Header";
            var article = ArticleMockRepository.GetCreateCommandLineToolArticleFragment(testHeader);

            var fixer = new MarkdownPreprocessor();
            var output = fixer.Fix(article);



        }
    }
}