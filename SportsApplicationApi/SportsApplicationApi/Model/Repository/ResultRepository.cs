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
            return db.Results.Where(t => t.TestId.Equals(id));
        }
        public void UpdateResult(Result result)
        {
            var query = (from r in db.Results
                         where r.Id.Equals(result.Id)
                         select r).Single();
            query.UserId = result.UserId;
            query.TestId = result.TestId;
            query.Distance = result.Distance;
            query.Fitness = result.Fitness;
            //db.Results.Update(result);
        }
        public Result getResultById(int id)
        {
            return db.Results.SingleOrDefault(m => m.Id == id);
        }

        public void DeleteResult(Result result)
        {
            db.Results.Remove(result);
        }

        public void RemoveParticipant(string id)
        {
            var temp = db.Tests.Single(r => r.Id.Equals(id));
            temp.Count = temp.Count - 1;
        }


    }

}
