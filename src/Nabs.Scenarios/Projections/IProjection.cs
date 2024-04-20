namespace Nabs.Scenarios.Projections;

public interface IProjection // Marker interface
{
}

public interface IListProjection<TListItemProjection> : IProjection // Marker interface
    where TListItemProjection : IListItemProjection
{
    IEnumerable<TListItemProjection> Items { get; }
}

public interface IListItemProjection : IProjection // Marker interface
{

}

public interface IRequestItemProjection : IProjection // Marker interface
{

}

public interface IResponseItemProjection : IProjection // Marker interface
{

}
