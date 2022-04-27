using AgendaAPI.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly Context _context;

        public ClienteController(Context context)
        {
            _context = context;
        }
        
        [HttpGet]

        public IActionResult get()
        {
            var result = _context.Clientes.ToList();
            return Ok(result);
        }
    }
}
