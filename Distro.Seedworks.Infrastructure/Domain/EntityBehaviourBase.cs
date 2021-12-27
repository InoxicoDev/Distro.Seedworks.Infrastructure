
namespace Distro.Seedworks.Infrastructure.Domain
{
    public abstract class EntityBehaviourBase<T>
        where T : IDomainEntity
    {        public T DomainEntity { get; set; }

        protected EntityBehaviourBase(T domainEntity)
        {
            DomainEntity = domainEntity;
        }
    }
}
