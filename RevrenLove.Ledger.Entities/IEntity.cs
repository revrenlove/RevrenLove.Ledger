namespace RevrenLove.Ledger.Entities;

// TODO: JE - Move this... This interface is the only reason the dataaccessor relies on the `Entities` project.
public interface IEntity
{
    Guid Id { get; set; }
}
