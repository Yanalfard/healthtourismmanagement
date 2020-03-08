using System.Net;
using DashBoard.Models.Regular;

namespace DashBoard.Models.Dto
{
    public class DtoTblConfig
    {
        public int Id { get; set; }
        public string JwtKey { get; set; }

        public HttpStatusCode StatusEffect { get; set; }

        public DtoTblConfig(TblConfig config, HttpStatusCode statusEffect)
        {
            Id = config.Id;
            JwtKey = config.JwtKey;

            StatusEffect = statusEffect;
        }

        public DtoTblConfig()
        {
        }
    }
}