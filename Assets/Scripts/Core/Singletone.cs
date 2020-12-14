using System;

namespace Core
{
    public class Singletone<T>
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Singletone is not exist");
                }

                return _instance;
            }
        }
        public Singletone()
        {
            if (_instance != null)
            {
                throw new Exception("More than one singletone");
            }
        }
        
    }
}