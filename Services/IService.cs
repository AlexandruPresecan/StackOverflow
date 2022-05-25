namespace StackOverflow.Services
{
    public interface IService<K, V>
    {
        public ServiceResult Get();
        public ServiceResult Get(K id);
        public ServiceResult Post(V value, HttpContext httpContext);
        public ServiceResult Put(K id, V value, HttpContext httpContext);
        public ServiceResult Delete(K id, HttpContext httpContext);
    }
}
