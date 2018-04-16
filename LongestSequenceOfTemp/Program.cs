using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LongestSequence
{
  private readonly Dictionary<int, int> _tempsPerDay;
  private readonly int _daysCount;
  public LongestSequence()
  {
    _tempsPerDay = new Dictionary<int, int>();
    _daysCount = API.GetNumDays();
    for (var i = 0; i < _daysCount; i++)
    {
      _tempsPerDay[i] = API.GetTemperatureOnDay(i);
    }
  }

  /**
   * Return the start day for the longest sequence of days where the mean 
   * temperature is within five degrees.
   */
  public int GetStartDay()
  {
    var sequence = GetSequence();
    return sequence.Item1;
  }

  /**
   * Return the end day for the longest sequence of days where the mean 
   * temperature is within five degrees.
   * 
   * Note that the start and end day of the sequence may be the same.
   */
  public int GetEndDay()
  {
    var sequence = GetSequence();
    return sequence.Item2;
  }

  private Tuple<int, int> GetSequence()
  {
    var startDay = 0;
    var endDay = 0;
    var bestLenght = 0;
    for (var i = 0; i < _daysCount; i++)
    {
      for (var j = i; j < _daysCount; j++)
      {
        if (!CheckBorders(i, j))
        {
          break;
        }
        if ((j - i) > bestLenght)
        {
          bestLenght = j - i;
          startDay = i;
          endDay = j;
        }
      }
    }
    return new Tuple<int, int>(startDay, endDay);
  }

  private bool CheckBorders(int startDay, int endDay)
  {
    for (var i = startDay; i < _daysCount; i++)
    {
      var currentDayTemp = _tempsPerDay[i];
      for (var j = i; j <= endDay; j++)
      {
        if (Math.Abs(_tempsPerDay[j] - currentDayTemp) > 5)
        {
          return false;
        }
      }
    }
    return true;
  }
 
}


class Program
{
  static void Main(string[] args)
  {
    var s = new LongestSequence();
    Console.WriteLine("Start day of longest sequence: " + s.GetStartDay() + " and end day of longest sequence: " + s.GetEndDay());
    Console.Read();
  }
}
public static class API
{
  public static int GetNumDays()
  {
    return 8;
  }

  public static int GetTemperatureOnDay(int i)
  {
    var temp = new List<int>();
    temp.Add(7);
    temp.Add(12);
    temp.Add(5);
    temp.Add(3);
    temp.Add(11);
    temp.Add(6);
    temp.Add(10);
    temp.Add(2);
    temp.Add(9);
    return temp[i];
  }
}
