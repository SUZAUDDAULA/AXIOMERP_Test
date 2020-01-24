using ERP.Data.Entities.Restaurant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Services.RestaurantServices.Interfaces
{
    public interface IpaymentDetailsService
    {
        Task<bool> SavePaymentDetails(PaymentDetails paymentDetails);
        Task<PaymentDetails> GetPaymentDetailsById(int id);
        Task<IEnumerable<PaymentDetails>> GetAllPaymentDetails();
        Task<bool> DeletePaymentDetailsById(int id);

        bool ExistPayment(int id);
    }
}
