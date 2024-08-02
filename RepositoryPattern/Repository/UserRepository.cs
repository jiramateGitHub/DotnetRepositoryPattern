using Dapper;
using RepositoryPattern.Helper;
using RepositoryPattern.Models;
using System.Data;

namespace RepositoryPattern.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
    }
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public UserRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var connection = _dbHelper.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "sp_GetUserById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using (var connection = _dbHelper.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", user.Name);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "sp_CreateUser",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                user.Id = parameters.Get<int>("@Id");
                return user;
            }
        }
    }
}
