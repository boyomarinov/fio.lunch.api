using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fio.Lunch.API.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public byte[] Data { get; set; }
        public string Description { get; set; }
    }
}
