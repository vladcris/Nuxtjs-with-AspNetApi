using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/tricks")]
public class TricksController : ControllerBase
{
    private readonly TrickStore _trickStore;

    public TricksController(TrickStore trickStore)
    {
        _trickStore = trickStore;
    }

    [HttpGet]
    public IActionResult All() => Ok(_trickStore.All);

    [HttpGet("{id:int}")]
    public IActionResult Get(int id) => Ok(_trickStore.All.FirstOrDefault((t => t.Id == id)));

    [HttpPost]
    public IActionResult Create([FromBody] Trick trick)
    {
        _trickStore.Add(trick);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] Trick trick)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
        => throw new NotImplementedException();

}