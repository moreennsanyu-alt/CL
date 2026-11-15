using Xunit.Sdk;
using Xunit;

namespace ClinicManager.E2E.Tests;

public class CalculatorTests
{
    #region PairwiseThreeParameters
    [Theory, PairwiseData]
    public void CheckValidAge1(bool p1, bool p2, bool p3)
    {
        // Pairwise generates these 4 test cases:
        // false false false
        // false true  true
        // true  false true
        // true  true  false
    }
    #endregion
        
    #region CombinatorialThreeParameters
    [Theory, CombinatorialData]
    public void CheckValidAge2(bool p1, bool p2, bool p3)
    {
        // Combinatorial generates these 8 test cases:
        // false false false
        // false false true
        // false true  false
        // false true  true
        // true  false false
        // true  false true
        // true  true  false
        // true  true  true
    }
    #endregion

    #region CombinatorialTwoParameters
    [Theory, CombinatorialData]
    public void CheckValidAge3(
        [CombinatorialValues(5, 18, 21, 25)] int age,
        bool friendlyOfficer)
    {
        // This will run with all combinations:
        // 5  true
        // 18 true
        // 21 true
        // 25 true
        // 5  false
        // 18 false
        // 21 false
        // 25 false
    }
    #endregion

    #region ValuesOverRange
    [Theory, CombinatorialData]
    public void CombinatorialCustomRange(
        [CombinatorialRange(0, 5)] int p1,
        [CombinatorialRange(0, 3, 2)] int p2)
    {
        // Combinatorial generates these test cases:
        // 0 0
        // 1 0
        // 2 0
        // 3 0
        // 4 0
        // 0 2
        // 1 2
        // 2 2
        // 3 2
        // 4 2
    }
    #endregion

    #region GeneratedByMethod
    public static IEnumerable<int> GetRange(int start, int count)
    {
        return Enumerable.Range(start, count);
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromParameterizedMethods(
        [CombinatorialMemberData(nameof(GetRange), 0, 5)] int p1)
    {
        Assert.True(true);
    }
    #endregion

    #region GeneratedByProperty
    public static IEnumerable<int> IntPropertyValues
    {
        get
        {
            for (int i = 0; i < 5; i++)
            {
                yield return Random.Shared.Next();
            }
        }
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromProperties(
        [CombinatorialMemberData(nameof(IntPropertyValues))] int p1)
    {
        Assert.True(true);
    }
    #endregion

    #region GeneratedByProperty
    public static IEnumerable<int> IntPropertyValues
    {
        get
        {
            for (int i = 0; i < 5; i++)
            {
                yield return Random.Shared.Next();
            }
        }
    }

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromProperties(
        [CombinatorialMemberData(nameof(IntPropertyValues))] int p1)
    {
        Assert.True(true);
    }
    #endregion

    #region GeneratedByField
    public static readonly IEnumerable<int> IntFieldValues =
        Enumerable.Range(0, 5).Select(_ => Random.Shared.Next());

    [Theory, CombinatorialData]
    public void CombinatorialMemberDataFromFields(
        [CombinatorialMemberData(nameof(IntFieldValues))] int p2)
    {
        Assert.True(true);
    }
    #endregion

     #region CombinatorialRandomData
    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCount(
        [CombinatorialRandomData(Count = 10)] int p1)
    {
        Assert.InRange(p1, 0, int.MaxValue);
    }

    [Theory, CombinatorialData]
    public void CombinatorialRandomValuesCountMinMaxValues(
        [CombinatorialRandomData(Count = 10, Minimum = -20, Maximum = -5)] int p1)
    {
        Assert.InRange(p1, -20, -5);
    }
    #endregion
}
