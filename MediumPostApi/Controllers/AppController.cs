using MediumPostApi.models;
using MediumPostApi.utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;


namespace MediumPostApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]/v1/")]
    public class AppController : ControllerBase
    {
        [HttpGet("GetUserProfile/{username}")]
        public async Task<ResponseModel<MediumUser>> GetUserProfile(string username)
        {
            var response = new ResponseModel<MediumUser>();
            
            var jsonData = await Utility.GetUserJsonData(username);

            if (jsonData.StatusCode == ResponseCode.Success && jsonData.Data != null)
            {
                response.Data = new MediumUser()
                {
                    FeedTitle = jsonData.Data.feed.title,
                    UserName = username,
                    Description = jsonData.Data.feed.description,
                    ProfileImage = jsonData.Data.feed.image,
                    ProfileLink = jsonData.Data.feed.link
                };
            }

            response.StatusCode = jsonData.StatusCode;
            response.Message = jsonData.Message;

            return response;
        }

        [HttpGet("GetPosts/{username}")]
        public async Task<ResponseModel<List<MediumPost>>> GetPosts(string username)
        {
            var response = new ResponseModel<List<MediumPost>>();
            List<MediumPost> mediumPosts = [];

            var jsonData = await Utility.GetUserJsonData(username);

            if (jsonData.StatusCode == ResponseCode.Success && jsonData.Data != null)
            {
                foreach (var item in jsonData.Data?.items)
                {
                    var post = new MediumPost
                    {
                        Title = item.title,
                        Desciption = item.description,
                        Link = item.link,
                        Author = item.author,
                        Content = item.content,
                        Date = Convert.ToDateTime(item.pubDate),
                        Tags = item.categories.ToObject<string[]>(),
                    };
                    post.Thumbnail = Utility.GetPostThumbnail(post.Desciption);

                    post.Desciption = post.GetDescription();

                    mediumPosts.Add(post);
                }
            }

            response.StatusCode = jsonData.StatusCode;
            response.Message = jsonData.Message;
            response.Data = mediumPosts;

            return response;
        }
    }
}
