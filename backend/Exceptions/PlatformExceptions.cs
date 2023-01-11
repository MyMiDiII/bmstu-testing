using System;

namespace ServerING.Exceptions {
    public class PlatformConflictException: Exception {
        public PlatformConflictException(int id) : 
            base(string.Format("Platform conflict with id = {0}", id)) {}
    }
}
