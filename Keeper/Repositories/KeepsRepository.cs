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
        public List<Keep> GetKeepsByAccount(string profileId)
        {
            string sql = @"SELECT 
            k.*, a.* 
            FROM keeps k 
            JOIN accounts a on a.id = k.creatorId
            WHERE k.creatorId = @profileId;";
            return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }, new{ profileId }).ToList();
        }

//  THIS WILL GET ALL KEEPS -------------------------------------
        public List<Keep> GetAll()
        {
            string sql = @"
            SELECT k.*, a.* 
            FROM keeps k
            JOIN accounts a on a.id = k.creatorId;";
            return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
            {
                k.Creator = a;
                return k;
            }).ToList();
        }

//  THIS WILL GET A KEEP BY ITS ID -------------------------------
        public Keep GetById(int keepId)
        {
            string sql = @"
            UPDATE keeps k
            SET
            views = k.views + 1
            WHERE k.id = @keepId;
            SELECT k.*, a.*
            FROM keeps k
            JOIN accounts a on a.id = k.creatorId
            WHERE k.id = @keepId AND k.isDeleted = 0;";
            return _db.Query<Keep, Profile, Keep>(sql, (k, a) => 
            {
                k.Creator = a;
                return k;
            }, new{ keepId }).FirstOrDefault();
        }

// THIS WILL CREATE A NEW KEEP ----------------------------------
        public Keep Create(Keep newData)
        {
            string sql = @"
            INSERT INTO keeps(creatorId, name, description, img, isDeleted, views, shares, keeps)
            VALUES(@CreatorId, @Name, @Description, @Img, @isDeleted, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newData);
            newData.Id = id;
            return newData;
        }

//  THIS WILL UPDATE A KEEP ------------------------------------------
        public ActionResult<Keep> Update(Keep foundKeep)
        {
            string sql = @"
            UPDATE keeps
            SET 
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id LIMIT 1;";
            var rowsAffected = _db.Execute(sql, foundKeep);
            if(rowsAffected == 0)
            {
                throw new System.Exception("Update failed.....");
            }
            return foundKeep;
        }

//  THIS WILL SOFT DELETE A KEEP ---------------------------------
        public void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1;";
            _db.Execute(sql, new{ id } );
            
        }
    }
}