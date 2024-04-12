using Dapper;
using DataAccess.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(Configure configure)
        {
            _connectionString = configure.ConnectionString;
        }
        public async Task<User> Create(User user)
        {
            using var connect = new SqlConnection(_connectionString);
            string query = @"INSERT INTO dbo.[User] (FirstName, LastName)
                             VALUES (@FirstName, @LastName)";

            var dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("FirstName", user.FirstName);
            dynamicParameter.Add("LastName", user.LastName);

            var result = await connect.QueryAsync<User>(query, dynamicParameter);
            return result.FirstOrDefault();
        }

        public async Task<int> Delete(int id)
        {
            using var connect = new SqlConnection(_connectionString);
            string query = @"DELETE FROM dbo.[User]
                             OUTPUT Deleted.ID
                             WHERE Id = @Id";

            var dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Id", id);

            var result = await connect.QueryAsync<int>(query, dynamicParameter);
            return result.First();
        }

        public async Task<IEnumerable<User>> Get()
        {
            using var connect = new SqlConnection(_connectionString);
            string query = "SELECT * FROM dbo.[User]";

            var users = await connect.QueryAsync<User>(query);
            return users;
        }

        public async Task<User> Get(int id)
        {
            using var connect = new SqlConnection(_connectionString);
            string query = "SELECT * FROM dbo.[User] WHERE Id = @Id";

            var dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Id", id);

            var users = await connect.QueryAsync<User>(query, dynamicParameter);
            return users.FirstOrDefault();
        }

        public async Task<int> Update(User user)
        {
            using var connect = new SqlConnection(_connectionString);
            string query = @"
                            UPDATE dbo.[User]
                            SET FirstName = @FirstName, LastName = @LastName
                            OUTPUT INSERTED.ID
                            WHERE Id = @Id
                            ";

            var dynamicParameter = new DynamicParameters();
            dynamicParameter.Add("Id", user.Id);
            dynamicParameter.Add("FirstName", user.FirstName);
            dynamicParameter.Add("LastName", user.LastName);

            var insertedId = await connect.QueryAsync<int>(query, dynamicParameter);

            return insertedId.FirstOrDefault();
        }
    }
}
