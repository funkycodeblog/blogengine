using FluentAssertions;
using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FunkyCode.Blog.Test.Unit
{
    public class ArticleItemParserShould_
    {
        [Test]
        public void Convert_String_To_ArticleItemInfo()
        {
            const string input = "<!-- L:dropclean:Dropping -->";;

            var parser = new ArticleInfoItemParser();

            var info = parser.Parse(input);

            info.Id.Should().Be("dropclean");
            info.ItemType.Should().Be(ArticleItemTypeEnum.Listing);
            info.RawCode.Should().Be(input);
        }

        [Test]
        public void Test()
        {
            string text = "C# is <the> best language there <is> in the world.";
            string search = "<[a-zA-Z]+>";
            MatchCollection matches = Regex.Matches(text, search);
            Console.WriteLine("there was {0} matches for '{1}'", matches.Count, search);


        }

    }
}
