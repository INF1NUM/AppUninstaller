using System;
using System.Collections;
using System.Windows.Forms;

namespace AppUninstaller
{
    class ItemComparer : IComparer
    {
        int columnIndex = 0;
        bool sortAscending = true;
        //Это свойство инициализируется при каждом клике на column header'e
        public int ColumnIndex
        {
            set
            {
                //предыдущий клик был на этой же колонке?
                if (columnIndex == value)
                    //да - меняем направление сортировки
                    sortAscending = !sortAscending;
                else
                {
                    columnIndex = value;
                    sortAscending = true;
                }
            }
        }
        //здесь непосредственно производится сравнение
        //возвращаемые значения:
        // < 0 если x < y
        // 0 если x = y
        // > 0 если x > y
        public int Compare(object x, object y)
        {
            string value1 = ((ListViewItem)x).SubItems[columnIndex].Text;
            string value2 = ((ListViewItem)y).SubItems[columnIndex].Text;
            return String.Compare(value1, value2) * (sortAscending ? 1 : -1);
        }
    }
}
