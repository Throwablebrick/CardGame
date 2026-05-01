using System;

public class FrequencyTable
{
    //our hash table
    private CardInstanceTable[] _frequencyTable;
    public FrequencyTable()
    {
        //default size for possible card ids
        _frequencyTable = new CardInstanceTable[65536];
        InitializeFrequencyTable();
    }
    public FrequencyTable(int frequencyTableSize, int cardInstantTableSize)
    {
        //custom sizes for testing purposes
        _frequencyTable = new CardInstanceTable[frequencyTableSize];
        InitializeFrequencyTable(cardInstantTableSize);

    }
    private void InitializeFrequencyTable()
    {
        //default settings for intializing all of the card ids
        int index = 0;
        while (index < _frequencyTable.Length)
        {
            _frequencyTable[index] = new CardInstanceTable();
        }
    }
    private void InitializeFrequencyTable(int tableSize)
    {
        //custom intialization size
        int index = 0;
        while (index < _frequencyTable.Length)
        {
            _frequencyTable[index] = new CardInstanceTable(tableSize);
        }
    }
    //grabs what the card ID is from an instance ID, acts as a hash function for our hash table
    public int GetIndexFromID(string InstanceID)
    {
        string baseID = InstanceID[6].ToString() + InstanceID[7].ToString() + InstanceID[8].ToString() + InstanceID[9].ToString();
        int index = Convert.ToInt32(baseID, 16);
        return index;
    }
    //grabs what the mod ID is from an instance ID
    public string GetModificationFromID(string instanceID)
    {
        return instanceID[0].ToString() + instanceID[1].ToString() + instanceID[2].ToString() + instanceID[3].ToString() + instanceID[4].ToString() + instanceID[5].ToString();
    }
    //adds a card to the frequency table
    public void AddCardToTable(string instanceID)
    {
        int baseID = GetIndexFromID(instanceID);
        _frequencyTable[baseID].AddInstanceToCardTable(GetModificationFromID(instanceID));
    }
    //removes a card to the frequency table
    public void RemoveCardFromTable(string instanceID)
    {
        int baseID = GetIndexFromID(instanceID);
        _frequencyTable[baseID].RemoveInstanceFromTheCardTable(GetModificationFromID(instanceID));
    }
}
