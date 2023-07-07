using Microsoft.AspNetCore.Mvc;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/videos")]
public class VideosController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public VideosController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet("{video}")]
    public IActionResult GetVideo(string video)
    {
        var savePath = Path.Combine(_env.WebRootPath, video);
        var fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
        return new FileStreamResult(fileStream, "video/*");
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadVideo(IFormFile video)
    {
        var mime = video.FileName.Split('.').Last();
        var webRootPath = _env.WebRootPath; //wwwroot path

        var filename = string.Concat(Path.GetRandomFileName(), ".", mime);
        var savePath = Path.Combine(webRootPath, filename);

        await using var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write);

        await video.CopyToAsync(fileStream);
        
        return Ok(filename);
    }
}