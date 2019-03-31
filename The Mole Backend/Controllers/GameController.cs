﻿using AdminPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminPage.Controllers
{
    public class GameController : ApiController
    {
        // GET: api/Game
        [HttpGet]
        [Route("api/Games")]
        public IEnumerable<Game> Get()
        {
            try
            {
                List<Game> gamesList = new List<Game>();
                Game g = new Game();
                gamesList = g.Read();
                return gamesList;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }

        // GET: api/Game/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Game
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Game/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
        }
    }
}
