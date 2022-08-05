using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;

namespace ListModel.Test
{
    [TestClass]
    public class Iterator_should
    {
        [TestMethod]
        public void NullIteration()
        {
            var task = new Task("Something");
            var i = task.CreateIterator();
            var iterationCount = 0;
            for (i.First(); !i.IsDone(); i.Next())
                iterationCount++;

            Assert.AreEqual(0, iterationCount);
        }

        [TestMethod]
        public void SimpleIteration()
        {
            var composite = new Target("Reach target");
            composite.Add(new Task("Do something1"));
            composite.Add(new Task("Do something2"));
            var sum = "";
            var i = composite.CreateIterator();
            for (i.First(); !i.IsDone(); i.Next())
            {
                sum += i.GetCurrent().GetValue();
            }

            Assert.AreEqual("Do something1Do something2", sum);
        }

         [TestMethod]
        public void Bigteration()
        {
            var composite = new Target("Reach target");
            var red = new Target("Do somethingred");
            var blue = new Target("Do somethingblue");
            var blueCompos = new Target("BlueCompos");
            composite.Add(red);
            composite.Add(blue);
            blueCompos.Add(new Task("HardWorkBeatsTallent"));

            red.Add(new Task("Red1"));
            red.Add(new Task("Red2"));
            blue.Add(new Task("Blue1"));
            blue.Add(blueCompos);
            var sum = "";
            var i = composite.CreateIterator();
            for (i.First(); !i.IsDone(); i.Next())
            {
                sum += i.GetCurrent().GetValue();
            }
            var expepting = red.GetValue() + "Red1" + "Red2" + blue.GetValue() + "Blue1" + blueCompos.GetValue() + "HardWorkBeatsTallent";
            Assert.AreEqual(expepting, sum);
        }
    }
}
