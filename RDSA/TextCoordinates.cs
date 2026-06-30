using eSign.text.pdf;
using eSign.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static RDSA.ContentSearch;

namespace RDSA
{
    public class TextCoordinates : LocationTextExtractionStrategy
    {
        public List<Coord> Coords = new List<Coord>();
        public string SearchPattern { set; get; }
        private StringBuilder ChunkText { set; get; }
        private List<TextRenderInfo> Text;

        public TextCoordinates()
        {
            Reset();
        }

        public void Reset()
        {
            BeginTextBlock();
        }
        public override void BeginTextBlock()
        {
            ChunkText = new StringBuilder();
            Text = new List<TextRenderInfo>();
        }

        public override void EndTextBlock()
        {
            string line = ChunkText.ToString();
            Regex reg = new Regex(@SearchPattern);
            MatchCollection matches = reg.Matches(line);
            foreach (Match m in matches)
            {
                Vector letterStart = Text[m.Index].GetBaseline().GetStartPoint();
                Vector letterEnd = Text[m.Index + SearchPattern.Length - 1].GetAscentLine().GetEndPoint();
                Coords.Add(new Coord { X1 = letterStart[Vector.I1], Y1 = letterStart[Vector.I2], X2 = letterEnd[Vector.I1], Y2 = letterEnd[Vector.I2] });
            }
        }
        public override void RenderText(TextRenderInfo renderInfo)
        {
            Text.AddRange(renderInfo.GetCharacterRenderInfos());
            ChunkText.Append(renderInfo.GetText());
        }

        public string GetCoordinates(PdfReader PDFReader, string TextToSearch, string Offset, int Height, int Width, Position Position)
        {
            string Coordinates = "";
            int OffX = int.Parse(Offset.Split('|')[0]);
            int OffY = int.Parse(Offset.Split('|')[1]);

            var Pr = new TextCoordinates
            {
                SearchPattern = TextToSearch
            };

            for (int i = 1; i <= PDFReader.NumberOfPages; i++)
            {
                Pr.Coords.Clear();
                PdfTextExtractor.GetTextFromPage(PDFReader, i, Pr);
                Int64 lastIndex = 1;
                if (Pr.Coords.Count > 0)
                {
                    foreach (var C in Pr.Coords)
                    {
                        if (lastIndex >= Pr.Coords.Count)
                            Coordinates = Coordinates + i + "," + GetCordFromPosition(C, Position.ToString(), OffX, OffY, Height, Width)+";";
                        else
                            Coordinates = Coordinates + i + "," + GetCordFromPosition(C, Position.ToString(), OffX, OffY, Height, Width) + ";";
                        lastIndex++;
                    }
                }
            }
            return Coordinates;
        }

        private static String GetCordFromPosition(Coord C, String Position, int OffX, int OffY, int Height, int Width)
        {
            Position = Position.ToUpper();
            switch (Position)
            {
                case "OTL":
                    {
                        return Math.Round(C.X1 + OffX - Width) + "," + Math.Round(C.Y2 + OffY) + "," + Math.Round(C.X1 + OffX) + "," + Math.Round(C.Y2 + OffY + Height);
                    }
                case "OTM":
                    {
                        return Math.Round(C.X1 + OffX + (C.X2 - C.X1 - Width) / 2) + "," + Math.Round(C.Y2 + OffY) + "," + Math.Round(C.X1 + OffX + (C.X2 - C.X1 + Width) / 2) + "," + Math.Round(C.Y2 + OffY + Height);
                    }
                case "OTR":
                    {
                        return Math.Round(C.X2 + OffX) + "," + Math.Round(C.Y2 + OffY) + "," + Math.Round(C.X2 + OffX + Width) + "," + Math.Round(C.Y2 + OffY + Height);
                    }
                case "OBL":
                    {
                        return Math.Round(C.X1 + OffX - Width) + "," + Math.Round(C.Y1 + OffY - Height) + "," + Math.Round(C.X1 + OffX) + "," + Math.Round(C.Y1 + OffY);
                    }
                case "OBM":
                    {
                        return Math.Round(C.X1 + OffX + (C.X2 - C.X1 - Width) / 2) + "," + Math.Round(C.Y1 + OffY - Height) + "," + Math.Round(C.X1 + OffX + (C.X2 - C.X1 + Width) / 2) + "," + Math.Round(C.Y1 + OffY);
                    }
                case "OBR":
                    {
                        return Math.Round(C.X2 + OffX) + "," + Math.Round(C.Y1 + OffY - Height) + "," + Math.Round(C.X2 + OffX + Width) + "," + Math.Round(C.Y1 + OffY);
                    }
                case "ITL":
                    {
                        return Math.Round(C.X1 + OffX) + "," + Math.Round(C.Y2 + OffY - Height) + "," + Math.Round(C.X1 + OffX + Width) + "," + Math.Round(C.Y2 + OffY);
                    }
                case "ITM":
                    {
                        return Math.Round(C.X1 + OffX + (C.X2 - C.X1 - Width) / 2) + "," + Math.Round(C.Y2 + OffY - Height) + "," + Math.Round(C.X1 + OffX + (C.X2 - C.X1 + Width) / 2) + "," + Math.Round(C.Y2 + OffY);
                    }
                case "ITR":
                    {
                        return Math.Round(C.X2 + OffX - Width) + "," + Math.Round(C.Y2 + OffY - Height) + "," + Math.Round(C.X2 + OffX) + "," + Math.Round(C.Y2 + OffY);
                    }
                case "IML":
                    {
                        return Math.Round(C.X1 + OffX) + "," + Math.Round(C.Y1 + OffY - (C.Y2 - C.Y1 - Height) / 2) + "," + Math.Round(C.X1 + OffX + Width) + "," + Math.Round(C.Y1 + OffY + (C.Y2 - C.Y1 + Height) / 2);
                    }
                case "IMC":
                    {
                        return Math.Round(C.X1 + OffX + (C.X2 - C.X1 - Width) / 2) + "," + Math.Round(C.Y1 + OffY + (C.Y2 - C.Y1 - Height) / 2) + "," + Math.Round(C.X1 + OffX + (C.X2 - C.X1 + Width) / 2) + "," + Math.Round(C.Y1 + OffY + (C.Y2 - C.Y1 + Height) / 2);
                    }
                case "IMR":
                    {
                        return Math.Round(C.X2 + OffX - Width) + "," + Math.Round(C.Y1 + OffY + (C.Y2 - C.Y1 - Height) / 2) + "," + Math.Round(C.X2 + OffX) + "," + Math.Round(C.Y1 + OffY + (C.Y2 - C.Y1 + Height) / 2);
                    }
                case "IBL":
                    {
                        return Math.Round(C.X1 + OffX) + "," + Math.Round(C.Y1 + OffY) + "," + Math.Round(C.X1 + OffX + Width) + "," + Math.Round(C.Y1 + OffY + Height);
                    }
                case "IBM":
                    {
                        return Math.Round(C.X1 + OffX + (C.X2 - C.X1 - Width) / 2) + "," + Math.Round(C.Y1 + OffY) + "," + Math.Round(C.X1 + OffX + (C.X2 - C.X1 + Width) / 2) + "," + Math.Round(C.Y1 + OffY + Height);
                    }
                case "IBR":
                    {
                        return Math.Round(C.X2 + OffX - Width) + "," + Math.Round(C.Y1 + OffY) + "," + Math.Round(C.X2 + OffX) + "," + Math.Round(C.Y1 + OffY + Height);
                    }
            }
            return "";
        }

        public class Coord
        {
            /// <summary>
            /// Makes signature appearance to be on Outer Top Left.
            /// </summary>
            public float X1 { get; set; }
            /// <summary>
            /// Makes signature appearance to be on Outer Top Left.
            /// </summary>
            public float Y1 { get; set; }
            /// <summary>
            /// Makes signature appearance to be on Outer Top Left.
            /// </summary>
            public float X2 { get; set; }
            /// <summary>
            /// Makes signature appearance to be on Outer Top Left.
            /// </summary>
            public float Y2 { get; set; }
        }

    }
}
