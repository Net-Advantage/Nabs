namespace Nabs;

public sealed class NewValueService
{
    private bool _isUnderTest;
    private Guid _testGuid;
    private DateTime _testDateTime;

    public NewValueService(bool isUnderTest = false)
    {
        _isUnderTest = isUnderTest;
    }

    public void SetTestGuid(Guid guidValue)
    {
        _testGuid = guidValue;
    }

    public void SetTestDateTime(DateTime dateTimeValue)
    {
        _testDateTime = dateTimeValue;
    }

    public Guid NewGuid()
    {
        if(!_isUnderTest)
        {
            return Guid.NewGuid();
        }

        return _testGuid;
    }

    public DateTime NewUtcNow()
    {
        if (!_isUnderTest)
        {
            return DateTime.UtcNow;
        }

        return _testDateTime;
    }

    public DateOnly NewDateOnly()
    {
        if (!_isUnderTest)
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }

        return DateOnly.FromDateTime(_testDateTime);
    }
}
