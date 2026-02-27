using Problem2.domain;

namespace Problem2.repository;

public interface IRepository<TId, TE> where TE: Entity<TId>
{
    /// <summary>
    /// Saves a given entity in memory
    /// </summary>
    /// <param name="entity">the entity to be saved</param>
    /// <returns>the entity, if it was saved successfully</returns>
    TE Save(TE entity);

    /// <summary>
    /// Returns all entities saved in repository
    /// </summary>
    /// <returns>an enumerable with all entities</returns>
    IEnumerable<TE> FindAll();

    /// <summary>
    /// Searches for an entity by id and returns it
    /// </summary>
    /// <param name="id">the id value</param>
    /// <returns>an entity with the given id, if exists, null otherwise</returns>
    TE? FindOneById(TId id);
}