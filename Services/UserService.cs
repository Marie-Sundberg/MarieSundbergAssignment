using MarieSundbergAssignment.Models;
using MarieSundbergAssignment.Models.User;
using Microsoft.EntityFrameworkCore;

namespace MarieSundbergAssignment.Services
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(UserModel request);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<UserModel> UpdateUserAsync(int id, UserModel request);
        public Task<bool> DeleteUserAsync(int id);

    }



    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }



        public async Task<User> CreateUserAsync(UserModel request)
        {
            if (!await _context.Users.AnyAsync(x => x.Email == request.UserEmail))
            {
                var userEntity = new UserEntity
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.UserPassword,
                    Email = request.UserEmail,
                    StreetAddress = request.StreetAddress,
                    ZipCode = request.ZipCode,
                    City = request.City,

                };

                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();

                // skicka tillbaka en ny user
                return new User
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName= userEntity.LastName,
                    UserPassword = userEntity.Password,
                    Email = userEntity.Email,
                    StreetAddress = userEntity.StreetAddress,
                    ZipCode = userEntity.ZipCode,
                    City = userEntity.City,

                };
            }

            return null!;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity != null)
            {
                userEntity.FirstName = "Anonymous";
                userEntity.LastName = "Anonymous";
                userEntity.Password = "Anonymous";
                userEntity.Email = "Anonymous";
                userEntity.StreetAddress = "Anonymous";
                userEntity.ZipCode = "Anonymous";
                userEntity.City = "Anonymous";

                _context.Entry(userEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var user = new List<User>();
            foreach (var item in await _context.Users.ToListAsync())
                user.Add(new User
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    UserPassword = item.Password,
                    Email = item.Email,
                    StreetAddress = item.StreetAddress,
                    ZipCode = item.ZipCode,
                    City = item.City,
                });

            return user;
        }

        public async Task<UserModel> UpdateUserAsync(int id, UserModel request)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userEntity != null)
            {
                userEntity.FirstName = request.FirstName;
                userEntity.LastName = request.LastName;
                userEntity.Password = request.UserPassword;
                userEntity.Email = request.UserEmail;
                userEntity.StreetAddress = request.StreetAddress;
                userEntity.ZipCode = request.ZipCode;
                userEntity.City = request.City;

                _context.Entry(userEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new UserModel(userEntity.FirstName, userEntity.LastName, userEntity.Password, userEntity.Email, userEntity.StreetAddress, userEntity.ZipCode, userEntity.City);

            }

            return null!;
        }
    }
}
