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
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid subject ID.");
                }
                var subject = await subjectService.GetByIdAsync(id);
                if (subject == null)
                {
                    return NotFound();
                }
                return Ok(subject);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
            try
            {
                var subjects = await subjectService.GetAllAsync();
                return Ok(subjects);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                var subject = await subjectService.AddAsync(createSubjectDto);
                return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid subject ID.");
                }
                await subjectService.UpdateAsync(id, subject);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid subject ID.");
                }
                var result = await subjectService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}