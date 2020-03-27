using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Blog
{
    public interface IBlogPostUploadService
    {

        void Upload(string host, string folderPath, bool isOverrideWhenExists);

    }
}
