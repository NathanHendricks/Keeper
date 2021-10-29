using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

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

//  THIS WILL SOFT DELETE A KEEP ---------------------------------
        internal void Delete(int id)
        {
            string sql = @"
            UPDATE keeps
            SET IsDeleted = 1
            WHERE id = @id;";
            _db.Execute(sql, new{ id} );
        }
    }
}