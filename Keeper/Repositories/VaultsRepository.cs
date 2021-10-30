using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;
using Microsoft.AspNetCore.Mvc;

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
        public List<Vault> GetVaultsByAccount(string profileId)
        {
            string sql = "SELECT * FROM vaults v WHERE v.creatorId = @profileId;";
            return _db.Query<Vault>(sql, new{ profileId }).ToList();
        }

//  THIS WILL GET A VAULT BY ITS ID ---------------------------------------------
        public Vault GetById(int vaultId)
        {
            string sql = @"
            SELECT v.*, a.*
            FROM vaults v
            JOIN accounts a on a.id = v.creatorId
            WHERE v.id = @vaultId;";
            return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { vaultId }).FirstOrDefault();
        }

//  THIS WILL CREATE A NEW VAULT -------------------------------------------
        public Vault Create(Vault newVault)
        {
            string sql = @"
            INSERT INTO vaults(creatorId, name, description, isPrivate)
            VALUES(@CreatorId, @Name, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            newVault.Id = id;
            return newVault;
        }

//  THIS WILL UPDATE A VAULT ---------------------------------------------
        public ActionResult<Vault> Update(Vault foundVault)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
            WHERE id = @Id LIMIT 1 ;";
            var rowsAffected = _db.Execute(sql, foundVault);
            if(rowsAffected == 0)
            {
                throw new System.Exception("Update Failed .....");
            }
            return foundVault;
        }

        public void Delete(int vaultId)
        {
            string sql = "DELETE FROM vaults WHERE id = @Id;";
            _db.Execute(sql, new { vaultId });
        }
    }
}