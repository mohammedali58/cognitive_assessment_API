using APP.Application.Common;
using Application.DTOs;
using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APP.Controllers;

[ApiController]
[Route("journals")]
[Authorize]
public class JournalController : ControllerBase
{
    private readonly IJournalRepository _journalRepo;
    private readonly IJournalScoringService _scoringService;

    public JournalController(IJournalRepository journalRepo, IJournalScoringService scoringService)
    {
        _journalRepo = journalRepo;
        _scoringService = scoringService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit(JournalEntryDto dto)
    {
        var score = await _scoringService.ScoreTextAsync(dto.Text);

        var entry = new JournalEntry
        {
            Text = dto.Text,
            Score = score,
        };

        await _journalRepo.AddAsync(entry);
        return Ok(new { entry.Id, entry.Score });
    }

    [HttpGet("{id}/score")]
    public async Task<IActionResult> GetJournalById(int id)
    {
        var entries = await _journalRepo.GetJournalByIdAsync(id);
    if (entries is null) return NotFound($"no journals found with id = {id}");
        return Ok(entries);
    }
}
