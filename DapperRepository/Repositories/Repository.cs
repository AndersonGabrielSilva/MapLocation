using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace DapperRepository.Repositories
{
    public class Repository<TModel> where TModel : class
    {
        private readonly SqlConnection _connection;

        public Repository(SqlConnection connection)
            => _connection = connection;

        public virtual void Create(TModel model) { if (_connection != null) _connection.Insert(model); }

        public virtual List<TModel> Read() => _connection != null ? _connection.GetAll<TModel>().ToList() : new List<TModel>();

        public virtual TModel Read(int id) => _connection != null ? _connection.Get<TModel>(id) : null;

        public virtual void Update(TModel model) { if(_connection != null) _connection.Update(model);}

        public virtual void Delete(TModel model) { if (_connection != null) _connection.Delete(model); }
    }
}
