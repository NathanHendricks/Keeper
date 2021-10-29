using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

//  THIS WILL GET ALL KEEPS BY ACCOUNT ID ------------------
        internal List<Keep> GetKeepsByAccount(string userId)
        {
            string sql = "SELECT * FROM keeps k WHERE k.creatorId = @userId;";
            return _db.Query<Keep>(sql, new{userId}).ToList();
        }

//  THIS WILL GET ALL KEEPS -------------------------------------
        internal List<Keep> GetAll()
        {
            string sql = "SELECT * FROM keeps;";
            return _db.Query<Keep>(sql).ToList();
        }

//  THIS WILL GET A KEEP BY ITS ID -------------------------------
        internal Keep GetById(int keepId)
        {
            string sql = @"
            SELECT a.*, k.*
            FROM keeps k
            JOIN accounts a on k.creatorId = a.id
            WHERE k.id = @keepId;";
            return _db.Query<Keep, Account, Keep>(sql, (k, a) => 
            {
                k.Creator = a;
                return k;
            }, new{keepId}).FirstOrDefault();
        }

// THIS WILL CREATE A NEW POST ----------------------------------
        internal Keep Post(Keep kData)
        {
            string sql =@"
            INSERT INTO keeps(creatorId, name, description, img, isDeleted, views, shares, keeps, creator, createdAt, updatedAt)
            VALUES(@CreatorId, @Name, @Description, @Img, @isDeleted, @Views, @Shares, @Keeps, @Creator, @CreatedAt, @UpdatedAt)
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, kData);
            kData.Id = id;
            return kData;
        }

        internal ActionResult<Keep> Update(Keep foundKeep)
        {
            string sql =@"
            UPDATE keeps
            SET 
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id
            LIMIT 1;";
            var rowsAffected = _db.Execute(sql, foundKeep);
            if(rowsAffected == 0)
            {
                throw new System.Exception("Update failed.....");
            }
            return foundKeep;
        }

//  THIS WILL SOFT DELETE A KEEP ---------------------------------
        internal void Delete(int id)
        {
            string sql = @"
            UPDATE keeps
            SET IsDeleted = 1
            WHERE id = @id
            LIMIT 1;";
            var rowsAffected =_db.Execute(sql, new{ id} );
            if(rowsAffected == 0)
            {
                throw new System.Exception("Delete Failed....");
            }
        }
    }
}