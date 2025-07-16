using learner.API.Interfaces.Services;
using learner.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace learner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController(IEntryService entryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            var entries = await entryService.GetAllEntriesAsync();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntriesBySubjectId(Guid id)
        {
            var entries = await entryService.GetEntriesBySubjectIdAsync(id);
            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto entry)
        {
            var createdEntry = await entryService.CreateEntryAsync(entry);
            return CreatedAtAction(nameof(GetEntriesBySubjectId), new { id = createdEntry.SubjectId }, createdEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(Guid id, [FromBody] UpdateEntryDto entry)
        {
            var updatedEntry = await entryService.UpdateEntryAsync(id, entry);
            return Ok(updatedEntry);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            await entryService.DeleteEntryAsync(id);
            return NoContent();
        }
    }
}