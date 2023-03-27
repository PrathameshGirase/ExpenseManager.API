using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManager.Data;
using ExpenseManager.Models.Category;
using AutoMapper;
using ExpenseManager.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoriesController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController( IMapper mapper, ICategoriesRepository categoriesRepository)
        {
            this._mapper = mapper;
            this.categoriesRepository = categoriesRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> GetCategories()
        {
            var categories = await categoriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCategoryDto>>(categories);
            return Ok(records);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await categoriesRepository.GetDetails(id);

            if (category == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<CategoryDto>(category);

            return Ok(record);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return BadRequest();
            }

            /*_context.Entry(category).State = EntityState.Modified;*/
            var category = await categoriesRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(updateCategoryDto, category);

            try
            {
                await categoriesRepository.UpdateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryDto createCategory)
        {
            var category = _mapper.Map<Category>(createCategory);
            await categoriesRepository.AddAsync(category);

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoriesRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await categoriesRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await categoriesRepository.Exists(id);
        }
    }
}
