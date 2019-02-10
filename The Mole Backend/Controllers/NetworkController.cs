using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using The_Mole_Backend.Models;

namespace The_Mole_Backend.Controllers
{
    public class NetworkController : ApiController
    {
        // GET: api/Network
        public IEnumerable<List<string>> Get100Paths()
        {
            //MainAlgorithm mainAlgorithm = new MainAlgorithm("united states", "portugal");
            MainAlgorithm mainAlgorithm = new MainAlgorithm();
            List<List<string>> literalPaths = new List<List<string>>();
            List<int> pathsCount = new List<int>();
            pathsCount = mainAlgorithm.GetPathFor100(ref literalPaths);
            
            return literalPaths;
        }

        //// GET: api/Network/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Network
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Network/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Network/5
        //public void Delete(int id)
        //{
        //}
    }
}
