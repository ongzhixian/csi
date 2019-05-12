using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csi.Data
{
    public interface IEntity<T> where T : struct
    {
        T Id { get; set; }
    }
    public abstract class Entity<T> where T : struct
    {
        public abstract T Id { get; set; }
    }

    public abstract class GuidEntity : IEntity<Guid>
    {
        public abstract Guid Id { get; set; }
    }



}
