public class CardTableInstance
{
    private string[] _cardTable;
    public string[] CardTable
    {
        get {return _cardTable;}
    }
    private int[] _frequencyTable;
    public int[] FrequencyTable
    {
        get {return _frequencyTable;}
    }
    private int _instancesAdded;
    public int InstancesAdded
    {
        get {return _instancesAdded;}
    }
    public void PrintDetails(int index)
    {
        Console.WriteLine("Index: " + index + ", cardID: " + _cardTable[index] + ", frequency: " + _frequencyTable[index] + ", instances added: " + _instancesAdded);
    }
    public void ModifyFrequencyAtIndex(int index, int value)
    {
        _frequencyTable[index] = value;
    }
    public void ModifyCardTableAtIndex(int index, string value)
    {
        _cardTable[index] = value;
    }
    public int GetFrequencyAtIndex(int index)
    {
        return _frequencyTable[index];
    }
    public string GetCardTableAtIndex(int index)
    {
        return _cardTable[index];
    }
    public void AddInstanceToCardTable(string instanceID)
    {
        if (Convert.ToInt32(instanceID, 16) == 0)
        {
            _frequencyTable[0]++;
        }
        else
        {
            int mergeChecker = BinarySearch(this, instanceID, _instancesAdded, 0);
            if (mergeChecker != -1)
            {
                //Console.WriteLine("Dupe instance found, frequency incremented instead");
                _frequencyTable[mergeChecker]++;
                
            }
            else
            {
                _instancesAdded++;
                _cardTable[_instancesAdded] = instanceID;
                _frequencyTable[_instancesAdded]++;
                int[] tempCardTable = new int[_instancesAdded+1];
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    tempCardTable[index] = Convert.ToInt32(_cardTable[index], 16);
                }
                tempCardTable = MergeSort(tempCardTable);
                RearrangeFrequencyElements(tempCardTable);
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    _cardTable[index] = Convert.ToString(tempCardTable[index], 16);
                }
            }
        }
    }
    public void RemoveInstanceFromTheCardTable(string instanceID)
    {
        Console.WriteLine(instanceID);
        if (Convert.ToInt32(instanceID, 16) == 0)
        {
            _frequencyTable[0]--;
        }
        else
        {
            int indexOfInstanceToRemove = BinarySearch(this, instanceID, _instancesAdded, 0);
            _frequencyTable[indexOfInstanceToRemove]--;
            if (_frequencyTable[indexOfInstanceToRemove] == 0)
            {
                _cardTable[indexOfInstanceToRemove] = null;
                int[] tempCardTable = new int[_instancesAdded+1];
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    tempCardTable[index] = Convert.ToInt32(_cardTable[index], 16);
                }
                tempCardTable = MergeSort(tempCardTable);
                tempCardTable = ShiftTable(-1,tempCardTable);
                _instancesAdded--;
                RearrangeFrequencyElements(tempCardTable);
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    _cardTable[index] = Convert.ToString(tempCardTable[index],16);
                }
                _cardTable[_instancesAdded+1] = null;
            }
        }
    }
    public static int[] ShiftTable(int amountToShift, int[] arrayToShift)
    {
        int[] tempArray = new int[arrayToShift.Length+amountToShift];
        if (amountToShift < 0)
        {
            for (int index = 0; index < tempArray.Length; index++)
            {
                tempArray[index] = arrayToShift[index-amountToShift];
            }
        }
        else
        {
            for (int index = tempArray.Length-1; index > 0; index--)
            {
                tempArray[index] = arrayToShift[index-amountToShift]; 
            }
        }
        return tempArray;
    }
    public void RearrangeFrequencyElements(int[] sortedDecimalArray)
    {
        int[] tempFrequencyTable = new int[_instancesAdded+1];
        for (int index = 0; index <= _instancesAdded; index++)
        {
            int binarySearchIndex = BinarySearch(this, Convert.ToString(sortedDecimalArray[index],16), _instancesAdded, 0);
            if (binarySearchIndex == -1)
            {
                int unsortedIndex = index;
                tempFrequencyTable[index] = _frequencyTable[_instancesAdded];
                tempFrequencyTable[_instancesAdded] = _frequencyTable[index];
            }
            else
            {
                tempFrequencyTable[index] = _frequencyTable[binarySearchIndex];
            }
        }
        
        for (int index = 0; index <= _instancesAdded; index++)
        {
            _frequencyTable[index] = tempFrequencyTable[index];
        }
    }
    public static int BinarySearch(CardTableInstance cardTableToSearch, string instanceID, int upperBound, int lowerBound)
    {
        int midValue = (lowerBound + upperBound)/2;
        string IDOfArrayToSearch = cardTableToSearch.GetCardTableAtIndex(midValue); 
        if (lowerBound == upperBound)
        {
            if (instanceID == IDOfArrayToSearch)
            {
                return midValue;
            }
            else
            {
                return -1;
            }
        }
        else if (instanceID == IDOfArrayToSearch)
        {
            return midValue;
        }
        else if (Convert.ToInt32(instanceID, 16) > Convert.ToInt32(IDOfArrayToSearch, 16))
        {
            return BinarySearch(cardTableToSearch, instanceID, upperBound, midValue+1);
        }
        else if (Convert.ToInt32(instanceID, 16) < Convert.ToInt32(IDOfArrayToSearch, 16))
        {
            return BinarySearch(cardTableToSearch, instanceID, midValue, lowerBound);
        }
        else
        {
            Console.WriteLine("Something went wrong");
            return -1;
        }
        
    }
    public int[] MergeSort(int[] arrayToSort)
    {
        if (arrayToSort.Length == 1)
        {
            return arrayToSort;
        }
        else
        {
            int size = arrayToSort.Length/2;
            int[] array1 = new int[size];
            int[] array2 = new int[arrayToSort.Length-size];
            for(int index = 0; index < arrayToSort.Length; index++)
            {
                if (index < size)
                {
                    array1[index] = arrayToSort[index];
                }
                else
                {
                    array2[index-size] = arrayToSort[index];
                }
            }
            array1 = MergeSort(array1);
            array2 = MergeSort(array2);
            return ShuffleSort(array1, array2);
        }
    }
    public static int[] ShuffleSort(int[] arrayToSort1, int[] arrayToSort2)
    {
        int[] sortedArray = new int[arrayToSort1.Length + arrayToSort2.Length];
        int sortCount1 = 0;
        int sortCount2 = 0;
        while (sortCount1 < arrayToSort1.Length && sortCount2 < arrayToSort2.Length)
        {
            while (arrayToSort1[sortCount1] >= arrayToSort2[sortCount2])
            {
                sortedArray[sortCount1+sortCount2] = arrayToSort2[sortCount2];
                sortCount2++;
                if (sortCount2 == arrayToSort2.Length)
                {
                    while (sortCount1 < arrayToSort1.Length)
                    {
                        sortedArray[sortCount1+sortCount2] = arrayToSort1[sortCount1];
                        sortCount1++;
                        if (sortCount1 == arrayToSort1.Length)
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            if (sortCount2 != arrayToSort2.Length || sortCount1 != arrayToSort1.Length)
            {
                while (arrayToSort2[sortCount2] > arrayToSort1[sortCount1])
                {
                    sortedArray[sortCount1+sortCount2] = arrayToSort1[sortCount1];
                    sortCount1++;
                    if (sortCount1 == arrayToSort1.Length)
                    {
                        while (sortCount2 < arrayToSort2.Length)
                        {
                            sortedArray[sortCount1+sortCount2] = arrayToSort2[sortCount2];
                            sortCount2++;
                            if (sortCount2 == arrayToSort2.Length)
                            {
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
        return sortedArray;
    }
    public CardTableInstance()
    {
        _cardTable = new string[260];
        _frequencyTable = new int[260];
        _cardTable[0] = "0";
    }
    public CardTableInstance(int cardTableSize)
    {
        _cardTable = new string[cardTableSize];
        _frequencyTable = new int[cardTableSize];
        _cardTable[0] = "0";
    }
}