using Microsoft.Data.Sqlite;
using Dapper;
using Parcial1_P4_Chayanne.Models;
namespace Parcial1_P4_Chayanne.Services
{
    public class NumberService
    {
        private readonly string _conectionString;

        public NumberService(IConfiguration confle)
        {
            _conectionString = confle.GetConnectionString("DbSqlite_Ds");
        }

        private SqliteConnection createConection => new SqliteConnection(_conectionString);

        public async Task InitializeAsync()
        {
            const string query = @"CREATE TABLE IF NOT EXISTS Numeros (" +
                " Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                " Fecha TEXT NOT NULL," +
                " Numero INTEGER NOT NULL," +
                " Resultado INTEGER NOT NULL);";

            using var conexion = createConection;

            await conexion.ExecuteAsync(query);
        }
        public async Task<bool> SaveAsync(NumberRecord number)
        {
            const string query = @"INSERT INTO Numeros (Fecha, Numero, Resultado)" +
                                " VALUES (DATETIME('now'), @Numero, @Resultado)";

            using var conexion = createConection;

            int filasAfectadas = await conexion.ExecuteAsync(query, number);
            return filasAfectadas > 0;
        }
        public async Task<bool> UpdateAsync(NumberRecord number)
        {
            const string query = @"UPDATE Numeros Set" +
                                " Fecha = DATETIME('now')," +
                                " Numero = @Numero," + 
                                " Resultado = @Resultado" +
                                " WHERE Id = @Id";

            using var conexion = createConection;

            int filasAfectadas = await conexion.ExecuteAsync(query, number);
            return filasAfectadas > 0;
        }
        public async Task<NumberRecord?> GetByIdAsync(int Id)
        {
            const string query = "SELECT Id, Fecha, Numero, Resultado" +
                                " FROM Numeros WHERE Id = @Id";

            using var conexion = createConection;

            return await conexion.QueryFirstOrDefaultAsync<NumberRecord>(query, new { Id } );
        }
        public async Task<IEnumerable<NumberRecord>> GetListAsync()
        {
            const string query = "SELECT Id, Fecha, Numero, Resultado" +
                                " FROM Numeros";

            using var conexion = createConection;

            return await conexion.QueryAsync<NumberRecord>(query);
        }


        /*Preparar InitializeAsync() dentro del servicio.
	Crear SaveAsync() para guardar los cálculos.
	Crear UpdateAsync() para guardar los cálculos.
	Crear GetByIdAsync() para consultar el historial.
	Crear GetListAsync() para consultar el historial.*/
    }
}
