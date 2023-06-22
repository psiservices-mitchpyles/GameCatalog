using Microsoft.AspNetCore.Mvc;
using GameCatalog.Models;
using GameCatalog.ViewModels;
using Insight.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace GameCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public GameController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("GameCatalog");
            _dbConnection = new SqlConnection(connectionString);
        }
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return _dbConnection.Query<Game>("GetAllGames");
        }
        [HttpGet("{id}")]
        public IEnumerable<Game> Get(int id)
        {
            return _dbConnection.Query<Game>("GetId", new { Id = id });
        }
        [HttpPost]
        public void Post([FromBody] AddGame request)
        {
            if (String.IsNullOrEmpty(request.Name))
            {
                throw new Exception("Invalid Input. Name Cannot be Blank");
            }
            if (String.IsNullOrEmpty(request.Developer))
            {
                throw new Exception("Invalid Input. Name Cannot be Blank");
            }
            var game = new Game
            {
                Name = request.Name,
                Price = request.Price,
                Developer = request.Developer
            };
            _dbConnection.Execute("AddGame", game);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UpdateGame request)
        {           
            if (String.IsNullOrEmpty(request.Name))
            {
                throw new Exception("Invalid Input. Name Cannot be Blank");
            }
            if (String.IsNullOrEmpty(request.Developer))
            {
                throw new Exception("Invalid Input. Name Cannot be Blank");
            }
            var game = new Game
            {
                Id = id,
                Name = request.Name,
                Price = request.Price,
                Developer = request.Developer
            };
            _dbConnection.Execute("UpdateGame", game);       
            
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dbConnection.Query<Game>("DeleteGame", new { Id = id });
        }
    }
}
    

