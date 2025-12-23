// FILE: RoomDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class RoomDB : BaseDB
    {
        public RoomList SelectAll()
        {
            command.CommandText = "SELECT * FROM Rooms";
            return new RoomList(base.Select());
        }

        public static Room SelectById(int id)
        {
            RoomDB db = new RoomDB();
            db.command.CommandText = "SELECT * FROM Rooms WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            RoomList list = new RoomList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Room r = entity as Room ?? new Room();

            r.RoomName = reader["RoomName"].ToString();

            if (reader["HotelID"] != DBNull.Value) r.Hotel = HotelsDB.SelectById(Convert.ToInt32(reader["HotelID"]));
            if (reader["AdultRate"] != DBNull.Value) r.AdultRate = Convert.ToInt32(reader["AdultRate"]);
            if (reader["ChildRate"] != DBNull.Value) r.ChildRate = Convert.ToInt32(reader["ChildRate"]);
            if (reader["Bedrooms"] != DBNull.Value) r.Bedrooms = Convert.ToInt32(reader["Bedrooms"]);
            if (reader["Bathrooms"] != DBNull.Value) r.Bathrooms = Convert.ToInt32(reader["Bathrooms"]);

            r.HasKitchen = reader["HasKitchen"] != DBNull.Value && Convert.ToBoolean(reader["HasKitchen"]);
            r.HasParking = reader["HasParking"] != DBNull.Value && Convert.ToBoolean(reader["HasParking"]);
            r.HasBalcony = reader["HasBalcony"] != DBNull.Value && Convert.ToBoolean(reader["HasBalcony"]);
            r.HasLivingRoom = reader["HasLivingRoom"] != DBNull.Value && Convert.ToBoolean(reader["HasLivingRoom"]);

            base.CreateModel(r);
            return r;
        }

        public override BaseEntity NewEntity() => new Room();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Room r) return;
            cmd.CommandText = "DELETE FROM Rooms WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", r.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Room r) return;

            cmd.CommandText =
                "INSERT INTO Rooms (HotelID, RoomName, AdultRate, ChildRate, Bedrooms, Bathrooms, HasKitchen, HasParking, HasBalcony, HasLivingRoom) " +
                "VALUES (?,?,?,?,?,?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@hotelId", DbVal(r.Hotel?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@name", r.RoomName));
            cmd.Parameters.Add(new OleDbParameter("@aRate", r.AdultRate));
            cmd.Parameters.Add(new OleDbParameter("@cRate", r.ChildRate));
            cmd.Parameters.Add(new OleDbParameter("@beds", r.Bedrooms));
            cmd.Parameters.Add(new OleDbParameter("@baths", r.Bathrooms));
            cmd.Parameters.Add(new OleDbParameter("@kitchen", r.HasKitchen));
            cmd.Parameters.Add(new OleDbParameter("@parking", r.HasParking));
            cmd.Parameters.Add(new OleDbParameter("@balcony", r.HasBalcony));
            cmd.Parameters.Add(new OleDbParameter("@living", r.HasLivingRoom));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Room r) return;

            cmd.CommandText =
                "UPDATE Rooms SET HotelID=?, RoomName=?, AdultRate=?, ChildRate=?, Bedrooms=?, Bathrooms=?, HasKitchen=?, HasParking=?, HasBalcony=?, HasLivingRoom=? " +
                "WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@hotelId", DbVal(r.Hotel?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@name", r.RoomName));
            cmd.Parameters.Add(new OleDbParameter("@aRate", r.AdultRate));
            cmd.Parameters.Add(new OleDbParameter("@cRate", r.ChildRate));
            cmd.Parameters.Add(new OleDbParameter("@beds", r.Bedrooms));
            cmd.Parameters.Add(new OleDbParameter("@baths", r.Bathrooms));
            cmd.Parameters.Add(new OleDbParameter("@kitchen", r.HasKitchen));
            cmd.Parameters.Add(new OleDbParameter("@parking", r.HasParking));
            cmd.Parameters.Add(new OleDbParameter("@balcony", r.HasBalcony));
            cmd.Parameters.Add(new OleDbParameter("@living", r.HasLivingRoom));
            cmd.Parameters.Add(new OleDbParameter("@id", r.Id));
        }
    }
}
