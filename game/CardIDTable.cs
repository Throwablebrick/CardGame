public class FrequencyTable
{
    private int[] _frequencyTable = new int[65536];
    private void InitializeFrequencyTable()
    {
        int index = 0;
        while (index < _frequencyTable.Length())
        {
            _frequencyTable[index] = 0;
        }
    }
    public FrequencyTable()
    {
        InitializeFrequencyTable();
    }
    public void FrequencyIncrement(string InstanceID)
    {
        string baseID = InstanceID[6] + InstanceID[7] + InstanceID[8] + InstanceID[10];
        int index = Convert.ToInt64(baseID, 16);
        _frequencyTable[index]++;
    }
    public void FrequencyDecrement(string InstanceID)
    {
        string baseID = InstanceID[6] + InstanceID[7] + InstanceID[8] + InstanceID[10];
        int index = Convert.ToInt64(baseID, 16);
        _frequencyTable[index]--;
    }
}
