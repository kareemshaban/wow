using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Chat.Models.Repositries;
using Fitness.Areas.Identity.Data;
using Fitness.Models;
using Fitness.Models.Repositries;
using FolaChat.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Runtime.CompilerServices;
using System.IO;
using System;
using Fitness.Areas.Identity.Pages.Account;
using System.Security.Policy;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Routing;
namespace Fitness
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            services.AddMvc().AddRazorPagesOptions(options => options.Conventions.AddPageRoute("/Account/Login", "Identity"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Add 
            services.AddScoped<IFitnessRepositry<Category>, CategoryRepositry>();
            services.AddScoped<IFitnessRepositry<Product>, ProductRepositry>();
            services.AddScoped<IFitnessRepositry<Gold>, GoldRepositry>();
            services.AddScoped<IFitnessRepositry<Post>, PostRepositry>();
            services.AddScoped<IFitnessRepositry<Wallet>, WalletRepositry>();
            services.AddScoped<IFitnessRepositry<Level>, LevelRepositry>();
            services.AddScoped<IFitnessRepositry<UserProduct>, UserProductRepositry>();
            services.AddScoped<IFitnessRepositry<FollowUser>, FollowUserRepositry>();
            services.AddScoped<IFitnessRepositry<BlockUser>, BlockUserRepositry>();
            services.AddScoped<IFitnessRepositry<ApplicationUser>, ApplicationUserRepositry>();
            services.AddScoped<IFitnessRepositry<User2UserMsg>, User2UserMsgRepositry>();
            services.AddScoped<IFitnessRepositry<ConnectionIdTbl>, ConnectionIdTblRepositry>();          
            services.AddScoped<IFitnessRepositry<OtherLevel>, OtherOtherLevelRepositry>();
            services.AddScoped<IFitnessRepositry<SiteSetting>, SiteSettingRepositry>();
            services.AddScoped<IFitnessRepositry<Gift>, GiftRepositry>();
            services.AddScoped<IFitnessRepositry<UserGift>, UserGiftRepositry>();
            services.AddScoped<IFitnessRepositry<Visitor>, VisitorRepositry>();
            services.AddScoped<IFitnessRepositry<MainBanner>, MainBannerRepositry>();
            services.AddScoped<IFitnessRepositry<FestivalBanner>,FestivalBannerRepositry>();
            services.AddScoped<IFitnessRepositry<ChatRoom>, ChatRoomRepositry>();
            services.AddScoped<IFitnessRepositry<ChatRoomMember>, ChatRoomMemberRepositry>();
            services.AddScoped<IFitnessRepositry<ChatRoomMsg>, ChatRoomMsgRepositry>();
            services.AddScoped<IFitnessRepositry<ChatRoomCategory>, ChatRoomCategoryRepositry>();
            services.AddScoped<IFitnessRepositry<ChatRoomFollower>, ChatRoomFollowerRepositry>();
            services.AddScoped<IFitnessRepositry<Background>, BackgroundRepositry>();
            services.AddScoped<IFitnessRepositry<Emosion>, EmosionRepositry>();
            services.AddScoped<IFitnessRepositry<Country>, CountryRepositry>();
            services.AddScoped<IFitnessRepositry<ChargingLevel>, ChargingLevelRepositry>();
            services.AddScoped<IFitnessRepositry<Rollet>, RolletRepositry>();
            services.AddScoped<IFitnessRepositry<RolletMember>, RolletMemberRepositry>();
            services.AddScoped<IFitnessRepositry<Music>, MusicRepositry>();
            services.AddScoped<IFitnessRepositry<Like>, LikeRepositry>();
            services.AddScoped<IFitnessRepositry<Comment>, CommentRepositry>();
            services.AddScoped<IFitnessRepositry<UserImage>, UserImageRepositry>();
            services.AddScoped<IFitnessRepositry<UserImageLike>, UserImageLikeRepositry>();
            services.AddScoped<IFitnessRepositry<Notification>, NotificationRepositry>();
            services.AddScoped<IFitnessRepositry<MicClosedState>, MicClosedStateRepositry>();
            services.AddScoped<IFitnessRepositry<HelpCenter>, HelpCenterRepositry>();
            services.AddScoped<IFitnessRepositry<Lucky>, LuckyRepositry>();
            services.AddDbContext<FitnessDbContext>(options => {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("FitnessDbContextConnection"));            
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Default SignIn settings.
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            //End

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                 {
                     options.LoginPath = new PathString("/Account/Login");
                     options.AccessDeniedPath = new PathString("/Home/Index");
                 });
            services.AddAuthorization();
            services.AddCors();
            services.AddSignalR( );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(
            options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSignalR(routes => {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
           
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });           
        }
      
    }
}
