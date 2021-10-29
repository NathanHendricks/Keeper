using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

//  THIS WILL GET ALL THE VAULTS BY THE ACCOUNT ID -----------
        internal List<Vault> GetVaultsByAccount(string userId)
        {
            string sql = "SELECT * FROM vaults v WHERE v.creatorId = @userId;";
            return _db.Query<Vault>(sql, new{userId}).ToList();
        }
    }
}