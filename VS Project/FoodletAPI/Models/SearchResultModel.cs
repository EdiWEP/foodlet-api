using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodletAPI.Models
{
    public class SearchResultModel
    {

        public object Result;
        // 200 -> success, 204 -> empty results, 404 -> user not found
        public int Code;
    }
}
