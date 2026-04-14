// FILE: HotelsDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class HotelsDB : BaseDB
    {
        // ---------- SELECT ----------

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

        public HotelList SelectByOwnerId(int ownerId)
        {
            command.CommandText = "SELECT * FROM Hotels WHERE OwnerID=?";
            command.Parameters.Clear();
            command.Parameters.Add(new OleDbParameter("@ownerId", ownerId));
            return new HotelList(base.Select());
        }

        // ---------- CREATE MODEL ----------

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Hotel h = entity as Hotel ?? new Hotel();

            h.Name = reader["Name"].ToString();
            h.PhoneNumber = reader["PhoneNumber"].ToString();
            h.Email = reader["Email"].ToString();
            h.WebSite = reader["WebSite"].ToString();
            h.StreetAddress = reader["StreetAddress"].ToString();

            h.MainHotelImageLink =
                reader["MainHotelImageLink"] != DBNull.Value
                    ? reader["MainHotelImageLink"].ToString()
                    : "";

            if (reader["OwnerID"] != DBNull.Value)
                h.Owner = OwnerDB.SelectById(Convert.ToInt32(reader["OwnerID"]));

            if (reader["CityID"] != DBNull.Value)
                h.City = CityDB.SelectById(Convert.ToInt32(reader["CityID"]));

            if (reader["StarRating"] != DBNull.Value)
                h.StarRating = Convert.ToInt32(reader["StarRating"]);

            h.HasPool = Convert.ToBoolean(reader["HasPool"]);
            h.HasGym = Convert.ToBoolean(reader["HasGym"]);
            h.HasRestaurant = Convert.ToBoolean(reader["HasRestaurant"]);

            base.CreateModel(h);
            return h;
        }

        public override BaseEntity NewEntity() => new Hotel();

        // ---------- DELETE ----------

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;
            cmd.CommandText = "DELETE FROM Hotels WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", h.Id));
        }

        // ---------- INSERT ----------

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;

            cmd.CommandText =
                "INSERT INTO Hotels " +
                "(Name, PhoneNumber, Email, WebSite, OwnerID, CityID, StreetAddress, StarRating, HasPool, HasGym, HasRestaurant, MainHotelImageLink) " +
                "VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@name", h.Name));
            cmd.Parameters.Add(new OleDbParameter("@phone", h.PhoneNumber));
            cmd.Parameters.Add(new OleDbParameter("@mail", h.Email));
            cmd.Parameters.Add(new OleDbParameter("@site", h.WebSite));
            cmd.Parameters.Add(new OleDbParameter("@owner", h.Owner.Id));
            cmd.Parameters.Add(new OleDbParameter("@city", h.City.Id));
            cmd.Parameters.Add(new OleDbParameter("@addr", h.StreetAddress));
            cmd.Parameters.Add(new OleDbParameter("@stars", h.StarRating));
            cmd.Parameters.Add(new OleDbParameter("@pool", h.HasPool));
            cmd.Parameters.Add(new OleDbParameter("@gym", h.HasGym));
            cmd.Parameters.Add(new OleDbParameter("@rest", h.HasRestaurant));
            cmd.Parameters.Add(new OleDbParameter("@img", h.MainHotelImageLink));
        }

        // ---------- UPDATE ----------
        // ❗ CityID ו-OwnerID מתעדכנים רק אם קיימים

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Hotel h) return;

            bool hasOwner = h.Owner != null && h.Owner.Id > 0;
            bool hasCity = h.City != null && h.City.Id > 0;

            if (hasOwner && hasCity)
            {
                cmd.CommandText =
                    "UPDATE Hotels SET " +
                    "Name=?, PhoneNumber=?, Email=?, WebSite=?, OwnerID=?, CityID=?, StreetAddress=?, StarRating=?, HasPool=?, HasGym=?, HasRestaurant=?, MainHotelImageLink=? " +
                    "WHERE ID=?";

                cmd.Parameters.Add(new OleDbParameter("@name", h.Name));
                cmd.Parameters.Add(new OleDbParameter("@phone", h.PhoneNumber));
                cmd.Parameters.Add(new OleDbParameter("@mail", h.Email));
                cmd.Parameters.Add(new OleDbParameter("@site", h.WebSite));
                cmd.Parameters.Add(new OleDbParameter("@owner", h.Owner.Id));
                cmd.Parameters.Add(new OleDbParameter("@city", h.City.Id));
            }
            else
            {
                cmd.CommandText =
                    "UPDATE Hotels SET " +
                    "Name=?, PhoneNumber=?, Email=?, WebSite=?, StreetAddress=?, StarRating=?, HasPool=?, HasGym=?, HasRestaurant=?, MainHotelImageLink=? " +
                    "WHERE ID=?";

                cmd.Parameters.Add(new OleDbParameter("@name", h.Name));
                cmd.Parameters.Add(new OleDbParameter("@phone", h.PhoneNumber));
                cmd.Parameters.Add(new OleDbParameter("@mail", h.Email));
                cmd.Parameters.Add(new OleDbParameter("@site", h.WebSite));
            }

            cmd.Parameters.Add(new OleDbParameter("@addr", h.StreetAddress));
            cmd.Parameters.Add(new OleDbParameter("@stars", h.StarRating));
            cmd.Parameters.Add(new OleDbParameter("@pool", h.HasPool));
            cmd.Parameters.Add(new OleDbParameter("@gym", h.HasGym));
            cmd.Parameters.Add(new OleDbParameter("@rest", h.HasRestaurant));

            // ✅ זה מה שהיה חסר
            cmd.Parameters.Add(new OleDbParameter("@img", h.MainHotelImageLink));

            cmd.Parameters.Add(new OleDbParameter("@id", h.Id));
        }
    }
}
