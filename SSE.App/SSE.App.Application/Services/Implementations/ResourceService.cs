using SSE.App.Application.Constants;
using SSE.App.Application.Services.Interfaces;
using SSE.App.Domain.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Services.Implementations
{
    public class ResourceService : IResourceService
    {
        private readonly IUnitOfWork _uow;
        public ResourceService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public string? Get(ResourceNames resourceName)
        {
            return _uow.ResourceRepository.Get(resourceName.ToString(), GetLanguageCode());
        }

        public string? GetLanguageCode()
        {
            return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}
