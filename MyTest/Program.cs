using System;
using System.Linq;
using Model;
using ViewModel;

#region HELPERS (Title + Show* functions)
static void Title(string t)
{
    Console.WriteLine("\n========================================");
    Console.WriteLine(t);
    Console.WriteLine("========================================");
}

static void ShowCities(string title)
{
    var db = new CityDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var c in list) Console.WriteLine($"{c.Id} | {c.CityName}");
}

static void ShowUsers(string title)
{
    var db = new UserDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var u in list) Console.WriteLine($"{u.Id} | {u.FullName} | {u.Email} | {u.GuestID}");
}

static void ShowOwners(string title)
{
    var db = new OwnerDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var o in list) Console.WriteLine($"{o.Id} | {o.FullName} | {o.GuestID} | Active={o.IsActive}");
}

static void ShowHotels(string title)
{
    var db = new HotelsDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var h in list) Console.WriteLine($"{h.Id} | {h.Name} | City={h.City?.CityName} | OwnerId={h.Owner?.Id}");
}

static void ShowRooms(string title)
{
    var db = new RoomDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var r in list) Console.WriteLine($"{r.Id} | {r.RoomName} | Hotel={r.Hotel?.Name}");
}

static void ShowRoomImages(string title)
{
    var db = new RoomImagesDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var ri in list) Console.WriteLine($"{ri.Id} | Room={ri.Room?.RoomName} | {ri.ImageLink}");
}

static void ShowRoomAvailability(string title)
{
    var db = new RoomAvailabilityDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var ra in list) Console.WriteLine($"{ra.Id} | Room={ra.Room?.RoomName} | {ra.StartDate:d} - {ra.EndDate:d}");
}

static void ShowBookings(string title)
{
    var db = new BookingDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var b in list) Console.WriteLine($"{b.Id} | User={b.User?.FullName} | Room={b.Room?.RoomName} | {b.StartDate:d}-{b.EndDate:d} | {b.Status}");
}

static void ShowPayments(string title)
{
    var db = new PaymentDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var p in list) Console.WriteLine($"{p.Id} | {p.PayMethod} | Amount={p.Amount} | User={p.User?.FullName} | BookingId={p.Booking?.Id}");
}

static void ShowReviews(string title)
{
    var db = new ReviewDB();
    var list = db.SelectAll();
    Console.WriteLine($"\n=== {title} ===");
    foreach (var r in list) Console.WriteLine($"{r.Id} | {r.Rating}★ | {r.Comment} | User={r.User?.FullName} | Room={r.Room?.RoomName}");
}
#endregion

#region BOOTSTRAP (minimal dependencies)
Title("Bootstrap minimal dependencies (if missing)");

var cdb = new CityDB();
var odb = new OwnerDB();
var udb = new UserDB();
var hdb = new HotelsDB();
var rdb = new RoomDB();
var bdb = new BookingDB();

City ensureCity = cdb.SelectAll().FirstOrDefault();
if (ensureCity == null)
{
    ensureCity = new City { CityName = "Bootstrap City" };
    cdb.Insert(ensureCity);
    cdb.SaveChanges();
    Console.WriteLine($"[Bootstrap] City -> {ensureCity.Id}");
}

Owner ensureOwner = odb.SelectAll().FirstOrDefault();
if (ensureOwner == null)
{
    ensureOwner = new Owner { FullName = "Bootstrap Owner", GuestID = "111111111", Email = "owner@ex.com", Phone = "050-0000000", PassHash = "X", IsActive = true };
    odb.Insert(ensureOwner);
    odb.SaveChanges();
    Console.WriteLine($"[Bootstrap] Owner -> {ensureOwner.Id}");
}

User ensureUser = udb.SelectAll().FirstOrDefault();
if (ensureUser == null)
{
    ensureUser = new User { FullName = "Bootstrap User", GuestID = "222222222", Email = "user@ex.com", Phone = "050-1111111", PassHash = "Y" };
    udb.Insert(ensureUser);
    udb.SaveChanges();
    Console.WriteLine($"[Bootstrap] User -> {ensureUser.Id}");
}

Hotel ensureHotel = hdb.SelectAll().FirstOrDefault();
if (ensureHotel == null)
{
    ensureHotel = new Hotel
    {
        Name = "Bootstrap Hotel",
        PhoneNumber = "03-5555555",
        Email = "h@ex.com",
        Owner = ensureOwner,
        City = ensureCity,
        StreetAddress = "Main 1",
        StarRating = 3,
        HasPool = false,
        HasGym = false,
        HasRestaurant = true
    };
    hdb.Insert(ensureHotel);
    hdb.SaveChanges();
    Console.WriteLine($"[Bootstrap] Hotel -> {ensureHotel.Id}");
}

Room ensureRoom = rdb.SelectAll().FirstOrDefault();
if (ensureRoom == null)
{
    ensureRoom = new Room
    {
        Hotel = ensureHotel,
        RoomName = "Bootstrap Room",
        AdultRate = 100,
        ChildRate = 50,
        Bedrooms = 1,
        Bathrooms = 1,
        HasKitchen = false,
        HasParking = false,
        HasBalcony = false,
        HasLivingRoom = true
    };
    rdb.Insert(ensureRoom);
    rdb.SaveChanges();
    Console.WriteLine($"[Bootstrap] Room -> {ensureRoom.Id}");
}

Booking ensureBooking = bdb.SelectAll().FirstOrDefault();
if (ensureBooking == null)
{
    ensureBooking = new Booking
    {
        User = ensureUser,
        Room = ensureRoom,
        CreatedAt = DateTime.Now,
        StartDate = DateTime.Today.AddDays(2),
        EndDate = DateTime.Today.AddDays(4),
        AdultCount = 2,
        ChildCount = 0,
        Status = "Pending"
    };
    bdb.Insert(ensureBooking);
    bdb.SaveChanges();
    Console.WriteLine($"[Bootstrap] Booking -> {ensureBooking.Id}");
}
#endregion

#region CITY (SHOW / INSERT / UPDATE / DELETE)
Title("CITY – SHOW / INSERT / UPDATE / DELETE");
bool DO_SHOW = true, DO_INSERT = true, DO_UPDATE = true, DO_DELETE = true;

int rows;

if (DO_SHOW) ShowCities("Cities (Before)");

City cityInserted = null;
if (DO_INSERT)
{
    cityInserted = new City { CityName = "Nes Ziona" };
    cdb.Insert(cityInserted);
    rows = cdb.SaveChanges();
    Console.WriteLine($"\n[City INSERT] rows={rows} | new ID={cityInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureCity.Id;  // שנה למה שתרצה
    string newName = "Jerusalem";
    City x = cdb.SelectAll().Find(t => t.Id == idToUpdate);
    if (x != null)
    {
        x.CityName = newName;
        cdb.Update(x);
        rows = cdb.SaveChanges();
        Console.WriteLine($"[City UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[City UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = cityInserted?.Id ?? -1; // מחק את מה שהכנסת עכשיו (או שנה)
    City x = cdb.SelectAll().Find(t => t.Id == idToDelete);
    if (x != null)
    {
        cdb.Delete(x);
        rows = cdb.SaveChanges();
        Console.WriteLine($"[City DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[City DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowCities("Cities (After)");
#endregion

#region USERS (SHOW / INSERT / UPDATE / DELETE)
Title("USERS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowUsers("Users (Before)");

User userInserted = null;
if (DO_INSERT)
{
    userInserted = new User { FullName = "Demo User", GuestID = "333333333", Email = "demo@ex.com", Phone = "050-2222222", PassHash = "Z" };
    udb.Insert(userInserted);
    rows = udb.SaveChanges();
    Console.WriteLine($"\n[User INSERT] rows={rows} | new ID={userInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureUser.Id;
    User u = UserDB.SelectById(idToUpdate);
    if (u != null)
    {
        u.FullName = "User Updated";
        udb.Update(u);
        rows = udb.SaveChanges();
        Console.WriteLine($"[User UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[User UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = userInserted?.Id ?? -1;
    User u = UserDB.SelectById(idToDelete);
    if (u != null)
    {
        udb.Delete(u);
        rows = udb.SaveChanges();
        Console.WriteLine($"[User DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[User DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowUsers("Users (After)");
#endregion

#region OWNERS (SHOW / INSERT / UPDATE / DELETE)
Title("OWNERS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowOwners("Owners (Before)");

Owner ownerInserted = null;
if (DO_INSERT)
{
    ownerInserted = new Owner { FullName = "Owner Demo", GuestID = "444444444", Email = "own@ex.com", Phone = "050-3333333", PassHash = "OWN", IsActive = true };
    odb.Insert(ownerInserted);
    rows = odb.SaveChanges();
    Console.WriteLine($"\n[Owner INSERT] rows={rows} | new ID={ownerInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureOwner.Id;
    Owner o = OwnerDB.SelectById(idToUpdate);
    if (o != null)
    {
        o.IsActive = !o.IsActive;
        o.FullName = "Owner Updated";
        odb.Update(o);
        rows = odb.SaveChanges();
        Console.WriteLine($"[Owner UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[Owner UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = ownerInserted?.Id ?? -1;
    Owner o = OwnerDB.SelectById(idToDelete);
    if (o != null)
    {
        odb.Delete(o);
        rows = odb.SaveChanges();
        Console.WriteLine($"[Owner DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[Owner DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowOwners("Owners (After)");
#endregion

#region HOTELS (SHOW / INSERT / UPDATE / DELETE)
Title("HOTELS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowHotels("Hotels (Before)");

Hotel hotelInserted = null;
if (DO_INSERT)
{
    hotelInserted = new Hotel
    {
        Name = "Hotel Demo",
        PhoneNumber = "03-7777777",
        Email = "hd@ex.com",
        WebSite = "",
        Owner = ensureOwner,
        City = ensureCity,
        StreetAddress = "Demo 5",
        StarRating = 4,
        HasPool = false,
        HasGym = false,
        HasRestaurant = true
    };
    hdb.Insert(hotelInserted);
    rows = hdb.SaveChanges();
    Console.WriteLine($"\n[Hotel INSERT] rows={rows} | new ID={hotelInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureHotel.Id;
    Hotel h = HotelsDB.SelectById(idToUpdate);
    if (h != null)
    {
        h.Name = "Hotel Updated";
        hdb.Update(h);
        rows = hdb.SaveChanges();
        Console.WriteLine($"[Hotel UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[Hotel UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = hotelInserted?.Id ?? -1;
    Hotel h = HotelsDB.SelectById(idToDelete);
    if (h != null)
    {
        hdb.Delete(h);
        rows = hdb.SaveChanges();
        Console.WriteLine($"[Hotel DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[Hotel DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowHotels("Hotels (After)");
#endregion

#region ROOMS (SHOW / INSERT / UPDATE / DELETE)
Title("ROOMS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowRooms("Rooms (Before)");

Room roomInserted = null;
if (DO_INSERT)
{
    roomInserted = new Room
    {
        Hotel = ensureHotel,
        RoomName = "Room Demo",
        AdultRate = 250,
        ChildRate = 100,
        Bedrooms = 1,
        Bathrooms = 1,
        HasKitchen = true,
        HasParking = false,
        HasBalcony = false,
        HasLivingRoom = true
    };
    rdb.Insert(roomInserted);
    rows = rdb.SaveChanges();
    Console.WriteLine($"\n[Room INSERT] rows={rows} | new ID={roomInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureRoom.Id;
    Room r = RoomDB.SelectById(idToUpdate);
    if (r != null)
    {
        r.RoomName = "Room Updated";
        rdb.Update(r);
        rows = rdb.SaveChanges();
        Console.WriteLine($"[Room UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[Room UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = roomInserted?.Id ?? -1;
    Room r = RoomDB.SelectById(idToDelete);
    if (r != null)
    {
        rdb.Delete(r);
        rows = rdb.SaveChanges();
        Console.WriteLine($"[Room DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[Room DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowRooms("Rooms (After)");
#endregion

#region ROOM IMAGES (SHOW / INSERT / UPDATE / DELETE)
Title("ROOM IMAGES – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowRoomImages("RoomImages (Before)");

RoomImage imgInserted = null;
var imgdb = new RoomImagesDB();
if (DO_INSERT)
{
    imgInserted = new RoomImage { Room = ensureRoom, ImageLink = "https://example.com/demo.jpg" };
    imgdb.Insert(imgInserted);
    rows = imgdb.SaveChanges();
    Console.WriteLine($"\n[RoomImage INSERT] rows={rows} | new ID={imgInserted.Id}");
}

if (DO_UPDATE)
{
    var first = imgdb.SelectAll().FirstOrDefault();
    if (first != null)
    {
        first.ImageLink = "https://example.com/updated.jpg";
        imgdb.Update(first);
        rows = imgdb.SaveChanges();
        Console.WriteLine($"[RoomImage UPDATE] rows={rows} | ID={first.Id}");
    }
    else Console.WriteLine("[RoomImage UPDATE] no records");
}

if (DO_DELETE)
{
    int idToDelete = imgInserted?.Id ?? -1;
    var toDel = imgdb.SelectAll().Find(x => x.Id == idToDelete);
    if (toDel != null)
    {
        imgdb.Delete(toDel);
        rows = imgdb.SaveChanges();
        Console.WriteLine($"[RoomImage DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[RoomImage DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowRoomImages("RoomImages (After)");
#endregion

#region ROOM AVAILABILITY (SHOW / INSERT / UPDATE / DELETE)
Title("ROOM AVAILABILITY – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowRoomAvailability("RoomAvailability (Before)");

RoomAvailability availInserted = null;
var radb = new RoomAvailabilityDB();
if (DO_INSERT)
{
    availInserted = new RoomAvailability { Room = ensureRoom, StartDate = DateTime.Today.AddDays(7), EndDate = DateTime.Today.AddDays(9) };
    radb.Insert(availInserted);
    rows = radb.SaveChanges();
    Console.WriteLine($"\n[RoomAvailability INSERT] rows={rows} | new ID={availInserted.Id}");
}

if (DO_UPDATE)
{
    var first = radb.SelectAll().FirstOrDefault();
    if (first != null)
    {
        first.EndDate = first.EndDate.AddDays(1);
        radb.Update(first);
        rows = radb.SaveChanges();
        Console.WriteLine($"[RoomAvailability UPDATE] rows={rows} | ID={first.Id}");
    }
    else Console.WriteLine("[RoomAvailability UPDATE] no records");
}

if (DO_DELETE)
{
    int idToDelete = availInserted?.Id ?? -1;
    var toDel = radb.SelectAll().Find(x => x.Id == idToDelete);
    if (toDel != null)
    {
        radb.Delete(toDel);
        rows = radb.SaveChanges();
        Console.WriteLine($"[RoomAvailability DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[RoomAvailability DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowRoomAvailability("RoomAvailability (After)");
#endregion

#region BOOKINGS (SHOW / INSERT / UPDATE / DELETE)
Title("BOOKINGS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowBookings("Bookings (Before)");

Booking bookingInserted = null;
if (DO_INSERT)
{
    bookingInserted = new Booking
    {
        User = ensureUser,
        Room = ensureRoom,
        CreatedAt = DateTime.Now,
        StartDate = DateTime.Today.AddDays(3),
        EndDate = DateTime.Today.AddDays(5),
        AdultCount = 2,
        ChildCount = 0,
        Status = "Pending"
    };
    bdb.Insert(bookingInserted);
    rows = bdb.SaveChanges();
    Console.WriteLine($"\n[Booking INSERT] rows={rows} | new ID={bookingInserted.Id}");
}

if (DO_UPDATE)
{
    int idToUpdate = ensureBooking.Id;
    Booking b = BookingDB.SelectById(idToUpdate);
    if (b != null)
    {
        b.Status = "Confirmed";
        bdb.Update(b);
        rows = bdb.SaveChanges();
        Console.WriteLine($"[Booking UPDATE] rows={rows} | ID={idToUpdate}");
    }
    else Console.WriteLine($"[Booking UPDATE] ID {idToUpdate} not found");
}

if (DO_DELETE)
{
    int idToDelete = bookingInserted?.Id ?? -1;
    Booking b = BookingDB.SelectById(idToDelete);
    if (b != null)
    {
        bdb.Delete(b);
        rows = bdb.SaveChanges();
        Console.WriteLine($"[Booking DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[Booking DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowBookings("Bookings (After)");
#endregion

#region PAYMENTS (SHOW / INSERT / UPDATE / DELETE)
Title("PAYMENTS – SHOW / INSERT / UPDATE / DELETE");
DO_SHOW = true; DO_INSERT = true; DO_UPDATE = true; DO_DELETE = true;

if (DO_SHOW) ShowPayments("Payments (Before)");

Payment paymentInserted = null;
var pdb = new PaymentDB();
if (DO_INSERT)
{
    paymentInserted = new Payment { User = ensureUser, Booking = ensureBooking, Amount = 123.45m, PayMethod = "CreditCard", CreatedAt = DateTime.Now };
    pdb.Insert(paymentInserted);
    rows = pdb.SaveChanges();
    Console.WriteLine($"\n[Payment INSERT] rows={rows} | new ID={paymentInserted.Id}");
}

if (DO_UPDATE)
{
    var first = pdb.SelectAll().FirstOrDefault();
    if (first != null)
    {
        first.PayMethod = "Cash";
        pdb.Update(first);
        rows = pdb.SaveChanges();
        Console.WriteLine($"[Payment UPDATE] rows={rows} | ID={first.Id}");
    }
    else Console.WriteLine("[Payment UPDATE] no records");
}

if (DO_DELETE)
{
    int idToDelete = paymentInserted?.Id ?? -1;
    var toDel = pdb.SelectAll().Find(x => x.Id == idToDelete);
    if (toDel != null)
    {
        pdb.Delete(toDel);
        rows = pdb.SaveChanges();
        Console.WriteLine($"[Payment DELETE] rows={rows} | ID={idToDelete}");
    }
    else Console.WriteLine($"[Payment DELETE] ID {idToDelete} not found");
}

if (DO_SHOW) ShowPayments("Payments (After)");

Title("Done");
#endregion
