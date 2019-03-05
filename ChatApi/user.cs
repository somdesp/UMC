using System.ComponentModel.DataAnnotations;

namespace ChatApi
{
    public class User
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}