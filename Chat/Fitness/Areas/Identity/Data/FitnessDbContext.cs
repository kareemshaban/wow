using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Fitness.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Models
{
    public class FitnessDbContext : IdentityDbContext<ApplicationUser>
    {
        public FitnessDbContext(DbContextOptions<FitnessDbContext> options)
            : base(options)
        {   }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Level> Level { get; set; }
        public DbSet<UserProduct> UserProduct { get; set; }
        public DbSet<FollowUser> FollowUser { get; set; }
        public DbSet<BlockUser> BlockUser { get; set; }
        public DbSet<User2UserMsg> User2UserMsg { get; set; }
        public DbSet<ConnectionIdTbl> ConnectionIdTbl { get; set; }
        public DbSet<OtherLevel> OtherLevel { get; set; }
        public DbSet<SiteSetting> SiteSetting { get; set; }
        public DbSet<Gift> Gift { get; set; }
        public DbSet<UserGift> UserGift { get; set; }
        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<MainBanner> MainBanner { get; set; }
        public DbSet<FestivalBanner> FestivalBanner { get; set; }
        //Chat room 
        public DbSet<ChatRoom> ChatRoom { get; set; }
        public DbSet<ChatRoomMember> ChatRoomMember { get; set; }
        public DbSet<ChatRoomMsg> ChatRoomMsg { get; set; }
        public DbSet<ChatRoomCategory> ChatRoomCategory { get; set; }
        public DbSet<ChatRoomFollower> ChatRoomFollower { get; set; }
        public DbSet<Emosion> Emosion { get; set; }
        public DbSet<Background> Background { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<ChargingLevel> ChargingLevel { get; set; }
        public DbSet<Rollet> Rollet { get; set; }
        public DbSet<RolletMember> RolletMember { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<UserImageLike> UserImageLike { get; set; }
        public DbSet<HelpCenter> HelpCenter { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<MicClosedState> MicClosedState { get; set; }
        public DbSet<Lucky> Lucky { get; set; }


    }
}
