using AspNetCoreSpa.Core;
using AspNetCoreSpa.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreSpa.Infrastructure
{
    public interface ISeedData
    {
        void Initialise();
    }

    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SeedData(
            ApplicationDbContext context,
            ILogger<SeedData> logger,
            IWebHostEnvironment hostingEnvironment
            )
        {
            _context = context;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public void Initialise()
        {
            _context.Database.Migrate();

            AddLocalisedData();
            AddTourData();
           // AddShopData();
        }
        private void AddLocalisedData()
        {
            if (!_context.Cultures.Any())
            {
                var translations = _hostingEnvironment.GetTranslationFile();

                var locales = translations.First().Split(",").Skip(1).ToList();

                var currentLocale = 0;

                locales.ForEach(locale =>
                {
                    currentLocale += 1;

                    var culture = new Culture
                    {
                        Name = locale
                    };
                    var resources = new List<Resource>();
                    translations.Skip(1).ToList().ForEach(t =>
                    {
                        var line = t.Split(",");
                        resources.Add(new Resource
                        {
                            Culture = culture,
                            Key = line[0],
                            Value = line[currentLocale]
                        });
                    });

                    culture.Resources = resources;

                    _context.Cultures.Add(culture);

                    _context.SaveChanges();
                });
            }
        }

        private void AddTourData()
        {
            
            if (!_context.PostCategories.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.PostCategories.Add(new PostCategory
                    {
                        Name = "Category " + i,
                    });
                }

                _context.SaveChanges();
            }

            if (!_context.Posts.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    _context.Posts.Add(new Post
                    {
                        Name = "Post " + i,
                        PostContent = "Post content " + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                            Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                        MetaDescription = "Post Meta description " + i,
                        MetaKeyWord = "Post Meta description " + i,
                        Alias = "Post description " + i,
                        Status = true,
                        Censorship = true,
                        Deleted = false,
                        PostCategoryId =  new Random().Next(1, 11),
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.Contacts.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.Contacts.Add(new Contact
                    {
                        FullName = "Contact " + i,
                        Email = $"{i}test@test.com",
                        Address = "Address " + i,
                        Phone = "037489202"+i,
                        Title = "Content Tile Contact "+i
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.Prices.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    _context.Prices.Add(new Price
                    {
                        Name = "Post " + i,
                        PromotionPrice = new Random().Next(100000,200000),
                        OriginalPrice = new Random().Next(1000000,5000000),
                        StartDatePro = DateTime.UtcNow,
                        TouristTypeId = new Random().Next(1,4)
                    });
                }
                _context.SaveChanges();
            }
            // if (!_context.Provincess.Any())
            // {
            //     for (int i = 0; i < 20; i++)
            //     {
            //         _context.Prices.Add(new Price
            //         {
            //             Name = "Post " + i,
            //             PromotionPrice = new Random().Next(100000,200000),
            //             OriginalPrice = new Random().Next(1000000,5000000),
            //             StartDatePro = DateTime.UtcNow,
            //             TouristTypeId = new Random().Next(1,4)
            //         });
            //     }
            //     _context.SaveChanges();
            // }
            if (!_context.Tours.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.Tours.Add(new Tour
                    {
                        Name = "Post " + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                            Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                        DepartureDate = DateTime.UtcNow,
                        Status = true,
                        Censorship = true,
                        Deleted = false,
                        Slot = new Random().Next(1,20),
                        DepartureId = new Random().Next(1,4),
                        CategoryId = new Random().Next(1,4)
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourBookings.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.TourBookings.Add(entity: new TourBooking
                    {
                        FullName = "Tour Booking " + i,
                        Email = $"{i}test@test.com",
                        Address = "Address " + i,
                        Mobile = "037489202"+i,
                        Note = "Note " + i,
                        Status = true,
                        Deleted = false,
                        UserId = new Random().Next(1,10),
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourBookingDetails.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.TourBookingDetails.Add(new TourBookingDetail
                    {
                        TourId = new Random().Next(1,4),
                        TourBookingId = new Random().Next(1,4)
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourCategories.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    _context.TourCategories.Add(new TourCategory()
                    {
                        Name = "Post " + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                            Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourCustomers.Any())
            {
                             for (int i = 0; i < 10; i++)
                             {
                                 _context.TourCustomers.Add(new TourCustomer
                                 {
                                     FullName = "Tony " +i,
                                     BirthDay = DateTime.Now.AddDays(i),
                                     Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
                                     TourBookingId = new Random().Next(1,5),
                                     TouristTypeId = new Random().Next(1,4)
                                 });
                             }
                             _context.SaveChanges();
            }
            if (!_context.TouristTypes.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    _context.TouristTypes.Add(new TouristType
                    {
                        Name = "Touristype " +i,
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourPrograms.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.TourPrograms.Add(new TourProgram
                    {
                        Date = DateTime.Now.AddDays(i),
                        OrderNumber = 1 + i,
                        Title = @"Lorem ipsum dolor sit amet Nulla quis sem at nibh elemen",
                        Description = @"Lorem ipsum dolor sit amet Nulla quis sem at nibh elemen",
                        DestinationId = new Random().Next(1,4),
                        TourId = new Random().Next(1,10)
                        
                    });
                }
                _context.SaveChanges();
            }
            
        }
        private void AddShopData()
        {
//            if (!_context.Customers.Any())
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    _context.Customers.Add(new Customer
//                    {
//                        Name = "John Doe " + i,
//                        Email = $"{i}test@test.com",
//                        DateOfBirth = DateTime.Now.AddDays(i),
//                        PhoneNumber = "0123456789" + i,
//                        Address = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
//                    Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
//                        City = "Lorem Ipsum " + i,
//                        Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
//                        UpdatedDate = DateTime.UtcNow,
//                        CreatedDate = DateTime.UtcNow
//                    });
//                }
//
//                _context.SaveChanges();
//            }
//
//            if (!_context.ProductCategories.Any())
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    _context.ProductCategories.Add(new ProductCategory
//                    {
//                        Name = "Category " + i,
//                        Description = "Category description " + i,
//                        UpdatedDate = DateTime.UtcNow,
//                        CreatedDate = DateTime.UtcNow
//                    });
//                }
//
//                _context.SaveChanges();
//            }
//
//            if (!_context.Products.Any())
//            {
//                for (int i = 0; i < 100; i++)
//                {
//                    _context.Products.Add(new Product
//                    {
//                        Name = "Product " + i,
//                        Description = "Product description " + i,
//                        BuyingPrice = 100 + i,
//                        SellingPrice = 110 + i,
//                        UnitsInStock = 10 + i,
//                        IsActive = true,
//                        ProductCategoryId = new Random().Next(1, 11),
//                        CreatedDate = DateTime.UtcNow,
//                        UpdatedDate = DateTime.UtcNow
//                    });
//                }
//
//                _context.SaveChanges();
//            }
//
//            if (!_context.Orders.Any())
//            {
//                var customer = _context.Customers.First();
//                for (int i = 0; i < 10; i++)
//                {
//                    _context.Orders.Add(new Order
//                    {
//                        Discount = 500 + 1m,
//                        Comments = i + " Lorem ipsum is just a dummy text e.g the quick brown fox jumps over the lazy dog.",
//                        CustomerId = customer.Id,
//                        CreatedDate = DateTime.UtcNow,
//                        UpdatedDate = DateTime.UtcNow,
//                        OrderDetails = null
//                    });
//                }
//                _context.SaveChanges();
//            }

        }
    }
}
