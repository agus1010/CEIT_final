using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


namespace CEIT
{
	public class Range : IEnumerable<int>
	{
		private int start;
		private int finish;

		public Range(int start, int finish)
		{
			this.start = start;
			this.finish = finish;
		}

		public IEnumerator<int> GetEnumerator()
			=> new UpwardsIntRangeEnumerator(start, finish);

		IEnumerator IEnumerable.GetEnumerator()
			=> new UpwardsIntRangeEnumerator(start, finish);
	}


	public class UpwardsIntRangeEnumerator : IEnumerator<int>
	{
		private int start;
		private int finish;

		public int Length => finish - start;

		public UpwardsIntRangeEnumerator(int start, int finish)
		{
			this.start = start;
			this.finish = finish;
			Reset();
		}

		public int Current { get; private set; } = -1;
		object IEnumerator.Current => Current;

		public void Dispose() { }

		public bool MoveNext()
		{
			if (Current + 1 >= finish) return false;
			Current += 1;
			return true;
		}

		public void Reset()
		{
			Current = start - 1;
		}
	}
}