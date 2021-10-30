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
        internal List<VaultKeep> GetKeepVaults(int vaultId)
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
            return _db.Query<VaultKeep>(sql, new { vaultId }).ToList();
        }

//  THIS WILL GET A VAULTKEEP BY ITS ID ---------------------------------------
        internal VaultKeep GetById(int vaultKeepId)
        {
            string sql = @"
            SELECT vk.*, a.*
            FROM vault_keeps vk
            JOIN account a ON a.id = vk.creatorId
            WHERE vk.id = @vaultKeepId AND vk.isPrivate = 0 ;";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, a) => 
            {
                vk.Creator = a;
                return vk;
            }, new { vaultKeepId }).FirstOrDefault();
        }

//  THIS WILL CREATE A NEW VAULTKEEP -------------------------------------------
        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = @"
            INSERT INTO vault_keeps(vaultId, keepId)
            VALUES (@VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            newVK.Id = _db.ExecuteScalar<int>(sql, newVK);
            return newVK;
        }

//  THIS WILL SOFT DELETE A VAULTKEEP
        internal void Delete(int id)
        {
            string sql = @"
            UPDATE vault_keeps
            SET IsPrivate = 1
            WHERE id = @id
            LIMIT 1 ;";
            var rowAffected = _db.Execute(sql, new{ id });
            if (rowAffected == 0)
            {
                throw new System.Exception("Delete Failed .....");
            }
        }
    }
}