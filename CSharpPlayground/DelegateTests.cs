using FluentAssertions;
using NUnit.Framework;
using System;

namespace CSharpPlayground
{
    delegate string MyDelegate(string s);
    delegate int MyMathDelegate(int s);

    public class DelegateTests
    {
        [Test]
        public void DelegateTest()
        {
            MyDelegate d = Print;
            d("Input").Should().Be("Input");

            MyDelegate e = new MyDelegate(Print);
            e("Input").Should().Be("Input");
            e.Invoke("Input").Should().Be("Input");

            MyDelegate dnull = null;
            dnull?.Invoke("Input").Should().Be("Input");
        }

        private static string Print(string t)
        {
            return t;
        }

        [Test]
        public void DelegateAnonymousMethodTest()
        {
            MyDelegate d = delegate (string s)
            {
                return s;
            };
            d("Anonymous Method").Should().Be("Anonymous Method");
        }


        [Test]
        public void DelegateLambdaTest()
        {
            MyDelegate d = (string s) =>
            {
                return s;
            };

            MyDelegate d2 = (string s) => s;
            
            d("Expression lambda").Should().Be("Expression lambda");
            d2("Statememnt lambda").Should().Be("Statememnt lambda");
        }

        [Test]
        public void MulticastDelegateReturnValueTest()
        {
            MyMathDelegate doubleMe = (int num) =>
            {
                return num * 2;
            };

            MyMathDelegate powMe = (int num) => (int)Math.Pow(num,2);

            MyMathDelegate myDoubleMe = doubleMe;
            myDoubleMe(4).Should().Be(8);

            MyMathDelegate myPowMe = powMe;
            myPowMe(4).Should().Be(16);

            MyMathDelegate myAccumulator = doubleMe;
            myAccumulator += powMe;
            myAccumulator(4).Should().Be(16); // not 64

            MyMathDelegate myAccumulator2 = doubleMe;
            myAccumulator2 += doubleMe;
            myAccumulator2(4).Should().Be(8); // not 16

            //acc += powMe;

            //acc(2).Should().Be(16);

            //acc -= doubleMe;

            //acc(10).Should().Be(100);

        }

    }

}