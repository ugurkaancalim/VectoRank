using AutoMapper;
using SSE.App.Domain.Models;
using SSE.App.Domain.Models.Types;
using SSE.App.Infrastructure.Data.Entities;
using SSE.App.Infrastructure.Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Mapping
{
    public static class MapperCreator
    {
        private static IMapper? _mapper = null;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                    _mapper = CreateMapper();
                return _mapper;
            }
        }
        private static IMapper CreateMapper()
        {
            var mb = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppModel, AppEntity>().ReverseMap();
                cfg.CreateMap<AppTypeModel, AppType>().ReverseMap();
            });
            return mb.CreateMapper();
        }
    }
}


