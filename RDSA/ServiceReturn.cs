using System.Collections.Generic;

namespace RDSA
{
    public class ServiceReturn
    {
        public string ResponseCode { get; internal set; }
        public string TransactionID { get; internal set; }
        public Status ReturnStatus { get; internal set; }
        public string ErrorCode { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public string RequestXML { get; internal set; }
        public string ResponseXML { get; internal set; }
        public List<ReturnDocument> ReturnDocuments { get; internal set; }
    }
}
