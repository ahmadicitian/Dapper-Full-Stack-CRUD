using Dapper;
using DapperFullStack.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperFullStack.Repositories
{
    public class DapperRepository
    {
        private readonly string _connectionString;

       

        public DapperRepository(IConfiguration configuration)
        {
            // Correctly get the connection string from the configuration
            _connectionString = configuration.GetConnectionString("ConnectionName");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public void AddUser(Student std)
        {
            using (var con=CreateConnection())
            {
                if (std.Id == 0)
                {
                    string sql = "INSERT INTO Student (Name,Marks,Image) VALUES (@Name, @Marks,@Image)";
                    con.Execute(sql, std);

                }
                else
                {
                    string sql = "Update student set Name=@Name,Marks=@Marks,Image=@Image where id=@id";
                    con.Execute(sql, std);
                }
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            using (var con=CreateConnection()) {
              return  con.Query<Student>("SELECT * FROM Student");
            }
        }
        public Student GetStudent(int id)
        {
            using (var con = CreateConnection())
            {
                return con.QuerySingleOrDefault<Student>("select * from student where id=@id", new { Id = id });
            }
        }

        public async Task<int> DeleteStudent(int id)
        {
            using (var con = CreateConnection())
            {
                return await con.ExecuteAsync("Delete from Student where id = @id", new { Id = id });
            }
        }
    }
}
