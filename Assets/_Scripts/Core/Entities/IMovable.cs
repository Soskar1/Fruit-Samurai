namespace Core.Entities
{
    public interface IMovable
    {
        public float Speed { get; }

        void Move(float direction);
    }
}