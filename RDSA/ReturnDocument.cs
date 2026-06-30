using eSign.text.pdf;

namespace RDSA
{
    public class ReturnDocument
    {
        public string SignedDocument { get; set; }
        public string DocumentHash { get; set; }
        public int DocId { get; set; }
        public string TempFilePath { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public Status SignedStatus { get; set; }
        internal PdfSignatureAppearance SignatureAppreance { get; set; }
    }
}