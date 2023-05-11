namespace Food_Ordering.Areas.Users.Models
{
    public class UsersModel
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
