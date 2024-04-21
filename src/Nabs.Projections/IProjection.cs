namespace Nabs.Projections;

public interface IProjection; // Marker interface

public interface IListProjection<TListItemProjection> : IProjection
    where TListItemProjection : IListItemProjection
{
    IEnumerable<TListItemProjection> Items { get; }
}

public interface IListItemProjection : IProjection;

public interface IRequestItemProjection : IProjection;

public interface IResponseItemProjection : IProjection;
