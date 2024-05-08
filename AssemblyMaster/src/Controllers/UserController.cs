using Microsoft.AspNetCore.Mvc;
using AssemblyMaster.Services;
using AssemblyMaster.Models.SwaggerExamples;

namespace AssemblyMaster.Controllers
{
    
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] UserLoginModel user)
        {
            try
            {
                var result = _userService.CreateService(user.Password, user.Email);
                if (string.IsNullOrEmpty(result)) 
                    return Ok("Usuário cadastrado com Sucesso");
                else 
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("updatepassword")]
        public IActionResult UpdatePassword([FromBody] UserLoginModel user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
            
                var result = _userService.PasswordService(user.Password, user.Email);

                if (string.IsNullOrEmpty(result))
                    return Ok(result);
                else
                    return BadRequest("Não foi possível alterar a senha do usuário.");    
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar senha do usuário: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("delete/{uuid}")]
        public ActionResult<String> Delete()
        {  
            Guid uuid = Guid.Parse(RouteData.Values["uuid"]?.ToString());
            try
            {
                var result = _userService.DeleteUserService(uuid);

                if (!string.IsNullOrEmpty(result))
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir usuário: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getusers")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.ReadService();
                if (users != null && users.Any())
                    return Ok(users);
                else
                    return NotFound("Nenhum usuário foi encontrado");
            }catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao procurar usuários: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("getguid/{uuid}")]
        public ActionResult<String> GetGUID()
        {
           Guid uuid = Guid.Parse(RouteData.Values["uuid"]?.ToString());
            try
            {
                var user = _userService.ReadGuidService(uuid);
                
                if (user != null)
                    return Ok(user);
                else
                    return NotFound("Nenhum usuário foi encontrado");
                
            }catch(Exception ex)
            {
                return StatusCode(500, $"Erro ao procurar usuário: {ex.Message}");
            }    
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {
            try
            {
                var result = _userService.LoginService(userLoginModel.Password, userLoginModel.Email); 
                if(result)
                    return Ok("Login bem sucedido.");
                else
                    return BadRequest("Email ou senha incorretos.");
            } 
            catch(Exception e)
            {
                return StatusCode(500, "Erro ao tentar efetuar login, mensagem de erro: " + e.Message);
            }
        }
    }
}