using System;
using System.Collections.Generic;
using System.Text;

namespace MyPlace.Services.DTOs
{
    public class CompositeEntityUsersDTO
    {
        public List<MinUserDTO> EntityUsers { get; set; }
        public List<MinUserDTO> AllNotEntityUsers { get; set; }
    }
}
