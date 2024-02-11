using HtmlAgilityPack;
using System.Xml;

namespace MediumPostApi.utility
{
    public class TextProcessor
    {
        private static readonly string[] sourceArray = ["a", "div", "figure"];

        public static List<string> TransformText(List<object> inputText)
        {
            List<string> processedText = []; // Initialize the list properly

            foreach (object item in inputText)
            {
                string description = item.ToString();

                var doc = new HtmlDocument();
                doc.LoadHtml(description);

                foreach (HtmlNode node in doc.DocumentNode.Descendants().ToList()) // ToList() to avoid modifying collection while iterating
                {
                    if (sourceArray.Contains(node.Name.ToLower()))
                    {
                        node.ParentNode.RemoveChild(node);
                    }
                }

                // Extract specific content based on your requirements
                HtmlNode firstParagraphNode = doc.DocumentNode.SelectSingleNode("//p");
                if (firstParagraphNode != null)
                {
                    string sanitizedText = firstParagraphNode.InnerText;
                    processedText.Add(sanitizedText);
                }
                else
                {
                    processedText.Add(""); // Add empty string if no <p> tags found
                }
            }

            return processedText;
        }

    }

}
