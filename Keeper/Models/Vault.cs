using System;

namespace Keeper.Models
{
    public class Vault
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public Profile Creator { get; set; }
        public Keep Keep { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class VaultkeepViewModel : Vault
    {
        public int vaultKeepId { get; set; }
    }
}