using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FunkyCode.Blog.App.Internals.Tools
{
    public class ArticleItemInfoBuilder : IArticleItemInfoBuilder, IParser<ArticleItemInfo>
    {
        Dictionary<string, ArticleItemTypeEnum> articleTypeDictionary = new Dictionary<string, ArticleItemTypeEnum> { { "L", ArticleItemTypeEnum.Listing }, { "F", ArticleItemTypeEnum.Figure } };

        public string BuildItemDescription(ArticleItemInfo info)
        {
            var name = info.ItemType == ArticleItemTypeEnum.Figure ? "Figure" : "Listing";

            if (info.ItemType == ArticleItemTypeEnum.Figure)
            {
                var description = $"<sup>{name} {info.Position}. {info.Description}</sup>";
                return description;
            }
            else if (info.ItemType == ArticleItemTypeEnum.Listing)
            {
                var description = $"<sub style=\"position: relative; top: -15px; \">{name} {info.Position}. {info.Description}</sub>";
                return description;
            }

            throw new ArgumentException();

        }

        public List<string> GetReferences(string line)
        {
            string search = "<<.+>>";
            var matches = Regex.Matches(line, search);
            return matches.Select(m => m.Value.Replace("<<", "").Replace(">>","")).ToList();
        }

        public string UpdateReferenceInLine(string line, ArticleItemInfo info, string reference)
        {
            var name = info.ItemType == ArticleItemTypeEnum.Figure ? "Figure" : "Listing";

            var description = $"*{name} {info.Position}.*";

            var refDecoded = $"<<{reference}>>";

            var replaced = line.Replace(refDecoded, description);

            return replaced;
        }

        public ArticleItemInfo Parse(string str)
        {

            if (IsFigure(str))
            {
                var i1 = str.IndexOf("[");
                var i2 = str.IndexOf("]");
                var content = str.Substring(i1 + 1, (i2 - i1 - 1));

                var id = content;
                string description = null;

                var items = content.Split(':');
                if (items.Length == 2)
                {
                    id = items[0];
                    description = items[1];
                }

                return new ArticleItemInfo
                {
                    Id = id,
                    Description = description,
                    RawCode = str,
                    ItemType = ArticleItemTypeEnum.Figure
                };
            }

            if (IsListing(str))
            {
                var id = "[Anonymous]";
                string description = null;

                if (str.Contains(":"))
                {
                    var items = str.Split(":");
                    id = items[1];

                    if (items.Length == 3)
                    {
                        description = items[2];
                    }
                }

                return new ArticleItemInfo
                {
                    Id = id,
                    RawCode = str,
                    Description = description,
                    ItemType = ArticleItemTypeEnum.Listing
                };
            }

            throw new ArgumentException();


        }

        public bool TryParse(string str, out ArticleItemInfo result)
        {
            result = null;
            if (!(IsFigure(str) || IsListing(str))) return false;


            try
            {
                result = Parse(str);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        bool IsFigure(string str)
        {
            return str.StartsWith("![") && str.Contains("]");
        }

        bool IsListing(string str)
        {
            return str.StartsWith("```") && str.Length > 3;
        }


    }
}
