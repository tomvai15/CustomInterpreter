using AutoFixture;

namespace ElPi.UnitTests
{
    public static class Dummy
    {
        private static IFixture fixture = new Fixture();

        public static T Any<T>() => fixture.Create<T>();
        public static IEnumerable<T> AnyMany<T>(int count = 3) => fixture.CreateMany<T>(count);
    }
}
