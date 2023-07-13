using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Data;
using TrickingLibrary.Models;

namespace TrickingLibrary.Api.Controllers;

[ApiController]
[Route("api/submissions")]
public class SubmissionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubmissionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Submission> All() 
        => _context.Submissions?.ToList() ?? Enumerable.Empty<Submission>();

    [HttpGet("{id:int}")]
    public Submission Get(int id)
    {
        if (id <= 0) return default!;
        return _context.Submissions?.FirstOrDefault(t => t.Id == id) ?? new Submission();
    }
    
    
    [HttpPost]
    public async Task<Submission> Create([FromBody] Submission submission)
    {
        _context.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    [HttpPut]
    public async Task<Submission> Update([FromBody] Submission submission)
    {
        if (submission.Id == 0) return default!;

        _context.Update(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    [HttpDelete("{id:int}")]
    public async  Task<Submission> Delete(int id)
    {
        var submission = _context.Submissions?.FirstOrDefault(t => t.Id == id);
        if (submission is null)
        {
            return default!;
        }
        
        submission.Deleted = true;
        await _context.SaveChangesAsync();
        return submission;
    }
}