using System;
using System.Collections.Generic;
using AspNetCoreSpa.Core.Entities;
using AutoMapper;

namespace AspNetCoreSpa.Core.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountVM>().ReverseMap();
            CreateMap<Banner, BannerVM>().ReverseMap();
            CreateMap<BookingPrice, BookingPriceVM>().ReverseMap();
            CreateMap<Contact, ContactVM>().ReverseMap();
            CreateMap<Evaluation, EvaluationVM>().ReverseMap();
            CreateMap<PostCategory, PostCategoryVM>().ReverseMap();
            CreateMap<Post, PostCategoryVM>().ReverseMap();
            CreateMap<Price, PriceVM>().ReverseMap();
            CreateMap<Province, ProvinceVM>().ReverseMap();
            CreateMap<Resource, ResourceVM>().ReverseMap();
            CreateMap<RoleVM, RoleVM>().ReverseMap();
            CreateMap<TourBookingDetail, TourBookingDetailVM>().ReverseMap();
            CreateMap<TourCategory, TourCategoryVM>().ReverseMap();
            CreateMap<TourCustomer, TourCustomerVM>().ReverseMap();
            CreateMap<TouristType, TouristTypeVM>().ReverseMap();
            CreateMap<TourProgram, TourProgramVM>().ReverseMap();
            CreateMap<Tour, TourVM>().ReverseMap();
        }
    }
}
