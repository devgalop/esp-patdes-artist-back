using CulturalEventsManagement.Shared.Domain;

namespace CulturalEventsManagement.Shared.Repositories;

public interface ICulturalEventRepository
{
    /// <summary>
    /// Guarda un evento cultural. Si el evento ya existe (basado en el Id), se actualiza; de lo contrario, se crea uno nuevo.
    /// </summary>
    /// <param name="culturalEvent">El evento cultural a guardar.</param>
    /// <returns>Tarea que representa la operación asincrónica.</returns>
    Task SaveAsync(CulturalEvent culturalEvent);
    /// <summary>
    /// Recupera todos los eventos culturales almacenados en el repositorio.
    /// </summary>
    /// <returns>Tarea que representa la operación asincrónica que devuelve una colección de eventos culturales.</returns>
    Task<IEnumerable<CulturalEvent>> GetAll();
    /// <summary>
    /// Recupera un evento cultural por su Id.
    /// </summary>
    /// <param name="id">El Id del evento cultural.</param>
    /// <returns>Tarea que representa la operación asincrónica que devuelve el evento cultural si se encuentra; de lo contrario, null.</returns>
    Task<CulturalEvent?> GetByIdAsync(Guid id);
}
