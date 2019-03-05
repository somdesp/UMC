using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class UserActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OnlineID { get; set; }
        public string UserID { get; set; }
        public string ConnectionID { set; get; }
        public String Status { set; get; }
        public String Avater { set; get; }
    }
}