using learner.API.Interfaces.Services;
using learner.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace learner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectController(ISubjectService subjectService) : ControllerBase
    {
        /// <summary>
        /// Get a subject by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the subject to retrieve.
        /// </param>
        /// <returns>
        /// A subject DTO if found; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(Guid id)
        {
            var subject = await subjectService.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }

        /// <summary>
        /// Get all subjects.
        /// </summary>
        /// <returns>
        /// A list of subject DTOs.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await subjectService.GetAllAsync();
            return Ok(subjects);
        }

        /// <summary>
        /// Create a new subject.
        /// </summary>
        /// <param name="createSubjectDto">
        /// The data transfer object containing the details of the subject to create.
        /// </param>
        /// <returns>
        /// A 201 Created response with the created subject DTO.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto createSubjectDto)
        {
            var subject = await subjectService.AddAsync(createSubjectDto);
            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
        }

        /// <summary>
        /// Update an existing subject.
        /// </summary>
        /// <param name="id">
        /// The ID of the subject to update.
        /// </param>
        /// <param name="subject">
        /// The updated subject data.
        /// </param>
        /// <returns>
        /// A 204 No Content response.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] UpdateSubjectDto subject)
        {
            await subjectService.UpdateAsync(id, subject);
            return NoContent();
        }

        /// <summary>
        /// Delete a subject by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the subject to delete.
        /// </param>
        /// <returns>
        /// A 204 No Content response if the subject was deleted; otherwise, a 404 Not Found response.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            var result = await subjectService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}