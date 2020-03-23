using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.Domain
{
    public class ArchiveYearDto
    {
        public int Year { get; set; }
        public List<ArchiveArticleDto> Articles { get; set; }
    }
}
