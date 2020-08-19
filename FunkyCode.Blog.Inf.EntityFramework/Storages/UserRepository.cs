using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunkyCode.Blog.App.Core;
using FunkyCode.Blog.App.Core.Infrastructure.Persistence;
using FunkyCode.Blog.Domain.Entites;
using FunkyCode.Blog.Domain.Entites.Client;
using FunkyCode.Blog.Inf.EntityFramework.Context;
using FunkyCode.Blog.Inf.EntityFramework.Tools;
using Microsoft.EntityFrameworkCore;

namespace FunkyCode.Blog.Inf.EntityFramework.Storages
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<BlogContext> _options;

        public UserRepository(DbContextOptions<BlogContext> options)
        {
            _options = options;
        }

        public async Task<bool> CheckIfSubscribed(string email)
        {
            using (var context = new BlogContext(_options))
            {
                var user = await context.Subscribers.FindAsync(email);
                if (null == user) return false;

                return user.Status == SubscriptionStatusTypeEnum.Active;
            }
        }

        public async Task<bool> Subscribe(SubscribeDto user)
        {
            using (var context = new BlogContext(_options))
            {

                var subscriber = await context.Subscribers.FindAsync(user.Email);
                if (null != subscriber)
                {
                    subscriber.Status = SubscriptionStatusTypeEnum.Active;
                    await context.SaveChangesAsync();
                    return true;
                }

                var newSubscriber = new Subscriber
                {
                    Name = user.Username,
                    Email = user.Email,
                    Status = SubscriptionStatusTypeEnum.Active,
                    IsTester = false,
                    SubscriptionStart = DateTime.UtcNow
                };

                await context.Subscribers.AddAsync(newSubscriber);

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> Unsubscribe(SubscribeDto subscribeDto)
        {
            using (var context = new BlogContext(_options))
            {
                var user = await context.Subscribers.FindAsync(subscribeDto.Email);
                if (null == user) return false;

                user.Status = SubscriptionStatusTypeEnum.Inactive;

                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
