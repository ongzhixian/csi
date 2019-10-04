using System;
using Csi.Data;

namespace Csi.Services
{
    public interface ITagService
    {
        Tag Add(Tag record);
        int Remove(int id);
        int Update(Tag record);
        Tag Get(int id);
    }

    public class TagService : ITagService
    {
        public Tag Add(Tag record)
        {
            throw new NotImplementedException();
        }

        public Tag Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Tag record)
        {
            throw new NotImplementedException();
        }
    }
}
