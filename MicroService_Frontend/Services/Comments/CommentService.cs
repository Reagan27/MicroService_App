using Frontend.Models;
using Frontend.Models.Comments;
using MicroService_Frontend.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Services
{
    public class CommentsService : ICommentInterface
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7216";

        public CommentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDto> CreateComment(CommentRequestDto commentRequestDto)
        {
            var request = JsonConvert.SerializeObject(commentRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Comments", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (result.Success)
            {
                return result;
            }
            return new ResponseDto();
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _httpClient.GetAsync($"{_baseUrl}/api/Comments");
            var content = await comments.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.Success)
            {
                return JsonConvert.DeserializeObject<List<Comment>>(results.Data.ToString());
            }
            return new List<Comment>();
        }


    }
}