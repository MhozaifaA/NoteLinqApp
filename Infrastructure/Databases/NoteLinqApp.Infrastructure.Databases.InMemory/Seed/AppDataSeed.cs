using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NoteLinqApp.Infrastructure.Databases.InMemory;
using NoteLinqApp.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteLinqApp.Infrastructure.Databases.InMemory
{
    public static class AppDataSeed
    {
        /// <summary>
        /// Add default admin use .. 
        /// <para> UserName: user, Password: user</para>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static async Task<NoteLinqAppInMemoryDbContext> AccountsSeedAsync(this NoteLinqAppInMemoryDbContext context,
         IServiceProvider provider) //local scoped
        {

            var userManager = provider.GetRequiredService<UserManager<Account>>();
           
            var account = new Account()
            {
                Name = "NorLinq Huzaifa",
                UserName = "huzaifa",
                Email = "huzaifa@norlinq.com.",
            };

            await CreateUser(userManager, account);


            account = new Account()
            {
                Name = "NorLinq User",
                UserName = "user",
                Email = "user@norlinq.com.",
            };

            await CreateUser(userManager, account);

          

             account = new Account()
            {
                Name = "NorLinq Bjarni",
                UserName = "bjarni",
                Email = "bjarni@norlinq.com.",
            };

            await CreateUser(userManager, account);



            return context;
        }

        private static async Task CreateUser(UserManager<Account> userManager, Account account)
        {
          
          var  one = await userManager.FindByNameAsync(account.UserName);
            if (one is null)
                await userManager.CreateAsync(account, "huzaifa");
        }
    }
}
