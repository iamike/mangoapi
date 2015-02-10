using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mangoeasy.weixin.Services
{
    public class BaseService
    {
        public readonly ApplicationDbContext DbContext;

        public BaseService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}