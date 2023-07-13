using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/tricks")]
public class TricksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TricksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Trick> All() 
        => _context.Tricks?.ToList() ?? Enumerable.Empty<Trick>();

    [HttpGet("{id}")]
    public Trick Get(string id)
    {
        if (string.IsNullOrEmpty(id)) return default!;
        return _context.Tricks?
            .FirstOrDefault(t => t.Id!.Equals(id, StringComparison.InvariantCultureIgnoreCase)) ?? new Trick();
    }

    [HttpGet("{trickId}/submissions")]
    public IEnumerable<Submission> ListSubmissionsForTrick(string trickId)
        => _context.Submissions?
            .Where(s => s.TrickId!.Equals(trickId, StringComparison.InvariantCultureIgnoreCase))
            .ToList() ?? Enumerable.Empty<Submission>();        

    
    [HttpPost]
    public async Task<Trick> Create([FromBody] Trick trick)
    {
        trick.Id = trick.Name!.Replace(" ", "-").ToLowerInvariant();
        _context.Add(trick);
        await _context.SaveChangesAsync();
        return trick;
    }

    [HttpPut]
    public async Task<Trick> Update([FromBody] Trick trick)
    {
        if (string.IsNullOrEmpty(trick.Id)) return default!;

        _context.Update(trick);
        await _context.SaveChangesAsync();
        return trick;
    }

    [HttpDelete("{id}")]
    public async  Task<Trick> Delete(string id)
    {
        var trick = _context.Tricks?.FirstOrDefault(t => t.Id == id);
        if (trick is null)
        {
            return default!;
        }
        
        trick.Deleted = true;
        await _context.SaveChangesAsync();
        return trick;
    }
}