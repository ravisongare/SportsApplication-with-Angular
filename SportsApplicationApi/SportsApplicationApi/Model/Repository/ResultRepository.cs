using SportsApplication.Models.Repository;
using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class ResultRepository:IResultRepository
    {
        private readonly SportsApplicationApiDbContext db;

        public ResultRepository(SportsApplicationApiDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<Result> GetResultsById(string id)
        {
            return db.Results.Where(t => t.test_id.Equals(id));
        }
        public void UpdateResult(Result result)
        {
            var query = (from r in db.Results
                         where r.id.Equals(result.id)
                         select r).Single();
            query.user_id = result.user_id;
            query.test_id = result.test_id;
            query.distance = result.distance;
            query.Fitness = result.Fitness;
            //db.Results.Update(result);
        }
        public Result getResultById(int id)
        {
            return db.Results.SingleOrDefault(m => m.id == id);
        }

        public void DeleteResult(Result result)
        {
            db.Results.Remove(result);
        }

        public void RemoveParticipant(string id)
        {
            var temp = db.Tests.Single(r => r.id.Equals(id));
            temp.count = temp.count - 1;
        }


    }

}
