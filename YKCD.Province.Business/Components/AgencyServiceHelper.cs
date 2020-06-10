using System;
using System.ServiceModel;
using YKCD.Province.Business.AgencyService;
using YKCD.Province.Business.Entities;

namespace YKCD.Province.Business.Components
{
    public static class AgencyServiceHelper
    {
        public static AgencyServicesSoapClient agencyClient = new AgencyServicesSoapClient();

        public static bool ReceiveRequest(string serviceUrl, Request request, Perform perform, bool isDeleted = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl?.Trim()))
                {
                    agencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    var xmlobject = XmlHelper.ConvertToXmlObject(request, perform, isDeleted);
                    agencyClient.ReceiveRequest(xmlobject.ToXml());
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool ReceiveReport(string serviceUrl, Report report, Perform perform)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl?.Trim()))
                {
                    agencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    var xmlobject = XmlHelper.ConvertToXmlObject(report, perform);
                    agencyClient.ReceiveReport(xmlobject.ToXml());
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DeleteRequest(string serviceUrl, long requestID)
        {
            try
            {
                if (!string.IsNullOrEmpty(serviceUrl?.Trim()))
                {
                    agencyClient.Endpoint.Address = new EndpointAddress(serviceUrl);
                    agencyClient.DeleteRequest(requestID);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}