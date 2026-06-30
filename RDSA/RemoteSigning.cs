using eSign.text;
using eSign.text.pdf;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;

namespace RDSA
{
    /// <summary>
    /// Represents
    /// Remote Signing
    /// </summary>
    public sealed class RemoteSigning
    {


        /// <summary>
        /// Remote signing constructor for only license path RemoteSigning
        /// (
        ///     <paramref name="ClientId"/>
        ///     <paramref name="RDSAURL"/>
        /// )
        /// <para>For Proxy use <see cref="RDSA.RemoteSigning(string,string,int)"/></para>
        /// <para>For Proxy with Authentication use <see cref="RemoteSigning(string,string,int,string,string)"/></para>
        /// </summary>
        /// <param name="ClientId">Unique client ID.</param>
        public RemoteSigning(string ClientId, string RDSAURL)
        {
            RDSASettings.ClientId = ClientId;
            RDSASettings.RDSAURL = RDSAURL;
            RDSASettings.IsProxyRequired = false;
        }


        /// <summary>
        /// Remote signing constructor for license path and proxy with authentication RemoteSigning
        /// (
        ///     <paramref name="ClientId"/>
        ///     <paramref name="RDSAURL"/>
        ///     <paramref name="IsProxyRequireAuth"/>
        ///     <paramref name="ProxyIP"/>
        ///     <paramref name="ProxyPort"/>
        ///     <paramref name="ProxyUserName"/>
        ///     <paramref name="ProxyPassword"/>
        /// )
        /// </summary>
        /// <param name="ClientId">Unique client ID.</param>
        /// <param name="RDSAURL">Endpoint URL of the remote signing service.</param>
        /// <param name="IsProxyRequireAuth">True if the proxy server requires authentication.</param>
        /// <param name="ProxyIP">IP Address or URL of proxy server.</param>
        /// <param name="ProxyPort">Port Number for Proxy IP.</param>
        /// <param name="ProxyUserName">UserName to be used for proxy Authentication.</param>
        /// <param name="ProxyPassword">Password for proxy Authentication.</param>
        public RemoteSigning(string ClientId, string RDSAURL, bool IsProxyRequireAuth, string ProxyIP, int ProxyPort, string ProxyUserName, string ProxyPassword)
        {
            RDSASettings.ClientId = ClientId;
            RDSASettings.RDSAURL = RDSAURL;
            RDSASettings.IsProxyRequired = true;
            RDSASettings.IsProxyRequireAuth = IsProxyRequireAuth;
            RDSASettings.ProxyIP = ProxyIP;
            RDSASettings.ProxyPort = ProxyPort;
            RDSASettings.ProxyUserName = ProxyUserName;
            RDSASettings.ProxyPassword = ProxyPassword;
        }

        /// <summary>
        /// Remote signing constructor for license path and proxy with authentication RemoteSigning
        /// (
        ///     <paramref name="KeyID"/>
        ///     <paramref name="AccessKey"/>
        ///     <paramref name="TransactionID"/>
        ///     <paramref name="TempFolder"/>
        ///     <paramref name="Inputs"/>
        /// )
        /// </summary>
        /// <param name="KeyID">Key Id provided by eMudhra.</param>
        /// <param name="AccessKey">Access Key provided by eMudhra.</param>
        /// <param name="TransactionID">Unique transaction ID to track transaction.</param>
        /// <param name="TempFolder">Absolute path of any folder for temporary file storage.</param>
        /// <param name="Inputs">List of RDSAInput, maximum 10 documents.</param>
        public ServiceReturn Sign(string KeyID, string AccessKey, string TransactionID, string TempFolder, List<RDSAInput> Inputs)
        {
            ServiceReturn serviceReturn = new ServiceReturn();
            FileStream os = null;
            List<FileStream> fileStreamsVariables = new List<FileStream>();
            try
            {
                string Timestamp = DateTimeOffset.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                int ContentEstimated = 20000;
                string OrganizationID = string.Empty;
                List<ReturnDocument> returnDocuments = new List<ReturnDocument>() { };
                TransactionID = string.IsNullOrWhiteSpace(TransactionID) ? DateTime.Now.ToString("yyyyMMddHHmmssfff") : TransactionID;
                serviceReturn.TransactionID = TransactionID;

                #region Validation
                
                if (string.IsNullOrWhiteSpace(KeyID))
                {
                    serviceReturn.ErrorCode = "RDSA-102";
                    serviceReturn.ErrorMessage = $"Invalid input KeyID is Mandatory";
                    return serviceReturn;
                }
                if (string.IsNullOrWhiteSpace(AccessKey))
                {
                    serviceReturn.ErrorCode = "RDSA-102";
                    serviceReturn.ErrorMessage = $"Invalid input AccessKey is Mandatory";
                    return serviceReturn;
                }
                if (Inputs is null)
                {
                    serviceReturn.ErrorCode = "RDSA-103";
                    serviceReturn.ErrorMessage = $"Invalid inputs cannot be null";
                    return serviceReturn;
                }
                if (Inputs.Count == 0)
                {
                    serviceReturn.ErrorCode = "RDSA-103";
                    serviceReturn.ErrorMessage = $"Inputs must contain at least one RDSAInput";
                    return serviceReturn;
                }
                if (Inputs.Count > 10)
                {
                    serviceReturn.ErrorCode = "RDSA-103";
                    serviceReturn.ErrorMessage = $"Inputs can contain maximum of only 10 RDSAInput";
                    return serviceReturn;
                }
                if (string.IsNullOrWhiteSpace(TempFolder))
                {
                    serviceReturn.ErrorCode = "RDSA-104";
                    serviceReturn.ErrorMessage = "Temp folder path cannot be empty.";
                    return serviceReturn;
                }
                if (!Directory.Exists(TempFolder))
                {
                    Directory.CreateDirectory(TempFolder);
                }
                #endregion

                OrganizationID = RDSASettings.ClientId;
                int DocID = 1;
                foreach (RDSAInput Input in Inputs)
                {
                    try
                    {
                        byte[] PDFdocument = Convert.FromBase64String(Input.DocBase64.ToString().Trim());
                        string TempFilePath = $"{TempFolder}\\{TransactionID}_{DocID}.pdf";
                        os = new FileStream(TempFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                        PdfReader reader = new PdfReader(PDFdocument);
                        PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0', null, true);
                        #region Appearance creation
                        PdfSignatureAppearance signatureAppearance = stamper.SignatureAppearance;
                        signatureAppearance.ResizeValidationMessge = Input.ResizeValidationMessage;
                        Font font = new Font(Font.FontFamily.HELVETICA, Input.SignatureFontSize, Font.NORMAL);

                        if (Input.customStyle != null)
                        {
                            font.SetColor(Input.customStyle.fontColor.R, Input.customStyle.fontColor.G, Input.customStyle.fontColor.B);
                        }
                        if (Input.ContentSearch != null)
                        {
                            TextCoordinates objtxtCoordinates = new TextCoordinates();
                            string codinate = objtxtCoordinates.GetCoordinates(reader, Input.ContentSearch.searchText, Input.ContentSearch.offset, Input.ContentSearch.height, Input.ContentSearch.width, Input.ContentSearch.position);
                            if (String.IsNullOrEmpty(codinate))
                            {
                                serviceReturn.ErrorCode = ("RDSA-120");
                                serviceReturn.ErrorMessage = ("Unable to find content.");
                                return serviceReturn;
                            }
                            Input.PageTobeSigned = Page.PAGE_LEVEL;
                            Input.PageLevelCoordinates = codinate.Remove(codinate.Length - 1);
                        }

                        if (Input.AppearanceType == AppearanceType.OneLiner)
                        {
                            signatureAppearance.TOP_SECTION = 0.0f;
                            signatureAppearance.Layer2Font = font;
                            signatureAppearance.Layer2Text = Input.OneLiner.ToString();
                            signatureAppearance.Acro6Layers = !Input.SignatureValidationMark;
                            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
                        }
                        else if (Input.AppearanceType == AppearanceType.CustomContent)
                        {
                            signatureAppearance.TOP_SECTION = 0.0f;
                            signatureAppearance.Layer2Font = font;
                            signatureAppearance.Layer2Text = Input.CustomContent.ToString();
                            signatureAppearance.Acro6Layers = !Input.SignatureValidationMark;
                            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
                        }
                        else if (Input.AppearanceType == AppearanceType.Standard)
                        {
                            StringBuilder layer2text = new StringBuilder();
                            if (!string.IsNullOrEmpty(Input.OrganizationName))
                                layer2text.Append($"For {Input.OrganizationName}\nSignature Valid\n\nDigitally Signed.\n");
                            else
                                layer2text.Append("Digitally Signed.\n");
                            if (!string.IsNullOrWhiteSpace(Input.SignedBy))
                            {
                                layer2text.Append(string.Format("Name: {0} \n", Input.SignedBy));
                                signatureAppearance.Contact = Input.SignedBy;
                            }
                            if (!string.IsNullOrWhiteSpace(Input.Reason))
                            {
                                layer2text.Append(string.Format("Reason: {0} \n", Input.Reason));
                                signatureAppearance.Reason = Input.Reason;
                            }
                            if (!string.IsNullOrWhiteSpace(Input.Location))
                            {
                                layer2text.Append(string.Format("Location: {0} \n", Input.Location));
                                signatureAppearance.Location = Input.Location;
                            }

                            layer2text.Append(string.Format("Date: {0}", DateTime.Now.AddMinutes(1).ToString("dd-MMM-yyyy HH:mm:ss")));
                            if (!string.IsNullOrWhiteSpace(Input.AdditionalInformation))
                            {
                                layer2text.Append(string.Format("\n\n{0}", Input.AdditionalInformation));
                            }
                            if (Input.CertifiedByBar)
                                signatureAppearance.TOP_SECTION = 0.2f;
                            else
                                signatureAppearance.TOP_SECTION = 0f;
                            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
                            signatureAppearance.SignDate = DateTime.Now.AddMinutes(1);
                            signatureAppearance.Layer2Text = layer2text.ToString();
                            signatureAppearance.Acro6Layers = !Input.SignatureValidationMark;
                            signatureAppearance.Layer2Font = font;

                        }
                        else if (Input.AppearanceType == AppearanceType.SignatureImage)
                        {
                            #region Layer2Text Creations
                            StringBuilder layer2text = new StringBuilder();
                            if (!string.IsNullOrEmpty(Input.OrganizationName))
                                layer2text.Append($"For {Input.OrganizationName}\nSignature Valid\n\nDigitally Signed.\n");
                            else
                                layer2text.Append("Digitally Signed.\n");
                            if (!string.IsNullOrWhiteSpace(Input.SignedBy))
                            {
                                layer2text.Append(string.Format("Name: {0} \n", Input.SignedBy));
                                signatureAppearance.Contact = Input.SignedBy;
                            }
                            if (!string.IsNullOrWhiteSpace(Input.Reason))
                            {
                                layer2text.Append(string.Format("Reason: {0} \n", Input.Reason));
                                signatureAppearance.Reason = Input.Reason;
                            }
                            if (!string.IsNullOrWhiteSpace(Input.Location))
                            {
                                layer2text.Append(string.Format("Location: {0} \n", Input.Location));
                                signatureAppearance.Location = Input.Location;
                            }

                            layer2text.Append(string.Format("Date: {0}", DateTime.Now.AddMinutes(1).ToString("dd-MMM-yyyy HH:mm:ss")));
                            #endregion

                            Image img = Image.GetInstance(Convert.FromBase64String(Input.SignatureImage));
                            signatureAppearance.SignatureRenderingMode = (string.IsNullOrEmpty(Input.SignedBy)
                                && string.IsNullOrEmpty(Input.Location)
                                && string.IsNullOrEmpty(Input.Reason)
                                && string.IsNullOrEmpty(Input.OrganizationName)) ? PdfSignatureAppearance.RenderingMode.GRAPHIC : Input.ImagePosition == SignatureImagePosition.LEFT_OF_TEXT ? PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION : PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION_RTL;

                            img.SetAbsolutePosition(0, 0);
                            Image imgFinal = img;
                            if (img.Width != img.Height)
                            {
                                //float appearanceHeight = 90 * 0.57f;
                                //float appearanceWidth = 280 * 0.57f;
                                float appearanceHeight = 280 * 0.57f;
                                float appearanceWidth = 280 * 0.87f;
                                img.ScaleAbsolute(appearanceWidth, appearanceHeight);
                                PdfTemplate templateTemp = PdfTemplate.CreateTemplate(stamper.Writer, appearanceWidth, appearanceHeight);
                                templateTemp.AddImage(img);
                                imgFinal = Image.GetInstance(templateTemp);
                                imgFinal.SetAbsolutePosition(2, 0);
                                imgFinal.ScaleAbsolute(appearanceWidth, appearanceHeight);
                            }
                            else
                            {
                                PdfTemplate templateTemp = PdfTemplate.CreateTemplate(stamper.Writer, img.Height, img.Width);
                                templateTemp.AddImage(img);
                                imgFinal = Image.GetInstance(templateTemp);
                                imgFinal.SetAbsolutePosition(2, 0);
                                imgFinal.ScaleAbsolute(img.Height, img.Width);
                            }

                            Font Fittingfont = layer2text.ToString().Length > 90 ? new Font(Font.FontFamily.HELVETICA) : new Font(Font.FontFamily.HELVETICA, 8);
                            signatureAppearance.Layer2Font = Fittingfont;
                            signatureAppearance.SignatureGraphic = imgFinal;
                            signatureAppearance.Layer2Text = layer2text.ToString();
                            signatureAppearance.Acro6Layers = !Input.SignatureValidationMark;
                        }

                        else if (Input.AppearanceType == AppearanceType.Advanced)
                        {
                            Image img = Image.GetInstance(Convert.FromBase64String(Input.advanceSignature.imagebase64));

                            img.SetAbsolutePosition(0, 0);
                            
                            PdfTemplate templateTemp = PdfTemplate.CreateTemplate(stamper.Writer, img.Height, img.Width);                           
                            ImgTemplate ImgTemplate = new ImgTemplate(templateTemp);                            
                            signatureAppearance.SignerName = (Input.advanceSignature.leftSideText);
                            signatureAppearance.Layer2Font = font;
                            signatureAppearance.Layer2Text = Input.advanceSignature.rightSideText;
                            signatureAppearance.Image = img;
                            signatureAppearance.ImageScale = -1f;
                            signatureAppearance.SignatureGraphic = (Image)ImgTemplate;
                            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.NAME_GRAPHIC_AND_DESCRIPTION;
                            signatureAppearance.Acro6Layers = true;

                        }
                        signatureAppearance.CertificationLevel = Input.CertifiedByBar ? PdfSignatureAppearance.CERTIFIED_NO_CHANGES_ALLOWED : PdfSignatureAppearance.NOT_CERTIFIED;

                        int[] pages = null;
                        ArrayList ar = null;
                        switch (Input.PageTobeSigned)
                        {
                            case Page.FIRST:
                                {
                                    pages = new int[1];
                                    pages[0] = 1;
                                }
                                break;
                            case Page.LAST:
                                {
                                    pages = new int[1];
                                    pages[0] = reader.NumberOfPages;
                                }
                                break;
                            case Page.EVEN:
                                {
                                    ar = new ArrayList();
                                    for (int i = 2; i <= reader.NumberOfPages; i = i + 2)
                                    {
                                        ar.Add(i);
                                    }
                                    pages = new int[ar.Count];
                                    for (int j = 0; j < ar.Count; j++)
                                    {
                                        pages[j] = Convert.ToInt32(ar[j]);
                                    }
                                }
                                break;
                            case Page.ODD:
                                {
                                    ar = new ArrayList();
                                    for (int i = 1; i <= reader.NumberOfPages; i = i + 2)
                                    {
                                        ar.Add(i);

                                    }
                                    pages = new int[ar.Count];
                                    for (int j = 0; j < ar.Count; j++)
                                    {
                                        pages[j] = Convert.ToInt32(ar[j]);
                                    }
                                }
                                break;
                            case Page.ALL:
                                {
                                    ar = new ArrayList();
                                    pages = new int[reader.NumberOfPages];
                                    for (int i = 0; i < reader.NumberOfPages; i++)
                                    {
                                        ar.Add(i + 1);
                                    }
                                    for (int j = 0; j < pages.Length; j++)
                                    {
                                        pages[j] = Convert.ToInt32(ar[j]);
                                    }

                                }
                                break;
                            case Page.SPECIFY:
                                string[] Pagelevel;
                                Pagelevel = Input.PageNumbers.Split(',');
                                pages = new int[Pagelevel.Length];
                                for (int j = 0; j < Pagelevel.Length; j++)
                                {
                                    pages[j] = Convert.ToInt32(Pagelevel[j]);
                                }
                                break;
                        }
                        string cuCoordinates = "";
                        switch (Input.Coordinates)
                        {
                            case Coordinates.Top_Left:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "25,725,150,810" : "25,725,150,785";
                                break;
                            case Coordinates.Top_Center:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "225 , 725,350,810" : "225 , 725,350,785";
                                break;
                            case Coordinates.Top_Right:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "425, 725, 550,810" : "425, 725, 550,785";
                                break;
                            case Coordinates.Middle_Left:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "7,485,124,443" : "7,485,124,423";
                                break;
                            case Coordinates.Middle_Right:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "468,483,590,441" : "468,483,590,421";
                                break;
                            case Coordinates.Middle_Center:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? " 243,484,365,442" : " 243,484,365,422";
                                break;
                            case Coordinates.Bottom_Left:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "7,65,125,23" : "7,65,125,3";
                                break;
                            case Coordinates.Bottom_Center:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "248,65,370,23" : "248,65,370,3";
                                break;
                            case Coordinates.Bottom_Right:
                                cuCoordinates = !string.IsNullOrWhiteSpace(Input.AdditionalInformation) ? "468,66,590,24" : "468,66,590,4";
                                break;
                        }

                        Rectangle rect = null;
                        Rectangle[] rList;
                        if (Input.PageTobeSigned == Page.PAGE_LEVEL)
                        {
                            string[] Cordinatespagelevel, Pagelevel;
                            Cordinatespagelevel = Input.PageLevelCoordinates.Split(';');
                            pages = new int[Cordinatespagelevel.Length];
                            rList = new Rectangle[Cordinatespagelevel.Length];
                            for (int i = 0; i < Cordinatespagelevel.Length; i++)
                            {
                                Pagelevel = Cordinatespagelevel[i].Split(',');
                                if (Pagelevel.Length > 1)
                                {
                                    pages[i] = Convert.ToInt32(Pagelevel[0]);
                                    rect = new Rectangle(Convert.ToInt32(Pagelevel[1]), Convert.ToInt32(Pagelevel[2]), Convert.ToInt32(Pagelevel[3]), Convert.ToInt32(Pagelevel[4]));
                                    rList[i] = rect;
                                }
                            }
                            signatureAppearance.SetVisibleSignature(rList, pages, $"SIG{DateTime.Now.Ticks}");
                        }
                        else
                        {
                            string[] Cordinatesarr = cuCoordinates.Split(',');
                            rect = new Rectangle(Convert.ToInt32(Cordinatesarr[0]), Convert.ToInt32(Cordinatesarr[1]), Convert.ToInt32(Cordinatesarr[2]), Convert.ToInt32(Cordinatesarr[3]));
                            signatureAppearance.SetVisibleSignature(new Rectangle[] { rect }, pages, $"SIG{DateTime.Now.Ticks}");
                        }
                        if (Input.IsBorderRequired)
                        {
                            signatureAppearance.GetAppearance();
                            PdfTemplate sigAppLayer2 = signatureAppearance.GetLayer(2);
                            Rectangle BorderRect = sigAppLayer2.BoundingBox;
                            sigAppLayer2.SetRGBColorFill(255, 0, 0);
                            sigAppLayer2.SetLineWidth(1);
                            sigAppLayer2.Rectangle(BorderRect.Left + 0.5f, BorderRect.Bottom + 0.5f, BorderRect.Width - 1, BorderRect.Height - 1);
                            sigAppLayer2.Stroke();
                        }
                        Dictionary<PdfName, int> exc = new Dictionary<PdfName, int>
                        {
                            [PdfName.CONTENTS] = ContentEstimated * 2 + 2
                        };
                        PdfSignature dic = new PdfSignature(PdfName.ADOBE_PPKLITE, PdfName.ADBE_PKCS7_DETACHED)
                        {
                            Reason = signatureAppearance.Reason,
                            Location = signatureAppearance.Location,
                            Contact = signatureAppearance.Contact,
                            SignatureCreator = "eMudhra",
                            Date = new PdfDate(signatureAppearance.SignDate)
                        };

                        signatureAppearance.CryptoDictionary = dic;
                        signatureAppearance.PreClose(exc);
                        Stream ostr = signatureAppearance.GetRangeStream();
                        #endregion
                        HashAlgorithm sha = new SHA256CryptoServiceProvider();
                        int read = 0;
                        byte[] buff = new byte[ContentEstimated];
                        while ((read = ostr.Read(buff, 0, ContentEstimated)) > 0)
                        {
                            sha.TransformBlock(buff, 0, read, buff, 0);
                        }
                        sha.TransformFinalBlock(buff, 0, 0);
                        byte[] hashd = Hex.Encode(sha.Hash);
                        string hashdocument = Encoding.UTF8.GetString(hashd, 0, hashd.Length);
                        returnDocuments.Add(new ReturnDocument()
                        {
                            DocumentHash = hashdocument,
                            DocId = DocID,
                            TempFilePath = TempFilePath,
                            SignatureAppreance = signatureAppearance
                        });

                        DocID++;
                        fileStreamsVariables.Add(os);
                    }
                    catch (Exception ex)
                    {
                        CloseFileStream(os);
                        DocID++;
                        returnDocuments.Add(new ReturnDocument()
                        {
                            DocId = 0,
                            ErrorMessage = $"error while creating document hash - {ex.Message}",
                            ErrorCode = "RDSA-105"
                        });
                    }

                }
                serviceReturn.ReturnDocuments = returnDocuments;
                if (returnDocuments.Where(i => i.DocId != 0).Count() <= 0)
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = "RDSA-106";
                    serviceReturn.ErrorMessage = $"Error while creating document hash, Please refer Error message in ReturnDocuments";
                    return serviceReturn;
                }
                string RequestXML = RDSAUtility.GetRequestXML(returnDocuments, KeyID, AccessKey, TransactionID, Timestamp);
                serviceReturn.RequestXML = RequestXML;
                string URLEncodedSignedXML = HttpUtility.UrlEncode(RequestXML);
                string ResponseXML = string.Empty;
                try
                {
                    ResponseXML = RDSAUtility.HttpsWebClientSendRequest(RDSASettings.RDSAURL, URLEncodedSignedXML);
                }
                catch (WebException ex)
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = "RDSA-107";
                    serviceReturn.ErrorMessage = $"Web exception while calling eSign API - {ex.Message}";
                    return serviceReturn;
                }
                if (string.IsNullOrWhiteSpace(ResponseXML))
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = "RDSA-108";
                    serviceReturn.ErrorMessage = "Empty response XML from API.";
                    return serviceReturn;
                }
                serviceReturn.ResponseXML = ResponseXML;
                XmlDocument ResponseXMLDoc = new XmlDocument();
                ResponseXMLDoc.LoadXml(ResponseXML);
                XmlNode SignRespElement = ResponseXMLDoc.SelectSingleNode("//SignDocResp");


                if (SignRespElement == null)
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = "RDSA-109";
                    serviceReturn.ErrorMessage = "Invalid ResponseXML";
                    return serviceReturn;
                }
                string status = SignRespElement?.Attributes["status"]?.Value ?? "0";
                string errorMessage = SignRespElement?.Attributes["errorMessage"]?.Value ?? "ErrorMessage attribute not found";
                string errorCode = SignRespElement?.Attributes["errorCode"]?.Value ?? "RDSA-999";
                string responseCode = SignRespElement?.Attributes["respCode"]?.Value ?? string.Empty;
                serviceReturn.ResponseCode = responseCode;
                if (status != "1")
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = errorCode;
                    serviceReturn.ErrorMessage = $"Error in response XML {errorMessage}";
                    return serviceReturn;
                }
                XmlNodeList DocumentSignatureNodes = SignRespElement.SelectNodes("//DocSignature");
                if (DocumentSignatureNodes.Count == 0)
                {
                    CloseFileStream(fileStreamsVariables);
                    serviceReturn.ErrorCode = "RDSA-110";
                    serviceReturn.ErrorMessage = "No DocSignature node in XML";
                    return serviceReturn;
                }
                List<ReturnDocument> docsToReturn = new List<ReturnDocument>() { };
                foreach (XmlNode DocumentSignature in DocumentSignatureNodes)
                {
                    int index = 0;
                    try
                    {
                        string DocumentID = DocumentSignature?.Attributes["id"]?.Value ?? "";
                        if (!int.TryParse(DocumentID, out int docId))
                        {
                            ReturnDocument returnDoc = new ReturnDocument
                            {
                                ErrorCode = "RDSA=111",
                                ErrorMessage = $"Invalid Document ID - {DocumentID} in XML",
                                SignedStatus = Status.Failure
                            };
                            docsToReturn.Add(returnDoc);
                            continue;
                        }
                        if (docId <= 0)
                        {
                            ReturnDocument returnDoc = new ReturnDocument
                            {
                                ErrorCode = "RDSA=111",
                                ErrorMessage = $"Invalid Document ID - {DocumentID} in XML",
                                SignedStatus = Status.Failure
                            };
                            docsToReturn.Add(returnDoc);
                            continue;
                        }

                        ReturnDocument returnDocument = returnDocuments.Where(i => i.DocId == docId).First();
                        if (returnDocument == null)
                        {
                            ReturnDocument returnDoc = new ReturnDocument
                            {
                                ErrorCode = "RDSA=112",
                                ErrorMessage = $"Unable to find return document for id - {DocumentID}",
                                SignedStatus = Status.Failure
                            };
                            docsToReturn.Add(returnDoc);
                            continue;
                        }

                        string ErrorCode = DocumentSignature?.Attributes["docErrorCode"]?.Value ?? "";
                        string ErrorMessage = DocumentSignature?.Attributes["docErrorMessage"]?.Value ?? "";
                        if (!string.IsNullOrWhiteSpace(ErrorCode))
                        {
                            returnDocument.ErrorCode = ErrorCode;
                            returnDocument.ErrorMessage = ErrorMessage;
                            returnDocument.SignedStatus = Status.Failure;
                            returnDocument.SignatureAppreance = null;
                            docsToReturn.Add(returnDocument);
                            continue;
                        }
                        string signedHash = DocumentSignature.InnerText;
                        PdfSignatureAppearance pdfSignatureAppearance = returnDocument.SignatureAppreance;
                        byte[] sigbytes = Convert.FromBase64String(signedHash);
                        byte[] paddedSig = new byte[ContentEstimated];
                        Array.Copy(sigbytes, 0, paddedSig, 0, sigbytes.Length);
                        PdfDictionary dic2 = new PdfDictionary();
                        dic2.Put(PdfName.CONTENTS, new PdfString(paddedSig).SetHexWriting(true));
                        pdfSignatureAppearance.Close(dic2);
                        string signedPDFBase64 = Convert.ToBase64String(File.ReadAllBytes(returnDocument.TempFilePath));
                        returnDocument.SignedDocument = signedPDFBase64;
                        returnDocument.SignedStatus = Status.Success;
                        returnDocument.SignatureAppreance = null;
                        docsToReturn.Add(returnDocument);
                        index++;
                    }
                    catch (Exception ex)
                    {
                        CloseFileStream(fileStreamsVariables[index]);
                        ReturnDocument returnDocument = new ReturnDocument
                        {
                            ErrorCode = "RDSA=113",
                            ErrorMessage = $"Unable to complete signature - {ex.Message}",
                            SignedStatus = Status.Failure
                        };
                        docsToReturn.Add(returnDocument);
                        continue;
                    }
                }
                serviceReturn.ReturnDocuments = docsToReturn;
                serviceReturn.ReturnStatus = Status.Success;
                return serviceReturn;
            }
            catch (Exception ex)
            {
                CloseFileStream(fileStreamsVariables);
                serviceReturn.ErrorCode = "RDSA-999";
                serviceReturn.ErrorMessage = ex.Message;
                return serviceReturn;
            }
        }

        public void CloseFileStream(List<FileStream> fileStreams)
        {
            foreach (var i in fileStreams)
            {
                if (i.CanWrite)
                    i.Close();
            }
        }

        public void CloseFileStream(FileStream fileStream)
        {
            if (fileStream.CanWrite)
                fileStream.Close();
        }
    }
}
