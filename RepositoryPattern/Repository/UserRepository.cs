using Dapper;
using Microsoft.Data.SqlClient;
using RepositoryPattern.Helper;
using RepositoryPattern.Models;
using System.ComponentModel;
using System.Data;

namespace RepositoryPattern.Repository
{
    public interface IUserRepository
    {
        object User(RequestMessage<User> request);
    }
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public UserRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public object User(RequestMessage<User> request)
        {
            ResponseMessage<List<object>> response = new ResponseMessage<List<object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);

                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }

                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

                using (var connection = _dbHelper.CreateConnection())
                {
                    response.body = connection.Query<object>("sp_User", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
