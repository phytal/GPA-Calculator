
namespace GPA_Calculator
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public string FullInfo
        {
            get
            {
                //William Zhang -- Zhang.W -- password1234 
                return $"{ Name } -- { Username } -- { Password }";
            }
        }
    }
}
