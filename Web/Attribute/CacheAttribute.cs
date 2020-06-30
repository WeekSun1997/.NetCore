using System;

namespace AttributeArray
{
    public class CacheAttribute : Attribute
    {
        public int invalidData { get; set; }
        public string CacheName { get; set; }
        public string CacheListName { get; set; }
        public string CacheType { get; set; }

    }

}
