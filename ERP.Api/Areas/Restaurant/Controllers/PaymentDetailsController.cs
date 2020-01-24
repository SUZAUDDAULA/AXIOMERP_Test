using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Data.Entities.Restaurant;
using ERP.Services.RestaurantServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Api.Areas.Restaurant.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IpaymentDetailsService ipaymentDetailsService;

        public PaymentDetailsController(IpaymentDetailsService ipaymentDetailsService)
        {
            this.ipaymentDetailsService = ipaymentDetailsService;
        }
        // Get: api/PaymentDetails/GetPaymentDetails
        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetPaymentDetails()
        {
            return await ipaymentDetailsService.GetAllPaymentDetails();
        }

        // Get: api/PaymentDetails/GetPaymentDetailsById/1
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetailsById(int id)
        {
            var paymentDetail=await ipaymentDetailsService.GetPaymentDetailsById(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            return paymentDetail;
        }

        // Put: api/PaymentDetails/UpdatePaymentDetail/1
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaymentDetail(int id,PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.Id)
            {
                return BadRequest();
            }

            await ipaymentDetailsService.SavePaymentDetails(paymentDetails);

            return NoContent();
        }

        // POST: api/PaymentDetails/SavePaymentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetails>>SavePaymentDetail(PaymentDetails paymentDetails)
        {
           var paymentId= await ipaymentDetailsService.SavePaymentDetails(paymentDetails);

            return CreatedAtAction("GetPaymentDetailsById",new { id=paymentId},paymentDetails);
        }

        // Delete: api/PaymentDetails/DeletePaymentDetail/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetails>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await ipaymentDetailsService.GetPaymentDetailsById(id);

            await ipaymentDetailsService.DeletePaymentDetailsById(id);

            return paymentDetail;
        }

    }
}