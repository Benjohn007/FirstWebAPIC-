using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.Models;

namespace TodoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDosController : Controller
    {
        private readonly ToDoListApiDbContext dbContext;

        public ToDosController(ToDoListApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllToDos()
        //{
        //    return Ok(await dbContext.ToDoAPIs.ToListAsync());
        //}


        [HttpGet]
        public async Task<IActionResult> GetToDos([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await dbContext.ToDoAPIs
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await dbContext.ToDoAPIs.CountAsync();
            return Ok(new PagedResponse<List<ToDoAPI>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
        }


        //[HttpGet]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        //{
        //    var todo = await dbContext.ToDoAPIs.FindAsync(id);
        //    if(todo == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(todo);
        //}

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.ToDoAPIs.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(new Response<ToDoAPI>(todo));
        }

        [HttpPost]
        public async Task<IActionResult> AddToDos(AddToDoRequest addToDoRequest)
        {
            var todo = new ToDoAPI()
            {
                Id = Guid.NewGuid(),
                Task = addToDoRequest.Task,
                Priority = addToDoRequest.Priority,
                Category = addToDoRequest.Category,
            };
             await dbContext.ToDoAPIs.AddAsync(todo);
             await dbContext.SaveChangesAsync();

            return Ok(todo);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateToDos([FromRoute] Guid id,UpdateToDoRequest updateToDoRequest)
        {
            var todo = await dbContext.ToDoAPIs.FindAsync(id);
            if(todo != null)
            {
                todo.Priority = updateToDoRequest.Priority;
                todo.Category = updateToDoRequest.Category; 
                todo.Task = updateToDoRequest.Task;

                await dbContext.SaveChangesAsync();
                return Ok(todo);

            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] Guid id)
        {
            var todo = await dbContext.ToDoAPIs.FindAsync(id);
            if( todo != null)
            {
                dbContext.ToDoAPIs.Remove(todo);
               await dbContext.SaveChangesAsync();
                return Ok(todo);
            }
            return NotFound();
        }
    }
}
