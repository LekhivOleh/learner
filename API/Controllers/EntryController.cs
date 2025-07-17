using learner.API.Interfaces.Services;
using learner.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace learner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController(IEntryService entryService) : ControllerBase
    {
        /// <summary>
        /// Get all entries.
        /// </summary>
        /// <returns>
        /// A list of entries.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            var entries = await entryService.GetAllEntriesAsync();
            return Ok(entries);
        }

        /// <summary>
        /// Get entries by subject ID.
        /// </summary>
        /// <param name="id">The subject ID.</param>
        /// <returns>
        /// A list of entries associated with the specified subject ID.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntriesBySubjectId(Guid id)
        {
            var entries = await entryService.GetEntriesBySubjectIdAsync(id);
            return Ok(entries);
        }

        /// <summary>
        /// Create a new entry.
        /// </summary>
        /// <param name="entry">The entry to create.</param>
        /// <returns>
        /// The created entry.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto entry)
        {
            var createdEntry = await entryService.CreateEntryAsync(entry);
            return CreatedAtAction(nameof(GetEntriesBySubjectId), new { id = createdEntry.SubjectId }, createdEntry);
        }

        /// <summary>
        /// Update an existing entry.
        /// </summary>
        /// <param name="id">The ID of the entry to update.</param>
        /// <param name="entry">The updated entry data.</param>
        /// <returns>
        /// The updated entry.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(Guid id, [FromBody] UpdateEntryDto entry)
        {
            var updatedEntry = await entryService.UpdateEntryAsync(id, entry);
            return Ok(updatedEntry);
        }

        /// <summary>
        /// Delete an entry.
        /// </summary>
        /// <param name="id">The ID of the entry to delete.</param>
        /// <returns>
        /// No content if the deletion was successful.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            await entryService.DeleteEntryAsync(id);
            return NoContent();
        }
    }
}