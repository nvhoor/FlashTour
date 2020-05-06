﻿
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
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostCategoryVM>().ReverseMap();
            CreateMap<Price, PriceVM>().ReverseMap();
            CreateMap<Province, ProvinceVM>().ReverseMap();
            CreateMap<Resource, ResourceVM>().ReverseMap();
            CreateMap<Role, RoleVM>().ReverseMap();
            CreateMap<TourCategory, TourCategoryVM>().ReverseMap();
            CreateMap<TourCustomer, TourCustomerVM>().ReverseMap();
            CreateMap<TourProgram, TourProgramVM>().ReverseMap();
            CreateMap<Tour, TourVM>().ReverseMap();
            CreateMap<TourBooking, TourBookingVM>().ReverseMap();
        }
    }
}
