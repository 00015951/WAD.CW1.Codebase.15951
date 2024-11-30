using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAD.DAL.Dtos;
using WAD.DAL.Models;

namespace WAD.DAL.Mapping
{
    public MappingProfile()
    {
        // User Mappings
        CreateMap<User, UserDTO>();
        CreateMap<CreateUserDTO, User>();
        CreateMap<EditUserDTO, User>();

        // Activity Mappings
        CreateMap<Activity, ActivityDTO>();
        CreateMap<CreateActivityDTO, Activity>();
        CreateMap<EditActivityDTO, Activity>();
    }
}
