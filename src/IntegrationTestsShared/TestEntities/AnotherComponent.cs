namespace IntegrationTestsShared.TestEntities
{
    public class AnotherComponent
    {
        protected AnotherComponent() { }

        public AnotherComponent(int number, string text)
        {
            Number = number;
            Text = text;
        }

        public int Number { get; }
        public string Text { get; }
    }
}