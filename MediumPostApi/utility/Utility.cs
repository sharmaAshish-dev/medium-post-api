using HtmlAgilityPack;
using MediumPostApi.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediumPostApi.utility
{
    public class Utility
    {
        private static readonly string RssToJsonUrl = "https://api.rss2json.com/v1/api.json?rss_url=";
        private static readonly string MediumDataUrl = "https://medium.com/feed/@";
        public static async Task<ResponseModel<dynamic>> GetUserJsonData(string username)
        {
            var responseModel = new ResponseModel<dynamic>();

            try
            {
                HttpClient client = new();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{RssToJsonUrl}{MediumDataUrl}{username}");

                var response = await client.SendAsync(request);

                // Read the JSON content from the response and deserialize the JSON into a dynamic object
                dynamic jsonData = JObject.Parse(await response.Content.ReadAsStringAsync());

                // Create a JsonResult object with the parsed JSON data
                responseModel.StatusCode = jsonData.status == "ok" ? ResponseCode.Success : ResponseCode.BadRequest;
                responseModel.Message = responseModel.StatusCode == ResponseCode.Success ? "OK" : "User not found, Please ensure that username is correct.";
                responseModel.Data = jsonData;

                return responseModel;
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., network errors)
                responseModel.StatusCode = ResponseCode.InternalServerError;
                responseModel.Message = ex.Message;
                return responseModel;
            }
        }

        public static string GetPostThumbnail(string text)
        {
            const string mediumUrlBlocked = "https://medium.com/_/stat?event=post";
            const string placeholderUrl = "https://placehold.jp/bdbdc2/ffffff/720x420.png?text=No%20thumbnail";

            HtmlDocument doc = new();
            doc.LoadHtml(text);

            HtmlNode imgNode = doc.DocumentNode.SelectSingleNode("//img");

            string? imageUrl = imgNode?.Attributes["src"]?.Value;

            imageUrl = (imageUrl?.Contains(mediumUrlBlocked) ?? false) ? placeholderUrl : imageUrl;

            return imageUrl ?? placeholderUrl;
        }


    }
}
