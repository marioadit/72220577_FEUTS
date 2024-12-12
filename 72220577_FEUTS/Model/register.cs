using System;

namespace _72220577_FEUTS.Model
{
    public class register
    {
        private string _email;

        // User model fields
        public string email
        {
            get => _email;
            set
            {
                _email = value;
                userName = value;  // Automatically set userName when email is set
            }
        }

        public string userName { get; private set; } // Shared between user and registeruserrole
        public string password { get; set; }
        public string fullName { get; set; }

        // Registeruserrole model fields
        public string roleName { get; set; }
    }
}
