using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class CityDB : BaseDB
    {
        // -------- SELECT --------
        public CityList SelectAll()
        {
            // נשארים עם אותה טבלה לכל הפעולות: City
            command.CommandText = "SELECT * FROM City";
            return new CityList(base.Select());
        }

        public static City SelectById(int id)
        {
            CityDB db = new CityDB();
            CityList list = db.SelectAll();
            return list.Find(item => item.Id == id);
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            City c = entity as City ?? new City();
            c.CityName = reader["CityName"].ToString();
            base.CreateModel(c); // ממלא Id
            return c;
        }

        public override BaseEntity NewEntity() => new City();

        // -------- DELETE --------
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not City c) return;

            cmd.CommandText = "DELETE FROM City WHERE ID=@id";
            cmd.Parameters.Add(new OleDbParameter("@id", c.Id));
        }

        // -------- INSERT --------
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not City c) return;

            cmd.CommandText = "INSERT INTO City (CityName) VALUES (@cName)";
            cmd.Parameters.Add(new OleDbParameter("@cName", c.CityName));
        }

        // -------- UPDATE --------
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not City c) return;

            // חשוב: אותה טבלה כמו ב-SELECT
            cmd.CommandText = "UPDATE City SET CityName=@cName WHERE ID=@id";
            cmd.Parameters.Add(new OleDbParameter("@cName", c.CityName));
            cmd.Parameters.Add(new OleDbParameter("@id", c.Id));
        }
    }
}
