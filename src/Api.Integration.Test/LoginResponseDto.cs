using Newtonsoft.Json;

namespace Api.Integration.Test
{
    public class LoginResponseDto
    {
        [JsonProperty("authenticated")]
        public bool authenticated {get; set;}

        [JsonProperty("create")]
        public DateTime create {get; set;}       
        
        [JsonProperty("expiration")]
        public DateTime expiration {get; set;}  
        
        [JsonProperty("accesToken")]
        public string? accesToken {get; set;}  
        
        [JsonProperty("userName")]
        public string? userName {get; set;}  
        
        [JsonProperty("name")]
        public String? name {get; set;}     

        [JsonProperty("message")]
        public String? message {get; set;}     
    }
}