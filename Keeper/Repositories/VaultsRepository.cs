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
        internal List<Vault> GetVaultsByAccount(string userId)
        {
            string sql = "SELECT * FROM vaults v WHERE v.creatorId = @userId;";
            return _db.Query<Vault>(sql, new{userId}).ToList();
        }

//  THIS WILL GET A VAULT BY ITS ID ---------------------------------------------
        internal Vault GetById(int vaultId)
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
        internal Vault Create(Vault newVault)
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
        internal ActionResult<Vault> Update(Vault foundVault)
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

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @Id;";
            _db.Execute(sql, new { id });
        }
    }
}