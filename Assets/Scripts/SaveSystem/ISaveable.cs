public interface ISaveable
{
    ObjectData SaveMyData();
    void LoadMyData(ObjectData savedData);
    void WipeMyData();
}
