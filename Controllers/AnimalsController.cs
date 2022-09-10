using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
      private readonly AnimalShelterContext _context;

        public AnimalsController(AnimalShelterContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets list of all animals.
        /// </summary>
        /// <param></param>
        /// <returns>A list of animals</returns>
        /// <response code="200">Returns an array of all animals</response>
        /// <response code="400">Bad request</response>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        /// <summary>
        /// Gets animal by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a specific animal.</returns>
        /// <response code="200">Returns a specific animal by their id.</response>
        /// <response code="400">Bad request</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }
        /// <summary>
        /// Edits animal by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns an updated animal by id.</returns>
        /// <response code="200">This request was successful.  The database was updated with your new data.</response>
        /// <response code="400">Bad request</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        /// <summary>
        /// Adds animal to database.
        /// </summary>
        /// <returns>test</returns>
        ///  <remarks>
        ///  Sample request: 
        ///
        ///   POST/api/Animals
        ///   <example>
        ///   {
        ///      "animalId": 1,
        ///       {
        ///         "name": "Joe",
        ///         "species": "Terrier",
        ///         "age": 4,
        ///         "gender": "male"
        ///       }
        ///    }
        ///    </example>
        /// </remarks>
        /// <response code="200">Returns status 200.</response>
        /// <response code="400">Bad request</response>
      
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.AnimalId }, animal);
        }

        /// <summary>
        /// Deletes animal from the database
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        /// <returns>Response code 200.</returns>
        /// <response code="200">This request was successful.  The database has removed your selection by id.</response>
        /// <response code="400">Bad request</response>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.AnimalId == id);
        }
    }
}

