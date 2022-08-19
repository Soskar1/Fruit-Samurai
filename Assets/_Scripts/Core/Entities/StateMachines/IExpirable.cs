using System.Collections;

namespace Core.Entities.StateMachines
{
    public interface IExpirable
    {
        IEnumerator Expire();
    }
}