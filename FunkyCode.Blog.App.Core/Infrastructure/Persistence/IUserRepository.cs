using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FunkyCode.Blog.Domain.Entites.Client;

namespace FunkyCode.Blog.App.Core.Infrastructure.Persistence
{
    public interface IUserRepository
    {
        Task<bool> CheckIfSubscribed(string email);
        Task<bool> Subscribe(SubscribeDto user);
        Task<bool> Unsubscribe(SubscribeDto user);
    }
}
