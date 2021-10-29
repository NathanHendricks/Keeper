using System;

namespace Keeper.Models
{
    public class Profile
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}