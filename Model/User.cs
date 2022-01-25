using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }


        [JsonIgnore]
        public string Password { get; set; }

    }
}
