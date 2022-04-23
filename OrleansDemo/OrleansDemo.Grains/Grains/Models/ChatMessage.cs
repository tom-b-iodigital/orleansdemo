using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain.Grains.Models
{
    public class ChatMessage
    {
        public DateTime PostedAt { get; set; }
        public string LocationId { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
