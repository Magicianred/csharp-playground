using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpPlayground
{
    class InTests
    {
        [Test]
        public void InTest()
        {
            var c = new MyClass(1000, 2000);
            c.MyField.Should().Be(1000);
            c.MyFieldByMutableParam.Should().Be(100);
        }

    }

    public class MyClass
    {
        public readonly int MyFieldByMutableParam;
        public readonly int MyField;
        public MyClass(in int initValue, int mutableValue)
        {
            //initValue = 100;
            mutableValue = 100;
            MyField = initValue;
            MyFieldByMutableParam = mutableValue;
        }
    }
}
