using FunkyCode.Blog.App.Core.Infrastructure.Internals;
using FunkyCode.Blog.App.Internals;
using FunkyCode.Blog.App.Internals.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FunkyCode.Blog.Inf.MarkdownService
{
    public class ArticleItemsProcessor : IArticleItemsProcessor
    {
        private readonly IParser<ArticleItemInfo> _articleItemInfoParser;
        private readonly IArticleItemInfoBuilder _articleItemInfoBuilder;
        List<ArticleItemInfo> _figures = new List<ArticleItemInfo>();
        List<ArticleItemInfo> _listings = new List<ArticleItemInfo>();

        public ArticleItemsProcessor(IParser<ArticleItemInfo> articleItemInfoParser, IArticleItemInfoBuilder articleItemInfoBuilder)
        {
            _articleItemInfoParser = articleItemInfoParser;
            _articleItemInfoBuilder = articleItemInfoBuilder;
        }

        public string Process(string input)
        {
            var lines = input.Split(Environment.NewLine);

            var output = new List<string>();

            string pendingDescription = null;
            var isCombined = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string lineToTrim = lines[i];
                var line = lineToTrim.Trim();
               

                if (line.Contains("3 times"))
                {

                }

                if (_articleItemInfoParser.TryParse(line, out ArticleItemInfo info))
                {
                    if (info.ItemType == ArticleItemTypeEnum.Listing || info.ItemType == ArticleItemTypeEnum.ListingWithFigure)
                    {
                        _listings.Add(info);
                        info.Position = _listings.Count;
                        var description = _articleItemInfoBuilder.BuildItemDescription(info);
                        output.Add(line);
                        pendingDescription = description;

                        if (info.ItemType == ArticleItemTypeEnum.ListingWithFigure)
                            isCombined = true;
                    }
                    else if (info.ItemType == ArticleItemTypeEnum.Figure)
                    {
                        _figures.Add(info);
                        info.Position = _figures.Count;
                        var description = _articleItemInfoBuilder.BuildItemDescription(info);
                        output.Add(line + "<br>");
                        output.Add(description);
                        isCombined = false;
                    }
                    else if (info.ItemType == ArticleItemTypeEnum.FigureAsListingPart)
                    {
                        output.Add(line);
                        output.Add(pendingDescription.ToString());
                        isCombined = false;
                        pendingDescription = null;

                    }
                        else
                        throw new ArgumentException();
                }
                else
                {
                    output.Add(line);

                    if (line.StartsWith("```") && null != pendingDescription && !isCombined)
                    {
                        output.Add(pendingDescription.ToString());
                        pendingDescription = null;
                    }

                }
            }

            for (int i = 0; i < output.Count; i++)
            {
                var line = output[i];

                if (line.Contains("ombine"))
                { }    

                var references = _articleItemInfoBuilder.GetReferences(line);

                if (references.Count == 0) continue;


                var updated = line.ToString();
                foreach (var reference in references)
                {
                    var item = _listings.FirstOrDefault(x => x.Id == reference);
                    if (null == item)
                        item = _figures.FirstOrDefault(x => x.Id == reference);
                    if (null == item)
                        throw new ArgumentException("Empty caption item");

                    updated = _articleItemInfoBuilder.UpdateReferenceInLine(updated, item, reference);
                }

                output[i] = updated;
            }

            var result = string.Join(Environment.NewLine, output);

            return result;

        }
    }
}
