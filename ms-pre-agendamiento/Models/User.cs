using System.Text.Json.Serialization;
using ms_pre_agendamiento.Dto;

namespace ms_pre_agendamiento.Models
{
    public class User
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        
        public LoginResponse MapToUserResponse(string token)
        {
            return new LoginResponse {Id = Id, Name = Name, Token = token};
        }
    }
    
    
}