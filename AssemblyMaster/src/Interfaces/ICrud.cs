
namespace AssemblyMaster.Interfaces
{    
    //Interface padrão de crud no banco de dados MKM
    public interface ICrud <T> where T : class
    {   
        public string Create(T model);
        public IEnumerable<T> Read();
        public T ReadId(Guid guid);
        public string Update(T t);
        public string Delete(Guid guid);
    }
}

