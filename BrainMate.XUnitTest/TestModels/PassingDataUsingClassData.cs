using System.Collections;

namespace BrainMate.XUnitTest.TestModels
{
	public class PassingDataUsingClassData : IEnumerable<object[]>
	{
		private readonly List<object[]> data = new List<object[]>()
		{
			new object[] { 1 }
		};
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
