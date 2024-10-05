using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly DataContext _context;

     // Constructor with DataContext injection
        public TodoItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetIncompleteToDos()
        {
            return await _context.TodoItems
                                   .Where(t => t.CompletedDate == null)
                                   .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        //Create a Put request that takes two arguments: Id and ToDoItem and fills in the CompletedDate with the current datetime.
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)

{
    // Check if the ID in the URL matches the ID of the item in the request body
    if (id != todoItem.Id)
    {
        return BadRequest();
    }

    // Find the existing item in the database by ID
    var existingItem = await _context.TodoItems.FindAsync(id);
    if (existingItem == null)
    {
        return NotFound();
    }

    // Update the CompletedDate to the current date and time
    existingItem.CompletedDate = DateTime.Now;

    // Mark the entity as modified
    _context.Entry(existingItem).State = EntityState.Modified;

    try
    {
        // Save the changes to the database
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        // Handle concurrency issues if the item was updated by another user
        if (!TodoItemExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    // Return 204 No Content when the update is successful
    return NoContent();
}

// Helper method to check if a TodoItem exists by ID
private bool TodoItemExists(long id)
{
    return _context.TodoItems.Any(e => e.Id == id);
}

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        //Create a Post request that takes one argument of ToDoItem and creates a new ToDoItem.
        public async Task<ActionResult<TodoItem>> PostTodoItem(IEnumerable<TodoItem> todoItem)
        {
            await _context.TodoItems.AddRangeAsync(todoItem);
            await _context.SaveChangesAsync();

            return Ok(); // CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExist(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
