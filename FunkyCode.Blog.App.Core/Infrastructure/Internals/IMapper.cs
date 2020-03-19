using System;
using System.Collections.Generic;
using System.Text;

namespace FunkyCode.Blog.App.Core.Infrastructure.Internals
{
    public interface IMapper<TFirst, TTSecond>
    {
        TTSecond Map(TFirst first);
        TFirst Map(TTSecond second);
        List<TTSecond> Map(List<TFirst> firstCollection);
        List<TFirst> Map(List<TTSecond> secondCollection);

    }
}
