using Microsoft.EntityFrameworkCore;
using WebApiCoreBasics.Models.DbCtx;
using WebApiCoreBasics.Models.DTO.User;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.DataLayer
{
    public class UserDataLayer : IUserDataLayer
    {
        public async Task<User> AddUser(User newuser)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Users.Add(newuser);
                await db.SaveChangesAsync();

                return newuser;
            }

        }

        public async Task<bool> DeleteUserById(long id)
        {
            using(MyDbContext db = new MyDbContext())
            {
                User userToDelete = db.Users.FirstOrDefault(x => x.Id == id);

                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;

            }
        }

        public async Task<List<User>> GetAll()
        {
            using(MyDbContext db = new MyDbContext())
            {
                return await db.Users.ToListAsync();
            }
        }

        public async Task<User> GetUserById(long id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User userById = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

                return userById != null ? userById : new User();
            }
        }

        public async Task<bool> UpdateUser(User userToEdit)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User userById = await db.Users.FirstOrDefaultAsync(x => x.Id == userToEdit.Id);

                if (userById != null)
                {
                    userById.Name = userToEdit.Name;
                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }

        public async Task<bool> UpdateUserPassword(long id, byte[] passwordHash)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User userToUpdatepass = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (userToUpdatepass == null) return false;
                userToUpdatepass.PasswordHash = passwordHash;
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}
