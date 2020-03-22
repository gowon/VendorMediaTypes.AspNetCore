namespace WebApiExample.Models
{
    using System;

    public class DetailedPong : Pong
    {
        public Guid Id { get; set; }
        public string InstanceId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}