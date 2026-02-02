namespace TransactionService.Domain.ValueObjects;

public sealed class TransactionId : ValueObject
{
    public Guid Value { get; }

    public TransactionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("TransactionId cannot be empty.", nameof(value));

        Value = value;
    }

    public static TransactionId New() => new(Guid.NewGuid());
    public static TransactionId From(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        //Retorna aqui os campos de igualdade
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}