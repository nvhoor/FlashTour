using System;
using System.Collections.Generic;
using System.Linq;
using AspNetCoreSpa.Core.Entities;
using AspNetCoreSpa.Core.ViewModels;
using AspNetCoreSpa.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                tourBookingTourCustomer.TourBookingId = tourBooking.Id;
            }
            _uow.TourBookings.Add(_mapper.Map<TourBooking>(tourBooking));
            var tour = _uow.Tours.GetSingleOrDefault(x=>x.Id ==tourBooking.TourId);
            tour.Slot -= tourBooking.TourCustomers.Count;
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
            var t = _uow.TourBookings.Get(id);
            t.Status = true;
            _uow.TourBookings.Update(t);
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
        
    }
}