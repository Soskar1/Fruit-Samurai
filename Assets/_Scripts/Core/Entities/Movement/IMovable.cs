namespace Core.Entities.Movement
{
    public interface IMovable
    {
        public float Speed { get; }

        void Move(float direction);
    }
}