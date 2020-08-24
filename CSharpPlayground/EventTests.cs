using FluentAssertions;
using NUnit.Framework;
using System;

namespace CSharpPlayground
{
    class EventTests
    {
        [Test]
        public void PublisherSubscribersTest()
        {
            Subscriber s = new Subscriber();
            Publisher p = new Publisher();

            p.Added += s.AddedEventHandler;
            p.Add(10); // AddEvent occurred
            s.LastIndex.Should().Be(0);
            p.Add(20); // AddEvent occurred
            s.LastIndex.Should().Be(1);
        }

        [Test]
        public void PublisherMultiSubscribersTest()
        {
            Subscriber s = new Subscriber();
            Subscriber s2 = new Subscriber();
            Publisher p = new Publisher();

            p.Added += s.AddedEventHandler;
            p.Added += s2.AddedEventHandler;
            p.Add(10); // AddEvent occurred
            s.LastIndex.Should().Be(0);
            s2.LastIndex.Should().Be(0);
            p.Add(20); // AddEvent occurred
            s.LastIndex.Should().Be(1);
            s2.LastIndex.Should().Be(1);
        }

        [Test]
        public void PublisherSubscriberClearedTest()
        {
            Subscriber s = new Subscriber();
            Publisher p = new Publisher();

            p.Added += s.AddedEventHandler;
            p.Cleared += s.ClearedEventHandler;
            p.Add(10); // AddEvent occurred
            p.Add(10); // AddEvent occurred
            p.Add(10); // AddEvent occurred
            s.LastIndex.Should().Be(2);

            p.Clear(); // Cleared occurred
            s.LastIndex.Should().Be(0);
        }

    }
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(int index)
        {
            Index = index;
        }

        public int Index { get; set; }
    }
    class Publisher : System.Collections.ArrayList
    {

        public delegate void EventHandlerDelegate(object sender, CustomEventArgs e);
        public event EventHandlerDelegate Added;

        public event EventHandler Cleared;

        protected virtual void OnAdded(CustomEventArgs e)
        {
            if (Added != null) Added(this, e);
        }

        public override int Add(object value)
        {
            int i = base.Add(value);
            OnAdded(new CustomEventArgs(i)); // System.EventArgs.Empty
            return i;
        }

        public override void Clear()
        {
            base.Clear();
            if (Cleared != null)
                Cleared(this, EventArgs.Empty);
        }
    }

    class Subscriber
    {
        public int? LastIndex;
        public void AddedEventHandler(object sender, CustomEventArgs e)
        {
            LastIndex = e.Index;
        }

        public void ClearedEventHandler(object sender, EventArgs e)
        {
            LastIndex = 0;
        }
    }
    /*
    class MyApp
    {
        static void Main()
        {
            Subscriber s = new Subscriber();
            Publisher p = new Publisher();

            p.Added += s.AddedEventHandler;
            p.Add(10); // AddEvent occurred
        }
    }
    */
}
