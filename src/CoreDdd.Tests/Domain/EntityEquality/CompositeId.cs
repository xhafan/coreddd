namespace CoreDdd.Tests.Domain.EntityEquality
{
    public struct CompositeId
    {
        public CompositeId(int idOne, int idTwo)
        {
            IdOne = idOne;
            IdTwo = idTwo;
        }

        public int IdOne { get; }
        public int IdTwo { get; }
    }
}