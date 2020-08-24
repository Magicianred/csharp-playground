using FluentAssertions;
using NUnit.Framework;

namespace CSharpPlayground
{
    public class RefTests
    {

        [Test]
        public void RefTest()
        {
            MyApp m = new MyApp();
            int x = 0;    // value type
            m.Set(ref x); // pass reference to value type
            x.Should().Be(10);

        }

        [Test]
        public void RefGetterTest()
        {
            MyApp m = new MyApp();
            ref int refField = ref m.GetField();
            refField.Should().Be(5);
            refField = 6;
            refField.Should().Be(6);
            m.myField.Should().Be(6);
        }

        [Test]
        public void CopyRefGetterTest()
        {
            MyApp m = new MyApp();
           var copyField = m.GetField();
            copyField.Should().Be(5);
            copyField = 6;
            copyField.Should().Be(6);
            m.myField.Should().Be(5); // 6 <- in ref example
        }

        [Test]
        public void RefReadonlyGetterTest()
        {
            MyApp m = new MyApp();
            ref readonly int refReadonlyField = ref m.GetReadonlyField();
            refReadonlyField.Should().Be(50);
            //refReadonlyField = 60;
            //m.myReadonlyField = 60;
            refReadonlyField.Should().Be(50);
            m.myReadonlyField.Should().Be(50);

            var refReadonlyField2 = m.GetReadonlyField();
        }

    }
    class MyApp
    {
        // Ref keyword
        public void Set(ref int i) { i = 10; }

        public int myField = 5;
        public ref int GetField() { return ref myField; }

        public readonly int myReadonlyField = 50;
        public ref readonly int GetReadonlyField() { return ref myReadonlyField; }

    }

}