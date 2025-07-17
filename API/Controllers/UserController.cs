using learner.API.Interfaces.Services;
using learner.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace learner.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid user ID.");
                }
                var user = await userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
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
        /// Get a user by email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user with the specified email.</returns>
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email cannot be empty.");
                }
                var user = await userService.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
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
        /// Create a new user.
        /// </summary>
        /// <param name="createUserDto">The user to create.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var user = await userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
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
        /// Update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updateUserDto">The updated user data.</param>
        /// <returns>The updated user.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                if (id == Guid.Empty)
                {
                    return BadRequest("Invalid user ID.");
                }
                var user = await userService.UpdateUserAsync(id, updateUserDto);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>No content if the user was deleted, or not found if the user does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound();
                }
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
    }
}