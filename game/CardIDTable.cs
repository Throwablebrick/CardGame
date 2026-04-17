public class CardIDTable
{
    private string[] _hashTable = new string[65536];
    private void InitializeHashTable()
    {
        int index = 0;
        while (index < _hashTable.Length())
        {
            _hashTable[index] = "";
        }
    }
    public CardIDTable()
    {
        InitializeHashTable();
    }
}