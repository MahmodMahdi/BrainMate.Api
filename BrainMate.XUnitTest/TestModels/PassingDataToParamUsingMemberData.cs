using System.Collections;

namespace BrainMate.XUnitTest.TestModels
{
	public class PassingDataToParamUsingMemberData : IEnumerable<object[]>
	{
		public static IEnumerable<object[]> GetParamData()
		{
			return new List<object[]>
			{
				new object[]{1}
			};
		}
		public IEnumerator<object[]> GetEnumerator()
		{
			return (IEnumerator<object[]>)GetParamData();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
