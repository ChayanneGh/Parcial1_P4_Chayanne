namespace Parcial1_P4_Chayanne.Models
{
    public record struct NumberRecord
    {
        public int Id { get; init; }
        public DateTime Fecha { get; init; }
        public int Numero { get; init; }
        public int Resultado { get; init; }
    }
}
