using ERP.Data;
using ERP.Data.Entities.Restaurant;
using ERP.Services.RestaurantServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Services.RestaurantServices
{
    public class PaymentDetailService:IpaymentDetailsService
    {
        private readonly ERPDbContext _context;
        public PaymentDetailService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDetails>> GetAllPaymentDetails()
        {
            var result = _context.PaymentDetails.AsNoTracking().ToListAsync();
            return await result;
        }

        public async Task<PaymentDetails> GetPaymentDetailsById(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        public async Task<bool> SavePaymentDetails(PaymentDetails paymentDetails)
        {
            try
            {
                if (paymentDetails.Id != 0)
                {
                    //_context.PaymentDetails.Update(paymentDetails);
                    _context.Entry(paymentDetails).State = EntityState.Modified;
                }
                else
                {
                    paymentDetails.Id = 0;
                    _context.PaymentDetails.Add(paymentDetails);
                }
                   

                return 1 == await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePaymentDetailsById(int id)
        {
            _context.PaymentDetails.Remove(_context.PaymentDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public bool ExistPayment(int id)
        {
            return _context.PaymentDetails.Any(e=>e.Id==id);
        }
    }
}
