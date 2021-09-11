using CEN.ERRU.Api.Contract.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CEN.ERRU.Api.Service
{
    public class ErruService : IErruService
    {
        public Task<Response> Operation(Request request)
        {
            return Task.FromResult( new Response() {FullName = $"{request.FirstName} {request.LastName}" });
        }
    }
}
