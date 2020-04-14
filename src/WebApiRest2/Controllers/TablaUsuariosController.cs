using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiRest2.Models;
using WebApiRest2.Models.DTO;

namespace WebApiRest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaUsuariosController : ControllerBase
    {
        private readonly sistemaLoginContext _context;
        private readonly JWTSettings _jwtsettings;

        // se modifica el constructor para recibir la configuracion del jwt
        public TablaUsuariosController(sistemaLoginContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        // --- modificacion para logins --- //

        // POST: api/TablaUsuarios
        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login([FromBody] LoginUsrDTO  user)
        {
            // lo que esta dentro del action result, es el resultado que va a mostrar el metodo

            //se hace la consulta en bd si el usuario y clave existen

            TablaUsuarios usuario = await _context.TablaUsuarios.Where(u => u.Usuario == user.Usuario 
                                                && u.Pass  == user.Pass ).FirstOrDefaultAsync();

            LoginDTO loginDTO = new LoginDTO();

            if (usuario != null)
            {
                //RefreshToken refreshToken = GenerateRefreshToken();
                //user.RefreshTokens.Add(refreshToken);
                //await _context.SaveChangesAsync();

                //userWithToken = new UserWithToken(user);
                //userWithToken.RefreshToken = refreshToken.Token;
                loginDTO.AccessToken = GenerateAccessToken((double) usuario.IdUsuarios );
            }

            if (usuario == null)
            {
                return NotFound();
            }

            //sign your token here here..
            
            return loginDTO;
        }

        private string GenerateAccessToken(double userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId)),
                    new Claim("miValor", "Lo que yo quiera")
                }),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var peko = tokenHandler.WriteToken(token);
            if (peko == "")
            {
                return "";
            }
            else {
                return peko;
            }
            
        }

        // GET: api/TablaUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TablaUsuarios>>> GetTablaUsuarios()
        {
            return await _context.TablaUsuarios.ToListAsync();
        }

        // GET: api/TablaUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TablaUsuarios>> GetTablaUsuarios(decimal id)
        {
            var tablaUsuarios = await _context.TablaUsuarios.FindAsync(id);

            if (tablaUsuarios == null)
            {
                return NotFound();
            }

            return tablaUsuarios;
        }

        // PUT: api/TablaUsuarios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablaUsuarios(decimal id, TablaUsuarios tablaUsuarios)
        {
            if (id != tablaUsuarios.IdIntegracionAdministracion)
            {
                return BadRequest();
            }

            _context.Entry(tablaUsuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablaUsuariosExists(id))
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

        // POST: api/TablaUsuarios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TablaUsuarios>> PostTablaUsuarios(TablaUsuarios tablaUsuarios)
        {
            _context.TablaUsuarios.Add(tablaUsuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablaUsuarios", new { id = tablaUsuarios.IdIntegracionAdministracion }, tablaUsuarios);
        }

        // DELETE: api/TablaUsuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TablaUsuarios>> DeleteTablaUsuarios(decimal id)
        {
            var tablaUsuarios = await _context.TablaUsuarios.FindAsync(id);
            if (tablaUsuarios == null)
            {
                return NotFound();
            }

            _context.TablaUsuarios.Remove(tablaUsuarios);
            await _context.SaveChangesAsync();

            return tablaUsuarios;
        }

        private bool TablaUsuariosExists(decimal id)
        {
            return _context.TablaUsuarios.Any(e => e.IdIntegracionAdministracion == id);
        }
    }
}
