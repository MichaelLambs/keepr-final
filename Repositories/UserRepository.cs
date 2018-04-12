using System;
using System.Data;
using keepr_final.Models;
using Dapper;

namespace keepr_final.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public UserReturnModel Register(UserCreateModel userData)
        {
            //GENERATE GUID (GLOBAL UNIQUE ID) ID
            Guid g;
            g = Guid.NewGuid();
            string id = g.ToString();

            // PASSWORD FROM INCOMING USERDATA GETS ENCRYPTED
            string pass = BCrypt.Net.BCrypt.HashPassword(userData.Password);

            // CONSTRUCT A USER
            User user = new User()
            {
                Id = id,
                Name = userData.Name,
                Email = userData.Email,
                Password = pass
            };

            // RUN A SQL COMMAND FOR INSERT IE CREATE
            var success = _db.Execute(@"
            INSERT INTO users(
            id,
            name, 
            email,
            password
            ) VALUES(
            @Id,
            @Name,
            @Email,
            @Password
            )
             ", user); // USER IS THE NEW CONSTRUCTED USER

            if (success < 1) // EXECUTE RETURNS ROWS AFFECTED. SO IF LESS THAN 1 IT DIDNT WORK.
            {
                throw new Exception("INVALID CRENDENTIALS");
            }

            // CREATE & RETURN USER RETURN MODEL FROM CONSTRUCTED USER
            return new UserReturnModel()
            {
                Name = user.Name,
                Email = user.Email,
                Id = user.Id
            };
        }

        public UserReturnModel Login(UserLoginModel userData)
        {
            // LOOK UP USER BY EMAIL
            User user = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE EMAIL = @Email", userData);

            // VALIDATE/VERIFY PASSWORD
            bool valid = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
            if (valid)
            {
                return new UserReturnModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email
                };
            }
            throw new Exception("INVALID CRENDENTIALS");
        }

        public UserReturnModel GetUserById(string id)
        {
            // FIND/RETURN USER BY ID
            User user = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @Id", new { Id = id });

            if (user == null) { throw new Exception("OH BOY, OH BOY"); } // MORE THAN TWICE YOU ARE GETTIG HACKED

            return new UserReturnModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

        }
    }
}