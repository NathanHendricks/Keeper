using System;

namespace Keeper.Models
{
    public class Vaultkeep
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set;}
        public Profile Creator { get; set;}
        public Keep Keep { get; set; }
        public Vault vault { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}