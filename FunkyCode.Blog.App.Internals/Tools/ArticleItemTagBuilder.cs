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
            else if (info.ItemType == ArticleItemTypeEnum.Listing || info.ItemType == ArticleItemTypeEnum.ListingWithFigure)
            {
                var description = $"<sub style=\"position: relative; top: -15px; \">{name} {info.Position}. {info.Description}</sub>";
                return description;
            }

            throw new ArgumentException();

        }

        public List<string> GetReferences(string line)
        {
            string search = "<<.*?>>";
            var matches = Regex.Matches(line, search);
            return matches.Select(m => m.Value.Replace("<<", "").Replace(">>", "")).ToList();
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
                var isPartOfListing = false;

                var items = content.Split(':');
                if (items.Length == 2)
                {
                    id = items[0];
                    description = items[1];

                }

                isPartOfListing = id.EndsWith("+");

                return new ArticleItemInfo
                {
                    Id = id.Replace("+", ""),
                    Description = description,
                    RawCode = str,
                    ItemType = isPartOfListing ? ArticleItemTypeEnum.FigureAsListingPart : ArticleItemTypeEnum.Figure
                };
            }

            if (IsListing(str))
            {
                var id = "[Anonymous]";
                string description = null;
                var hasFigure = false;

                if (str.Contains(":"))
                {
                    var items = str.Split(":");
                    id = items[1];
                    hasFigure = id.EndsWith("+");

                    if (items.Length == 3)
                    {
                        description = items[2];
                    }
                }

                return new ArticleItemInfo
                {
                    Id = id.Replace("+", ""),
                    RawCode = str,
                    Description = description,
                    ItemType = hasFigure ? ArticleItemTypeEnum.ListingWithFigure : ArticleItemTypeEnum.Listing
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

        public bool IsFigure(string str)
        {
            if (!str.StartsWith("![")) return false;

            var i1 = str.IndexOf("[");
            var i2 = str.IndexOf("]");
            if (i1 == -1 || i2 == -1 || i1 > i2) return false;
            
            var content = str.Substring(i1 + 1, (i2 - i1 - 1));
            if (content.Contains("*")) return false;
            return true;
            
        }

        public bool IsListing(string str)
        {
            return str.StartsWith("```") && str.Length > 3;
        }


    }
}
