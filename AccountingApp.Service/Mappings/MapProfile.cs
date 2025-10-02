using AccountingApp.Core.DTOs;
using AccountingApp.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Service.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<GroupInRole,GroupInRoleDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Role,RoleDto>().ReverseMap();
            CreateMap<Sale,SaleDto>().ReverseMap();
            CreateMap<User,UserDto>().ReverseMap();

        }
    }
}
