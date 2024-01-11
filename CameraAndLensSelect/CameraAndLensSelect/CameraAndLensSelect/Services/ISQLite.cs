using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CameraAndLensSelect.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
