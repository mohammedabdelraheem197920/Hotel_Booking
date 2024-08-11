namespace Core.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public List<T> GetAll(string[] include = null);

        T GetById(int? id, string[] includes = null);

        List<T> Get(Func<T, bool> where, string? include = null);
        public List<T> GetRange(Func<T, bool> where, int take, string? include = null);

        void Insert(T item);

        void Update(T item);

        void Delete(T item);

        void Save();
    }
}
