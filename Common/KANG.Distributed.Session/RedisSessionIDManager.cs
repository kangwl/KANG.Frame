using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace KANG.Distributed.Session {
    public class RedisSessionIDManager : SessionIDManager {
          
        public override string CreateSessionID(System.Web.HttpContext context) {
            string theServer = "A";
            string sessionId = string.Format("{0}.{1}", theServer, base.CreateSessionID(context));
            return sessionId;
        }

        public override bool Validate(string id) {
            var arr = id.Split('.');
            if (arr.Length < 2) return false;
            var realId = arr[1];
            return base.Validate(realId);
        }
    }
}
