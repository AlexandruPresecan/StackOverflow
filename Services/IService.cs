namespace StackOverflow.Services
{
    public interface IService<K, V>
    {
        public ServiceResult Get();
        public ServiceResult Get(K id);
        public ServiceResult Post(V value);
        public ServiceResult Put(K id, V value);
        public ServiceResult Delete(K id);
    }
}
