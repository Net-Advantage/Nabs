using System;

namespace Nabs.Persistence
{
    public interface IRelationalEntity<TKey>
    {
        TKey Id { get; set;}
    }

    public abstract record RelationalEntityBase<TKey>(TKey Id)
    {
        protected TKey GetId()
        {
            return Id;
        }
    }

    public record Person(
        Guid Id, 
        string Username, 
        string FirstName) 
        : RelationalEntityBase<Guid>(Id);

    public static class Exx
    {
        
    }

    public class A
    {
        public A()
        {
            var p1 = new Person(Guid.NewGuid(), "dwschreyer", "Darrel");
            var p2 = new Person(p1.Id, "dwschreyer", "Darrel");

            var x = p1 == p2;

            var b = p1 as RelationalEntityBase<Guid>;


        }
    }
}