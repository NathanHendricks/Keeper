using System.Data;
using System.Linq;
using Dapper;
using Keeper.Models;

namespace Keeper.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;
        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        public Profile GetById(string id)
        {
            string sql = "SELECT p.* WHERE p.id = @id;";
            return _db.Query<Profile>(sql).FirstOrDefault();
        }
    }
}