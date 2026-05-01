using System;
public class CardInstanceTable
{
    //Holds the Modification IDs of a card
    private string[] _cardTable;
    public string[] CardTable
    {
        get {return _cardTable;}
    }
    //Holds the amount of instances of a certain card modification
    private int[] _frequencyTable;
    public int[] FrequencyTable
    {
        get {return _frequencyTable;}
    }
    private int _instancesAdded;
    //Keeps track of the number of unique instances added
    public int InstancesAdded
    {
        get {return _instancesAdded;}
    }
    //Prints the Index, modification ID, and the amount of instances with that ID. Also states the current number of unique instances
    public void PrintDetails(int index)
    {
        Console.WriteLine("Index: " + index + ", cardID: " + _cardTable[index] + ", frequency: " + _frequencyTable[index] + ", instances added: " + _instancesAdded);
    }
    //Modify the amount directly
    public void ModifyFrequencyAtIndex(int index, int value)
    {
        _frequencyTable[index] = value;
    }
    //modify the mod ID directly
    public void ModifyCardTableAtIndex(int index, string value)
    {
        _cardTable[index] = value;
    }
    //get the amount using an index
    public int GetFrequencyAtIndex(int index)
    {
        return _frequencyTable[index];
    }
    //get the mod ID using an index
    public string GetCardTableAtIndex(int index)
    {
        return _cardTable[index];
    }
    //add a instance to the card using the mod ID
    public void AddInstanceToCardTable(string instanceID)
    {
        //checks for base card (No modifications)
        if (Convert.ToInt32(instanceID, 16) == 0)
        {
            _frequencyTable[0]++;
        }
        else
        {
            //Checks if it already exists using a binary serach algorithm, returning -1 if it does not exist, and its index if it does
            int mergeChecker = BinarySearch(this, instanceID, _instancesAdded, 0);
            if (mergeChecker != -1)
            {
                //Console.WriteLine("Dupe instance found, frequency incremented instead");
                //increments the amount using the index found
                _frequencyTable[mergeChecker]++;
                
            }
            else
            {
                //increases instances added
                _instancesAdded++;
                //sets the next available slot to be the given mod ID
                _cardTable[_instancesAdded] = instanceID;
                //increments the amount by 1
                _frequencyTable[_instancesAdded]++;
                //Creates a temporay card table for sorting purposes
                int[] tempCardTable = new int[_instancesAdded+1];
                //copy current table to temp card table
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    tempCardTable[index] = Convert.ToInt32(_cardTable[index], 16);
                }
                //sort the temp card table
                tempCardTable = MergeSort(tempCardTable);
                //match the freqeuncy element indexes to the temp card table indexes
                RearrangeFrequencyElements(tempCardTable);
                //copy temp card table to current card table
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    _cardTable[index] = Convert.ToString(tempCardTable[index], 16);
                }
            }
        }
    }
    //remove an instance from the card using the mod ID
    public void RemoveInstanceFromTheCardTable(string instanceID)
    {
        //checks for base card (No mods)
        if (Convert.ToInt32(instanceID, 16) == 0)
        {
            _frequencyTable[0]--;
        }
        else
        {
            //finds the index of the mod ID that is being removed using the binary search
            int indexOfInstanceToRemove = BinarySearch(this, instanceID, _instancesAdded, 0);
            //decrements by one at the given index
            _frequencyTable[indexOfInstanceToRemove]--;
            //checks if it is at zero
            if (_frequencyTable[indexOfInstanceToRemove] == 0)
            {
                //sets the value to null
                _cardTable[indexOfInstanceToRemove] = null;
                //creates a temp card table for sorting purposes
                int[] tempCardTable = new int[_instancesAdded+1];
                //copies current card table to temp card table
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    tempCardTable[index] = Convert.ToInt32(_cardTable[index], 16);
                }
                //sorts the temp card table
                tempCardTable = MergeSort(tempCardTable);
                //shifts it over to the left by one to account for the removed value (Which would be 0)
                tempCardTable = ShiftTable(-1,tempCardTable);
                //decrements the amount of unique mods
                _instancesAdded--;
                //rearranges frequency elements to match the card table
                RearrangeFrequencyElements(tempCardTable);
                //copies the temp card table to the current card table
                for (int index = 0; index <= _instancesAdded; index++)
                {
                    _cardTable[index] = Convert.ToString(tempCardTable[index],16);
                }
                //sets the last value to null since there shouldn't be a value there
                _cardTable[_instancesAdded+1] = null;
            }
        }
    }
    public static int[] ShiftTable(int amountToShift, int[] arrayToShift)
    {
        //creates a temp array to return
        int[] tempArray = new int[arrayToShift.Length+amountToShift];
        //shifts values to the left
        if (amountToShift < 0)
        {
            for (int index = 0; index < tempArray.Length; index++)
            {
                tempArray[index] = arrayToShift[index-amountToShift];
            }
        }
        //shifts values to the right
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
        //creates a temp frequency table for modification
        int[] tempFrequencyTable = new int[_instancesAdded+1];
        for (int index = 0; index <= _instancesAdded; index++)
        {
            //using the sorted array, searches for the index of the unsorted element in the unsorted array using binary search
            int binarySearchIndex = BinarySearch(this, Convert.ToString(sortedDecimalArray[index],16), _instancesAdded, 0);
            //if it returns -1, this means that this value is missplaced, and so swaps it with the newly added value in the frequency table. not 100% sure if this works, but it works well enough
            if (binarySearchIndex == -1)
            {
                int unsortedIndex = index;
                tempFrequencyTable[index] = _frequencyTable[_instancesAdded];
                tempFrequencyTable[_instancesAdded] = _frequencyTable[index];
            }
            //otherwise it just puts it into the temp frequency table
            else
            {
                tempFrequencyTable[index] = _frequencyTable[binarySearchIndex];
            }
        }
        //finally copies over the values from the temp frequency table to the current frequency table
        for (int index = 0; index <= _instancesAdded; index++)
        {
            _frequencyTable[index] = tempFrequencyTable[index];
        }
    }
    public static int BinarySearch(CardInstanceTable cardTableToSearch, string instanceID, int upperBound, int lowerBound)
    {
        //finds mid value
        int midValue = (lowerBound + upperBound)/2;
        //finds the mod id of the midvalue
        string IDOfArrayToSearch = cardTableToSearch.GetCardTableAtIndex(midValue); 
        //checks if we are at the end of the binary search
        if (lowerBound == upperBound)
        {
            //if it matches, it returns the index, otherwise it returns -1 for if the value cannot be found
            if (instanceID == IDOfArrayToSearch)
            {
                return midValue;
            }
            else
            {
                return -1;
            }
        }
        //checks if the value matches even when it has not fully searched the list
        else if (instanceID == IDOfArrayToSearch)
        {
            return midValue;
        }
        //checks if the midvalue is less than, and if so checks the upper half of the mid value
        else if (Convert.ToInt32(instanceID, 16) > Convert.ToInt32(IDOfArrayToSearch, 16))
        {
            return BinarySearch(cardTableToSearch, instanceID, upperBound, midValue+1);
        }
        //checks if the midvalue is greater than, and if so checks the lower half of the mid value
        else if (Convert.ToInt32(instanceID, 16) < Convert.ToInt32(IDOfArrayToSearch, 16))
        {
            return BinarySearch(cardTableToSearch, instanceID, midValue, lowerBound);
        }
        //needed return value to avoid compiling error
        else
        {
            Console.WriteLine("Something went wrong");
            return -1;
        }
        
    }
    public int[] MergeSort(int[] arrayToSort)
    {
        //checks if the array is only one element (Meaning it is sorted)
        if (arrayToSort.Length == 1)
        {
            return arrayToSort;
        }
        else
        {
            //divides the array into two seperate arrays
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
            //merge sorts the first half
            array1 = MergeSort(array1);
            //merge sorts the second half
            array2 = MergeSort(array2);
            //shuffle sorts the two merge sorted arrays
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
            //checks all elements in the first array and see if they are bigger than all elements in the second array
            while (arrayToSort1[sortCount1] >= arrayToSort2[sortCount2])
            {
                //increments sortcount2 by one (meaning another element in the second array has been sorted)
                sortedArray[sortCount1+sortCount2] = arrayToSort2[sortCount2];
                sortCount2++;
                //checks if array 2 has been fully sorted and if so, adds the rest of the elements in array 1
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
            //checks to see if there are any unsorted elements in either
            if (sortCount2 != arrayToSort2.Length || sortCount1 != arrayToSort1.Length)
            {
                //does the same proccess as the previous while loop but instead compares array 2 to array 1 rather than array 1 to array 2
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
    public CardInstanceTable()
    {
        //default size (4 cards in deck + 256 token copies)
        _cardTable = new string[260];
        _frequencyTable = new int[260];
        //sets the index 0 to be the base id
        _cardTable[0] = "0";
    }
    public CardInstanceTable(int cardTableSize)
    {
        //custom size
        _cardTable = new string[cardTableSize];
        _frequencyTable = new int[cardTableSize];
        //sets the index 0 to be the base id
        _cardTable[0] = "0";
    }
}