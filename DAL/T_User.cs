//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_User
    {
        public T_User()
        {
            this.Addresses = new HashSet<Address>();
            this.Ads = new HashSet<Ad>();
            this.Categories = new HashSet<Category>();
            this.Comments = new HashSet<Comment>();
            this.Contacts = new HashSet<Contact>();
            this.FavLogoTitles = new HashSet<FavLogoTitle>();
            this.Log_Table = new HashSet<Log_Table>();
            this.Metas = new HashSet<Meta>();
            this.Posts = new HashSet<Post>();
            this.Posts1 = new HashSet<Post>();
            this.PostImages = new HashSet<PostImage>();
            this.PostTags = new HashSet<PostTag>();
            this.SocialMedias = new HashSet<SocialMedia>();
            this.Videos = new HashSet<Video>();
        }
    
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public string ImagePath { get; set; }
        public bool isAdmin { get; set; }
        public Nullable<bool> isDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public System.DateTime AddDate { get; set; }
        public int LastUpdateUserID { get; set; }
        public System.DateTime LastUpdateDate { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<FavLogoTitle> FavLogoTitles { get; set; }
        public virtual ICollection<Log_Table> Log_Table { get; set; }
        public virtual ICollection<Meta> Metas { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Post> Posts1 { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<SocialMedia> SocialMedias { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
