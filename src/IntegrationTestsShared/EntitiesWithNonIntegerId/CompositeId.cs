namespace IntegrationTestsShared.EntitiesWithNonIntegerId
{
    public struct CompositeId
    {
        public CompositeId(int idOne, string idTwo)
        {
            IdOne = idOne;
            IdTwo = idTwo;
        }

        public int IdOne { get; private set; }
        public string IdTwo { get; private set; }
    }
}