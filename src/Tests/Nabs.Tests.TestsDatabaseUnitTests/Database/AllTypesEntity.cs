namespace Nabs.Tests.TestDatabaseUnitTests.Database;

public sealed class AllTypesEntity : EntityBase<Guid>
{
    public string StringColumn { get; set; } = string.Empty;
    public string? NullableStringColumn { get; set; }

    public byte ByteColumn { get; set; }
    public byte? NullableByteColumn { get; set; }

    public sbyte SByteColumn { get; set; }
    public sbyte? NullableSByteColumn { get; set; }

    public char CharColumn { get; set; }
    public char? NullableCharColumn { get; set; }

    public decimal DecimalColumn { get; set; }
    public decimal? NullableDecimalColumn { get; set; }

    public double DoubleColumn { get; set; }
    public double? NullableDoubleColumn { get; set; }

    public float FloatColumn { get; set; }
    public float? NullableFloatColumn { get; set; }

    public int IntColumn { get; set; }
    public int? NullableIntColumn { get; set; }

    public uint UIntColumn { get; set; }
    public uint? NullableUIntColumn { get; set; }

    public long LongColumn { get; set; }
    public long? NullableLongColumn { get; set; }

    public ulong ULongColumn { get; set; }
    public ulong? NullableULongColumn { get; set; }

    public short ShortColumn { get; set; }
    public short? NullableShortColumn { get; set; }

    public ushort UShortColumn { get; set; }
    public ushort? NullableUShortColumn { get; set; }

    public bool BoolColumn { get; set; }
    public bool? NullableBoolColumn { get; set; }

    public DateTime DateTimeColumn { get; set; }
    public DateTime? NullableDateTimeColumn { get; set; }

    public DateTimeOffset DateTimeOffsetColumn { get; set; }
    public DateTimeOffset? NullableDateTimeOffsetColumn { get; set; }

    public DateOnly DateOnlyColumn { get; set; }
    public DateOnly? NullableDateOnlyColumn { get; set; }

    public TimeOnly TimeOnlyColumn { get; set; }
    public TimeOnly? NullableTimeOnlyColumn { get; set; }

    public Guid GuidColumn { get; set; }
    public Guid? NullableGuidColumn { get; set; }

    public SimpleEnum EnumColumn { get; set; }
    public SimpleEnum? NullableEnumColumn { get; set; }

    public SimpleEnum EnumAsIntColumn { get; set; }
    public SimpleEnum? NullableEnumAsIntColumn { get; set; }
}
