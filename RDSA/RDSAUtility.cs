using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace RDSA
{
    internal class RDSAUtility
    {
        internal static string HttpsWebClientSendRequest(string URI, string RequestPayload)
        {
            string result = string.Empty;
            WebClient webclient = null;
            try
            {
                //ServicePointManager.SecurityProtocol |= (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol |= (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                if (RDSASettings.IsProxyRequired)
                {
                    IWebProxy userWebProxy = new WebProxy(RDSASettings.ProxyIP, RDSASettings.ProxyPort);
                    if (!RDSASettings.IsProxyRequireAuth)
                    {
                        userWebProxy.Credentials = CredentialCache.DefaultCredentials;
                    }
                    else
                    {
                        userWebProxy.Credentials = new NetworkCredential(RDSASettings.ProxyUserName, RDSASettings.ProxyPassword);
                    }
                    webclient = new WebClient { Proxy = userWebProxy };
                }
                else
                {
                    IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
                    defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
                    webclient = new WebClient { Proxy = defaultWebProxy };
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                webclient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                result = webclient.UploadString(URI, RequestPayload);
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                webclient = null;
            }
        }
        internal static string CreateSha256Hash(string Text)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return BitConverter.ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(Text))).Replace("-", string.Empty).ToLower();
            }
        }

        internal static string GetRequestXML(List<ReturnDocument> ReturnDocuments, string KeyID, string AccessKey, string TransactionID, string Timestamp)
        {
            string ReturnDocXML = GetReturnDocXML(ReturnDocuments);
            string ReturnDocXMLBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ReturnDocXML));

            XmlDocument RequestXml = new XmlDocument();
            XmlElement SignDocReqTag = RequestXml.CreateElement("SignDocReq");

            SignDocReqTag.SetAttribute("sessionKey", "");
            SignDocReqTag.SetAttribute("version", "1.0");
            SignDocReqTag.SetAttribute("txn", TransactionID);
            SignDocReqTag.SetAttribute("ts", Timestamp);
            SignDocReqTag.SetAttribute("accessKeyhash", CreateSha256Hash($"{TransactionID}{AccessKey}{ReturnDocXMLBase64}"));
            SignDocReqTag.SetAttribute("keyID", KeyID);
            SignDocReqTag.SetAttribute("clientID", RDSASettings.ClientId);

            XmlElement DocsTag = RequestXml.CreateElement("Docs");
            DocsTag.InnerText = ReturnDocXMLBase64;
            SignDocReqTag.AppendChild(DocsTag);

            RequestXml.AppendChild(SignDocReqTag);
            return RequestXml.OuterXml;

        }
        private static string GetReturnDocXML(List<ReturnDocument> ReturnDocuments)
        {
            XmlDocument RequestXml = new XmlDocument();
            XmlElement Base64Tag = RequestXml.CreateElement("Base64");
            foreach (ReturnDocument ReturnDocument in ReturnDocuments)
            {
                if (ReturnDocument.DocId == 0)
                {
                    continue;
                }
                XmlElement InputHashTag = RequestXml.CreateElement("InputHash");
                InputHashTag.SetAttribute("hashAlgorithm", "SHA256");
                InputHashTag.SetAttribute("responseSigType", "pkcs7");
                InputHashTag.SetAttribute("id", ReturnDocument.DocId.ToString());
                InputHashTag.InnerText = ReturnDocument.DocumentHash;
                Base64Tag.AppendChild(InputHashTag);
            }
            RequestXml.AppendChild(Base64Tag);
            return RequestXml.OuterXml;
        }
    }
}
