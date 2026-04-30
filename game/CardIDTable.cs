using System;
/*
public class FrequencyTable
{
    private CardInstantTable[] _frequencyTable = new CardInstantTable[65536];
    private void InitializeFrequencyTable()
    {
        int index = 0;
        while (index < _frequencyTable.Length)
        {
            _frequencyTable[index] = 0;
        }
    }
    public FrequencyTable()
    {
        InitializeFrequencyTable();
    }
    public int GetIndexFromID(string InstanceID)
    {
        string baseID = InstanceID[6].ToString() + InstanceID[7].ToString() + InstanceID[8].ToString() + InstanceID[10].ToString();
        long index = Convert.ToInt64(baseID, 16);
        return index;
    }
    public int GetFrequencyIncrement(string InstanceID)
    {
        string baseID = InstanceID[6].ToString() + InstanceID[7].ToString() + InstanceID[8].ToString() + InstanceID[10].ToString();
        long index = Convert.ToInt64(baseID, 16);
        _frequencyTable[index];
    }
    public int FrequencyDecrement(string InstanceID)
    {
        string baseID = InstanceID[6].ToString() + InstanceID[7].ToString() + InstanceID[8].ToString() + InstanceID[10].ToString();
        long index = Convert.ToInt64(baseID, 16);
        _frequencyTable[index]--;
    }
}
*/
