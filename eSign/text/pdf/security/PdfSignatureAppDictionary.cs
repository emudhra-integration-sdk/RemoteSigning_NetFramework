namespace eSign.text.pdf.security {
    /**
     * A dictionary that stores the name of the application that signs the PDF.
     */

    internal class PdfSignatureAppDictionary : PdfDictionary {
        /** Creates new PdfSignatureAppDictionary */

        public PdfSignatureAppDictionary() : base() {
        }

        /**
         * Sets the signature created property in the Prop_Build dictionary's App
         * dictionary
         * 
         * @param name
         */

        virtual public string SignatureCreator {
            set { Put(PdfName.NAME, new PdfName(value)); }
        }

        PdfSignatureAppDictionary getPdfSignatureAppProperty()
        {
            PdfSignatureAppDictionary appPropDic = (PdfSignatureAppDictionary)GetAsDict(PdfName.APP);
            if (appPropDic == null)
            {
                appPropDic = new PdfSignatureAppDictionary();
                Put(PdfName.APP, appPropDic);
            }
            return appPropDic;
        }
    }
}
