using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.Dtos
{
    public class JwtConfig
    {
        public TimeSpan TokenLifeTime { get; set; }
        public string SecretKey { get; set; }
    }
}
