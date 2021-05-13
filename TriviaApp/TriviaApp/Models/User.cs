using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaApp.Models
{
    class User
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public List<AmericanQuestion> Questions { get; set; }
    }
}
