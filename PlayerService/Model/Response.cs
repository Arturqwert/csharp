using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerService.PlayerService.Model
{
    internal class Response<T>
    {
        public Statuses Status { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }

        public Response(Statuses status, string message, T payload)
        {
            this.Status = status;
            this.Message = message;
            this.Payload = payload;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
