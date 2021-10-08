using System.Threading;
using System.Threading.Tasks;

namespace Common.TimeLockAddUser
{
    public static class AddUserLock
    {
        private static bool _locker;

        static AddUserLock()
        {
            _locker = false;
        }
        
        public static bool Locker
        {
            get => _locker;
            set
            {
                if (!value) return;
                
                _locker = true;

                Task.Run(async () =>
                {
                    await Task.Delay(5000);
                    _locker = false;
                });
            }
        }
    }
}