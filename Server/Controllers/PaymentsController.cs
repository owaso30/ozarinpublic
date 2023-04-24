using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Ozarin.Server.Controllers
{
	[Route("subscriptioninfo-session")]
	[ApiController]
	public class InfoController : Controller
	{
        public InfoController()
		{
			StripeConfiguration.ApiKey = "***";
		}

        [HttpPost("{page}")]
        public ActionResult CreateCheckoutSession(string page)
        {
			string? email = User.Claims.FirstOrDefault(x => x.Type == "emails")?.Value;
			var cus_list = new CustomerListOptions { };
			var sub_list = new SubscriptionListOptions { };
			var cus_service = new CustomerService();
            var sub_service = new SubscriptionService();
			var cusid = cus_service.ListAutoPaging(cus_list).Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();
            Console.WriteLine(cusid);
            var status = sub_service.ListAutoPaging(sub_list).Where(x => x.CustomerId == cusid).Where(x => x.Items.Data[0].Price.Id == "price_1MxPkSFN75miYBxyH332AUej").Select(x => x.Status).FirstOrDefault();

            if (status != "active")
			{
				var options = new SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						Price = "price_1MxPkSFN75miYBxyH332AUej",
						Quantity = 1,
					},
				},
					// AADB2Cで登録したEmailと同じEmailを強制適用
					CustomerEmail = email,
					Mode = "subscription",
					SuccessUrl = "https://ozarin.net/scoreboard",
					CancelUrl = "https://ozarin.net",
					AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
				};
				var service = new SessionService();
				Session session = service.Create(options);
				return new OkObjectResult(session.Url);
			}
			else
			{
				var pageurl = "https://localhost:7282/" + page;
				return new OkObjectResult(pageurl);
			}
		}
    }

    [Route("create-portal-session")]
	[ApiController]
	public class PortalApiController : Controller
	{
		public PortalApiController()
		{
			StripeConfiguration.ApiKey = "***";
		}

		[HttpPost]
		public ActionResult Create()
		{
            string? email = User.Claims.FirstOrDefault(x => x.Type == "emails")?.Value;
            var cus_list = new CustomerListOptions { };
            var cus_service = new CustomerService();
            var cusid = cus_service.ListAutoPaging(cus_list).Where(x => x.Email == email).Select(x => x.Id).FirstOrDefault();

            var returnUrl = "https://ozarin.net/scoreboard";

			var options = new Stripe.BillingPortal.SessionCreateOptions
			{
				Customer = cusid,
				ReturnUrl = returnUrl,
			};
			var service = new Stripe.BillingPortal.SessionService();
			var session = service.Create(options);
			return new OkObjectResult(session.Url);
		}
	}
}
