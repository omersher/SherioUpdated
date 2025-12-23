// FILE: UserDB.cs
using Model;
using System.Data.OleDb;

namespace ViewModel
{
    public class UserDB : BaseDB
    {
        public UserList SelectAll()
        {
            command.CommandText = "SELECT * FROM Users";
            return new UserList(base.Select());
        }

        public static User SelectById(int id)
        {
            UserDB db = new UserDB();
            db.command.CommandText = "SELECT * FROM Users WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            UserList list = new UserList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            User u = entity as User ?? new User();

            u.FullName = reader["FullName"].ToString();
            u.GuestID = reader["GuestID"].ToString();
            u.Email = reader["Email"].ToString();
            u.Phone = reader["Phone"].ToString();
            u.PassHash = reader["PassHash"].ToString();

            base.CreateModel(u);
            return u;
        }

        public override BaseEntity NewEntity() => new User();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not User u) return;
            cmd.CommandText = "DELETE FROM Users WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", u.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not User u) return;

            cmd.CommandText =
                "INSERT INTO Users (FullName, GuestID, Email, Phone, PassHash) " +
                "VALUES (?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@name", u.FullName));
            cmd.Parameters.Add(new OleDbParameter("@guest", u.GuestID));
            cmd.Parameters.Add(new OleDbParameter("@mail", u.Email));
            cmd.Parameters.Add(new OleDbParameter("@phone", u.Phone));
            cmd.Parameters.Add(new OleDbParameter("@pass", u.PassHash));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not User u) return;

            cmd.CommandText =
                "UPDATE Users SET FullName=?, GuestID=?, Email=?, Phone=?, PassHash=? WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@name", u.FullName));
            cmd.Parameters.Add(new OleDbParameter("@guest", u.GuestID));
            cmd.Parameters.Add(new OleDbParameter("@mail", u.Email));
            cmd.Parameters.Add(new OleDbParameter("@phone", u.Phone));
            cmd.Parameters.Add(new OleDbParameter("@pass", u.PassHash));
            cmd.Parameters.Add(new OleDbParameter("@id", u.Id));
        }
    }
}
