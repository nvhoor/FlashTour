using AspNetCoreSpa.Core;
using AspNetCoreSpa.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Common;
using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        private List<int> roleIds;
        private List<int> touristTypeIds;
        private List<Guid> tourBookingIds;
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
        }

        private void readJSONData()
        {
            var json = File.ReadAllText(@"./DataSample/post.json");
            var objects = JArray.Parse(json); // parse as array  
            foreach(var jToken in objects)
            {
                var root = (JObject) jToken;
                foreach(KeyValuePair<string, JToken> app in root)
                {
                    Console.WriteLine(app.Value);
                }
            }
        }
        private void InitListGuId()
        {
           postCategoriesIds=new List<Guid>();
           for (var i = 0; i < 7; i++)
           {
               postCategoriesIds.Add(Guid.NewGuid());
           }
           postIds=new List<Guid>();
           for (var i = 0; i < 7; i++)
           {
               postIds.Add(Guid.NewGuid());
           }
           roleIds=new List<int>();
           {
               roleIds.Add(RoleEnum.Admin.ToRoleInt());
               roleIds.Add(RoleEnum.Staff.ToRoleInt());
               roleIds.Add(RoleEnum.User.ToRoleInt());
           }
           touristTypeIds=new List<int>();
           {
               touristTypeIds.Add(TouristTypeEnum.Adult.ToTouristTypeInt());
               touristTypeIds.Add(TouristTypeEnum.Children.ToTouristTypeInt());
               touristTypeIds.Add(TouristTypeEnum.Kid.ToTouristTypeInt());
           }
           tourIds=new List<Guid>();
           for (var i = 0; i < 38; i++)
           {
               tourIds.Add(Guid.NewGuid());
           }
           tourBookingIds=new List<Guid>();
           for (var i = 0; i < 50; i++)
           {
               tourBookingIds.Add(Guid.NewGuid());
           }
           tourCategoryIds=new List<Guid>();
           for (var i = 0; i < 7; i++)
           {
               tourCategoryIds.Add(Guid.NewGuid());
           }
           priceIds=new List<Guid>();
           for (var i = 0; i < 10; i++)
           {
               priceIds.Add(Guid.NewGuid());
           }
           provinceIds=new List<Guid>();
           for (var i = 0; i < 62; i++)
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
                for (int i = 0; i < 7; i++)
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
                var json = File.ReadAllText(@"./DataSample/post.json");
                var objects = JArray.Parse(json); // parse as array
                var i = 0;
                foreach(var jToken in objects)
                {
                    var root = (JObject) jToken;
                    var post = new Post
                    {
                        Id = postIds[i],
                        Alias = "Post description " + i,
                        Censorship = true,
                        MetaDescription = "Post Meta description " + i,
                        MetaKeyWord = "Post Meta description " + i,
                        Image = "post_" + i + ".jpg",
                        Status = true,
                        Deleted = false,
                        PostCategoryId = postCategoriesIds[i],
                    };
                   
                    foreach(KeyValuePair<string, JToken> app in root)
                    {
                        switch (app.Key)
                        {
                            case "Name":
                                post.Name = app.Value.ToString();
                                break;
                            case "PostContent":
                                post.Description = app.Value.ToString();
                                break;
                            case "Description":
                                post.PostContent = app.Value.ToString();
                                break;
                        }
                    }
                    _context.Posts.Add(post);
                    i++;
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
           
            if (!_context.Provinces.Any())
            {
                var json = File.ReadAllText(@"./DataSample/province.json");
                var objects = JArray.Parse(json); // parse as array
                var i = 0;
                foreach(var jToken in objects)
                {
                    var root = (JObject) jToken;
                    var province = new Province
                    {
                        Id = provinceIds[i],
                        Longitude = new Random().Next(100, 150),
                        Latitude = new Random().Next(100, 200)
                    };
                   
                    foreach(KeyValuePair<string, JToken> app in root)
                    {
                        switch (app.Key)
                        {
                            case "Name":
                                province.Name = app.Value.ToString();
                                break;
                        }
                    }
                    _context.Provinces.Add(province);
                    i++;
                }
                _context.SaveChanges();
            }
            if (!_context.TourCategories.Any())
            {
                var json = File.ReadAllText(@"./DataSample/tourcate.json");
                var objects = JArray.Parse(json); // parse as array
                var i = 0;
                foreach(var jToken in objects)
                {
                    var root = (JObject) jToken;
                    var tourCate = new TourCategory()
                    {
                        Id = tourCategoryIds[i],
                        Image = "tourCate_" + i + ".jpg"
                    };
                   
                    foreach(KeyValuePair<string, JToken> app in root)
                    {
                        switch (app.Key)
                        {
                            case "Name":
                                tourCate.Name = app.Value.ToString();
                                break;
                            case "Description":
                                tourCate.Description = app.Value.ToString();
                                break;
                        }
                    }
                    _context.TourCategories.Add(tourCate);
                    i++;
                }
                _context.SaveChanges();
            }
            if (!_context.Tours.Any())
            {
                var json = File.ReadAllText(@"./DataSample/tour.json");
                var objects = JArray.Parse(json); // parse as array
                var i = 0;
                foreach(var jToken in objects)
                {
                    var root = (JObject) jToken;
                    var tour = new Tour
                    {
                        Id = tourIds[i],
                        DepartureDate = DateTime.UtcNow.AddDays(new Random().Next(0, 10)),
                        Image = "tour_" + (i + 1) + ".jpg",
                        Images = "tour_" + (i + 1) + "_" + 1 + ".jpg|tour_" + (i + 1) + "_" + 2 + ".jpg|tour_" +
                                 (i + 1) + "_" + 3 + ".jpg",
                        Status = true,
                        Censorship = true,
                        Deleted = false,
                        Slot = new Random().Next(1, 20),
                        ViewCount = new Random().Next(100, 1000),
                        DepartureId = provinceIds[new Random().Next(0, 9)],
                        DestinationId = provinceIds[new Random().Next(0, 9)],
                        TourCategoryId = tourCategoryIds[new Random().Next(0, 6)],
                    };
                   
                    foreach(KeyValuePair<string, JToken> app in root)
                    {
                        switch (app.Key)
                        {
                            case "Name":
                                tour.Name = app.Value.ToString();
                                break;
                            case "Description":
                                tour.Description = app.Value.ToString();
                                break;
                        }
                    }
                    _context.Tours.Add(tour);
                    i++;
                }
                _context.SaveChanges();
            }
            if (!_context.Prices.Any())
            {
                var id = 0;
                tourIds.ForEach((x) =>
                {
                    var priceAdult = new Random().Next(300, 1000) ;
                    var i = 0;
                    touristTypeIds.ForEach(y =>
                    {
                        if (i == 0)
                        {
                            _context.Prices.Add(new Price
                            {
                                Id = Guid.NewGuid(),
                                Name = "price_" + (id++),
                                PromotionPrice = priceAdult-new Random().Next(0,50),
                                OriginalPrice = priceAdult,
                                StartDatePro = DateTime.UtcNow,
                                EndDatePro = DateTime.UtcNow.AddDays(new Random().Next(2,7)),
                                TourId = x,
                                TouristType =y,
                            }); 
                        }
                        else
                        {
                            var orginPrice = priceAdult - new Random().Next(0, 50);
                            _context.Prices.Add(new Price
                            {
                                Id = Guid.NewGuid(),
                                Name = "price_" + (id++),
                                PromotionPrice = orginPrice-new Random().Next(0,50),
                                OriginalPrice = orginPrice,
                                StartDatePro = DateTime.UtcNow,
                                EndDatePro = DateTime.UtcNow.AddDays(new Random().Next(2,7)),
                                TourId = x,
                                TouristType =y,
                            });  
                        }

                        i++;
                    }); 
                });
                _context.SaveChanges();
            }
            if (!_context.TourBookings.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    _context.TourBookings.Add(entity: new TourBooking
                    {
                        Id = tourBookingIds[i],
                        FullName = "TourBooking_" + i,
                        Email = $"{i}test@test.com",
                        Address = "Address_" + i,
                        Mobile = "037489202"+i,
                        Note = "Note " + i,
                        Status = i%2==0,
                        Deleted = false,
                        UserId = "user_"+(new Random().Next(0,10)),
                        TourId=tourIds[new Random().Next(0,37)]
                    });
                }
                _context.SaveChanges();
            }
         
            if (!_context.TourCustomers.Any())
            {
                var id = 0;
                             tourBookingIds.ForEach((x) =>
                             {
                                 touristTypeIds.ForEach(y =>
                                 {
                                     _context.TourCustomers.Add(new TourCustomer
                                     {
                                         Id = Guid.NewGuid(),
                                         FullName = "Tony Nguyen " +id,
                                         BirthDay = DateTime.Now.AddDays(id),
                                         Gender = id % 2 == 0 ? Gender.Male : Gender.Female,
                                         TourBookingId = x,
                                         TouristType = y,
                                     });
                                     _context.BookingPrices.Add(entity: new BookingPrice
                                     {
                                         Id = Guid.NewGuid(),
                                         TourBookingId = x,
                                         TouristType = y,
                                         Price = new Random().Next(100,1000)
                                     });
                                     id++;
                                 }); 
                             });
                             _context.SaveChanges();
            }
          
            if (!_context.TourPrograms.Any())
            {
                tourIds.ForEach((x) =>
                {
                    var tour = _context.Tours.Find(x);
                    for (var j = 0; j < 3; j++)
                    {
                        _context.TourPrograms.Add(new TourProgram
                        {
                            Id = Guid.NewGuid(),
                            Date =tour.DepartureDate.AddDays(j),
                            OrderNumber = 1 + j,
                            TourId = x,
                        }); 
                    }
                });
                _context.SaveChanges();
                var tourprograms = _context.TourPrograms.ToList();
                var json = File.ReadAllText(@"./DataSample/tourprogram.json");
                var objects = JArray.Parse(json); // parse as array
                var lengthObject = objects.Count;
                var i = 0;
                foreach(var jToken in objects)
                {
                    if (i < lengthObject)
                    {
                        var root = (JObject) jToken;
                        var tourProgram = tourprograms[i];

                        foreach (KeyValuePair<string, JToken> app in root)
                        {
                            switch (app.Key)
                            {
                                case "Title":
                                    tourProgram.Title = app.Value.ToString();
                                    break;
                                case "Description":
                                    tourProgram.Description = app.Value.ToString();
                                    break;
                                case "Destination":
                                    tourProgram.Destination = app.Value.ToString();
                                    break;
                            }
                        }
                        i++;  
                    }
                    
                }
                _context.SaveChanges();
            }
            if (!_context.Evaluations.Any())
            {
                foreach (var tourId in tourIds)
                {
                   _context.Evaluations.Add(new Evaluation
                                      {
                                          Id = Guid.NewGuid(),
                                          OneStar = new Random().Next(1,2000),
                                          TwoStar = new Random().Next(1,2000),
                                          ThreeStar = new Random().Next(1,2000),
                                          FourStar = new Random().Next(1,2000),
                                          FiveStar = new Random().Next(1,2000),
                                          TourId = tourId,
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
                       Description = @"Lorem ipsum dolor seit amet Nulla quis sem at nibh elemn",
                       PostId = postIds[i],
                       Censorship = true
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
