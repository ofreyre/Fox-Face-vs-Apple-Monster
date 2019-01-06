using UnityEditor;

namespace DBGads
{
    public class DBGadsEditor
    {
        [MenuItem("DBGads/Delete DB")]
        [MenuItem("Assets/DBGads/Delete DB")]
        public static void DeleteDB_new()
        {
            AdSDB.Delete();
        }
    }
}
