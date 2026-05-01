using System;

public class FrequencyTable
{
    private CardInstantTable[] _frequencyTable;
    public FrequencyTable()
    {
        _frequencyTable = new CardInstantTable[65536];
        InitializeFrequencyTable();
    }
    public FrequencyTable(int frequencyTableSize, int cardInstantTableSize)
    {
        _frequencyTable = new CardInstantTable[frequencyTableSize];
        InitializeFrequencyTable(cardInstantTableSize);

    }
    private void InitializeFrequencyTable()
    {
        int index = 0;
        while (index < _frequencyTable.Length)
        {
            _frequencyTable[index] = new CardInstantTable(360);
        }
    }
    private void InitializeFrequencyTable(int tableSize)
    {
        int index = 0;
        while (index < _frequencyTable.Length)
        {
            _frequencyTable[index] = new CardInstantTable(tableSize);
        }
    }
    public int GetIndexFromID(string InstanceID)
    {
        string baseID = InstanceID[6].ToString() + InstanceID[7].ToString() + InstanceID[8].ToString() + InstanceID[9].ToString();
        int index = Convert.ToInt32(baseID, 16);
        return index;
    }
    public string GetModificationFromID(string instanceID)
    {
        return instanceID[0].ToString() + instanceID[1].ToString() + instanceID[2].ToString() + instanceID[3].ToString() + instanceID[4].ToString() + instanceID[5].ToString();
    }
    public void AddCardToTable(string instanceID)
    {
        string baseID = instanceID[6].ToString() + instanceID[7].ToString() + instanceID[8].ToString() + instanceID[10].ToString();
        int index = Convert.ToInt32(baseID, 16);
        _frequencyTable[index].AddInstanceToCardTable(GetModificationFromID(instanceID));
    }
    public void RemoveCardFromTable(string instanceID)
    {
        string baseID = instanceID[6].ToString() + instanceID[7].ToString() + instanceID[8].ToString() + instanceID[10].ToString();
        int index = Convert.ToInt32(baseID, 16);
        _frequencyTable[index].RemoveInstanceFromTheCardTable(GetModificationFromID(instanceID));
    }
}
