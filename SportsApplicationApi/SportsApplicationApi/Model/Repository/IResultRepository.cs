using SportsApplicationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models.Repository
{
    interface IResultRepository
    {
        IEnumerable<Result> GetResultsById(string id);
        Result getResultById(int id);
        void DeleteResult(Result result);
        void UpdateResult(Result result);
        void RemoveParticipant(string id);
    }
}
