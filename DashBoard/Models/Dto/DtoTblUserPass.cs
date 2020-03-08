using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblUserPass
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsHelthApple { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblUserPass(TblUserPass userPass, HttpStatusCode statusEffect)
        {
            Id = userPass.Id;
            Username = userPass.Username;
            Password = userPass.Password;
            IsHelthApple = userPass.IsHelthApple;

            StatusEffect = statusEffect;
        }

        public DtoTblUserPass()
        {
        }
    }
}