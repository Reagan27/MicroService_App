using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using JituComments.Services;
using JituComments.Models.Dtos;
using JituComments.Models;
using JituComments.Services.IService;


namespace JituPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly ResponseDto _responseDto;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                if (comments == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Error Occurred";
                    return BadRequest(_responseDto);
                }

                _responseDto.Result = comments;
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
        public async Task<ActionResult<ResponseDto>> AddComment(CommentRequestDto commentRequestDto)
        {
            try
            {
                var newComment = _mapper.Map<Comments>(commentRequestDto);
                var response = await _commentService.CreateCommentAsync(newComment);

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
        public async Task<ActionResult<ResponseDto>> GetComment(Guid id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Comment not found";
                    return NotFound(_responseDto);
                }

                _responseDto.Result = comment;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdateComment(Guid id, CommentRequestDto commentRequestDto)
        {
            try
            {
                var existingComment = await _commentService.GetCommentByIdAsync(id);
                if (existingComment == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Comment not found";
                    return NotFound(_responseDto);
                }

                var updatedComment = _mapper.Map(commentRequestDto, existingComment);
                var response = await _commentService.UpdateCommentAsync(updatedComment);

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
        public async Task<ActionResult<ResponseDto>> DeleteComment(Guid id)
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Comment not found";
                    return NotFound(_responseDto);
                }

                var response = await _commentService.DeleteCommentAsync(comment);

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

        [HttpGet("Post/{id}")]
        public async Task<ActionResult<ResponseDto>> GetCommentByPost(Guid id)
        {
            // try
            // {
                var comment = await _commentService.GetCommentByPostAsync(id);
                if (comment == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Post Comments not found";
                    return NotFound(_responseDto);
                }

                _responseDto.Result = comment;
            // }
            // catch (Exception ex)
            // {
            //     _responseDto.IsSuccess = false;
            //     _responseDto.Message = ex.Message;
            //     return BadRequest(_responseDto);
            // }

            return Ok(_responseDto);
        }
    }
}
