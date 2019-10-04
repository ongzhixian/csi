using System;

namespace Csi.Services
{
    public interface IProjectService<T>
    {
        bool Add(T record);
        bool Remove(T record);
        bool Update(T record);
        bool Get(T record);
    }

    public class ProjectService 
    {
        
    }
}
