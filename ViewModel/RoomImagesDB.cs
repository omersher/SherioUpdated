// FILE: RoomImagesDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class RoomImagesDB : BaseDB
    {
        public RoomImagesList SelectAll()
        {
            command.CommandText = "SELECT * FROM RoomImages";
            return new RoomImagesList(base.Select());
        }

        public static RoomImage SelectById(int id)
        {
            RoomImagesDB db = new RoomImagesDB();
            db.command.CommandText = "SELECT * FROM RoomImages WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            RoomImagesList list = new RoomImagesList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            RoomImage ri = entity as RoomImage ?? new RoomImage();

            if (reader["RoomID"] != DBNull.Value) ri.Room = RoomDB.SelectById(Convert.ToInt32(reader["RoomID"]));
            ri.ImageLink = reader["ImageLink"].ToString();

            base.CreateModel(ri);
            return ri;
        }

        public override BaseEntity NewEntity() => new RoomImage();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomImage ri) return;
            cmd.CommandText = "DELETE FROM RoomImages WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", ri.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomImage ri) return;

            cmd.CommandText = "INSERT INTO RoomImages (RoomID, ImageLink) VALUES (?,?)";
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(ri.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@img", ri.ImageLink));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomImage ri) return;

            cmd.CommandText = "UPDATE RoomImages SET RoomID=?, ImageLink=? WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(ri.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@img", ri.ImageLink));
            cmd.Parameters.Add(new OleDbParameter("@id", ri.Id));
        }
    }
}
