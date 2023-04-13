using Minesweeper_GUI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Milestone_CST350.Models
{
    public class MongoUser
    {

        public MongoUser()
        {
        }

        public MongoUser(string firstName, string lastName, string sex, int age, string state, string email, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Sex = sex;
            Age = age;
            State = state;
            Email = email;
            Username = username;
            Password = password;
            Boards = new List<GridModel>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<GridModel> Boards { get; set; }



    }
}
