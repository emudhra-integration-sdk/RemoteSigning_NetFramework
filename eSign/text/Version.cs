using System;
using System.Reflection;

namespace eSign.text
{

    /**
     * This class contains version information about iText.
     * DO NOT CHANGE THE VERSION INFORMATION WITHOUT PERMISSION OF THE COPYRIGHT HOLDERS OF ITEXT.
     * Changing the version makes it extremely difficult to debug an application.
     * Also, the nature of open source software is that you honor the copyright of the original creators of the software.
     */
    public sealed class Version
    {

        // membervariables
        /**
	     * This String contains the name of the product.
	     * iText is a registered trademark by iText Group NV.
	     * Please don't change this constant.
	     */
        static private String eSign = "eSign";

        /**
	     * This String contains the version number of this iText release.
	     * For debugging purposes, we request you NOT to change this constant.
	     */
        static private String release = "5.5.0";

        /**
         * The license key.
         */
        private String key = null;

        /**
	     * This String contains the iText version as shown in the producer line.
	     * iText is a product developed by iText Group NV.
	     * iText Group requests that you retain the iText producer line
	     * in every PDF that is created or manipulated using iText.
	     */
        private String eSignVersion = eSign + " eMudhra " + release;

        /** The iText version instance. */
        private static Version version = null;

        /**
	     * Gets an instance of the iText version that is currently used.
	     * Note that iText Group requests that you retain the iText producer line
	     * in every PDF that is created or manipulated using iText.
	     */
        public static Version GetInstance()
        {
            if (version == null)
            {
                version = new Version();
                // try {
                //              Type type = Type.GetType("RDSA.eSign.license.LicenseKey, EmdhaLibrary.eSign.LicenseKey");
                //              MethodInfo m = type.GetMethod("GetLicenseeInfo");
                //              String[] info = (String[])m.Invoke(Activator.CreateInstance(type), null);
                //              if (info[3] != null && info[3].Trim().Length > 0)
                //              {
                //                  version.key = info[3];
                //              }
                //              else
                //              {
                //                  version.key = "Trial version ";
                //                  if (info[5] == null)
                //                  {
                //                      version.key += "unauthorised";
                //                  }
                //                  else
                //                  {
                //                      version.key += info[5];
                //                  }
                //              }
                //              if (info[4] != null && info[4].Trim().Length > 0)
                //              {
                //                  version.eSignVersion = info[4];
                //              }
                //              else if (info[2] != null && info[2].Trim().Length > 0)
                //              {
                //                  version.eSignVersion += " (" + info[2];
                //                  if (!version.key.ToLower().StartsWith("trial"))
                //                  {
                //                      version.eSignVersion += "; licensed version)";
                //                  }
                //                  else
                //                  {
                //                      version.eSignVersion += "; " + version.key + ")";
                //                  }
                //              }
                //              // fall back to contact name, if company name is unavailable
                //              else if (info[0] != null && info[0].Trim().Length > 0)
                //              {
                //                  version.eSignVersion += " (" + info[0];
                //                  if (!version.key.ToLower().StartsWith("trial"))
                //                  {
                //                      // we shouldn't have a licensed version without company name,
                //                      // but let's account for it anyway
                //                      version.eSignVersion += "; licensed version)";
                //                  }
                //                  else
                //                  {
                //                      version.eSignVersion += "; " + version.key + ")";
                //                  }
                //              }
                //              else
                //              {
                //                  throw new Exception();
                //              }

                // } catch (Exception) {
                //  version.eSignVersion += "";
                // }
            }
            return version;
        }

        /**
	     * Gets the product name.
	     * iText Group requests that you retain the iText producer line
	     * in every PDF that is created or manipulated using iText.
         * @return the product name
         */
        public String Product
        {
            get
            {
                return eSign;
            }
        }

        /**
	     * Gets the release number.
	     * iText Group requests that you retain the iText producer line
	     * in every PDF that is created or manipulated using iText.
         * @return the release number
         */
        public String Release
        {
            get
            {
                return release;
            }
        }

        /**
	     * Returns the iText version as shown in the producer line.
	     * iText is a product developed by iText Group NV.
	     * iText Group requests that you retain the iText producer line
	     * in every PDF that is created or manipulated using iText.
         * @return iText version
         */
        public String GetVersion
        {
            get
            {
                return eSignVersion;
            }
        }

        /**
        * Returns a license key if one was provided, or null if not.
        * @return a license key.
        */
        public String Key
        {
            get
            {
                return key;
            }
        }

    }
}
