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
        private List<Guid> tourCategoryIds;
        private List<Guid> tourIds;
        private List<Guid> postIds;
        private List<Guid> postCategoriesIds;
        private List<Guid> roleIds;
        private List<Guid> touristTypeIds;
        private List<Guid> tourBookingIds;
        private List<Guid> tourBookingDetailIds;
        private List<Guid> priceIds;
        private List<Guid> provinceIds;
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
            InitListGuId();
            AddLocalisedData();
            AddTourData();
           // AddShopData();
        }

        private void InitListGuId()
        {
           postCategoriesIds=new List<Guid>();
           for (var i = 0; i < 15; i++)
           {
               postCategoriesIds.Add(Guid.NewGuid());
           }
           postIds=new List<Guid>();
           for (var i = 0; i < 15; i++)
           {
               postIds.Add(Guid.NewGuid());
           }
           roleIds=new List<Guid>();
           for (var i = 0; i < 3; i++)
           {
               roleIds.Add(Guid.NewGuid());
           }
           touristTypeIds=new List<Guid>();
           for (var i = 0; i < 3; i++)
           {
               touristTypeIds.Add(Guid.NewGuid());
           }
           tourIds=new List<Guid>();
           for (var i = 0; i < 15; i++)
           {
               tourIds.Add(Guid.NewGuid());
           }
           tourBookingIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               tourBookingIds.Add(Guid.NewGuid());
           }
           tourBookingDetailIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               tourBookingDetailIds.Add(Guid.NewGuid());
           }
           tourCategoryIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               tourCategoryIds.Add(Guid.NewGuid());
           }
           priceIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               priceIds.Add(Guid.NewGuid());
           }
           provinceIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               provinceIds.Add(Guid.NewGuid());
           }
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
                for (int i = 0; i < 15; i++)
                {
                    _context.PostCategories.Add(new PostCategory
                    {
                        Id = postCategoriesIds[i],
                        Name = "Category_" +i,
                    });
                }

                _context.SaveChanges();
            }

            if (!_context.Posts.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    _context.Posts.Add(new Post
                    {
                        Id = postIds[i],
                        Alias = "Post description " + i,
                        Censorship = true,
                        Name = "Post_" + i,
                        PostContent = "Post content " + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                        MetaDescription = "Post Meta description " + i,
                        MetaKeyWord = "Post Meta description " + i,
                        Image = "post_"+i+".jpg",
                        Status = true,
                        Deleted = false,
                        PostCategoryId =  postCategoriesIds[i],
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
                        Id = Guid.NewGuid(),
                        FullName = "Contact_" + i,
                        Email = $"{i}test@test.com",
                        Address = "Address " + i,
                        Phone = "037489202"+i,
                        Title = "Content Title Contact "+i
                    });
                }
                _context.SaveChanges();
            }

            if (!_context.Roles.Any())
            {
                    _context.Roles.Add(new Role
                    {
                        Id = roleIds[0],
                        RoleName = "ADMIN"
                    });
                    _context.Roles.Add(new Role
                    {
                        Id = roleIds[1],
                        RoleName = "STAFF"
                    });
                    _context.Roles.Add(new Role
                    {
                        Id = roleIds[2],
                        RoleName = "USER"
                    });
                    
                    _context.SaveChanges();
            }
            if (!_context.Accounts.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.Accounts.Add(new Account
                    {
                        UserId = "user_"+i,
                        Address = "Da Nang",
                        Avatar = "avatar_"+i,
                        BirthDay = DateTime.UtcNow,
                        Deleted = false,
                        Email = "user"+i+"@email.com",
                        Name = "User "+i,
                        Password = "asfsdfs",
                        Phone = "0900014324",
                        RoleId = roleIds[new Random().Next(0,2)]
                    });
                }
            }
            if (!_context.TouristTypes.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    _context.TouristTypes.Add(new TouristType
                    {
                        Id = touristTypeIds[i],
                        Name = "Touristype_" +i,
                    });
                }
                _context.SaveChanges();
            }
           
            if (!_context.Provinces.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.Provinces.Add(new Province
                    {
                        Id = provinceIds[i],
                        Name = "Provine_" + i,
                        Longitude = new Random().Next(100,150),
                        Latitude = new Random().Next(100, 200)
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.TourCategories.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    _context.TourCategories.Add(new TourCategory()
                    {
                        Id = tourCategoryIds[i],
                        Name = "TourCategory_" + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                            Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.Tours.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    _context.Tours.Add(new Tour
                    {
                        Id = tourIds[i],
                        Name = "Post_" + i,
                        Description = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio.
                            Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet",
                        DepartureDate = DateTime.UtcNow,
                        Image = "tour_"+i+".jpg",
                        Images = "ImagesTour_.png",
                        Status = true,
                        Censorship = true,
                        Deleted = false,
                        Slot = new Random().Next(1,20),
                        DepartureId = provinceIds[new Random().Next(0,9)],
                        TourCategoryId = tourCategoryIds[new Random().Next(0,9)],
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
                        Id = Guid.NewGuid(),
                        Name = "price_" + i,
                        PromotionPrice = new Random().Next(100000,200000),
                        OriginalPrice = new Random().Next(1000000,5000000),
                        StartDatePro = DateTime.UtcNow,
                        TourId = tourIds[i],
                        TouristTypeId =touristTypeIds[new Random().Next(0,2)],
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
                        Id = tourBookingIds[i],
                        FullName = "TourBooking_" + i,
                        Email = $"{i}test@test.com",
                        Address = "Address_" + i,
                        Mobile = "037489202"+i,
                        Note = "Note " + i,
                        Status = true,
                        Deleted = false,
                        UserId = "user_"+i
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
                        Id = tourBookingDetailIds[i],
                        TourId = tourIds[i],
                        TourBookingId = tourBookingIds[i]
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
                                     FullName = "Tony Nguyen " +i,
                                     BirthDay = DateTime.Now.AddDays(i),
                                     Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
                                     TourBookingId = tourBookingIds[i],
                                     TouristTypeId = touristTypeIds[new Random().Next(0,2)],
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
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now.AddDays(i),
                        OrderNumber = 1 + i,
                        Title = @"Lorem ipsum dolor seit amet Nulla quis sem at nibh elemn",
                        Description = @"Lorem ipsum dolor sit amet Nulla quis sem at nibh elemen",
                        Destination = "Hai Chau, Da Nang",
                        TourId = tourIds[i],
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.Evaluations.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    _context.Evaluations.Add(new Evaluation
                    {
                        Id = Guid.NewGuid(),
                        OneStar = new Random().Next(1,2000),
                        TwoStar = new Random().Next(1,2000),
                        ThreeStar = new Random().Next(1,2000),
                        FourStar = new Random().Next(1,2000),
                        FiveStar = new Random().Next(1,2000),
                        TourId = tourIds[i],
                    });
                }
                _context.SaveChanges();
            }
            if (!_context.Banners.Any())
            {
                for (int i = 0; i < 4; i++)
                {
                    _context.Banners.Add(new Banner
                    {
                        Id = Guid.NewGuid(),
                       Name = "Bander_" +i,
                       Image = "banner_"+i+".jpg",
                       Description = @"Lorem ipsum dolor seit amet Nulla quis sem at nibh elemn"
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
