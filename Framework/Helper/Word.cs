using System.Collections.Generic;
using GemBox.Document;

namespace Framework.Helper
{
    public static class WordExtensions
    {
        public static ParagraphStyle BoldTextStyle;
        public static ParagraphStyle DefaultFontSizeStyle;
        public static ParagraphStyle RedTextStyle;

        public static DocumentModel Load(string path, int fontSize = 13)
        {
            ComponentInfo.SetLicense("DTZX-HTZ5-B7Q6-2GA6");
            DocumentModel document = DocumentModel.Load(path);
            document.DefaultCharacterFormat.FontName = "Times New Roman";
            document.DefaultCharacterFormat.Size = fontSize;

            BoldTextStyle = new ParagraphStyle("BoldText") { CharacterFormat = { Bold = true, Size = fontSize } };
            document.Styles.Add(BoldTextStyle);

            DefaultFontSizeStyle = new ParagraphStyle("DefaultFontSizeStyle") { CharacterFormat = { Size = fontSize } };
            document.Styles.Add(DefaultFontSizeStyle);

            RedTextStyle = new ParagraphStyle("RedTextStyle") { CharacterFormat = { Size = fontSize, FontColor = Color.Red } };
            document.Styles.Add(RedTextStyle);

            return document;
        }

        public static Paragraph Center(this Paragraph paragraph)
        {
            paragraph.ParagraphFormat.Alignment = HorizontalAlignment.Center;
            return paragraph;
        }

        public static Paragraph SetFontSize(this Paragraph paragraph, int size)
        {
            paragraph.CharacterFormatForParagraphMark.Size = size;
            return paragraph;
        }

        public static Paragraph SetStyle(this Paragraph paragraph, ParagraphStyle style)
        {
            paragraph.ParagraphFormat.Style = style;
            return paragraph;
        }

        public static Paragraph DisplayList(this List<string> items, DocumentModel document)
        {
            Paragraph result = null;
            List<Inline> inlineItems = new List<Inline>();

            foreach (var item in items)
            {
                inlineItems.Add(new Run(document, item.RemoveBreakLineCharacters()));
                inlineItems.Add(new SpecialCharacter(document, SpecialCharacterType.LineBreak));
            }

            result = new Paragraph(document, inlineItems.ToArray());
            return result;
        }
    }
}