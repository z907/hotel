using Model.Entities;

namespace ViewModel.Services;

public class CustomerService:BaseService
{
    public CustomerService() : base()
    {
    }

    public Customer GetCustomerById(int id)
    {
        return db.Customers.Find(id);
    }
    public List<Customer> GetCustomerList()
    {
        return db.Customers.ToList();
    }

    public void AddCustomer(Customer item)
    {
        if(!db.Customers.ToList().Exists(c => c.Name == item.Name && c.Surname==item.Surname 
           && c.LastName==item.LastName && c.PhoneNumber==item.PhoneNumber && c.Email==item.Email))
        { 
        db.Customers.Add(item);
        db.SaveChanges();
        }
    }

    public int? GetId(Customer item)
    {
        return db.Customers.ToList().First(c => c.Name == item.Name && c.Surname==item.Surname 
         && c.LastName==item.LastName && c.PhoneNumber==item.PhoneNumber && c.Email==item.Email).Id;
    }
}