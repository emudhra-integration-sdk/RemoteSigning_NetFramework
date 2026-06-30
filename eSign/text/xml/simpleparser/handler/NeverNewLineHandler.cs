using System;
using eSign.text.xml.simpleparser;
/**
 *
 */

namespace eSign.text.xml.simpleparser.handler {

    /**
     * Always returns false.
     * @author Balder
     * @since 5.0.6
     *
     */
    public class NeverNewLineHandler : INewLineHandler {

        /*
         * (non-Javadoc)
         *
         * @see
         * com.itextpdf.text.xml.simpleparser.NewLineHandler#isNewLineTag(java.lang
         * .String)
         */
        virtual public bool IsNewLineTag(String tag) {
            return false;
        }
    }
}
