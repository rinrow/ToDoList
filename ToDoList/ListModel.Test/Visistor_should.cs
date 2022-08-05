using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;

namespace ListModel.Test
{
    [TestClass]
    public class Visistor_should
    {
        [TestMethod]
        public void ZeroDoneTasksPercent()
        {
            var compos = new Target("Def");
            var task1 = new Task("task1");
            var task2 = new Task("task2");

            compos.Add(task1);
            compos.Add(task2);
            var res1 = GetDonePercent(compos, true);
            Assert.AreEqual(res1, 0);
        }

        [TestMethod]
        public void FiftyDoneTasksPercent()
        {
            var compos = new Target("Def");
            var task1 = new Task("task1");
            var task2 = new Task("task2");

            compos.Add(task1);
            compos.Add(task2);
            task1.MarkAsDone();
            var res1 = GetDonePercent(compos, true);
            Assert.AreEqual(res1, 50);
        }

        [TestMethod]
        public void HundredPercentOfAll()
        {
            var compos = new Target("Def");
            var task1 = new Task("task1");
            var task2 = new Task("task2");

            compos.Add(task1);
            compos.Add(task2);
            task1.MarkAsDone();
            task2.MarkAsDone();
            var res1 = GetDonePercent(compos, false);
            Assert.AreEqual(res1, 100);
        }

        [TestMethod]
        public void SeventyFivePercentOfAll()
        {
            var compos = new Target("Def");
            var childCompos = new Target("small");
            var task1 = new Task("task1");
            var task2 = new Task("task2");
            var task3 = new Task("task3");
            var task4 = new Task("task4");
            compos.Add(task1);
            compos.Add(task2);
            compos.Add(childCompos);
            childCompos.Add(task3);
            task2.MarkAsDone();
            task3.MarkAsDone();
            var res1 = GetDonePercent(compos, false);
            Assert.AreEqual(res1, 75);
        }

        public int GetDonePercent(ToDoComposite composite, bool isExcluseTargets)
        {
            var i = composite.CreateIterator();
            var visitor = new DoneCountVisistor();
            for (i.First(); !i.IsDone(); i.Next())
            {
                i.GetCurrent().Accept(visitor);
            }
            if (isExcluseTargets)
                return visitor.GetDoneTasksPercent();
            else
                return visitor.GetDonePercent();
        }
    }
}
