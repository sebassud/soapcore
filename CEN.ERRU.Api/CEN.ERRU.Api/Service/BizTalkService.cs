using CEN.ERRU.Api.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CEN.ERRU.Api.Service
{
    public class BizTalkService : CEN.ERRU.Api.Contract.ERRU
    {
        private IHttpContextAccessor _httpContext;
        public BizTalkService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public CheckCommunityLicence_RequestResponse CheckCommunityLicence_Request(CheckCommunityLicence_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public Task<CheckCommunityLicence_RequestResponse> CheckCommunityLicence_RequestAsync(CheckCommunityLicence_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public CheckGoodRepute_RequestResponse CheckGoodRepute_Request(CheckGoodRepute_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public Task<CheckGoodRepute_RequestResponse> CheckGoodRepute_RequestAsync(CheckGoodRepute_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public Infringement_RequestResponse Infringement_Request(Infringement_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public Task<Infringement_RequestResponse> Infringement_RequestAsync(Infringement_RequestRequest request)
        {
            throw new NotSupportedException();
        }

        public Infringement_ResponseResponse Infringement_Response(Infringement_ResponseRequest request)
        {
            throw new NotSupportedException();
        }

        public Task<Infringement_ResponseResponse> Infringement_ResponseAsync(Infringement_ResponseRequest request)
        {
            throw new NotSupportedException();
        }

        public RoadSideInspection_RequestResponse RoadSideInspection_Request(RoadSideInspection_RequestRequest request)
        {
            string body = ToXML(request);
            return new RoadSideInspection_RequestResponse()
            {
                RoadSideInspection_Response = new RoadSideInspection_Response()
                {
                    Body = new rsiBodyResponseType()
                    {
                        respondingAuthority = "POLSKA GUROM",
                        originatingAuthority = "originatingAuthority",
                        businessCaseId = "BTM-123145",
                        statusCode = rsiResponseStatusCodeType.OK,
                        statusMessage = "Odebrano"
                    }
                }
            };
        }

        public async Task<RoadSideInspection_RequestResponse> RoadSideInspection_RequestAsync(RoadSideInspection_RequestRequest request)
        {
            string body = ToXML(request);

            return (new RoadSideInspection_RequestResponse()
            {
                RoadSideInspection_Response = new RoadSideInspection_Response()
                {
                    Body = new rsiBodyResponseType()
                    {
                        respondingAuthority = "POLSKA GUROM",
                        originatingAuthority = "originatingAuthority",
                        businessCaseId = "BTM-123145",
                        statusCode = rsiResponseStatusCodeType.OK,
                        statusMessage = "Odebrano"
                    }
                }
            });
        }

        public string ToXML(RoadSideInspection_RequestRequest request)
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(request.GetType());
                serializer.Serialize(stringwriter, request);
                return stringwriter.ToString();
            }
        }
    }
}
