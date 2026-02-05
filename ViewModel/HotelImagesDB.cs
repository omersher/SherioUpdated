using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class HotelImagesDB : BaseDB
    {
        public HotelImagesList SelectAll()
        {
            command.CommandText = "SELECT * FROM HotelImages";
            return new HotelImagesList(base.Select());
        }
        public HotelImagesList SelectByHotelId(int hotelId)
        {
            command.CommandText = "SELECT * FROM HotelImages WHERE HotelID=?";
            command.Parameters.Clear();
            command.Parameters.Add(new OleDbParameter("@hid", hotelId));
            return new HotelImagesList(base.Select());
        }

        public static HotelImage? SelectById(int id)
        {
            HotelImagesDB db = new HotelImagesDB();
            db.command.CommandText = "SELECT * FROM HotelImages WHERE ID=?";
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            HotelImagesList list = new HotelImagesList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            HotelImage hi = entity as HotelImage ?? new HotelImage();

            if (reader["HotelID"] != DBNull.Value)
                hi.Hotel = HotelsDB.SelectById(Convert.ToInt32(reader["HotelID"]));

            hi.ImageLink = reader["HotelImageLink"].ToString() ?? "";

            base.CreateModel(hi);
            return hi;
        }

        public override BaseEntity NewEntity() => new HotelImage();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not HotelImage hi) return;
            cmd.CommandText = "DELETE FROM HotelImages WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", hi.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not HotelImage hi) return;

            cmd.CommandText =
                "INSERT INTO HotelImages (HotelID, HotelImageLink) VALUES (?,?)";

            cmd.Parameters.Add(new OleDbParameter("@hotelId", DbVal(hi.Hotel?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@img", hi.ImageLink));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not HotelImage hi) return;

            cmd.CommandText =
                "UPDATE HotelImages SET HotelID=?, HotelImageLink=? WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@hotelId", DbVal(hi.Hotel?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@img", hi.ImageLink));
            cmd.Parameters.Add(new OleDbParameter("@id", hi.Id));
        }
    }
}
