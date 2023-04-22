public class Fruit
{
    public float SpotSize { get; }
    public float SpikeLength { get; }
    public bool Poisonous { get; }

    public Fruit(float spotSize, float spikeLength, bool poisonous)
    {
        SpotSize = spotSize;
        SpikeLength = spikeLength;
        Poisonous = poisonous;
    }
}