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
    
    public partial class LikedPost
    {
        public int Id { get; set; }
        public int TravelerId { get; set; }
        public int PostId { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
