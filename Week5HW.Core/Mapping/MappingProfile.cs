using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week5HW.Core.Dto;
using Week5HW.Core.Entities;

namespace Week5HW.Core.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<User, DtoUser>().ReverseMap();
        }
    }


}
