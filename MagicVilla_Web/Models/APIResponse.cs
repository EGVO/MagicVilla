﻿using System.Net;

namespace MagicVilla_Web.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessful { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }

        public int TotalPages { get; set; }
    }
}
