using SSE.Engine.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.Services.Interfaces
{
    public interface IQueryService
    {
        Task<SearchResponseDto> Execute(string query);
    }
}
