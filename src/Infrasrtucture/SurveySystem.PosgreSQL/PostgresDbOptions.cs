using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.PosgreSQL
{
    /// <summary>
    /// Конфиг проекта
    /// </summary>
    public class PostgresDbOptions
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string ConnectionString { get; set; } = default!;
    }
}
