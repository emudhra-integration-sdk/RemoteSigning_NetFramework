using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSA
{
    public class ContentSearch
    {
        public string searchText { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string offset { get; set; }
        public Position position { get; set; }

        public enum Position
        {
            /// <summary>
            /// Makes signature appearance to be on Outer Top Left.
            /// </summary>
            OTL,
            /// <summary>
            /// Makes signature appearance to be on Outer Top Middle.
            /// </summary>
            OTM,
            /// <summary>
            /// Makes signature appearance to be on Outer Top Right.
            /// </summary>
            OTR,
            /// <summary>
            /// Makes signature appearance to be on Outer Bottom Left.
            /// </summary>
            OBL,
            /// <summary>
            /// Makes signature appearance to be on Outer Bottom Middle.
            /// </summary>
            OBM,
            /// <summary>
            /// Makes signature appearance to be on Outer Bottom Right.
            /// </summary>
            OBR,
            /// <summary>
            /// Makes signature appearance to be on Inner Top Left.
            /// </summary>
            ITL,
            /// <summary>
            /// Makes signature appearance to be on Inner Top Middle.
            /// </summary>
            ITM,
            /// <summary>
            /// Makes signature appearance to be on Inner Top Right.
            /// </summary>
            ITR,
            /// <summary>
            /// Makes signature appearance to be on Inner Middle Left.
            /// </summary>
            IML,
            /// <summary>
            /// Makes signature appearance to be on Inner Middle Center.
            /// </summary>
            IMC,
            /// <summary>
            /// Makes signature appearance to be on Inner Middle Right.
            /// </summary>
            IMR,
            /// <summary>
            /// Makes signature appearance to be on Inner Bottom Left.
            /// </summary>
            IBL,
            /// <summary>
            /// Makes signature appearance to be on Inner Bottom Middle.
            /// </summary>
            IBM,
            /// <summary>
            /// Makes signature appearance to be on Inner Bottom Right.
            /// </summary>
            IBR
        }
    }

}
