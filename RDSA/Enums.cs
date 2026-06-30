namespace RDSA
{

    /// <summary>
    /// Represents signature style.
    /// </summary>
    public enum AppearanceType
    {
        /// <summary>
        /// Standard for signature appearance containing Name, Reason, Location and  Time.
        /// </summary>
        Standard,
        /// <summary>
        /// One line string as signature appearance.
        /// </summary>
        OneLiner,
        /// <summary>
        /// Image as signature appearance.
        /// </summary>
        SignatureImage,
        /// <summary>
        /// Custom Content string as signature appearance.
        /// </summary>
        CustomContent,
        /// <summary>
        /// Advanced signature appearance will contain background image and left and right side text.
        /// </summary>
        Advanced
    }

    /// <summary>
    /// Represents status of transaction.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Represents status as failure.
        /// </summary>
        Failure,
        /// <summary>
        /// Represents status as success.
        /// </summary>
        Success
    }


    /// <summary>
    /// Represents some predefined set of coordinates.
    /// </summary>
    public enum Coordinates
    {
        /// <summary>
        /// Makes signature appearance to be on Top and Left corner of Page.
        /// </summary>
        Top_Left = 1,
        /// <summary>
        /// Makes signature appearance to be on Top and Center corner of Page.
        /// </summary>
        Top_Center = 2,
        /// <summary>
        /// Makes signature appearance to be on Top and Right corner of Page.
        /// </summary>
        Top_Right = 3,
        /// <summary>
        /// Makes signature appearance to be on Middle and Left corner of Page.
        /// </summary>
        Middle_Left = 4,
        /// <summary>
        /// Makes signature appearance to be on Middle and Center corner of Page.
        /// </summary>
        Middle_Center = 5,
        /// <summary>
        /// Makes signature appearance to be on Middle and Right corner of Page.
        /// </summary>
        Middle_Right = 6,
        /// <summary>
        /// Makes signature appearance to be on Bottom and Left corner of Page.
        /// </summary>
        Bottom_Left = 7,
        /// <summary>
        /// Makes signature appearance to be on Bottom and Center corner of Page.
        /// </summary>
        Bottom_Center = 8,
        /// <summary>
        /// Makes signature appearance to be on Bottom and Right corner of Page.
        /// </summary>
        Bottom_Right = 9
    }
    public enum Page
    {
        /// <summary>
        /// Makes signature appearance to be on First Page of PDF.
        /// </summary>
        FIRST = 1,
        /// <summary>
        /// Makes signature appearance to be on Last Page of PDF.
        /// </summary>
        LAST = 2,
        /// <summary>
        /// Makes signature appearance to be on Even Page of PDF.
        /// </summary>
        EVEN = 3,
        /// <summary>
        /// Makes signature appearance to be on odd Page of PDF.
        /// </summary>
        ODD = 4,
        /// <summary>
        /// Makes signature appearance to be on All Page of PDF.
        /// </summary>
        ALL = 5,
        /// <summary>
        /// Makes signature appearance to be on Specific Page of PDF provided in PageNumbers Parameter.
        /// </summary>
        SPECIFY = 6,
        /// <summary>
        /// Makes signature appearance to be on Specific Page of PDF provided in PageLevelCoordinates Parameter.
        /// </summary>
        PAGE_LEVEL = 7
    }

    public enum ImageType
    {
        SVG, Other
    }
    public enum SignatureImagePosition
    {
        LEFT_OF_TEXT,
        RIGHT_OF_TEXT
    }
}
