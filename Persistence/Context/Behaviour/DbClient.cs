using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Persistence.Context.Interface;

namespace Persistence.Context.Behaviour
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Book> _books;

        public DbClient(IOptions<DbConfig> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.Server);
            var database = client.GetDatabase(dbConfig.Value.Database);
            _books = database.GetCollection<Book>(dbConfig.Value.BookCollection);
        }

        public IMongoCollection<Book> GetAllBooks()
        {
            return _books;
        }
    }
}
