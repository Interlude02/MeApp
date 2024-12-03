using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace MeApp
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Notecard>();
        }

        public async Task<List<Notecard>> GetNotecard()
        {
            return await _connection.Table<Notecard>().ToListAsync();
        }

        public async Task<Notecard> GetById(int id)
        {
            return await _connection.Table<Notecard>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Notecard notecard)
        {
            await _connection.InsertAsync(notecard);
        }

        public async Task Update(Notecard notecard)
        {
            await _connection.UpdateAsync(notecard);
        }

        public async Task Delete(Notecard notecard)
        {
            await _connection.DeleteAsync(notecard);
        }
    }
}
