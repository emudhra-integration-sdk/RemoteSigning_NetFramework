using Org.BouncyCastle.Crypto.Engines;

namespace RDSA
{
    public class RDSAInput
    {
        /// <summary>
        ///  Represents an RDSA Input  for Remote Signing.
        ///  Class <c>RDSAInput</c>
        /// </summary>
        public string DocBase64 { get; internal set; }
        public string SignedBy { get; internal set; }
        public string Location { get; internal set; }
        public string Reason { get; internal set; }
        public AppearanceType AppearanceType { get; internal set; }
        public Page PageTobeSigned { get; internal set; }
        public Coordinates Coordinates { get; internal set; }
        public string PageNumbers { get; internal set; }
        public string PageLevelCoordinates { get; internal set; }
        public string SignatureImage { get; internal set; }
        public bool IsBorderRequired { get; internal set; }
        public string OneLiner { get; internal set; }
        public string CustomContent { get; internal set; }
        public int SignatureFontSize { get; internal set; }
        public bool SignatureValidationMark { get; internal set; }
        public bool ResizeValidationMessage { get; internal set; }
        public bool CertifiedByBar { get; internal set; }
        public ContentSearch ContentSearch { get; internal set; }
        public string OrganizationName { get; internal set; }
        public string AdditionalInformation { get; internal set; }
        public CustomStyle customStyle { get; internal set; }
        public AdvanceSignature advanceSignature { get; internal set; }
        public SignatureImagePosition ImagePosition { get; internal set; }

        internal RDSAInput(string DocBase64) { this.DocBase64 = DocBase64; }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="PageToBeSigned"/>,
        /// <paramref name="Coordinates"/>,
        /// <paramref name="SignatureImage"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="PageToBeSigned">Page enum Predefined pages in which signature to be stamped.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, Page PageToBeSigned, Coordinates Coordinates, string SignatureImage, bool SignatureValidationMark = false, bool CertifiedByBar = false, string SignedBy = "", string Reason = "", string Location = "", string OrganizationName = "")
           : this(DocBase64, SignedBy, Reason, Location, string.Empty, PageToBeSigned, Coordinates, string.Empty, string.Empty, AppearanceType.SignatureImage, SignatureImage, false, string.Empty, string.Empty, 8, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="Coordinates"/>,
        /// <paramref name="PageNumbers"/>,
        /// <paramref name="SignatureImage"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="PageNumbers">Comma separated Page numbers in which signature to be stamped.</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, Coordinates Coordinates, string PageNumbers, string SignatureImage, bool SignatureValidationMark = false, bool CertifiedByBar = false, string SignedBy = "", string Reason = "", string Location = "", string OrganizationName = "")
        : this(DocBase64, SignedBy, Reason, Location, string.Empty, Page.SPECIFY, Coordinates, PageNumbers, string.Empty, AppearanceType.SignatureImage, SignatureImage, false, string.Empty, string.Empty, 8, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="PageLevelCoordinates"/>,
        /// <paramref name="SignatureImage"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="PageLevelCoordinates">String containing page numbers and coordinates. Please refer document how to pass page level coordinates.</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string PageLevelCoordinates, string SignatureImage, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string SignedBy = "", string Reason = "", string Location = "", string OrganizationName = "")
        : this(DocBase64, SignedBy, Reason, Location, string.Empty, Page.PAGE_LEVEL, Coordinates.Top_Left, string.Empty, PageLevelCoordinates, AppearanceType.SignatureImage, SignatureImage, false, string.Empty, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="OneLiner"/>,
        /// <paramref name="PageToBeSigned"/>
        /// <paramref name="Coordinates"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="OneLiner">Single Line string displayed on signature.</param>
        /// <param name="PageToBeSigned">Page enum Predefined pages in which signature to be stamped.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string OneLiner, Page PageToBeSigned, Coordinates Coordinates, bool IsBorderRequired, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
               : this(DocBase64, string.Empty, string.Empty, string.Empty, OneLiner, PageToBeSigned, Coordinates, string.Empty, string.Empty, AppearanceType.OneLiner, string.Empty, IsBorderRequired, string.Empty, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="CustomContent"/>,
        /// <paramref name="PageToBeSigned"/>
        /// <paramref name="Coordinates"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="PageToBeSigned">Page enum Predefined pages in which signature to be stamped.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="CustomContent">Custom Content string displayed on signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, Page PageToBeSigned, Coordinates Coordinates, bool IsBorderRequired, string CustomContent, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
               : this(DocBase64, string.Empty, string.Empty, string.Empty, string.Empty, PageToBeSigned, Coordinates, "", "", AppearanceType.CustomContent, string.Empty, IsBorderRequired, CustomContent, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="OneLiner"/>,
        /// <paramref name="Coordinates"/>
        /// <paramref name="PageNumbers"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="OneLiner">Single Line string displayed on signature.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="PageNumbers">Comma separated Page numbers in which signature to be stamped.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string OneLiner, Coordinates Coordinates, string PageNumbers, bool IsBorderRequired, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
        : this(DocBase64, string.Empty, string.Empty, string.Empty, OneLiner, Page.SPECIFY, Coordinates, PageNumbers, string.Empty, AppearanceType.OneLiner, string.Empty, IsBorderRequired, string.Empty, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="CustomContent"/>,
        /// <paramref name="Coordinates"/>
        /// <paramref name="PageNumbers"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="CustomContent">Custom Content string displayed on signature.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="PageNumbers">Comma separated Page numbers in which signature to be stamped.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, Coordinates Coordinates, string PageNumbers, bool IsBorderRequired, string CustomContent, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
       : this(DocBase64, string.Empty, string.Empty, string.Empty, string.Empty, Page.SPECIFY, Coordinates, PageNumbers, string.Empty, AppearanceType.CustomContent, string.Empty, IsBorderRequired, CustomContent, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="OneLiner"/>,
        /// <paramref name="PageLevelCoordinates"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="OneLiner">Single Line string displayed on signature.</param>
        /// <param name="PageLevelCoordinates">String containing page numbers and coordinates. Please refer document how to pass page level coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string OneLiner, string PageLevelCoordinates, bool IsBorderRequired, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
        : this(DocBase64, string.Empty, string.Empty, string.Empty, OneLiner, Page.PAGE_LEVEL, Coordinates.Top_Left, "", PageLevelCoordinates, AppearanceType.OneLiner, "", IsBorderRequired, string.Empty, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="CustomContent"/>,
        /// <paramref name="PageLevelCoordinates"/>
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="CustomContent">Custom Content string displayed on signature.</param>
        /// <param name="PageLevelCoordinates">String containing page numbers and coordinates. Please refer document how to pass page level coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string PageLevelCoordinates, bool IsBorderRequired, string CustomContent, int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
        : this(DocBase64, string.Empty, string.Empty, string.Empty, string.Empty, Page.PAGE_LEVEL, Coordinates.Top_Left, "", PageLevelCoordinates, AppearanceType.CustomContent, "", IsBorderRequired, CustomContent, string.Empty, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor for standard signature style. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="SignedBy"/>,
        /// <paramref name="Reason"/>,
        /// <paramref name="Location"/>
        /// <paramref name="PageToBeSigned"/>,
        /// <paramref name="Coordinates"/>,
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="SignedBy">Name of signer.</param>
        /// <param name="Reason">Reason for signing the document</param>
        /// <param name="Location">Location of signing the document.</param>
        /// <param name="PageToBeSigned">Page enum Predefined pages in which signature to be stamped.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string SignedBy, string Reason, string Location, Page PageToBeSigned, Coordinates Coordinates, bool IsBorderRequired, string AdditionalInformation = "", int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
               : this(DocBase64, SignedBy, Reason, Location, "", PageToBeSigned, Coordinates, "", "", AppearanceType.Standard, "", IsBorderRequired, string.Empty, AdditionalInformation, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor for standard signature style. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="SignedBy"/>,
        /// <paramref name="Reason"/>,
        /// <paramref name="Location"/>
        /// <paramref name="Coordinates"/>,
        /// <paramref name="PageNumbers"/>,
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="SignedBy">Name of signer.</param>
        /// <param name="Reason">Reason for signing the document</param>
        /// <param name="Location">Location of signing the document.</param>
        /// <param name="Coordinates">Coordinates enum for Predefined coordinates.</param>
        /// <param name="PageNumbers">Comma separated Page numbers in which signature to be stamped.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string SignedBy, string Reason, string Location, Coordinates Coordinates, string PageNumbers, bool IsBorderRequired, string AdditionalInformation = "", int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
        : this(DocBase64, SignedBy, Reason, Location, string.Empty, Page.SPECIFY, Coordinates, PageNumbers, string.Empty, AppearanceType.Standard, string.Empty, IsBorderRequired, string.Empty, AdditionalInformation, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor for standard signature style. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="SignedBy"/>,
        /// <paramref name="Reason"/>,
        /// <paramref name="Location"/>
        /// <paramref name="PageLevelCoordinates"/>,
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="SignedBy">Name of signer.</param>
        /// <param name="Reason">Reason for signing the document</param>
        /// <param name="Location">Location of signing the document.</param>
        /// <param name="PageLevelCoordinates">String containing page numbers and coordinates. Please refer document how to pass page level coordinates.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        public RDSAInput(string DocBase64, string SignedBy, string Reason, string Location, string PageLevelCoordinates, bool IsBorderRequired, string AdditionalInformation = "", int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string OrganizationName = "")
        : this(DocBase64, SignedBy, Reason, Location, string.Empty, Page.PAGE_LEVEL, Coordinates.Top_Left, string.Empty, PageLevelCoordinates, AppearanceType.Standard, string.Empty, IsBorderRequired, string.Empty, AdditionalInformation, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName) { }

        /// <summary>This method creates RDSAInput constructor for standard signature style. RDSAInput
        /// (
        /// <paramref name="DocBase64"/>,
        /// <paramref name="SignedBy"/>,
        /// <paramref name="Reason"/>,
        /// <paramref name="Location"/>
        /// <paramref name="PageLevelCoordinates"/>,
        /// <paramref name="IsBorderRequired"/>
        /// <paramref name="SignatureFontSize"/>
        /// <paramref name="SignatureValidationMark"/>
        /// <paramref name="CertifiedByBar"/>
        /// )
        /// </summary>
        /// <param name="DocBase64">Bytes of PDF File converted to Base64 encoded.</param>
        /// <param name="SignedBy">Name of signer.</param>
        /// <param name="Reason">Reason for signing the document</param>
        /// <param name="Location">Location of signing the document.</param>
        /// <param name="IsBorderRequired">Boolean value indicating if border is required around signature.</param>
        /// <param name="SignatureFontSize">Optional signature font size default set to 8</param>
        /// <param name="SignatureImage">Base64 encoded Image of signature.</param>
        /// <param name="SignatureValidationMark">SignatureValidationMark true to add validation tick mark</param>
        /// <param name="CertifiedByBar">CertifiedByBar true for certified</param>
        /// <param name="ContentSearch">Text in the pdf where signature will be added on the position</param>
        public RDSAInput(string DocBase64, string SignedBy, string Reason, string Location, bool IsBorderRequired, string AdditionalInformation = "", int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, ContentSearch ContentSearch = null, string OrganizationName = "")
        : this(DocBase64, SignedBy, Reason, Location, string.Empty, Page.PAGE_LEVEL, Coordinates.Top_Left, string.Empty, string.Empty, AppearanceType.Standard, string.Empty, IsBorderRequired, string.Empty, AdditionalInformation, SignatureFontSize, SignatureValidationMark, CertifiedByBar, OrganizationName, ContentSearch) { }

        private RDSAInput(string DocBase64, string SignedBy, string Reason, string Location, string OneLiner, Page PageTobeSigned, Coordinates Coordinates, string PageNumbers, string PageLevelCoordinates, AppearanceType AppearanceType, string SignatureImage, bool IsBorderRequired, string CustomContent, string AdditionalInformation = "", int SignatureFontSize = 8, bool SignatureValidationMark = false, bool CertifiedByBar = false, string organizationName = "", ContentSearch ContentSearch = null)
        {
            this.Coordinates = Coordinates;
            this.DocBase64 = DocBase64;
            this.Location = Location;
            this.PageTobeSigned = PageTobeSigned;
            this.PageLevelCoordinates = PageLevelCoordinates;
            this.PageNumbers = PageNumbers;
            this.Reason = Reason;
            this.SignedBy = SignedBy;
            this.AppearanceType = AppearanceType;
            this.SignatureImage = SignatureImage;
            this.IsBorderRequired = IsBorderRequired;
            this.OneLiner = OneLiner;
            this.CustomContent = CustomContent;
            this.SignatureFontSize = SignatureFontSize;
            this.SignatureValidationMark = SignatureValidationMark;
            this.CertifiedByBar = CertifiedByBar;
            this.OrganizationName = organizationName;
            this.ContentSearch = ContentSearch;
            this.AdditionalInformation = AdditionalInformation;
        }
    }
}
