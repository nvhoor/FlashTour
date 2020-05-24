using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace AspNetCoreSpa.Web.Controllers.api
{
    public class TourBookingController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public TourBookingController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/TourBookings
        [HttpGet]
        public IActionResult Get()
        {
            var allTourTourBookings = _uow.TourBookings.GetAll().Where(x=>x.Status);
            foreach (var allTourTourBooking in allTourTourBookings)
            {
                var toursCustomers = _uow.TourCustomers.Find(x => x.TourBookingId == allTourTourBooking.Id).OrderBy(o => o.TouristType).ToList();
                allTourTourBooking.TourCustomers = toursCustomers;
                var bookingPrices = _uow.BookingPrices.Find(x => x.TourBookingId == allTourTourBooking.Id).OrderBy(o => o.TouristType).ToList();
                allTourTourBooking.BookingPrices = bookingPrices;
            }
            return Ok(_mapper.Map<IEnumerable<TourBookingVM>>(allTourTourBookings));
        }
        // GET: api/TourBooking/Sensorships
        [HttpGet("Sensorships")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public IActionResult GetCensorships()
        {
            var allTourTourBookings = _uow.TourBookings.GetAll().Where(x=>!x.Status);
            foreach (var allTourTourBooking in allTourTourBookings)
            {
                var toursCustomers = _uow.TourCustomers.Find(x => x.TourBookingId == allTourTourBooking.Id).OrderBy(o => o.TouristType).ToList();
                allTourTourBooking.TourCustomers = toursCustomers;
                var bookingPrices = _uow.BookingPrices.Find(x => x.TourBookingId == allTourTourBooking.Id).OrderBy(o => o.TouristType).ToList();
                allTourTourBooking.BookingPrices = bookingPrices;
            }
            return Ok(_mapper.Map<IEnumerable<TourBookingVM>>(allTourTourBookings));
        }

        // GET: api/TourBookings/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var booking = _uow.TourBookings.Get(id);
            var toursCustomers = _uow.TourCustomers.Find(x => x.TourBookingId == id).OrderBy(o => o.TouristType).ToList();
            booking.TourCustomers = toursCustomers;
            return Ok(_mapper.Map<TourBookingVM>(booking));
        }

        // POST: api/TourBooking
        [HttpPost]
        public void Post([FromBody] TourBookingVM tourBooking)
        {
            tourBooking.BookingPrices.Clear();
            tourBooking.Id = Guid.NewGuid();
            string emailDatas = tourBooking.FullName+"|"+tourBooking.Email+"|THANK YOU TO BOOKs TOUR AT FLASHTOUR";
            decimal totalPrice = 0;
            for (var i=0;i<3;i++ )
            {
                var tourBookingPrice = new BookingPriceVM();
                tourBookingPrice.Id = Guid.NewGuid();
                tourBookingPrice.TourBookingId = tourBooking.Id;
                tourBookingPrice.TouristType = i;
                var prices = _uow.Prices.GetSingleOrDefault(x => x.TouristType == i && x.TourId == tourBooking.TourId);
                var toCheck = DateTime.UtcNow;
                var min = prices.StartDatePro;
                var max = prices.EndDatePro;
                if (DateTime.Compare(toCheck,min)>=0 && DateTime.Compare(toCheck, max)<=0)
                {
                    tourBookingPrice.Price = prices.PromotionPrice;
                }
                else
                {
                    tourBookingPrice.Price = prices.OriginalPrice;
                }
                tourBooking.BookingPrices.Add(tourBookingPrice);
            }
            foreach (var tourBookingTourCustomer in tourBooking.TourCustomers)
            {
                tourBookingTourCustomer.Id = Guid.NewGuid();
               var  pricesCustomer = tourBooking.BookingPrices.SingleOrDefault(x => x.TouristType == tourBookingTourCustomer.TouristType && x.TourBookingId == tourBooking.Id);
               if (pricesCustomer != null)
               {
                   totalPrice += pricesCustomer.Price;
               }
               tourBookingTourCustomer.TourBookingId = tourBooking.Id;
            }
            _uow.TourBookings.Add(_mapper.Map<TourBooking>(tourBooking));
            var tour = _uow.Tours.GetSingleOrDefault(x=>x.Id ==tourBooking.TourId);
            emailDatas += "|" + tour.Id+"|"+totalPrice+"|"+tour.DepartureDate.AddDays(-3);
            tour.Slot -= tourBooking.TourCustomers.Count;
            SendEmail(emailDatas,tourBooking,2);
            _uow.SaveChanges();
        }

        // PUT: api/TourBooking/5
        [HttpPut("{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void Put(Guid id, [FromBody] TourBookingVM tbooking)
        {
            var booking = _uow.TourBookings.Get(id);
            booking.FullName = tbooking.FullName;
            booking.Email = tbooking.Email;
            booking.Mobile = tbooking.Mobile;
            booking.Note = tbooking.Note;
            booking.Address = tbooking.Address;
            booking.UserId = tbooking.UserId;
            booking.Status = tbooking.Status;
            booking.Deleted = tbooking.Deleted;
            _uow.TourBookings.Update(booking);
            var result = _uow.SaveChanges();
        }
        [HttpPut("Censorship/{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void Put(Guid id)
        {
            var tourBooking = _uow.TourBookings.Get(id);
            tourBooking.Status = true;
            string emailDatas = tourBooking.FullName+"|"+tourBooking.Email+"|YOU BOOKs TOUR AT FLASHTOUR FINISHED";
            decimal totalPrice = 0;
            var tourCustomers = _uow.TourCustomers.Where(x => x.TourBookingId == tourBooking.Id);
            var bookingPrices=_uow.BookingPrices.GetAll().Where(x=>x.TourBookingId==tourBooking.Id);
            foreach (var tourBookingTourCustomer in tourCustomers)
            {
                var  pricesCustomer = bookingPrices.SingleOrDefault(x => x.TouristType == tourBookingTourCustomer.TouristType && x.TourBookingId == tourBooking.Id);
                if (pricesCustomer != null)
                {
                    totalPrice += pricesCustomer.Price;
                }
            }
            var tour = _uow.Tours.GetSingleOrDefault(x=>x.Id ==tourBooking.TourId);
            emailDatas += "|" + tour.Id+"|"+totalPrice+"|"+tour.DepartureDate.AddDays(-3);
            SendEmail(emailDatas,_mapper.Map<TourBookingVM>(tourBooking),1);
            _uow.TourBookings.Update(tourBooking);
            var result = _uow.SaveChanges();
        }
        // DELETE: api/TourBookings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = ("admin,Admin,staff,Staff"))]
        public void Delete(Guid id)
        {
            _uow.TourBookings.Remove(_uow.TourBookings.Get(id));
            _uow.SaveChanges();
        }
        [HttpGet("GetDashBoardData")]
        // [Authorize(Roles = ("admin,Admin"))]
        public IActionResult GetDashBoardData(string startDateTimeStamp,string endDateTimeStamp)
        {
           var startDate=(new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(startDateTimeStamp));
           var endDate=(new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(endDateTimeStamp));
           var dashBoardData=new DashBoardVM();
           List<ChartDataVM> revenues=new List<ChartDataVM>();
           List<ChartDataVM> tourists=new List<ChartDataVM>();
           do
           {
               var revenue=new ChartDataVM();
               var tourist=new ChartDataVM();
               var tourBookings=  _uow.TourBookings.Where(x => x.CreatedAt.Year==startDate.Year&&
                                                               x.CreatedAt.Month==startDate.Month&&
                                                               x.CreatedAt.Day==startDate.Day&&x.Status);
               DateTime dt1970 = new DateTime(1970, 1, 1);
               TimeSpan span = startDate - dt1970;
               tourist.X=revenue.X = (decimal) span.TotalMilliseconds;
               
               if (!tourBookings.Any())
               {
                   revenue.Y = 0;
                   tourist.Y = 0;
               }
               else
               {
                   decimal totalValueBooking=0;
                   decimal totalTourists = 0;
                   foreach (var tourBooking in tourBookings)
                   {
                       var TourCustomers = _uow.TourCustomers.Where(x => x.TourBookingId == tourBooking.Id);
                       foreach (var tourCustomer in TourCustomers)
                       {
                           var bookingPrice = _uow.BookingPrices.GetSingleOrDefault(x=>x.TourBookingId==tourBooking.Id&&
                                                                                       x.TouristType==tourCustomer.TouristType);
                           if (bookingPrice != null)
                           {
                               totalValueBooking += bookingPrice.Price;
                           }
                       }

                       totalTourists += TourCustomers.Any() ? TourCustomers.Count() : 0;
                   }
                   revenue.Y = totalValueBooking;
                   tourist.Y = totalTourists;
               }
               revenues.Add(revenue);
               tourists.Add(tourist);
               startDate = startDate.AddDays(1);
           } while (startDate.Date<=endDate.Date);

           dashBoardData.Revenues = revenues;
           dashBoardData.Tourists = tourists;
           return Ok(dashBoardData);
        }
        public bool SendEmail( string emailData,TourBookingVM tourBooking,int typeMail)
        {
            try
            {
                string[] emailDatas= emailData.Split("|");
                var name = emailDatas[0];
                var address =emailDatas[1];
                var subject=emailDatas[2];
                var tourId=emailDatas[3];
                var totalPrices = emailDatas[4];
                var departureDay = emailDatas[5];
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("FLASH TOUR", "nvhoorshadow@gmail.com"));
                message.To.Add(new MailboxAddress(name, address));
                message.Subject = subject;
                var text = "";
                if (typeMail == 2)
                {
                    text =
                        "Welcome " + name +
                        "\nThank you for choosing to book a tour at our Flash Tour Website!" +
                        "\nYou have booked the tour and successfully paid with the following information:" +
                        "\nTour code: " + tourId +
                        "\nCustomer information:" +
                        "\n-	Full name:" + name +
                        "\n-	Email: " + address +
                        "\n-  PhoneNumber:" + tourBooking.Mobile +
                        "\n-  Total Price:" + totalPrices +
                        "\nList tour customer:";
                    foreach (var tourBookingTourCustomer in tourBooking.TourCustomers)
                    {
                        text += "\nFull name: " + tourBookingTourCustomer.FullName;
                    }

                    text+= "\nPlease pay the above amount via your account number 007.137.008.9095 3 days before the tour departure. Too many days on the tour will automatically be canceled."+
                           "\n\nIf you have any questions, please contact our consultants or 24/7 customer service center at any FlashTour offices nationwide and foreign branches" +
                           "\nTour booking is valid to " + departureDay +
                           "\nThank you for your interest in the Flash Tour Website service." +
                           "\nThis is an automated email system, please do not reply to this email.";  
                }
                else
                {
                    text =
                        "Welcome " + name +
                        "\nThank you for choosing to book a tour at our Flash Tour Website!" +
                        "\nYou have booked the tour and successfully paid with the following information:" +
                        "\nTour code: " + tourId +
                        "\nTour booking code: " + tourBooking.Id +
                        "\nCustomer information:" +
                        "\n-	Full name:" + name +
                        "\n-	Email: " + address +
                        "\n-  PhoneNumber:" + tourBooking.Mobile +
                        "\n-  Total Price:" + totalPrices +
                        "\nList tour customer:";
                    foreach (var tourBookingTourCustomer in tourBooking.TourCustomers)
                    {
                        text += "\nFull name: " + tourBookingTourCustomer.FullName;
                    }

                    text+= "\n\nIf you have any questions, please contact our consultants or 24/7 customer service center at any FlashTour offices nationwide and foreign branches" +
                           "\nTour booking is valid to " + departureDay +
                           "\nThank you for your interest in the Flash Tour Website service." +
                           "\nThis is an automated email system, please do not reply to this email."; 
                }
                message.Body = new TextPart("plain")
                {
                    Text = text
                    
                };
                
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);

                    //SMTP server authentication if needed
                    client.Authenticate("flashtourdtu@gmail.com", "flashtour123");

                    client.Send(message);

                    client.Disconnect(true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}