using Newtonsoft.Json;

namespace DemoQA_Selenium.DataObjects
{
    public class StudentDTO
    {
        [JsonProperty("firstName")]  
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("subjects")]
        public string Subjects { get; set; }

        [JsonProperty("hobbies")]
        public string Hobbies { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("currentAddress")]
        public string CurrentAddress { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

    }

}
