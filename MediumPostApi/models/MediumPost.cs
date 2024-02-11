using MediumPostApi.utility;

namespace MediumPostApi.models
{
    public class MediumPost
    {
        public string Title { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Desciption { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string[] Tags { get; set; } = [];

        public string GetDescription()
        {
            if (string.IsNullOrEmpty(Desciption))
            {
                return "";
            }

            return TextProcessor.TransformText([Desciption])[0];
        }
    }
}
