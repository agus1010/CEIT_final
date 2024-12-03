using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace CEIT.TimeAndSpace
{
	public enum NamesOFPartsOfTheDay
	{
		Mañana = 8, Mediodía = 12, Tarde = 16, Noche = 22
	}


	public class PartsOfTheDay : IReadOnlyList<NamesOFPartsOfTheDay>
	{
		private static NamesOFPartsOfTheDay[] partsOfTheDayIndex = new NamesOFPartsOfTheDay[]
		{
			NamesOFPartsOfTheDay.Noche,     //0
			NamesOFPartsOfTheDay.Noche,     //1
			NamesOFPartsOfTheDay.Noche,     //2
			NamesOFPartsOfTheDay.Noche,     //3
			NamesOFPartsOfTheDay.Noche,     //4
			NamesOFPartsOfTheDay.Mañana,   //5
			NamesOFPartsOfTheDay.Mañana,   //6
			NamesOFPartsOfTheDay.Mañana,   //7
			NamesOFPartsOfTheDay.Mañana,   //8
			NamesOFPartsOfTheDay.Mañana,   //9
			NamesOFPartsOfTheDay.Mañana,   //10
			NamesOFPartsOfTheDay.Mañana,   //11
			NamesOFPartsOfTheDay.Mediodía, //12
			NamesOFPartsOfTheDay.Mediodía, //13
			NamesOFPartsOfTheDay.Mediodía, //14
			NamesOFPartsOfTheDay.Mediodía, //15
			NamesOFPartsOfTheDay.Mediodía, //16
			NamesOFPartsOfTheDay.Mediodía, //17
			NamesOFPartsOfTheDay.Tarde,   //18
			NamesOFPartsOfTheDay.Tarde,   //19
			NamesOFPartsOfTheDay.Tarde,   //20
			NamesOFPartsOfTheDay.Tarde,   //21
			NamesOFPartsOfTheDay.Tarde,   //22
			NamesOFPartsOfTheDay.Noche,     //23
		};


		public NamesOFPartsOfTheDay this[int hours]
		{
			get
			{
				int seconds = (int)System.TimeSpan.FromSeconds(hours * 60 * 60).TotalSeconds;
				int singleDaySeconds = MathUtils.ClampSecondsToSingleDay(seconds);
				int singleDayHours = singleDaySeconds * 24 / (24 * 60 * 60);
				var result = partsOfTheDayIndex[singleDayHours];
				return result;
			}
		}
		public int this[NamesOFPartsOfTheDay name] => (int)name;


		public int Count => 24;

		public IEnumerator<NamesOFPartsOfTheDay> GetEnumerator()
			=> partsOfTheDayIndex.AsEnumerable().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> partsOfTheDayIndex.AsEnumerable().GetEnumerator();
	}
}