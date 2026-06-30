using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSA
{
    public class RDSAInputBuilder
    {
        private RDSAInput rdsa = null;
        private RDSAInputBuilder(string docBase64)
        {
            rdsa = new RDSAInput(docBase64);
            rdsa.DocBase64 = docBase64;
        }
        public static RDSAInputBuilder Init(string docBase64)
        {
            return new RDSAInputBuilder(docBase64);
        }

        public RDSAInputBuilder SetContentSearch(ContentSearch ContentSearch)
        {
            rdsa.ContentSearch = ContentSearch;
            return this;
        }

        public RDSAInputBuilder SetSignedBy(String SignedBy)
        {
            rdsa.SignedBy = SignedBy;
            return this;
        }

        public RDSAInputBuilder SetLocation(String location)
        {
            rdsa.Location = location;
            return this;
        }

        public RDSAInputBuilder SetReason(String reason)
        {
            rdsa.Reason = reason;
            return this;
        }

        public RDSAInputBuilder SetAppearanceType(AppearanceType appearanceType)
        {
            rdsa.AppearanceType = appearanceType;
            return this;
        }

        public RDSAInputBuilder SetPageTobeSigned(Page pageTobeSigned)
        {
            rdsa.PageTobeSigned = pageTobeSigned;
            return this;
        }

        public RDSAInputBuilder SetCoordinates(Coordinates coordinates)
        {
            rdsa.Coordinates = coordinates;
            return this;
        }

        public RDSAInputBuilder SetPageNumbers(String pageNumbers)
        {
            rdsa.PageNumbers = pageNumbers;
            return this;
        }

        public RDSAInputBuilder SetPageLevelCoordinates(String pageLevelCoordinates)
        {
            rdsa.PageLevelCoordinates = pageLevelCoordinates;
            return this;
        }

        public RDSAInputBuilder SetSignatureImage(String signatureImage)
        {
            rdsa.SignatureImage = signatureImage;
            return this;
        }

        public RDSAInputBuilder SetIsBorderRequired(bool isBorderRequired)
        {
            rdsa.IsBorderRequired = isBorderRequired;
            return this;
        }

        public RDSAInputBuilder SetOneLiner(String oneLiner)
        {
            rdsa.OneLiner = oneLiner;
            return this;
        }

        public RDSAInputBuilder SetCustomContent(String customContent)
        {
            rdsa.CustomContent = customContent;
            return this;
        }

        public RDSAInputBuilder SetSignatureFontSize(int signatureFontSize)
        {
            rdsa.SignatureFontSize = signatureFontSize;
            return this;
        }

        public RDSAInputBuilder SetAdvanceSignature(AdvanceSignature advanceSignature)
        {
            rdsa.advanceSignature = advanceSignature;
            return this;
        }

        public RDSAInputBuilder SetCustomStyle(CustomStyle customStyle)
        {
            rdsa.customStyle = customStyle;
            return this;
        }
        public RDSAInputBuilder SetImageLeftOfText()
        {
            rdsa.ImagePosition = SignatureImagePosition.LEFT_OF_TEXT;
            return this;
        }
        public RDSAInputBuilder SetImageRightOfText()
        {
            rdsa.ImagePosition = SignatureImagePosition.RIGHT_OF_TEXT;
            return this;
        }

        public RDSAInputBuilder SetTickRequired(bool IsRequired)
        {
            rdsa.SignatureValidationMark = IsRequired;
            return this;
        }

        public RDSAInputBuilder ResizeValidationMessage(bool IsRequired)
        {
            rdsa.ResizeValidationMessage = IsRequired;
            return this;
        }

        public RDSAInput Build()
        {
            return rdsa;
        }
    }
}
