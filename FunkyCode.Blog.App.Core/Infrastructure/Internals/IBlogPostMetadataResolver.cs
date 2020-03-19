using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public interface IBlogPostMetadataResolver
    {
        ProcessingResult<BlogPostMetadata> Resolve(string blogAsMarkDown);
    }

    
}
