//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TripsAndTravelSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            this.DislikedPosts = new HashSet<DislikedPost>();
            this.FavoritePosts = new HashSet<FavoritePost>();
            this.LikedPosts = new HashSet<LikedPost>();
        }

        public enum PostStatus { ACCEPTED, DENIED, PENDING}
    
        public int PostId { get; set; }
        public int AgencyId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        public string Destination { get; set; }
        public string TripPhoto { get; set; }
        public System.DateTime TripDate { get; set; }
        public System.DateTime PostDate { get; set; }
        public string Status { get; set; }
        public int NumOfLikes { get; set; }
        public int NumOfDislikes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DislikedPost> DislikedPosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FavoritePost> FavoritePosts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LikedPost> LikedPosts { get; set; }
        public virtual User User { get; set; }
    }
}
