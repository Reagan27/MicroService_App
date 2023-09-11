using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JituPost.Models;
using JituPost.Service.IService;
using Models.Dtos;
using JituPost.Services;

namespace JituPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostServices _postService;
        private readonly ResponseDto _responseDto;

        public PostController(IMapper mapper, IPostServices postService)
        {
            _mapper = mapper;
            _postService = postService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetPostsAsync();
                if (posts == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Error Occurred";
                    return BadRequest(_responseDto);
                }

                _responseDto.Result = posts;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddPost(PostRequestDto postRequestDto)
        {
            try
            {
                var newPost = _mapper.Map<ThePost>(postRequestDto);
                var response = await _postService.CreatePostAsync(newPost);

                if (string.IsNullOrWhiteSpace(response))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Error Occurred";
                    return BadRequest(_responseDto);
                }

                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetPost(Guid id)
        {
            // try
            // {
                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post not found";
                    return NotFound(_responseDto);
                }

                _responseDto.Result = post;
            // }
            // catch (Exception ex)
            // {
            //     _responseDto.IsSuccess = false;
            //     _responseDto.Message = ex.Message;
            //     return BadRequest(_responseDto);
            // }

            return Ok(_responseDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdatePost(Guid id, PostRequestDto postRequestDto)
        {
            try
            {
                var existingPost = await _postService.GetPostByIdAsync(id);
                if (existingPost == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post not found";
                    return NotFound(_responseDto);
                }

                var updatedPost = _mapper.Map(postRequestDto, existingPost);
                var response = await _postService.UpdatePostAsync(updatedPost);

                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> DeletePost(Guid id)
        {
            try
            {
                var post = await _postService.GetPostByIdAsync(id);
                if (post == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post not found";
                    return NotFound(_responseDto);
                }

                var response = await _postService.DeletePostAsync(post);

                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }
    }
}
