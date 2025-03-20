//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Client_Invoice_System.Models; // or your custom user model
//using Client_Invoice_System.Data;

//namespace Client_Invoice_System.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _configuration;

//        public AuthController(ApplicationDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        // POST: api/Auth/Register
//        [HttpPost("Register")]
//        public async Task<IActionResult> Register([FromBody] RegisterModel model)
//        {
//            // Validate model and create user (custom logic here)
//            // For example, check if email exists, hash password, save user to DB, etc.
//            // We'll assume a simplified example here:
//            var user = new AppUser { Email = model.Email, UserName = model.Email };
//            // Save user to DB (implement your own user creation logic)
//            // _context.Users.Add(user);
//            // await _context.SaveChangesAsync();

//            return Ok(new { message = "User registered successfully" });
//        }

//        // POST: api/Auth/Login
//        [HttpPost("Login")]
//        public async Task<IActionResult> Login([FromBody] LoginModel model)
//        {
//            // Validate the user credentials (custom logic)
//            // For example, retrieve user from DB and verify password
//            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
//            if (user == null || model.Password != "yourpassword") // Replace with proper password check
//                return Unauthorized(new { message = "Invalid credentials" });

//            // Create JWT token
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new Claim[]
//                {
//                    new Claim(ClaimTypes.Name, user.Email),
//                    // Add more claims if needed
//                }),
//                Expires = DateTime.UtcNow.AddHours(2),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

//            return Ok(new { token = tokenString });
//        }

//        // You can similarly add endpoints for ForgotPassword and ResetPassword.
//    }

//    // Models for Login and Registration
//    public class LoginModel
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//    }

//    public class RegisterModel
//    {
//        public string Email { get; set; }
//        public string Password { get; set; }
//        // Add additional registration fields as needed.
//    }
//}
