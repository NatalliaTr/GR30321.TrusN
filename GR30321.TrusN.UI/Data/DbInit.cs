﻿using GR30321.TrusN.UI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GR30321.TrusN.UI.Data
{
    public class DbInit
    {     
           public static async Task SetupIdentityAdmin(WebApplication application)
            {
                using var scope = application.Services.CreateScope();
                var userManager = scope
                .ServiceProvider
                .GetRequiredService<UserManager<AppUser>>();
                var user = await userManager.FindByEmailAsync("admin@gmail.com");
                if (user == null)
                {
                    user = new AppUser();
                    await userManager.SetEmailAsync(user, "admin@gmail.com");
                    await userManager.SetUserNameAsync(user, user.Email);
                    user.EmailConfirmed = true;
                    await userManager.CreateAsync(user, "123456");
                    var claim = new Claim(ClaimTypes.Role, "admin");
                    await userManager.AddClaimAsync(user, claim);
                }
            }  
    }
}
