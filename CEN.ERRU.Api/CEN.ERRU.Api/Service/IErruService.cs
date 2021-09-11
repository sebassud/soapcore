using CEN.ERRU.Api.Contract.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CEN.ERRU.Api.Service
{
    [ServiceContract, XmlSerializerFormat]
    public interface IErruService
    {
        [OperationContract]
        Task<Response> Operation(Request request);
    }
}
