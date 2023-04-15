using Milestone_CST350.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestone_CST350.Services
{
    public class MongoUsrersDAO
    {
        private readonly IMongoCollection<MongoUser> _usersCollection;

        public MongoUsrersDAO()
        {
            var client = new MongoClient("mongodb+srv://Ace:squirty115@cluster0.og5dfyn.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("CST-350-Milestone");
            _usersCollection = database.GetCollection<MongoUser>("users");
        }

        public async Task CreateUserAsync(MongoUser user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<List<MongoUser>> GetAllUsersAsync()
        {
            var users = await _usersCollection.FindAsync(_ => true);
            return await users.ToListAsync();
        }

        public MongoUser GetUserByIdAsync(string id)
        {
            var user =  _usersCollection.Find(u => u.ID == id);
            return user.FirstOrDefault();
        }

        public bool FindByNameAndPassword(MongoUser user)
        {
            var filter = Builders<MongoUser>.Filter.Eq(u => u.Username, user.Username) &
                         Builders<MongoUser>.Filter.Eq(u => u.Password, user.Password);
            return _usersCollection.Find(filter).Any();
        }

        public MongoUser GetByNameAndPassword(MongoUser user)
        {
            var filter = Builders<MongoUser>.Filter.And(
                Builders<MongoUser>.Filter.Eq(u => u.Username, user.Username),
                Builders<MongoUser>.Filter.Eq(u => u.Password, user.Password)
            );

            var foundUser = _usersCollection.Find(filter).FirstOrDefault();

            return foundUser;
        }


        public async Task<MongoUser> GetUserByEmailAsync(string email)
        {
            var user = await _usersCollection.FindAsync(u => u.Email == email);
            return await user.FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(string id, MongoUser updatedUser)
        {
            await _usersCollection.ReplaceOneAsync(u => u.ID == id, updatedUser);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _usersCollection.DeleteOneAsync(u => u.ID == id);
        }
    }
}
