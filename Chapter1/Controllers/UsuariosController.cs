using Chapter1.Interfaces;
using Chapter1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chapter1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;
        
        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _iUsuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
    
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception)
            {
               
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                    return NotFound();
                return Ok(usuarioEncontrado);
            
            }
            catch(Exception)
            {
                throw;
            }

        }
        [HttpPost]

        public IActionResult Cadastrar(Usuario usuario)
        {

            try
            {
                _iUsuarioRepository.Cadastrar(usuario);
                return StatusCode(201);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Atualizar(id, usuario);
                return Ok("Usuario Alterado");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _iUsuarioRepository.Deletar(id);
                return Ok("Usuario deletado");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
