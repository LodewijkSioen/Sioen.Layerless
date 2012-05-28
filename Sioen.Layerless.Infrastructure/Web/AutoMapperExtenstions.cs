using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Sioen.Layerless.Infrastructure.Web
{
    public static class AutoMapperExtenstions
    {
        public static T To<T>(this object from)
        {
            return Mapper.Map<T>(from);
        }
    }
}
