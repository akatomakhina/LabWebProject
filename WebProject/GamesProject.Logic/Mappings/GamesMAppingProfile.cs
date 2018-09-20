using AutoMapper;
using GamesProject.DataAccess.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesProject.Logic.Mappings
{
    public class GamesMappingProfile : Profile
    {
        public GamesMappingProfile()
        {
            CreateMap<DbChannel, Common.Models.Channel>();
        }
    }
}
