using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebStore.DAL.EF;
using WebStore.Model;
using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;

namespace WebStore.Services.ConcreteServices
{
    public class OrderService : BaseService, IOrderService
    {
        public OrderService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public OrderVm AddOrUpdateOrder(AddOrUpdateOrderVm AddOrUpdateOrderVm)
        {
            try
            {
                if (AddOrUpdateOrderVm == null)
                    throw new ArgumentNullException("View model parameter is null");
                var orderEntity = Mapper.Map<Order>(AddOrUpdateOrderVm);
                if (AddOrUpdateOrderVm.Id.HasValue || AddOrUpdateOrderVm.Id == 0)
                    DbContext.Orders.Update(orderEntity);
                else
                    DbContext.Orders.Add(orderEntity);
                DbContext.SaveChanges();
                var orderVm = Mapper.Map<OrderVm>(orderEntity);
                return orderVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public bool DeleteOrder(Expression<Func<Order,bool>> filterExpression)
        {
            try{
                if(filterExpression==null)
                {
                    throw new ArgumentNullException("Null");
                }
                var extractedProduct= DbContext.Orders.Where(filterExpression).FirstOrDefault();
                if(extractedProduct!=null)
                {
                    DbContext.Remove(extractedProduct);
                    DbContext.SaveChanges();
                    return true;
                }
                return false;
                
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

    public OrderVm GetOrder(Expression<Func<Order, bool>> filterExpression)
    {
        try
        {
            if (filterExpression == null)
                throw new ArgumentNullException("Filter expression parameter is null");
            var orderEntity = DbContext.Orders.FirstOrDefault(filterExpression);
            var orderVm = Mapper.Map<OrderVm>(orderEntity);
            return orderVm;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public IEnumerable<OrderVm> GetOrders(Expression<Func<Order, bool>>? filterExpression = null)
    {
        try
        {
            var ordersQuery = DbContext.Orders.AsQueryable();
            if (filterExpression != null)
                ordersQuery = ordersQuery.Where(filterExpression);
            var orderVms = Mapper.Map<IEnumerable<OrderVm>>(ordersQuery);
            return orderVms;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
}