using SportsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplicationApi.Model
{
    public class TestDetailModel
    {
        public Test test { get; set; }
        public List<Detail> detail { get; set; }
    }
}
