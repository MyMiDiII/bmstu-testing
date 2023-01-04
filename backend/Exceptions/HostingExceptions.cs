using System;

namespace ServerING.Exceptions {
    public class HostingConflictException: Exception {
        public HostingConflictException(int id) : 
            base(string.Format("Hosting conflict with id = {0}", id)) {}
    }
}
