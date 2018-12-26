using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.UnitTest.Extension
{
    public class TheoryData<T1, T2, T3> : TheoryData
    {
        /// <summary>
        /// Adds data to the theory data set.
        /// </summary>
        /// <param name="p1">The first data value.</param>
        /// <param name="p2">The second data value.</param>
        /// <param name="p3">The third data value.</param>
        public void Add(T1 p1, T2 p2, T3 p3)
        {
            AddRow(p1, p2, p3);
        }
    }
    public abstract class TheoryData : IEnumerable<object[]>
    {
        readonly List<object[]> data = new List<object[]>();

        protected void AddRow(params object[] values)
        {
            data.Add(values);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
