using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopPhone.Application.Dto;
using ShopPhone.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShopPhone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;   
        }
        [HttpGet]
        public  IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post(UserDto user)
        {
            if (_userService.Add(user))
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            return Ok("Đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult Put(UserDto user)
        {
            if (_userService.Update(user))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_userService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì không tồn tại");
        }
        [HttpPost("Login")]
        public IActionResult Login(string account, string password)
        {
            //lấy user trong database
            var user = _userService.GetAll().Find(x => x.username == account
                                                    && x.Password == password);
            //nếu tồn tại user thì tạo token
            if (user != null)
            {
                //mã hóa khóa bí mật
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
                //ký vào khóa bí mật đã mã hóa
                var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                //tạo claims chứa thông tin bổ sung
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Name,account),
                    new Claim(ClaimTypes.MobilePhone,"0399654990"),
                };
                //tạo token với các thông số khớp với cấu hình trong file startup để validate
                var token = new JwtSecurityToken
                (
                    issuer: _config["Jwt:Isuser"],
                    audience: _config["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredential,
                    claims: claims
                );
                //sinh ra chuỗi token với các thông số ở trên
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user
                });
            }
            return Unauthorized();
        }
    }
}
