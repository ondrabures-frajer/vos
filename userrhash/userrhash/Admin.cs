using System;

namespace userrhash
{
    [Serializable]
    public class Admin : User
    {
        public override bool IsAdmin()
        {
            return true;
        }
    }
}
