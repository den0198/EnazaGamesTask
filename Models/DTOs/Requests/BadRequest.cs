using System.Collections.Generic;

namespace Models.DTOs.Requests
{
    public class BadRequest
    {
        public int Status { get; set; }
        public List<string> Errors { get; set; }

    }
}