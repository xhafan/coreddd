
namespace IntegrationTestsShared.TestEntities
{    
    public class Component
    {
        protected Component() {}

        public Component(int number, string text)
        {
            Number = number;
            Text = text;
            AnotherComponentOne = new AnotherComponent(number + 1, text + " 1");
            AnotherComponentTwo = new AnotherComponent(number + 2, text + " 2");
        }

        public int Number { get; }
        public string Text { get; }
        public AnotherComponent AnotherComponentOne { get; }
        public AnotherComponent AnotherComponentTwo { get; }
    }
}