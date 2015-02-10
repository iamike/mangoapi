using System;
using System.Linq;
using mangoeasy.weixin.Models;

namespace mangoeasy.weixin.Services
{
    
    public class CustomerService : BaseService
    {
        public CustomerService(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(Customer customer)
        {
            this.DbContext.Customers.Add(customer);
            this.DbContext.SaveChanges();
        }
        public Customer GetCustomer(Guid id)
        {
            return DbContext.Customers.FirstOrDefault(n=>n.Id==id);
        }
        public Customer GetCustomer(Guid id,string token)
        {
            return DbContext.Customers.FirstOrDefault(n => n.Id == id && n.Token == token);
        }
    }
}