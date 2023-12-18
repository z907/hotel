using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;

namespace Model.Repositories;

public class DbReposPgs:IDbRepos
{
    private HotelDbContext db;
    private ReservationRepositoryPgs reservationRepository;
    private ReservationStatusRepositoryPgs reservationStatusRepository;
    private PaymentRepositoryPgs paymentRepository;
    private UserRepositoryPgs userRepository;
    private UserRoleRepositoryPgs userRoleRepository;
    private RoomRepositoryPgs roomRepository;
    private RoomAttributeRepositoryPgs roomAttributeRepository;
    private RoomCapacityRepositoryPgs roomCapacityRepository;
    private RoomQualityRepositoryPgs roomQuality;
    private RoomViewTypeRepositoryPgs roomViewTypeRepository;
    private CustomerRepositoryPgs customerRepository;
    private AdditionalServiceRepositoryPgs additionalServiceRepository;
    private ResAddServiceRepositoryPgs resAddServiceRepository;
    //load all tables
    public DbReposPgs()
    {
        db = new HotelDbContext();
        db.Reservations.Load();
        db.ReservationStatuses.Load();
        db.ResAddServices.Load();
        db.RoomAttributes.Load();
        db.RoomCapacities.Load();
        db.RoomQualities.Load();
        db.RoomViewTypes.Load();
        db.AdditionalServices.Load();
        db.Customers.Load();
        db.Rooms.Load();
        db.Payments.Load();
        db.Users.Load();
        db.UserRoles.Load();
    }
    //LOAD ALL TABLES
    public IRepository<Reservation> Reservations
    {
        get
        {
            if (reservationRepository == null)
                reservationRepository = new ReservationRepositoryPgs(db);
            return reservationRepository;
        }
    }
    public IRepository<ReservationStatus> ReservationStatuses
    {
        get
        {
            if (reservationStatusRepository == null)
                reservationStatusRepository = new ReservationStatusRepositoryPgs(db);
            return reservationStatusRepository;
        }
    }
    public IRepository<AdditionalService> AdditionalServices
    {
        get
        {
            if (additionalServiceRepository == null)
                additionalServiceRepository = new AdditionalServiceRepositoryPgs(db);
            return additionalServiceRepository;
        }
    }
    public IRepository<Customer> Customers
    {
        get
        {
            if (customerRepository == null)
                customerRepository = new CustomerRepositoryPgs(db);
            return customerRepository;
        }
    }
}