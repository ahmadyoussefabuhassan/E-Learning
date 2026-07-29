using E_Learning.Application.InvtensivesVideos.Commands.AddInvtensiveVideo;
using E_Learning.Application.InvtensivesVideos.Commands.DeleteInvtensiveVideo;
using E_Learning.Application.InvtensivesVideos.Commands.UpdateInvtensiveVideo;
using E_Learning.Application.InvtensivesVideos.Queries.GetAllInvtensivesVideosByInvtensive;
using E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoById;
using E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoStream;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace E_Learning.Api.Controllers.InvtensivesVideos
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvtensivesVideosController : ControllerBase
    {
        private readonly ISender _sender;

        public InvtensivesVideosController(ISender sender)
            => _sender = sender;
        [HttpGet("stream/{Id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetStream(Guid Id)
        {
            var query = new GetInvtensiveVideoStreamQuery(Id);
            var result = await _sender.Send(query);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return File(result.Value, "video/mp4", enableRangeProcessing: true);
        }
        [HttpPost("AddVideo/{invtensiveId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddInvtensiveVideo(Guid invtensiveId , [FromForm] AddInvtensiveVideoRequest request, CancellationToken cancellationToken)
        {
            var command = new AddInvtensiveVideoCommand(invtensiveId, request.TitleUrl,request.VidoeUrl);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateVideo/{invtensivevideoId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateInvtensiveVideo(Guid invtensivevideoId , [FromForm] UpdateInvtensiveVideoRequest request , CancellationToken cancellationToken)
        {
            var command = new UpdateInvtensiveVideoCommand(invtensivevideoId, request.TitleUrl,request.VidoeUrl);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteVideo/{invtensivevideoId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteInvtensiveVideo(Guid invtensivevideoId , CancellationToken cancellationToken)
        {
            var command = new DeleteInvtensiveVideoCommand(invtensivevideoId);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/{invtensiveId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllInvtensivesVideos(Guid invtensiveId , CancellationToken cancellation)
        {
            var query = new GetAllInvtensivesVideosByInvtensiveQuery(invtensiveId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{invtensivevideoId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetInvtensiveVideo(Guid invtensivevideoId , CancellationToken cancellation)
        {
            var query = new GetInvtensiveVideoByIdQuery(invtensivevideoId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


    }
}
