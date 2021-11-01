using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
    public class VaultkeepsRepository
    {
        private readonly IDbConnection _db;
        public VaultkeepsRepository(IDbConnection db)
        {
            _db = db;
        }

//  THIS WILL GET ALL VAULTKEEPS ------------------------------------
        public List<VaultkeepViewModel> GetKeepVaults(int vaultId)
        {
            string sql = @"
            SELECT 
            v.*,
            k.*,
            vk.id as vaultKeepId,
            vk.vaultId as vaultId,
            vk.keepId as keepId
            FROM vault_keeps vk
            JOIN vaults v ON v.id = vk.vaultId
            JOIN keeps k ON k.id
            WHERE vk.vaultId = @vaultId;";
            return _db.Query<VaultkeepViewModel>(sql, new { vaultId }).ToList();
        }

//  THIS WILL GET A VAULTKEEP BY ITS ID ---------------------------------------
        public VaultkeepViewModel GetById(int vaultkeepId)
        {
            string sql = @"
            SELECT
            vk.*,
            a.*
            FROM vault_keeps vk
            JOIN accounts a ON a.id = vk.creatorId
            WHERE vk.id = @vaultkeepId;";
            return _db.Query<VaultkeepViewModel, Profile, VaultkeepViewModel>(sql, (vk, a) => 
            {
                vk.Creator = a;
                return vk;
            }, new { vaultkeepId }, splitOn:"id").FirstOrDefault();
        }

//  THIS WILL CREATE A NEW VAULTKEEP -------------------------------------------
        public Vaultkeep Create(Vaultkeep newVK)
        {
            string sql = @"
            INSERT INTO vault_keeps(vaultId, keepId)
            VALUES (@VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            newVK.Id = _db.ExecuteScalar<int>(sql, newVK);
            return newVK;
        }

//  THIS WILL SOFT DELETE A VAULTKEEP
        public void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @Id;";
            _db.Execute(sql, new { id });
        }
    }
}