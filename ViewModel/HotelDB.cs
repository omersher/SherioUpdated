// FILE: HotelsDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class HotelsDB : BaseDB
    {
        public HotelList SelectAll()
        {
            command.CommandText = "SELECT * FROM Hotels";
            return new HotelList(base.Select());
        }

        public static Hotel SelectById(int id)
        {
            HotelsDB db = new HotelsDB();
            db.command.CommandText = "SELECT * FROM Hotels WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            HotelList list = new HotelList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Hotel h = entity as Hotel ?? new Hotel();

            h.Name = reader["Name"].ToString();
            h.PhoneNumber = reader["PhoneNumber"].ToString();
            h.Email = reader["Email"].ToString();
            h.WebSite = reader["WebSite"].ToString();
            h.StreetAddress = reader["StreetAddress"].ToString();

            if (reader["OwnerID"] != DBNull.Value) h.Owner = OwnerDB.SelectById(Convert.ToInt32(reader["OwnerID"]));
            if (reader["CityID"] != DBNull.Value) h.City = CityDB.SelectById(Convert.ToInt32(reader["CityID"]));
            if (reader["StarRating"] != DBNull.Value) h.StarRating = Convert.ToInt32(reader["StarRating"]);

            h.HasPool = reader["HasPool"] != DBNull.Value && Convert.ToBoolean(reader["HasPool"]);
            h.HasGym = reader["HasGym"] != DBNull.Value && Convert.ToBoolean(reader["HasGym"]);
            h.HasRestaurant = reader["HasRestaurant"] != DBNull.Value && Convert.ToBoolean(reader["HasRestaurant"]);

            base.CreateModel(h);
            return h;
        }

        public override BaseEntity NewEntity() => new Hotel();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;
            cmd.CommandText = "DELETE FROM Hotels WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", h.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;

            cmd.CommandText =
                "INSERT INTO Hotels (Name, PhoneNumber, Email, WebSite, OwnerID, CityID, StreetAddress, StarRating, HasPool, HasGym, HasRestaurant) " +
                "VALUES (?,?,?,?,?,?,?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@name", h.Name));
            cmd.Parameters.Add(new OleDbParameter("@phone", h.PhoneNumber));
            cmd.Parameters.Add(new OleDbParameter("@mail", h.Email));
            cmd.Parameters.Add(new OleDbParameter("@site", h.WebSite));
            cmd.Parameters.Add(new OleDbParameter("@owner", DbVal(h.Owner?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@city", DbVal(h.City?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@addr", h.StreetAddress));
            cmd.Parameters.Add(new OleDbParameter("@stars", h.StarRating));
            cmd.Parameters.Add(new OleDbParameter("@pool", h.HasPool));
            cmd.Parameters.Add(new OleDbParameter("@gym", h.HasGym));
            cmd.Parameters.Add(new OleDbParameter("@rest", h.HasRestaurant));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;

            cmd.CommandText =
                "UPDATE Hotels SET Name=?, PhoneNumber=?, Email=?, WebSite=?, OwnerID=?, CityID=?, StreetAddress=?, StarRating=?, HasPool=?, HasGym=?, HasRestaurant=? " +
                "WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@name", h.Name));
            cmd.Parameters.Add(new OleDbParameter("@phone", h.PhoneNumber));
            cmd.Parameters.Add(new OleDbParameter("@mail", h.Email));
            cmd.Parameters.Add(new OleDbParameter("@site", h.WebSite));
            cmd.Parameters.Add(new OleDbParameter("@owner", DbVal(h.Owner?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@city", DbVal(h.City?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@addr", h.StreetAddress));
            cmd.Parameters.Add(new OleDbParameter("@stars", h.StarRating));
            cmd.Parameters.Add(new OleDbParameter("@pool", h.HasPool));
            cmd.Parameters.Add(new OleDbParameter("@gym", h.HasGym));
            cmd.Parameters.Add(new OleDbParameter("@rest", h.HasRestaurant));
            cmd.Parameters.Add(new OleDbParameter("@id", h.Id));
        }
    }
}
