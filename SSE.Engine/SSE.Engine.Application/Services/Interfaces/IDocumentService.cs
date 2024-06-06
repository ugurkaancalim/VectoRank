
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.Engine.Application.DataStructures.Document;
using SSE.Engine.Application.DTOs;

namespace SSE.Engine.Application.Services.Interfaces
{
    public interface IDocumentService
    {
        Task AddDocument(DocumentDto document);
        Task AddDocuments(DocumentDto[] documents);
    }
}
